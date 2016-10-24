using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Expressions
{
    public class ObjectCreation
    {
        public static byte[] objectCreation(ObjectCreationExpressionSyntax input)
        {
            List<byte> byteStream = new List<byte>();

            if (GeneralExpressionParser.parseArgumentList(input.ArgumentList) != null)
                byteStream.AddRange(GeneralExpressionParser.parseArgumentList(input.ArgumentList));

            return byteStream.ToArray();
        }
    }
}
