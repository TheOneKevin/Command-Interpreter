using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using libIL2AIL.Tracking;

namespace libIL2AIL.Parse
{
    public static class GeneralStatementParser
    {
        public static void Parse(SyntaxNode syntax)
        {
            if (syntax is VariableDeclarationSyntax)
                Statements.VariableDeclaration.Parse(syntax as VariableDeclarationSyntax);
            else if (syntax is FieldDeclarationSyntax)
                Statements.FieldDeclaration.Parse(syntax as FieldDeclarationSyntax);
            //else if(syntax is LocalDeclarationStatementSyntax)

            else
                ErrorHandler.registerError(ErrorCode.StatementSyntaxNotRecognized, "Type: " + syntax.GetType() + " is not valid", VEEE.getErrorList());
        }
    }
}
