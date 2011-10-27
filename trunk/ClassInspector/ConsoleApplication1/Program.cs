using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassInspector;
using ClassInspectorConsole;
using Common.Logging;
using Microsoft.Cci;
using NConsoler;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        [STAThread]
        private static void Main(string[] args)
        {
            Consolery.Run(typeof(Program), args);
        }

        [Action]
        public static void ProcessArguments(
                [Required] string className,
                [Required] string assemblyLocation
            )
        {
            var properties = Parse(className, assemblyLocation);


            var classInitializer = new ClassInitializer
                                       {
                                           ClassName = className, 
                                           Props = properties
                                       };
            var result = classInitializer.TransformText();
            Clipboard.SetText(result);
            Console.WriteLine(result);
        }

        private static IEnumerable<IPropertyDefinition> Parse(string className, string assemblyLocation)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(className));
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(assemblyLocation));
            Contract.Requires<FileNotFoundException>(File.Exists(assemblyLocation));

            using (var host = new PeReader.DefaultHost())
            {
                var assembly =
                    host.LoadUnitFrom(
                        assemblyLocation)
                    as IAssembly;
                var props = new List<IPropertyDefinition>();

                Log.Info("Inspecting types...");
                foreach (INamedTypeDefinition type in assembly.GetAllTypes())
                {
                    Log.DebugFormat("  {0}", type.Name);


                    
                    if (type.ResolvedType.ToString() == className)
                    {
                        props.AddRange(Inspector.GetProperties(type));

                        foreach (var baseType in type.BaseClasses)
                        {
                            props.AddRange(Inspector.GetProperties(baseType.ResolvedType as INamedTypeDefinition));
                        }

                        return props;
                    }


                }

                return props;
            }
        }
    }
}