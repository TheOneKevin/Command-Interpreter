using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterEngine
{
    public static class Error
    {
        static string error = "DEADBEEF";
        public static void throwError(string error, int line)
        {
            Engine.errFlag = true;
        }
    }
}
