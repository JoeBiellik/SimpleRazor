using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleRazor
{
    /// <summary>
    /// Thrown when template compiling fails.
    /// </summary>
    public class TemplateCompileException : Exception
    {
        /// <summary>
        /// Gets a list of compiler errors.
        /// </summary>
        /// <value>
        /// The compiler errors.
        /// </value>
        public IList<CompilerError> CompilerErrors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateCompileException"/> class.
        /// </summary>
        /// <param name="errors">The compiler errors.</param>
         public TemplateCompileException(CompilerErrorCollection errors) :
            base(string.Format("{0} error{1} occured during template compilation.", errors.Count, errors.Count == 1 ? string.Empty : "s"))
        {
            this.CompilerErrors = errors.Cast<CompilerError>().ToList().AsReadOnly();
        }

         /// <summary>
         /// Returns a <see cref="System.String" /> that represents this instance.
         /// </summary>
         /// <returns>
         /// A <see cref="System.String" /> that represents this instance.
         /// </returns>
         /// <PermissionSet>
         ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
         /// </PermissionSet>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine();

            foreach (var error in this.CompilerErrors)
            {
                builder.Append(error).AppendLine();
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
