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
        private string inputf, outputf;
        private int count = 0;
        private string line;
        //Interpret class. Set the variables
        public Interpret(string inputPath, string outputPath)
        {
            inputf = inputPath; outputf = outputPath;
        }
        //Does the actual interpreting
        public void Run()
        {
            if (File.Exists(inputf))
            {
                StreamReader file = new StreamReader(inputf);
                while((line = file.ReadLine()) != null)
                {

                    count++;
                }
            }
            else
                throw new FileNotFoundException("File not found!", inputf);
        }

        public int lineNumber() { return count;  }

        private string extractString(string input)
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

    }
}
