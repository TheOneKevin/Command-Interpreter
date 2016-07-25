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

namespace ProgramInterpreter
{
    public partial class MainForm : Form
    {
        Editor edit;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            edit = new Editor();
            edit.Show(dockPanel, DockState.Document);
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Undo(); }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Redo(); }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Copy(); }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Paste(); }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Cut(); }

    }
}
