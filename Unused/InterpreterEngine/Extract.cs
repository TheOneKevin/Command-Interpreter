using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterpreterEngine
{
    public class Extract
    {
        public string extractString(string input)
        {
            //http://stackoverflow.com/questions/2148587/finding-quoted-strings-with-escaped-quotes-in-c-sharp-using-a-regular-expression
            //GODLY :)
            var reg = new Regex(@"""[^""\\]*(?:\\.[^""\\]*)*""");
            var matches = reg.Matches(input);
            foreach (var item in matches)
            {
                string o = item.ToString().Remove(item.ToString().Length - 1);
                string str = Regex.Unescape(o.Remove(0, 1));
                return str;
            }
            return null;
        }

        public void addTokenS(string name, string value, Dictionary<string, string> d){ d.Add(name, value); }
        public void addTokenI(string name, string value, Dictionary<string, string> d){ d.Add(name, value); }
        public void addTokenB(string name, int value, Dictionary<string, int> d){ d.Add(name, value); }

        public string[] extractParenthesis(string input)
        {
            var regex = new Regex(@"
            \(                    # Match (
            (
                [^()]+            # all chars except ()
                | (?<Level>\()    # or if ( then Level += 1
                | (?<-Level>\))   # or if ) then Level -= 1
             )+                    # Repeat (to go from inside to outside)
            (?(Level)(?!))        # zero-width negative lookahead assertion
            \)                    # Match )",
            RegexOptions.IgnorePatternWhitespace);
            string[] ret = new string[regex.Match(input).Length]; int i = 0;
            foreach (Match c in regex.Matches(input))
            {
                ret[i] = c.Value.Trim('(', ')'); i++;
            }
            ret = ret.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return ret;
        }

    }
}
