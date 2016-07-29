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
        //Selector
        string selector = ""; string selname = "";

        Extract ex = new Extract(); public int line;
        //Final list of commands
        public static List<string> commands = new List<string>();

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

        #region Parse Statement

        public void parseStatement(string line)
        {

        }

        #endregion

        #region Parse Variable

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
                    if (objects.ContainsKey(foo[0]))
                    {
                        char[] myChar = { 'n', 'e', 'w' };
                        value = value.TrimStart(myChar).Trim();
                        if (ex.extractParenthesis(value).Length == 1 && value.Substring(0, value.IndexOf('(')) == foo[0])
                        {
                            string[] idx = ex.extractParenthesis(value)[0].Split(',');
                            if (getSelector(foo[0], idx) != null)
                            {
                                Type.Selectors fooBar = getSelector(foo[0], idx);
                                string p = Translate.getSelDecl(fooBar.x, fooBar.y, fooBar.z, fooBar.radiusMax, foo[0]);
                                if (!string.IsNullOrWhiteSpace(p))
                                    this.selector = p;
                                else
                                    Error.throwError("", this.line);
                                this.selname = foo[1];
                            }
                            else
                                Error.throwError("", this.line);
                        }
                        else
                            Error.throwError("", this.line);
                    }
                    else
                    {
                        //Find type, then add to list of commands
                        switch (foo[0])
                        {
                            case "int":
                                if (!ints.ContainsKey(foo[1]))
                                {
                                    ex.addTokenI(foo[1], value, ints); //Add to dictionary
                                    Translate.addInt(commands, value, foo[1]); //Add to list of commands
                                }
                                else Error.throwError("", this.line);
                                break;
                            case "string":
                                if (!strings.ContainsKey(foo[1]))
                                {
                                    ex.addTokenS(foo[1], value, strings);
                                }
                                else Error.throwError("", this.line);
                                break;
                            case "bool":
                                if (int.TryParse(value, out i))
                                    ex.addTokenB(foo[1], i, bools);
                                else Error.throwError("", this.line);
                                break;
                            default: Error.throwError("", this.line); break;
                        }
                    }
                }
            }
            else
                Error.throwError("", this.line);
        }

        #endregion

        #region Parse While/If/Whatnot

        public void parseWhile(string line)
        {

        }

        #endregion
        
        #region Parser

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

        public Parser(int line)
        {
            initObjects(); this.line = line; commands.Clear();
        }

        public void initObjects()
        {
            //The value of each entry is equal to the number of arguments it takes
            objects.Add("rPlayer", 4); objects.Add("aPlayer", 4); objects.Add("pPlayer", 4); objects.Add("aEntity", 4);
            objects.Add("Item", 4); objects.Add("Entity", 5); objects.Add("Text[]", 13);
        }

        #endregion

        #region Get Object Types

        public Type.Selectors getSelector(string name, string[] idx)
        {
            if (idx.Length == 4)
            {
                int[] xyzr = new int[4]; int i = 0;
                foreach (string s in idx)
                {
                    int.TryParse(idx[i], out xyzr[i]); i++;
                }
                return new Type.Selectors(xyzr[0], xyzr[1], xyzr[2], xyzr[3]);
            }
            else
                return null;
        }


        #endregion
    }
}