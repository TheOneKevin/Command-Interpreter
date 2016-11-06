using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.Tracking
{
    public enum ErrorCode
    {
        StatementSyntaxNotRecognized,
        ExpressionSyntaxNotRecognized,
        InvalidFileStructure,
        InvalidStatementStructure
    }

    public static class ErrorHandler
    {
        public static void registerError(ErrorCode ec, string msg, ErrorList el)
        {
            el.errorList.Add(new Error(ec, msg, 0));
        }
    }

    public class ErrorList
    {
        public List<Error> errorList;
        public ErrorList()
        {
            errorList = new List<Error>();
        }
    }

    public class Error
    {
        ErrorCode ErrCode { get; set; }
        string Message { get; set; }
        int LineNumber { get; set; }
        
        public Error(ErrorCode e, string msg, int line)
        {
            ErrCode = e;
            Message = msg;
            LineNumber = line;
        }
    }
}
