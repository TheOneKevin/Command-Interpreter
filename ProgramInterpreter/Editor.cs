using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Configuration;
using System.IO;

//Most of this consists of code from the ScintillaNet WIKI
namespace ProgramInterpreter
{
    public partial class Editor : DockContent
    {
        public bool needSave = false; public string filesOpen;
        private int maxLineNumberCharLength; int lastCaretPos = 0;
        private const int BOOKMARK_MARGIN = 1; // Conventionally the symbol margin
        private const int BOOKMARK_MARKER = 3; // Arbitrary. Any valid index would work.
        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.StyleClearAll();

            //Line number
            scintilla.Styles[Style.LineNumber].ForeColor = Color.Indigo;

            // Configure the CBIL lexer styles
            scintilla.Styles[CBILexar.StyleDefault].ForeColor = Color.DarkGray;
            scintilla.Styles[CBILexar.StyleDefault].Italic = true;
            scintilla.Styles[CBILexar.StyleComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[CBILexar.StyleNumber].ForeColor = Color.DarkOrchid;
            scintilla.Styles[CBILexar.StyleKeyword1].ForeColor = Color.DarkSlateBlue;
            scintilla.Styles[CBILexar.StyleKeyword2].ForeColor = Color.Tomato;
            scintilla.Styles[CBILexar.StyleKeyword3].ForeColor = Color.BlueViolet;
            scintilla.Styles[CBILexar.StyleKeyword3].Bold = true;
            scintilla.Styles[CBILexar.StyleIdentifier].ForeColor = Color.RoyalBlue;
            scintilla.Styles[CBILexar.StyleString].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[CBILexar.StyleThings].ForeColor = Color.Black;

            //Brace matching
            scintilla.IndentationGuides = IndentView.LookBoth;
            scintilla.Styles[Style.BraceLight].BackColor = Color.GreenYellow;
            scintilla.Styles[Style.BraceLight].ForeColor = Color.BlueViolet;
            scintilla.Styles[Style.BraceBad].ForeColor = Color.Red;

            //Breakpoints
            var margin = scintilla.Margins[BOOKMARK_MARGIN];
            margin.Width = 16;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = Marker.MaskAll;
            margin.Cursor = MarginCursor.Arrow;

            var marker = scintilla.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Bookmark;
            marker.SetBackColor(Color.Crimson);
            marker.SetForeColor(Color.Black);
        }

        private void scintilla_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            // Set the keywords
            string keywords1 = "while if true false _mc bool int float string";
            string keywords2 = "aPlayer rPlayer pPlayer aEntity Item";
            string keywords3 = "achievement blockdata clear clone defaultgamemode difficulty effect enchant " +
                "entitydata execute fill gamemode gamerule give kill particle playsound replaceitem say scoreboard setblock setworldspawn spawnpoint stats stopsound summon teleport tell tellraw testfor testforblock testforblocks time title toggledownfall trigger weather worldborder xp";
            string operands = "{ } [ ] ( ) ; : | + = - * / \\ ' < > . , ? ~";
            CBILexar l = new CBILexar(keywords1, keywords2, keywords3, operands);
            l.Style(scintilla, scintilla.GetEndStyled(), e.Position);
        }

        private void scintilla_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
            this.Text = this.Text.Trim('*') + "*"; needSave = true;
        }

        private static bool IsBrace(int c)
        {
            switch (c)
            {
                case '(':
                case ')':
                case '[':
                case ']':
                case '{':
                case '}':
                case '<':
                case '>':
                    return true;
            }

            return false;
        }

        private void scintilla_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            // Has the caret changed position?
            var caretPos = scintilla.CurrentPosition;
            if (lastCaretPos != caretPos)
            {
                lastCaretPos = caretPos;
                var bracePos1 = -1;
                var bracePos2 = -1;

                // Is there a brace to the left or right?
                if (caretPos > 0 && IsBrace(scintilla.GetCharAt(caretPos - 1)))
                    bracePos1 = (caretPos - 1);
                else if (IsBrace(scintilla.GetCharAt(caretPos)))
                    bracePos1 = caretPos;

                if (bracePos1 >= 0)
                {
                    // Find the matching brace
                    bracePos2 = scintilla.BraceMatch(bracePos1);
                    if (bracePos2 == Scintilla.InvalidPosition)
                    {
                        scintilla.BraceBadLight(bracePos1);
                        scintilla.HighlightGuide = 0;
                    }
                    else
                    {
                        scintilla.BraceHighlight(bracePos1, bracePos2);
                        scintilla.HighlightGuide = scintilla.GetColumn(bracePos1);
                    }
                }
                else
                {
                    // Turn off brace matching
                    scintilla.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                    scintilla.HighlightGuide = 0;
                }
            }
        }

        private void HighlightWord(string text)
        {
            // Indicators 0-7 could be in use by a lexer
            // so we'll use indicator 8 to highlight words.
            const int NUM = 8;

            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = NUM;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            // Update indicator appearance
            scintilla.Indicators[NUM].Style = IndicatorStyle.StraightBox;
            scintilla.Indicators[NUM].Under = true;
            scintilla.Indicators[NUM].ForeColor = Color.Green;
            scintilla.Indicators[NUM].OutlineAlpha = 50;
            scintilla.Indicators[NUM].Alpha = 30;

            // Search the document
            scintilla.TargetStart = 0;
            scintilla.TargetEnd = scintilla.TextLength;
            scintilla.SearchFlags = SearchFlags.None;
            while (scintilla.SearchInTarget(text) != -1)
            {
                // Mark the search results with the current indicator
                scintilla.IndicatorFillRange(scintilla.TargetStart, scintilla.TargetEnd - scintilla.TargetStart);

                // Search the remainder of the document
                scintilla.TargetStart = scintilla.TargetEnd;
                scintilla.TargetEnd = scintilla.TextLength;
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.needSave)
            {
                DialogResult d = MessageBox.Show("You have unsaved changes. Are you sure you want to quit?", "Unsaved Changes", MessageBoxButtons.YesNo);
                if (d == DialogResult.No)
                {
                    if (filesOpen != "" && File.Exists(filesOpen))
                    {
                        string file = this.scintilla.Text;
                        File.WriteAllText(filesOpen, file);
                        this.Text = "Editor" + " [" + filesOpen.Split('\\')[filesOpen.Split('\\').Length - 1] + "]";
                        this.needSave = false;
                    }
                    else
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.Filter = "ILanguage Code | *.cbil"; save.Title = "Save your file";
                        DialogResult s = save.ShowDialog();
                        if (s != DialogResult.Cancel)
                        {
                            if (File.Exists(this.filesOpen))
                            {
                                string file = this.scintilla.Text;
                                File.WriteAllText(this.filesOpen, file);
                                this.needSave = false;
                            }
                        }
                    }
                }
            }
        }

        private void scintilla_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = scintilla.Lines[scintilla.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }
    }
}
