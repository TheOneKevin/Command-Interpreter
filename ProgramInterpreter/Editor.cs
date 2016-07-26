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

namespace ProgramInterpreter
{
    public partial class Editor : DockContent
    {
        public bool needSave = false;
        private int maxLineNumberCharLength;
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
            scintilla.Styles[Style.LineNumber].ForeColor = Color.DarkSlateGray;
            scintilla.StyleClearAll();

            // Configure the CBIL lexer styles
            scintilla.Styles[CBILexar.StyleDefault].ForeColor = Color.Silver;
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
        }

        private void scintilla_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            // Set the keywords
            string keywords1 = "while if true false _mc";
            string keywords2 = "bool int float string aPlayer rPlayer pPlayer aEntity Item";
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
    }
}
