using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Cci;

namespace ClassInspector
{
    /// <summary>
    /// Helper functions for type inspections
    /// </summary>
    public static class Inspector
    {

        /// <summary>
        /// Returns the properties defined on the type
        /// </summary>
        /// <param name="type">The inspected type</param>
        /// <returns>The properties</returns>
        public static IEnumerable<IPropertyDefinition> GetProperties(INamedTypeDefinition type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            return type.Properties;
        }

        /// <summary>
        /// Returns the property names defined on the type
        /// </summary>
        /// <param name="type">The inspected type</param>
        /// <returns>The property names</returns>
        public static IEnumerable<string> GetPropertyNames(INamedTypeDefinition type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            var properties = GetProperties(type);
            return properties.Select(GetPropertyName);
        }

        /// <summary>
        /// Returns the base classes of the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<ITypeReference> GetBaseClasses(INamedTypeDefinition type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            return type.BaseClasses;
        }

        public static IEnumerable<string> GetBaseClassNames(INamedTypeDefinition type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            return GetBaseClasses(type).Select(baseClass => baseClass.ResolvedType.ToString());
        }

        public static IEnumerable<ITypeReference> GetInterfaces(INamedTypeDefinition type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            return type.Interfaces;
        }

        public static IEnumerable<ITypeReference> GetGenericArguments(ITypeReference implementedInterface)
        {
            Contract.Requires<ArgumentNullException>(implementedInterface != null);

            return implementedInterface.ResolvedType.InstanceType.GenericArguments;
        }

        public static string GetPropertyName(IPropertyDefinition property)
        {
            Contract.Requires<ArgumentNullException>(property != null);

            return property.Name.Value;
        }

        public static ITypeReference GetPropertyReturnType(IPropertyDefinition property)
        {
            Contract.Requires<ArgumentNullException>(property != null);

            return property.Getter.Type;
        }
    }
}
