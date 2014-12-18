using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Razor;
using Microsoft.CSharp;

namespace SimpleRazor
{
    /// <summary>
    ///     Provides access to the SimpleRazor engine.
    /// </summary>
    public static class Razor
    {
        /// <summary>
        ///     Gets the last templates generated code.
        /// </summary>
        /// <value>
        ///     The generated code.
        /// </value>
        public static string LastGeneratedCode { get; private set; }

        /// <summary>
        ///     Renders the specified template and model.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <returns>The rendered template output.</returns>
        /// <exception cref="TemplateCompileException">Thrown when template compiling fails.</exception>
        public static string Render<T>(string template, T model)
        {
            var anonymous = model.GetType().IsAnonymousType();

            var host = new RazorEngineHost(new CSharpRazorCodeLanguage())
            {
                DefaultBaseClass = anonymous ? "TemplateBase<dynamic>" : string.Format("TemplateBase<{0}>", typeof (T).FullName),
                DefaultClassName = "GeneratedTemplate",
                DefaultNamespace = "SimpleRazor"
            };

            host.NamespaceImports.Add("System");
            host.NamespaceImports.Add("System.Collections.Generic");
            host.NamespaceImports.Add("System.Linq");

            var engine = new RazorTemplateEngine(host);

            var reader = new StringReader(template);
            var razorResult = engine.GenerateCode(reader);
            reader.Dispose();

            var codeProvider = new CSharpCodeProvider();

            using (var writer = new StringWriter())
            {
                codeProvider.GenerateCodeFromCompileUnit(razorResult.GeneratedCode, writer, new CodeGeneratorOptions());
                LastGeneratedCode = writer.ToString();
            }

            var referencedAssemblies = new List<string>
            {
                Assembly.GetExecutingAssembly().Location,
                // Hack: Ensure System.Linq and Microsoft.CSharp are loaded
                typeof (System.Linq.Enumerable).Assembly.Location,
                typeof (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).Assembly.Location
            };
            referencedAssemblies.AddRange(GetReferencedAssemblies<T>());

            var compilerParameters = new CompilerParameters(referencedAssemblies.Distinct().ToArray())
            {
                GenerateInMemory = true
            };

            var compilerResults = codeProvider.CompileAssemblyFromDom(compilerParameters, razorResult.GeneratedCode);

            if (compilerResults.Errors.HasErrors)
            {
                throw new TemplateCompileException(compilerResults.Errors);
            }

            dynamic templateInstance;

            if (anonymous)
            {
                templateInstance = (TemplateBase<dynamic>) compilerResults.CompiledAssembly.CreateInstance("SimpleRazor.GeneratedTemplate");
                templateInstance.Model = model.ToExpando();
            }
            else
            {
                templateInstance = (TemplateBase<T>) compilerResults.CompiledAssembly.CreateInstance("SimpleRazor.GeneratedTemplate");
                templateInstance.Model = model;
            }

            return templateInstance.ToString();
        }

        private static IEnumerable<string> GetReferencedAssemblies<T>()
        {
            var declaringAssembly = typeof (T).Assembly;
            yield return declaringAssembly.Location;

            foreach (var assemblyName in declaringAssembly.GetReferencedAssemblies())
            {
                yield return Assembly.ReflectionOnlyLoad(assemblyName.FullName).Location;
            }
        }
    }
}
