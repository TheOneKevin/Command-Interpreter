using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Types
{
    public static class GetType
    {
        //This is very similar to identifyAndParse(ExpressionSyntax input)
        public static Type getTypeFromExpression(ExpressionSyntax input)
        {
            if (input is ObjectCreationExpressionSyntax)
                return new GenericObject();
            else if(input is IdentifierNameSyntax)
            {
                //Then we have problems on our hands... fuck me
                return null;
            }
            else
                return new Type(); //Void
        }
    }
}
