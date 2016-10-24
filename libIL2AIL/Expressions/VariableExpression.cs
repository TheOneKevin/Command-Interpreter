using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL
{
    public class VariableExpression
    {
        public static void parseConditionalExpression(ConditionalExpressionSyntax syntax)
        {
            if(syntax.Condition is BinaryExpressionSyntax)
            {
                var s = syntax.Condition as BinaryExpressionSyntax;
                int opcode = 0;
                switch(s.OperatorToken.ValueText)
                {
                    case "==": break;
                    case ">=": break;
                    default: break;
                }

                if(Expressions.GeneralExpressionParser.identifyAndParse(s.Left) != null &&
                Expressions.GeneralExpressionParser.identifyAndParse(s.Right) != null)
                {

                }
            }
        }
    }
}
