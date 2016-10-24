using libIL2AIL.ByteCode;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace libIL2AIL.Expressions
{
    public class GeneralExpressionParser
    {
        public static byte[] identifyAndParse(ExpressionSyntax input)
        {
            if (input is ObjectCreationExpressionSyntax)
                return ObjectCreation.objectCreation(input as ObjectCreationExpressionSyntax);
            else
                return null;
        }

        public static byte[] parseArgumentList(ArgumentListSyntax args)
        {
            if (args.Arguments.Count <= 0) return null;

            List<byte> byteStream = new List<byte>();
            foreach (var a in args.Arguments)
            {
                if (identifyAndParse(a.Expression) != null)
                {
                    byteStream.AddRange(identifyAndParse(a.Expression));
                }
            }
            return byteStream.ToArray();
        }
    }
}
