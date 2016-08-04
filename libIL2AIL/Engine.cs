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
            var root = tree.GetRoot() as CompilationUnitSyntax;
            var methods = root.Members.ToArray() as MethodDeclarationSyntax[]; //Cast it to a MethodDeclaration
            foreach (var method in methods) //Iterate through all the methods
            {
                var body = method.Body.ChildNodes(); //Get all the code inside the method
                foreach (var line in body)
                {
                    string ss = line.ToString(); //Iterate through all the lines in the code
                }
            }
        }

        #endregion
    }
}