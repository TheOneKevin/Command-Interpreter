using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace InterpreterEngine
{
    public class Interpret
    {
        #region Variables
        private string inputf, outputf;
        private int count = 0;
        private string line;
        public List<string> statements = new List<string>();
        //Interpret class. Set the variables
        public Interpret(string inputPath, string outputPath)
        {
            inputf = inputPath; outputf = outputPath;
        }
        #endregion

        #region Main Code
        //Loads the file, reads it line by line, and seperate the statements
        //And puts them into a list
        public void Compile()
        {
            if (File.Exists(inputf))
            {
                StreamReader file = new StreamReader(inputf);
                StringReader sr = new StringReader(pruneComments(file.ReadToEnd()));
                while ((line =sr.ReadLine()) != null)
                {
                    //We need to start the head-aching process of seperating
                    //the statements by semicolon and colon :(
                    if(getEOL(line).Count() != 0)
                    {
                        int c = 0; int[] t = getEOL(line);
                        foreach(int i in t)
                        {
                            //Loop through every entry in array, and seperate the line
                            //Based on semicolon.
                            if(c + 1 < t.Length)
                                statements.Add(line.Substring(t[c], t[c + 1] - t[c]));
                            c++;
                        }
                        parseStatement();
                    }
                    
                    count++;
                }
            }
            else
                throw new FileNotFoundException("File not found!", inputf);
        }

        #endregion

        #region Subroutines
        public int lineNumber() { return count;  }

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
        
        public int[] getEOL(string input)
        {
            var reg = new Regex("(?<=^([^\"\r\n]|\"([^\"\\\\\r\n]|\\\\.)*\")*)(;|:)");
            var matches = reg.Matches(input);
            int[] index = new int[matches.Count + 1];
            index[0] = 0; int c = 1; //Needed 0 in index[0]
            foreach (Match item in matches)
            {
                index[c] = item.Index + 1;
                c++;
            }
            return index;
        }

        public string pruneComments(string input)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            return Regex.Replace(input,
            blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
            me =>
            {
                if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                return me.Value.StartsWith("//") ? Environment.NewLine : "";
                // Keep the literal strings
                return me.Value;
            },
            RegexOptions.Singleline);
        }
        #endregion

        #region Parse

        public void parseStatement()
        {
            Parser p = new Parser();
            int i = 0; //Keep track on which statement we are processing
            foreach(string s in statements)
            {
                s.Trim();
                if (p.isStatement(s))
                    p.parseStatement(s);
                else if (p.isVariable(s))
                    p.parseVariable(s);
                else if (p.isWhile(s))
                    p.parseWhile(s);
                else
                    //Kill the PC

                i++;
            }
        }

        #endregion
    }
}
