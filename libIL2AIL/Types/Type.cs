using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Types
{
    public class Type
    {
        protected string TypeText   { get; set; }
        protected int    ID         { get; set; }

        public Type()
        {
            ID = 0;
            TypeText = "void";
        }
    }
}
