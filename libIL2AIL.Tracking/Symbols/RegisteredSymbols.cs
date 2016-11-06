using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Tracking.Symbols
{
    public static class RegisteredSymbols
    {
        // The id will be a hashed string
        public static readonly Dictionary<string, Symbol> syms = new Dictionary<string, Symbol>();
        static int tmpCounter;

        public static Symbol getSymbol(string symbolName)
        {
            Symbol ret = new Symbol();
            if (syms.ContainsKey(Hash.GetHashString(symbolName)))
                syms.TryGetValue(Hash.GetHashString(symbolName), out ret);
            if (ret.Name == symbolName)
                return ret;
            return null;
        }

        public static Symbol getTmpSym(int id)
        {
            Symbol ret = new Symbol();
            if (syms.ContainsKey("tmp" + id))
                syms.TryGetValue("tmp" + id, out ret);
            if (ret.Name == "tmp" + id)
                return ret;
            return null;
        }

        public static string registerSym(Symbol s)
        {
            if (!syms.ContainsKey(Hash.GetHashString(s.Name)) && Common.isValidName(s.Name))
            {
                syms.Add(Hash.GetHashString(s.Name), s);
                return Hash.GetHashString(s.Name);
            }

            else
                return null;
        }

        public static int registerTmp(Symbol s)
        {
            s.Name = "tmp" + tmpCounter; //We tamper with the name, but leave the code intact
            syms.Add("tmp" + tmpCounter, s);
            return tmpCounter++; //Return tempCounter, then increment
        }
    }
}
