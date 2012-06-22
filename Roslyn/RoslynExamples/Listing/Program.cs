using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;

namespace ConsoleApplication4
{
    internal class Program
    {
        private static void Main()
        {
            string definition = File.ReadAllText("Sample.cs");
            SyntaxTree tree = SyntaxTree.ParseCompilationUnit(definition);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            
            // USINGS
            var usingList = GetUsings(root);
            Console.WriteLine("USINGS");
            foreach (var usingDirectiveSyntax in usingList)
            {
                Console.WriteLine("   {0}", usingDirectiveSyntax.Name);
            }

            Console.WriteLine();
            Console.WriteLine();

            // Classes
            var classList = GetClasses(root);
            Console.WriteLine("CLASSES");
            foreach (var classDirectiveSyntax in classList)
            {
                Console.WriteLine("   {0}", classDirectiveSyntax.Identifier.GetText());
            }
        }

        private static IEnumerable<ClassDeclarationSyntax> GetClasses(CompilationUnitSyntax root)
        {
            var usings = from usingDirectiveSyntax in root.DescendantNodes().OfType<ClassDeclarationSyntax>()
                         select usingDirectiveSyntax;
            var usingList = usings.ToList();
            return usingList;
        }

        private static IEnumerable<UsingDirectiveSyntax> GetUsings(CompilationUnitSyntax root)
        {
            var usings = from usingDirectiveSyntax in root.DescendantNodes().OfType<UsingDirectiveSyntax>()
                         select usingDirectiveSyntax;
            var usingList = usings.ToList();
            return usingList;
        }
    }
}
