namespace ProgramInterpreter
{
    partial class ToolBox
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
            this.getDirs = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // getDirs
            // 
            this.getDirs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.getDirs.Location = new System.Drawing.Point(0, 238);
            this.getDirs.Name = "getDirs";
            this.getDirs.Size = new System.Drawing.Size(284, 23);
            this.getDirs.TabIndex = 0;
            this.getDirs.Text = "Refresh Tree...";
            this.getDirs.UseVisualStyleBackColor = true;
            this.getDirs.Click += new System.EventHandler(this.getDirs_Click);
            // 
            // open
            // 
            this.open.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.open.Location = new System.Drawing.Point(0, 215);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(284, 23);
            this.open.TabIndex = 1;
            this.open.Text = "Choose Directory...";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(284, 215);
            this.treeView.TabIndex = 2;
            // 
            // ToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.open);
            this.Controls.Add(this.getDirs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ToolBox";
            this.Text = "File System";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button getDirs;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.TreeView treeView;
    }
}