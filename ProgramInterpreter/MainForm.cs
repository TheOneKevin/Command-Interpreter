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
using libIL2AIL;
using System.Threading;

namespace ProgramInterpreter
{
    public partial class MainForm : Form
    {
        #region Variabls

        Editor edit; Output o;
        public MainForm()
        {
            InitializeComponent();
            dockPanel.Theme = new VS2005Theme();
        }

        #endregion

        #region Events

        private void Form1_Load(object sender, EventArgs e)
        {
            newDocument();
            ToolBox t = new ToolBox(); t.Show(dockPanel, DockState.DockRight);
            ErrorList t1 = new ErrorList(); t1.Show(dockPanel, DockState.DockBottom);
            o = new Output(); o.Show(dockPanel, DockState.DockBottom);
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Undo(); }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Redo(); }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Copy(); }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Paste(); }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e){ edit.scintilla.Cut(); }
        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e) { ToolBox t = new ToolBox(); t.Show(dockPanel, DockState.DockRight); }
        private void errorListToolStripMenuItem_Click(object sender, EventArgs e) { ErrorList t = new ErrorList(); t.Show(dockPanel, DockState.DockRight); }
        private void newToolStripMenuItem_Click(object sender, EventArgs e) { newDocument(); }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) { saveFilE(); }

        private void buildProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edit != null)
            {
                Engine i = new Engine();
                i.Compile(edit.filesOpen);
                if (o != null)
                {
                    //o.textBox1.Text = i.output();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setActive();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "ILanguage Code | *.cbil"; open.Title = "Open your file";
            DialogResult s = open.ShowDialog();
            if (s != DialogResult.Cancel)
            {
                if (open.FileName != "" || File.Exists(open.FileName))
                {
                    string file = edit.scintilla.Text;
                    edit.filesOpen = open.FileName;
                    edit.scintilla.Text = File.ReadAllText(open.FileName);
                    edit.Text = "Editor" + " [" + open.FileName.Split('\\')[open.FileName.Split('\\').Length - 1] + "]";
                }
            }
        }

        #endregion

        #region Sub Methods

        private void newDocument()
        {
            Editor e = new Editor();
            e.Show(dockPanel, DockState.Document);
            setActive();
        }

        private void setActive()
        {
            edit = dockPanel.ActiveDocument as Editor;
            if (edit == null)
            {
                newDocument();
                edit = dockPanel.ActiveDocument as Editor;
            }
        }

        private void saveFilE()
        {
            setActive();
            if (edit.filesOpen != "" && File.Exists(edit.filesOpen))
            {
                string file = edit.scintilla.Text;
                File.WriteAllText(edit.filesOpen, file);
                edit.Text = "Editor" + " [" + edit.filesOpen.Split('\\')[edit.filesOpen.Split('\\').Length - 1] + "]";
                edit.needSave = false;
            }
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "ILanguage Code | *.cbil"; save.Title = "Save your file";
                DialogResult s = save.ShowDialog();
                if (s != DialogResult.Cancel)
                {
                    if (File.Exists(edit.filesOpen))
                    {
                        //Users cannot modify during build!
                        try
                        {
                            string file = edit.scintilla.Text;
                            File.WriteAllText(edit.filesOpen, file);
                            edit.needSave = false;
                        }
                        catch (IOException e) { MessageBox.Show("This file is currently locked. " + e.ToString()); Thread.Sleep(1000); }
                    } //end if
                } //end if
            } //end else
        } //end method

        #endregion
    }
}
