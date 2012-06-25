using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.CSharp;

namespace SyntaxTransformation
{
    class Program
    {
        static void Main(string[] args)
        {


            SyntaxTree tree = SyntaxTree.ParseCompilationUnit(
                @"using System;
                using System.Collections;
                using System.Linq;
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

            NameSyntax name = Syntax.IdentifierName("System");
            name = Syntax.QualifiedName(name, Syntax.IdentifierName("Collections"));
            name = Syntax.QualifiedName(name, Syntax.IdentifierName("Generic"));

            var oldUsing = root.Usings[1];
            var newUsing = oldUsing.WithName(name);
            root = root.ReplaceNode(oldUsing, newUsing);

        }
    }
}
