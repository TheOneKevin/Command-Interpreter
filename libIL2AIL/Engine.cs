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
    public class Engine
    {
        #region Varibles

        //Variables

        #endregion

        #region Engine Start

        public void Compile(string inputF)
        {
            if(File.Exists(inputF))
            {
                using (var f = File.Open(inputF, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    StreamReader sr = new StreamReader(f); string text = sr.ReadToEnd();
                    parseCode(text);
                }
            }
        }

        public void CompileFromCache(string input)
        {
            parseCode(input);
        }

        #endregion

        #region Sub Routines

        public List<string> GetUsings(CompilationUnitSyntax tree)
        {
            List<string> l = new List<string>();
            var usings = tree.Usings;
            foreach(var u in usings)
            {
                l.Add(u.ToString());
            }
            return l;
        }

        #endregion

        #region Parse

        private void parseCode(string text)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
            if (tree.GetRoot() is CompilationUnitSyntax)
            {
                var root = tree.GetRoot() as CompilationUnitSyntax;
                var members = root.Members.ToArray(); //Cast it to a MethodDeclaration
                foreach (var m in members) //Iterate through all the methods
                {
                    if (m is MethodDeclarationSyntax)
                    {
                        var method = m as MethodDeclarationSyntax;
                        var body = method.Body.ChildNodes(); //Get all the code inside the method
                        foreach (var line in body)
                        {
                            string ss = line.ToString(); //Iterate through all the lines in the code
                            parseType(line);
                        }
                    }
                    //TODO: Add more syntax types
                    else
                        parseType(m); //This is just a *common* syntax type parser
                } //End foreach m in members
            } //End if root is syntax
            else
                ErrHandler.registerErr(new Error(ErrorCode.IncorrectCodeFormat, "")); //Error!
        }

        //Our common syntax parser method
        private void parseType(SyntaxNode node)
        {
            //Check what type it is
            if (node is LocalDeclarationStatementSyntax)
                Variables.parseLocalVariable(node as LocalDeclarationStatementSyntax);
            else if (node is VariableDeclarationSyntax)
                Variables.parseVariable(node as VariableDeclarationSyntax);
            else if (node is FieldDeclarationSyntax)
                Variables.parseFieldVariable(node as FieldDeclarationSyntax);

            else
                ErrHandler.registerErr(new Error(ErrorCode.UnknownSyntax, ""));
        }

        #endregion
    }
}