using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Symbols
{
    public static class SymbolMan
    {
        public static Dictionary<string, string> symbolIndex = new Dictionary<string, string>();
        static readonly Random r = new Random();

        public static string registerSymbol(string name)
        {
            if (name.Contains(" ") || symbolIndex.ContainsKey(name))
                return "Invalid symbol";
            else
            {
                symbolIndex.Add(Hash.SHA1Encoding.GetHashString(name), name);
                return Hash.SHA1Encoding.GetHashString(name); //Returns the key
            }
        }

        public static string registerTempSym()
        {
            string name = "_tmp." + r.Next();
            symbolIndex.Add(Hash.SHA1Encoding.GetHashString(name), name);
            return Hash.SHA1Encoding.GetHashString(name);
        }
    }
}
