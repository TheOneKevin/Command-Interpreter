using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using libIL2AIL.Tracking;
using libIL2AIL.Parse.Expressions;

namespace libIL2AIL.Parse
{
    public static class GeneralExpressionParser
    {
        public static void Parse(ExpressionSyntax syntax)
        {
            if (syntax is ObjectCreationExpressionSyntax)
                ObjectCreation.Parse(syntax as ObjectCreationExpressionSyntax);

            else
                ErrorHandler.registerError(ErrorCode.ExpressionSyntaxNotRecognized, "Type: " + syntax.GetType() + " is not valid", VEEE.getList());
        }
    }
}
