using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramInterpreter
{
    public class CBILexar
    {
        public const int StyleDefault = 0;
        public const int StyleKeyword1 = 1;
        public const int StyleIdentifier = 2;
        public const int StyleNumber = 3;
        public const int StyleString = 4;
        public const int StyleComment = 5;
        public const int StyleKeyword2 = 6;
        public const int StyleKeyword3 = 7;
        public const int StyleThings = 8;

        private const int STATE_UNKNOWN = 0;
        private const int STATE_IDENTIFIER = 1;
        private const int STATE_NUMBER = 2;
        private const int STATE_STRING = 3;
        private const int STATE_DEFAULT = 4;
        private const int STATE_LINECOMMENT = 5;

        private bool isComment = false;

        private HashSet<string> keywords1;
        private HashSet<string> keywords2;
        private HashSet<string> keywords3;
        private HashSet<string> operands;

        public void Style(Scintilla scintilla, int startPos, int endPos)
        {
            // Back up to the line start
            var line = scintilla.LineFromPosition(startPos);
            startPos = scintilla.Lines[line].Position;

            var length = 0;
            var state = STATE_UNKNOWN;

            // Start styling
            scintilla.StartStyling(startPos);
            while (startPos < endPos)
            {
                var c = (char)scintilla.GetCharAt(startPos);

                REPROCESS:
                switch (state)
                {
                    case STATE_UNKNOWN:
                        if (c == '"')
                        {
                            // Start of "string"
                            scintilla.SetStyling(1, StyleString);
                            state = STATE_STRING;
                        }
                        else if(c == '/')
                        {
                            scintilla.SetStyling(1, StyleComment);
                            length++; //Might conflict, IDK
                            state = STATE_LINECOMMENT;
                        }
                        else if (Char.IsDigit(c))
                        {
                            state = STATE_NUMBER;
                            goto REPROCESS;
                        }
                        else if (Char.IsLetter(c))
                        {
                            state = STATE_IDENTIFIER;
                            goto REPROCESS;
                        }
                        else if (operands.Contains(c.ToString()))
                            scintilla.SetStyling(1, StyleThings);

                        else
                        {
                            // Everything else
                            scintilla.SetStyling(1, StyleDefault);
                        }
                        break;

                    case STATE_STRING:
                        if (c == '"')
                        {
                            length++;
                            scintilla.SetStyling(length, StyleString);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                        {
                            length++;
                        }
                        break;

                    case STATE_LINECOMMENT:
                        if (c == '/' && length == 1)
                        {
                            isComment = true;
                            scintilla.SetStyling(1, StyleComment);
                        }
                        /*else if(c == '*' && line == 1)
                        {
                            isComment = true;
                            scintilla.SetStyling(1, StyleComment);
                        }*/
                        else if (isComment)
                            scintilla.SetStyling(1, StyleComment);
                        else
                        {
                            isComment = false;
                            state = STATE_UNKNOWN;
                        }
                        break;

                    case STATE_NUMBER:
                        if (Char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || c == 'x')
                        {
                            length++;
                        }
                        else
                        {
                            scintilla.SetStyling(length, StyleNumber);
                            length = 0;
                            state = STATE_UNKNOWN;
                            goto REPROCESS;
                        }
                        break;

                    case STATE_IDENTIFIER:
                        if (Char.IsLetterOrDigit(c))
                        {
                            length++;
                        }
                        else
                        {
                            var style = StyleIdentifier;
                            var identifier = scintilla.GetTextRange(startPos - length, length);
                            if (keywords1.Contains(identifier))
                                style = StyleKeyword1;
                            else if (keywords2.Contains(identifier))
                                style = StyleKeyword2;
                            else if (keywords3.Contains(identifier))
                                style = StyleKeyword3;
                            scintilla.SetStyling(length, style);
                            length = 0;
                            state = STATE_UNKNOWN;
                            goto REPROCESS;
                        }
                        break;
                }

                startPos++;
            }
        }

        public CBILexar(string words1, string words2, string words3, string operands)
        {
            // Put keywords in a HashSet
            this.keywords1 = new HashSet<string>(Regex.Split(words1 ?? string.Empty, @"\s+").Where(l => !string.IsNullOrEmpty(l)));
            this.keywords2 = new HashSet<string>(Regex.Split(words2 ?? string.Empty, @"\s+").Where(l => !string.IsNullOrEmpty(l)));
            this.keywords3 = new HashSet<string>(Regex.Split(words3 ?? string.Empty, @"\s+").Where(l => !string.IsNullOrEmpty(l)));
            this.operands  = new HashSet<string>(Regex.Split(Regex.Unescape(operands) ?? string.Empty, @"\s+").Where(l => !string.IsNullOrEmpty(l)));
        }
    }
}
