using libIL2AIL.ByteCode;
using libIL2AIL.Expressions;
using libIL2AIL.Statements;
using libIL2AIL.Types;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL
{
    public static class BooleanExpression
    {
        public static byte[] parseBinaryExpression(BinaryExpressionSyntax s, string nextSymbol)
        {
            List<byte> ret = new List<byte>();

            //if (GeneralExpressionParser.identifyAndParse(s.Left) != null &&
            //GeneralExpressionParser.identifyAndParse(s.Right) != null)
            //{
            GeneralExpressionParser.identifyAndParse(s.Left);
            GeneralExpressionParser.identifyAndParse(s.Right);

            byte left = 0, right = 0;
            // Pop out the right side first
            if (Types.GetType.getTypeFromExpression(s.Right) == new Types.Boolean())
                right = 0x01;
            else if (Types.GetType.getTypeFromExpression(s.Right) == new Types.GenericObject())
                ErrHandler.registerErr(new Error(ErrorCode.UnknownSyntax, "Object to object comparisons not allowed!"));
            else
                ErrHandler.registerErr(new Error(ErrorCode.UnknownSyntax, "Type not recognized."));

            // Pop out the left side next
            if (Types.GetType.getTypeFromExpression(s.Left) == new Types.Boolean())
                left = 0x01;
            else if (Types.GetType.getTypeFromExpression(s.Left) == new Types.GenericObject())
                ErrHandler.registerErr(new Error(ErrorCode.UnknownSyntax, "Object to object comparisons not allowed!"));
            else
                ErrHandler.registerErr(new Error(ErrorCode.UnknownSyntax, "Type not recognized."));


            //Evaluate what we want to do next
            switch (s.OperatorToken.ValueText)
            {
                /* Input if(left == right) doThis();
                 * Resulting opcode:
                 * lodr left
                 * lodr right
                 * cmp left, right
                 * je doThis
                 */

                case "==": ret.AddRange(new byte[] { (byte)OpCode.lodr, left, (byte)OpCode.lodr, right, (byte)OpCode.cmp, left, right, (byte)OpCode.je }); ret.AddRange(Encoding.ASCII.GetBytes(nextSymbol)); break;
                case ">=": ret.AddRange(new byte[] { (byte)OpCode.lodr, left, (byte)OpCode.lodr, right, (byte)OpCode.cmp, left, right, (byte)OpCode.jae }); ret.AddRange(Encoding.ASCII.GetBytes(nextSymbol)); break;
                case ">": ret.AddRange(new byte[] { (byte)OpCode.lodr, left, (byte)OpCode.lodr, right, (byte)OpCode.cmp, left, right, (byte)OpCode.ja }); ret.AddRange(Encoding.ASCII.GetBytes(nextSymbol)); break;
                case "<=": ret.AddRange(new byte[] { (byte)OpCode.lodr, left, (byte)OpCode.lodr, right, (byte)OpCode.cmp, left, right, (byte)OpCode.jbe }); ret.AddRange(Encoding.ASCII.GetBytes(nextSymbol)); break;
                case "<": ret.AddRange(new byte[] { (byte)OpCode.lodr, left, (byte)OpCode.lodr, right, (byte)OpCode.cmp, left, right, (byte)OpCode.jb }); ret.AddRange(Encoding.ASCII.GetBytes(nextSymbol)); break;

                //WHAT IS THE MEANING OF LIFE?! Agh. Stupid boolean logic.
                case "&&": ret.Add((byte)OpCode.je); break;
                case "||": ret.Add((byte)OpCode.je); break;
                    //default: break;
            }
            //}

            return ret.ToArray();
        }

        public static byte[] parseConditionalExpression(ConditionalExpressionSyntax syntax)
        {
            if (syntax.Condition is BinaryExpressionSyntax)
            {
                return parseBinaryExpression(syntax.Condition as BinaryExpressionSyntax, Symbols.SymbolMan.registerTempSym());
            }

            return null;
        }
    }
}
