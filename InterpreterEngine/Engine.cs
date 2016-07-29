using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace InterpreterEngine
{
    public class Engine
    {
        #region Variables
        //Make sure there is only 1 instance of this class at any given moment
        //Because we need to retain our variables :)
        Parser p;
        List<string> statements = new List<string>();
        private string inputf, outputf;
        public static int count = 0;
        private string line;
        public static bool errFlag;

        //Interpret class. Set the variables
        public Engine(string inputPath, string outputPath)
        {
            inputf = inputPath; outputf = outputPath;
        }
        #endregion

        #region Main Code
        //Loads the file, reads it line by line, and seperate the statements
        //And puts them into a list
        public void Compile()
        {
            line = "";
            p = new Parser(count);
            if (File.Exists(inputf))
            {
                //Load the file.
                using (var fo = File.Open(inputf, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    StreamReader file = new StreamReader(fo);
                    //To make our lives easier, we get rid of all the comments first
                    StringReader sr = new StringReader(pruneComments(file.ReadToEnd()));
                    while ((line = sr.ReadLine()) != null)
                    {
                        errFlag = false;
                        p.line = count;
                        //First, we reset the list of statements we stored for the last line.
                        statements.Clear();
                        //Then, we seperate the line into statements. Each statement could end in either a
                        //colon or semicolon, so we check for those.
                        if (!string.IsNullOrWhiteSpace(line) || getEOL(line).Length > 0)
                        {
                            int c = 0; int[] t = getEOL(line);
                            foreach (int i in t)
                            {
                                //Then we store each statement inside a list
                                if (c + 1 < t.Length)
                                    statements.Add(line.Substring(t[c], t[c + 1] - t[c]));
                                c++;
                            }
                            parseStatement(); //We pass off the list of statements to the parse function
                        }

                        count++; //Increment the line
                        if (errFlag)
                            break;
                    }
                }
            }
            else
                throw new FileNotFoundException("File not found!", inputf);
        }

        public string output()
        {
            List<string> commands = Parser.commands;
            string s = "";
            foreach(string s1 in commands)
            {
                s += s1 + Environment.NewLine;
            }
            return s;
        }

        #endregion

        #region Subroutines
        
        //Gets line number to use for errors
        public int lineNumber() { return count;  }
        
        //Returns an array of all the semicolons and colons in the line, check for quotes and escaped quotes too.
        //Array contains the index of all the seperators.
        public int[] getEOL(string input)
        {
            var reg = new Regex(@"(?<=^([^""\r\n]|""([^""\\\r\n]|\\.)*"")*)(\;|\:)");
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

        //Uses some dirty(?) regex to remove comments
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
            int i = 0; //Keep track on which statement we are processing
            foreach(string s in statements)
            {
                s.Trim(); //Clean up stuff
                if (p.isVariable(s))
                    p.parseVariable(s);
                else if (p.isStatement(s))
                    p.parseStatement(s);
                else if (p.isWhile(s))
                    p.parseWhile(s);
                else
                    Error.throwError("", count);
                i++;
            }
        }

        #endregion
    }
}
