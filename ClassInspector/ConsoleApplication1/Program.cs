using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassInspector;
using Microsoft.Cci;

namespace ConsoleApplication1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var cls = args.Length > 0 ? args[0] : string.Empty;
            var assemblyLocation = args.Length > 1
                                       ? args[1]
                                       : @"Epam.AgileTool.BackEndAdapter.dll";


            var properties = Parse(cls, assemblyLocation);


            var a = new TextTemplate1();
            a.ClassName = cls;
            a.Props = properties;
            var result = a.TransformText();
            Clipboard.SetText(result);
            Console.WriteLine(result);
        }

        private static IEnumerable<IPropertyDefinition> Parse(string cls, string assemblyLocation)
        {

            using (var host = new PeReader.DefaultHost())
            {
                var assembly =
                    host.LoadUnitFrom(
                        assemblyLocation)
                    as IAssembly;
                var props = new List<IPropertyDefinition>();

                foreach (INamedTypeDefinition type in assembly.GetAllTypes())
                {
                    Console.WriteLine(">>{0}", type.Name);


                    
                    if (type.ResolvedType.ToString() == cls)
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