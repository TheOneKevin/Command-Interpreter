using libIL2AIL.Tracking.Symbols;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace libIL2AIL.Tracking.Objects
{
    public class ObjectStruct
    {
        string Name { get; set; }
        List<Symbol> References { get; set; }
        List<FieldDeclarationSyntax> FieldDeclatraions { get; set; }

        public ObjectStruct(string name, List<Symbol> symbolRefs, List<FieldDeclarationSyntax> FieldDecls)
        {
            Name = name;
            References = symbolRefs;
            FieldDeclatraions = FieldDecls;
        }
    }
}
