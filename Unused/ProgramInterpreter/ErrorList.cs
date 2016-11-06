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
using libIL2AIL;

namespace ProgramInterpreter
{
    public partial class ErrorList : DockContent
    {
        public ErrorList()
        {
            this.Controls.Add(new ListBox() { DataSource = ErrHandler.errs });
            InitializeComponent();
        }
    }
}
