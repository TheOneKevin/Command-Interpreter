using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL
{
    public class Error
    {
        ErrorCode errcode;
        string errMsg;
        public Error(ErrorCode errorCode, string errorMessage)
        {
            this.errMsg = errorMessage;
            this.errcode = errorCode;
        }
    }

    public enum ErrorCode //An enum of code errors
    {
        IncorrectCodeFormat, UnknownSyntax
    };
}
