using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterEngine
{
    public class Translate
    {
        public static string getSelDecl(int x, int y, int z, int radius, string selector)
        {
            string s = "";
            switch (selector)
            {
                case "aPlayer": s = "@a"; break;
                case "rPlayer": s = "@r"; break;
                case "pPlayer": s = "@p"; break;
                case "ePlayer": s = "@e"; break;
                default: break;
            }
            if (!string.IsNullOrEmpty(s))
                return s + "[x=" + x + ",y=" + y + ",z=" + z + ",r=" + radius + "]";
            else
                return "";
        }
    }
}
