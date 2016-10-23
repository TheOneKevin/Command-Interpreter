using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace libIL2AIL
{
    public static class Variables
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

        static void parseVariableCommon(VariableDeclaratorSyntax v)
        {
            string variableName = v.Identifier.ToString();
            if (v.Initializer != null)
            {
                if (v.Initializer.Value is ObjectCreationExpressionSyntax)
                {

                }
                else if (v.Initializer.Value is LiteralExpressionSyntax)
                {

                }
                else if (v.Initializer.Value is ConditionalExpressionSyntax)
                {
                    var syntax = v.Initializer.Value as ConditionalExpressionSyntax;
                }
            }
        }

    }
}
