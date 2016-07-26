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
using System.IO;
using ScintillaNET;
using InterpreterEngine;

namespace ProgramInterpreter
{
    public partial class MainForm : Form
    {
        Editor edit; string save;
        public MainForm()
        {
            InitializeComponent();
            dockPanel.Theme = new VS2012LightTheme();
            edit = new Editor();
            edit.Show(dockPanel, DockState.Document);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (edit.needSave)
            {
                DialogResult d = MessageBox.Show("You have unsave changes. Are you sure you want to quit?", "Unsaved Changes", MessageBoxButtons.YesNo);
                if (d == DialogResult.No)
                    saveFilE();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Undo(); }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Redo(); }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Copy(); }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Paste(); }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Cut(); }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFilE();
        }

        private void saveFilE()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "ILanguage Code | *.cbil"; save.Title = "Save your file";
            DialogResult s = save.ShowDialog();
            if (s != DialogResult.Cancel)
            {
                if (save.FileName != "" || !File.Exists(save.FileName))
                {
                    string file = edit.scintilla.Text;
                    this.save = save.FileName;
                    File.WriteAllText(save.FileName, file);
                    edit.Text = "Editor" + " [" + save.FileName.Split('\\')[save.FileName.Split('\\').Length - 1] + "]";
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var document = edit.scintilla.Document;
            edit.scintilla.AddRefDocument(document);

            // Replace the current document with a new one
            edit.scintilla.Document = Document.Empty;
        }

        private void buildProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interpret i = new Interpret(save, "");
            i.Compile();
        }
    }
}
