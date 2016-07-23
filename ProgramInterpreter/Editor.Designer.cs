namespace ProgramInterpreter
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.syntaxTextbox = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // syntaxTextbox
            // 
            this.syntaxTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxTextbox.Location = new System.Drawing.Point(0, 0);
            this.syntaxTextbox.Name = "syntaxTextbox";
            this.syntaxTextbox.Size = new System.Drawing.Size(532, 304);
            this.syntaxTextbox.TabIndex = 0;
            this.syntaxTextbox.UseTabs = false;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 304);
            this.Controls.Add(this.syntaxTextbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Editor";
            this.Text = "Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla syntaxTextbox;
    }
}