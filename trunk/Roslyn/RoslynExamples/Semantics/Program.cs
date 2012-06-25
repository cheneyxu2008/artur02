using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;

namespace Semantics
{
    class Program
    {
        static void Main(string[] args)
        {
            SyntaxTree tree = SyntaxTree.ParseCompilationUnit(
                @"using System;
                using System.Collections.Generic;
                using System.Text;
 
                namespace HelloWorld
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine(""Hello, World!"");
                        }
                    }
                }");

            var root = (CompilationUnitSyntax)tree.GetRoot();

            var compilation = Compilation.Create("HelloWorld")
                                 .AddReferences(new AssemblyNameReference("mscorlib"))
                                 .AddSyntaxTrees(tree);


            var model = compilation.GetSemanticModel(tree);
            var nameInfo = model.GetSymbolInfo(root.Usings[0].Name);
            var systemSymbol = (NamespaceSymbol)nameInfo.Symbol;


            foreach (var ns in systemSymbol.GetNamespaceMembers())
            {
                Console.WriteLine(ns.Name);
                foreach (var s in ns.GetMembers())
                {
                    Console.WriteLine("  " + s.Name);
                }
            }

        }
    }
}
