using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Windows.Forms;
using ClassInspector;
using Common.Logging;
using Microsoft.Cci;
using NConsoler;
using TypeInspector;

namespace ClassInspectorConsole
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        [STAThread]
        private static void Main(string[] args)
        {
            Consolery.Run(typeof(Program), args);
        }

        /// <summary>
        /// Processes the command line arguments and executes the application code
        /// </summary>
        /// <param name="typeName">The class name</param>
        /// <param name="assemblyLocation">The assembly location</param>
        [Action]
        public static void ProcessArguments(
                [Required] string typeName,
                [Required] string assemblyLocation
            )
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(typeName));
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(assemblyLocation));
            Contract.Requires<ArgumentException>(File.Exists(assemblyLocation));

            var properties = Parse(typeName, assemblyLocation);

            var result = GenerateMarkup(typeName, properties);
            Clipboard.SetText(result);
            Console.WriteLine(result);

            Console.ReadKey();
        }

        /// <summary>
        /// Generates the class initilaizer markup
        /// </summary>
        /// <param name="typeName">The type name</param>
        /// <param name="properties">The properties</param>
        /// <returns>The initializer markup</returns>
        private static string GenerateMarkup(string typeName, IDictionary<IPropertyDefinition, ITypeDefinition> properties)
        {
            Log.Info("Generating markup...");

            var classInitializer = new ClassInitializer
                                       {
                                           ClassName = typeName,
                                           Props = properties
                                       };
            var result = classInitializer.TransformText();
            return result;
        }

        /// <summary>
        /// Pareses the types and returns the properties
        /// </summary>
        /// <param name="typeName">The type name we are looking for</param>
        /// <param name="assemblyLocation">The assembly conatining the inspected types.</param>
        /// <returns></returns>
        private static IDictionary<IPropertyDefinition, ITypeDefinition> Parse(string typeName, string assemblyLocation)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(typeName));
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(assemblyLocation));
            Contract.Requires<FileNotFoundException>(File.Exists(assemblyLocation));

            Log.Info("Parsing types...");

            using (var host = new PeReader.DefaultHost())
            {
                var assembly =
                    host.LoadUnitFrom(
                        assemblyLocation)
                    as IAssembly;

                var props = new Dictionary<IPropertyDefinition, ITypeDefinition>();
                if(assembly != null)
                {
                    Log.Info("Inspecting types...");
                    foreach (INamedTypeDefinition type in assembly.GetAllTypes())
                    {
                        Log.TraceFormat("  {0}", type.Name);

                        if (type.ResolvedType.ToString() == typeName)
                        {
                            var properties = Inspector.GetPropertiesWithType(type);
                            props.Merge(properties);

                            foreach (var baseType in type.BaseClasses)
                            {
                                var baseProperties =
                                    Inspector.GetPropertiesWithType(baseType.ResolvedType as INamedTypeDefinition);
                                props.Merge(baseProperties);
                            }

                            return props;
                        }


                    }
                }

                return props;
            }
        }
    }
}