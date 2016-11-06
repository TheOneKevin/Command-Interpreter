using libIL2AIL.Tracking.Symbols;

namespace libIL2AIL.Tracking.Objects
{
    public class ObjectStruct
    {
        Symbol Reference { get; set; }
        string Name { get; set; }

        public ObjectStruct(string name, Symbol symbolRef)
        {
            Name = name;
            Reference = symbolRef;
        }
    }
}
