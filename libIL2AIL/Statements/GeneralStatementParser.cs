using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Statements
{
    public static class GeneralStatementParser
    {
        //Our common syntax parser method
        public static void parseStatementType(SyntaxNode node)
        {
            //Check what type it is
            if (node is LocalDeclarationStatementSyntax)
                VariableStatement.parseLocalVariable(node as LocalDeclarationStatementSyntax);
            else if (node is VariableDeclarationSyntax)
                VariableStatement.parseVariable(node as VariableDeclarationSyntax);
            else if (node is FieldDeclarationSyntax)
                VariableStatement.parseFieldVariable(node as FieldDeclarationSyntax);

            else
                ErrHandler.registerErr(new Error(ErrorCode.UnknownSyntax, ""));
        }
    }
}
