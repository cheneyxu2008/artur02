using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cci;

namespace ClassInspector
{
    public static class Inspector
    {
        public static IEnumerable<IPropertyDefinition> GetProperties(INamedTypeDefinition type)
        {
            return type.Properties;
        }

        public static IEnumerable<string> GetPropertyNames(INamedTypeDefinition type)
        {
            var properties = GetProperties(type);
            return properties.Select(GetPropertyName);
        }

        public static IEnumerable<ITypeReference> GetBaseClasses(INamedTypeDefinition type)
        {
            return type.BaseClasses;
        }

        public static IEnumerable<string> GetBaseClassNames(INamedTypeDefinition type)
        {
            return GetBaseClasses(type).Select(baseClass => baseClass.ResolvedType.ToString());
        }

        public static IEnumerable<ITypeReference> GetInterfaces(INamedTypeDefinition type)
        {
            return type.Interfaces;
        }

        public static IEnumerable<ITypeReference> GetGenericArguments(ITypeReference implementedInterface)
        {
            return implementedInterface.ResolvedType.InstanceType.GenericArguments;
        }

        public static string GetPropertyName(IPropertyDefinition property)
        {
            return property.Name.Value.ToString();
        }
    }
}
