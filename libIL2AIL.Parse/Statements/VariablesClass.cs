using Microsoft.CodeAnalysis.CSharp.Syntax;
using libIL2AIL.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Parse.Statements
{
    public static class VariableDeclaration
    {
        public static void Parse(VariableDeclarationSyntax syntax)
        {
            if (syntax.Variables.Count > 0)
            {
                string type = syntax.Type.ToString();
                foreach (var v in syntax.Variables)
                {
                    string name = v.Identifier.Text;

                    GeneralExpressionParser.Parse(v.Initializer.Value);
                }
            }
            else
                ErrorHandler.registerError(ErrorCode.InvalidStatementStructure, "Variable statement invalid!", VEEE.getList());
        }
    }

    public static class FieldDeclaration
    {
        public static void Parse(FieldDeclarationSyntax syntax)
        {
            VariableDeclaration.Parse(syntax.Declaration);
        }
    }
}
