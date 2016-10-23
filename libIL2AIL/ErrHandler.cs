using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL
{
    public static class ErrHandler
    {
        //TODO: Add an error handler or message display
        public static List<Error> errs = new List<Error>(); //A list of code errors

        public static void registerErr(Error err)
        {
            errs.Add(err); //Let's add it to our list
        }

        public static bool hasErrors()
        {
            return errs.Count > 0;
        }
    }
}
