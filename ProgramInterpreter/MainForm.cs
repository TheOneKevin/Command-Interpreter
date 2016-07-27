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
        string filesOpen;
        Editor edit;
        public MainForm()
        {
            InitializeComponent();
            dockPanel.Theme = new VS2012LightTheme();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            newDocument();
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }

        private void newDocument()
        {
            Editor e = new Editor();
            e.Show(dockPanel, DockState.Document);
        }

        private void setActive() { edit = dockPanel.ActiveDocument as Editor; }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            setActive();
            if (edit != null && edit.needSave)
            {
                DialogResult d = MessageBox.Show("You have unsaved changes. Are you sure you want to quit?", "Unsaved Changes", MessageBoxButtons.YesNo);
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
            setActive();
            if (filesOpen != "" && File.Exists(filesOpen))
            {
                string file = edit.scintilla.Text;
                File.WriteAllText(filesOpen, file);
                edit.Text = "Editor" + " [" + filesOpen.Split('\\')[filesOpen.Split('\\').Length - 1] + "]";
                edit.needSave = false;
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
                        string file = edit.scintilla.Text;
                        File.WriteAllText(this.filesOpen, file);
                        edit.needSave = false;
                    }
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newDocument();
        }

        private void buildProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine i = new Engine(filesOpen, "");
            i.Compile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActive();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "ILanguage Code | *.cbil"; open.Title = "Open your file";
            DialogResult s = open.ShowDialog();
            if(s != DialogResult.Cancel)
            {
                if (open.FileName != "" || File.Exists(open.FileName))
                {
                    string file = edit.scintilla.Text;
                    this.filesOpen = open.FileName;
                    edit.scintilla.Text = File.ReadAllText(open.FileName);
                    edit.Text = "Editor" + " [" + open.FileName.Split('\\')[open.FileName.Split('\\').Length - 1] + "]";
                }
            }
        }
        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e) { ToolBox t = new ToolBox(); t.Show(dockPanel, DockState.DockRight); }

    }
}
