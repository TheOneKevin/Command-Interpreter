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
        static int line, charc;
        public static void updateLinePos(int aline, int achar)
        {
            line = aline;
            charc = achar;
        }

        public static void registerError(ErrorCode ec, string msg, ErrorList el)
        {
            el.errorList.Add(new Error(ec, msg, line, charc));
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
        int CharPos { get; set; }
        
        public Error(ErrorCode e, string msg, int line, int charpos)
        {
            ErrCode = e;
            Message = msg;
            LineNumber = line;
            CharPos = charpos;
        }
    }
}
