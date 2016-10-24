using libIL2AIL.ByteCode;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Text;

namespace libIL2AIL.Statements
{
    public class VariableStatement
    {
        public static void parseVariable(VariableDeclarationSyntax line)
        {

        }

        public static void parseLocalVariable(LocalDeclarationStatementSyntax line)
        {
            string type = line.Declaration.Type.ToString();
            var vars = line.Declaration.Variables;
        }

        public static void parseFieldVariable(FieldDeclarationSyntax line)
        {
            string type = line.Declaration.Type.ToString();
            var vars = line.Declaration.Variables;
            foreach (var v in vars)
                parseVariableCommon(v);
        }

        public static byte[] parseVariableCommon(VariableDeclaratorSyntax v)
        {
            string variableName = v.Identifier.ToString();
            List<byte> byteStream = new List<byte>();
            if (v.Initializer != null)
            {
                if (v.Initializer.Value is ObjectCreationExpressionSyntax)
                {
                    byteStream.Add((byte)OpCode.ro);
                    byteStream.AddRange(Encoding.Unicode.GetBytes(variableName));
                    Expressions.ObjectCreation.objectCreation(v.Initializer.Value as ObjectCreationExpressionSyntax);
                }

                else if (v.Initializer.Value is LiteralExpressionSyntax)
                {

                }
                else if (v.Initializer.Value is ConditionalExpressionSyntax)
                    VariableExpression.parseConditionalExpression(v.Initializer.Value as ConditionalExpressionSyntax);
            }

            return byteStream.ToArray();
        }
    }
}
