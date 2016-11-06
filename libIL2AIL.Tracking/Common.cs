using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Tracking
{
    public static class Common
    {
        static char[] invalidChars = { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '[', ']', '{', '}',
        ':', ';', '"', '\'', '|', '\\', '<', ',' /*, '.' */, '>', '?', '/', ' ' };

        public static bool isValidName(string name)
        {
            bool ret = true;
            foreach(char c in invalidChars)
            {
                if (ret && !name.Contains(c))
                    ret = true;
                else
                {
                    ret = false;
                    break;
                }
            }

            return ret;
        }
    }
}
