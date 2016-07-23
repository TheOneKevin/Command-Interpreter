using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterEngine
{
    class Parser
    {
        #region Get Line Type
        public bool isVariable(string line)
        {
            return false;
        }

        public bool isStatement(string line)
        {
            return false;
        }
        
        public bool isWhile(string line)
        {
            return false;
        }

        public bool isComment(string line)
        {
            return false;
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
        
        public void parseComment(string line)
        {

        }

        #endregion

    }
}
