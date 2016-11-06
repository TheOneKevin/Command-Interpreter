using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using libIL2AIL.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Parse
{
    public static class Parse
    {
        public static void ParseCode(string code)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code); //We first let Roslyn parse our code into a tree
            if (tree.GetRoot() is CompilationUnitSyntax) //Then we get the root of the tree, and check if it's valid
            {
                var root = tree.GetRoot() as CompilationUnitSyntax; //And place it in our root variable
                var members = root.Members.ToArray();
                foreach (var thidng in members) //Now we have a list of all the members of the tree (i.e., methods, enums, etc.)
                {

                }
            }
            else
                ErrorHandler.registerError(ErrorCode.InvalidFileStructure, "Code root type: " + tree.GetRoot().GetType() + " is not a valid type. Expected: CompilationUnitSyntax !");
        }
    }
}
