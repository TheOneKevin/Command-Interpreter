using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libIL2AIL.Parse;
using libIL2AIL.Tracking;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace libIL2AIL
{
    public class ParseEngine
    {
        #region Field Declarations

        string MainCode   { get; set; }
        string FilePath   { get; set; }
        int    LineNumber { get; set; }

        #endregion

        #region Initialization

        public ParseEngine(string code, string path)
        {
            MainCode = code;
            FilePath = path;
            Init();
        }

        void Init()
        {

        }

        #endregion

        #region Methods

        public void ParseFromCache(string input)
        {
            VEEE.Init();
            SyntaxTree tree = CSharpSyntaxTree.ParseText(input); //Parse code into tree
            if (tree.GetRoot() is CompilationUnitSyntax)
            {
                var root = tree.GetRoot() as CompilationUnitSyntax; //Convert tree type
                var members = root.Members.ToArray(); //Get all the things in the root
                foreach(var m in members)
                {
                    ErrorHandler.updateLinePos(m.GetLocation().GetMappedLineSpan().StartLinePosition.Line, m.GetLocation().GetMappedLineSpan().StartLinePosition.Character);
                    if (m is MethodDeclarationSyntax)
                    {
                        var method = m as MethodDeclarationSyntax;
                        var body = method.Body.ChildNodes();
                        foreach (var line in body)
                        {
                            GeneralStatementParser.Parse(line);
                        }
                    }

                    else
                        GeneralStatementParser.Parse(m);
                }
            }

            else
                ErrorHandler.registerError(ErrorCode.InvalidFileStructure,
                    "Expected type: CompilationUnitSyntax, recieved type: " + tree.GetRoot().GetType(), VEEE.getErrorList());

            VEEE.HeartBeat();
        }

        #endregion
    }
}
