using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterpreterEngine
{
    class Parser
    {
        #region Get Line Type

        public bool isVariable(string line)
        {
            var reg = new Regex("=([^;]*);");
            var matches = reg.Matches(line);
            return matches.Count >= 1;
        }

        public bool isStatement(string line)
        {
            var reg = new Regex("(?<=^([^\"\r\n]|\"([^\"\\\\\r\n]|\\\\.)*\")*)(;)");
            var matches = reg.Matches(line);
            return matches.Count >= 1;
        }
        
        public bool isWhile(string line)
        {
            var reg = new Regex("(?<=^([^\"\r\n]|\"([^\"\\\\\r\n]|\\\\.)*\")*)(:)");
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

        }

        public void parseWhile(string line)
        {

        }

        #endregion

    }
}
