using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace libIL2AIL
{
    class LineParser
    {
        #region Parse Variable

        public void parseVariableDecl(SyntaxNode line)
        {
            //TODO: Check if l is null or not! (Not necessary?)
            var l = line as LocalDeclarationStatementSyntax;
            //var vars = l.Declaration.Variables; //Get list of variables
            getDeclType(l.Declaration.Variables);
            string type = l.Declaration.Type.ToString();
            switch(type)
            {
                //Getting the type from the variable declaration
                //All the selectors
                case "aPlayer": break;
                case "rPlayer": break;
                case "pPlayer": break;
                case "aEntity": break;
                //All the objects in Minecraft
                case "Item": break;
                case "Entity": break;
                case "Block": break;
                //Special entities and objects
                case "ArmorStand": break; //ArmourStand (use the U.S. spelling when coding :/ )
                
                default: break;
            }
        }

        public void getDeclType(SeparatedSyntaxList<VariableDeclaratorSyntax> vars)
        {
            if (vars.Count > 0)
            {
                //Then we got a statement like: string s;
            }
            else
            {
                //We got a list of variables -> vars, and we're gonna iterate through dem
                foreach (var v in vars)
                {
                    string name = v.Identifier.ToString();
                    int kind = v.Initializer.Value.RawKind;
                    switch (v.Initializer.Value.Kind())
                    {
                        //We got a statement that looks something like this: object a = new object(args[]);
                        case SyntaxKind.ObjectCreationExpression: break; //8649
                        
                        default: break;
                    }
                }
            }
        }

        #endregion
    }
}
