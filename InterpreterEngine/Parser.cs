using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterpreterEngine
{
    public class Parser
    {
        #region Variables

        //Local variables
        Dictionary<string, string> strings = new Dictionary<string, string>();
        //Convertable variables
        Dictionary<string, string> ints = new Dictionary<string, string>();
        Dictionary<string, int> bools = new Dictionary<string, int>();
        //Objects
        Dictionary<string, int> objects = new Dictionary<string, int>();
        Extract ex = new Extract();

        #endregion

        #region Get Line Type

        public bool isVariable(string line)
        {
            var reg = new Regex(@"(?<=^([^""\r\n]|""([^""\\\r\n]|\\.)*"")*)(\=)");
            var matches = reg.Matches(line);
            return matches.Count >= 1;
        }

        public bool isStatement(string line)
        {
            var reg = new Regex(@"(?<=^([^""\r\n]|""([^""\\\r\n]|\\.)*"")*)(\;)");
            var matches = reg.Matches(line);
            return matches.Count >= 1;
        }
        
        public bool isWhile(string line)
        {
            var reg = new Regex(@"(?<=^([^""\r\n]|""([^""\\\r\n]|\\.)*"")*)(\:)");
            var matches = reg.Matches(line);
            return matches.Count >= 1;
        }

        #endregion

        #region Parse Line Type

        public void parseStatement(string line)
        {

        }

        public void parseVariable(string line)
        {
            string pattern = @"(?<=^([^""\r\n]|""([^""\\\r\n]|\\.)*"")*)(\=)";
            RegexOptions regexOptions = RegexOptions.None;
            Regex regex = new Regex(pattern, regexOptions);
            string[] result = regex.Split(line);
            //We are going to be VERY strict on syntax. A variable declaration MUST be
            //[type] [name] = [value]; i.e., int foo_bar = 3; When split with regex, we're
            //gonna get an array with 2 entries, "int foo_bar" and "3;"
            if (result.Length == 4)
            {
                string[] foo = result[0].Trim().Split();
                string value = removeEnding(false, result[3].Trim());
                int i;
                //Then we split the "int foo_bar" into "int" and "foo_bar" If array length exceeds
                //2, we throw and error.
                if (foo.Length == 2)
                {
                    //We are going to check what type the variable is. We assume the type is
                    //the first entry in array. If type is not found, we throw and error.
                    //If you want more types, then feel free to add more cases...... :)
                    if(objects.ContainsKey(foo[0]))
                    {
						char[] myChar = { 'n', 'e', 'w' };
						value = value.TrimStart(myChar).Trim();
                    }
                    switch (foo[0])
                    {
                        case "int":
                            ex.addTokenI(foo[1], value, ints);
                            //else throw error
                            break;
                        case "string":
                            ex.addTokenS(foo[1], value, strings);
                            //else throw error
                            break;
                        case "bool":
                            if (int.TryParse(value, out i))
                                ex.addTokenB(foo[1], i, bools);
                            //else throw error
                            break;
                        default: break; //Throw error
                    }
                }
            }
            //else
                //Error
        }

        public void parseWhile(string line)
        {

        }

        public string removeEnding(bool isWhile, string inputData)
        {
            string pattern;
            if(isWhile)
                pattern = @"(?<=^([^""\r\n]|""([^""\\\r\n]|\\.)*"")*)(\:)";
            else
                pattern = @"(?<=^([^""\r\n]|""([^""\\\r\n]|\\.)*"")*)(\;)";
            RegexOptions regexOptions = RegexOptions.None;
            Regex regex = new Regex(pattern, regexOptions);
            string replacement = @"";
            return regex.Replace(inputData, replacement);
        }

        #endregion

        public Parser()
        {
            initObjects();
        }

        public void initObjects()
        {
            objects.Add("rPlayer", 0); objects.Add("aPlayer", 1); objects.Add("pPlayer", 2); objects.Add("aEntity", 3);
            objects.Add("Item", 4); objects.Add("Entity", 5); objects.Add("Text[]", 6);
        }

    }
}
