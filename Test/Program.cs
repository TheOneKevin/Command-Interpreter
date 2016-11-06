using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libIL2AIL;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseEngine pe = new ParseEngine("", "");
            string code = 
                @"
                var s = new string();
                s++;
                ";
            pe.ParseFromCache(code);
        }
    }
}
