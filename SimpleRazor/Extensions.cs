using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SimpleRazor
{
    /// <summary>
    ///     Helper extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     Converts an object to an <c>ExpandoObject</c>
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns><c>ExpandoObject</c> representation of the input object.</returns>
        public static dynamic ToExpando(this object obj)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var prop in obj.GetType().GetProperties())
            {
                expando.Add(prop.Name, prop.PropertyType.IsAnonymousType() ? ToExpando(prop.GetValue(obj, null)) : prop.GetValue(obj, null));
            }

            return expando;
        }

        /// <summary>
        ///     Determines whether the specified type is anonymous.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if type is anonymous.</returns>
        /// <exception cref="System.ArgumentNullException">Type is null.</exception>
        internal static bool IsAnonymousType(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return Attribute.IsDefined(type, typeof (CompilerGeneratedAttribute), false)
                   && (type.Name.Contains("AnonymousType") || type.Name.Contains("AnonType"))
                   && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                   && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }
    }
}
