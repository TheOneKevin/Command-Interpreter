using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterEngine
{
    public class Translate
    {
        #region Raw Translation
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

        public static string setIntVar(string name)
        {
            return "scoreboard objectives add " + name + " dummy";
        }

        public static string setIntVal(int value, string name)
        {
            return "scoreboard players set @a " + name + " " + value;
        }
        #endregion

        #region Translate Ints

        public static void addInt(List<string> commands, string value, string name)
        {
            int p = 0; //Convert the value to an int
            if (int.TryParse(value, out p))
            {
                commands.Add(setIntVar(name)); commands.Add(setIntVal(p, name));
            }
            else
                Error.throwError("", Engine.count);
        }



        #endregion

    }
}
