namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    partial class SharpCodeDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharpCodeDialog));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsCompile = new System.Windows.Forms.ToolStripButton();
            this.tsSaveClose = new System.Windows.Forms.ToolStripButton();
            this.tsCancel = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tsImport = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsImport,
            this.tsCompile,
            this.tsSaveClose,
            this.tsCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(586, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsCompile
            // 
            this.tsCompile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsCompile.Image = ((System.Drawing.Image)(resources.GetObject("tsCompile.Image")));
            this.tsCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCompile.Name = "tsCompile";
            this.tsCompile.Size = new System.Drawing.Size(35, 22);
            this.tsCompile.Text = "编译";
            this.tsCompile.Click += new System.EventHandler(this.tsCompile_Click);
            // 
            // tsSaveClose
            // 
            this.tsSaveClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsSaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveClose.Image")));
            this.tsSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveClose.Name = "tsSaveClose";
            this.tsSaveClose.Size = new System.Drawing.Size(71, 22);
            this.tsSaveClose.Text = "保存并关闭";
            this.tsSaveClose.Click += new System.EventHandler(this.tsSaveClose_Click);
            // 
            // tsCancel
            // 
            this.tsCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsCancel.Image")));
            this.tsCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCancel.Name = "tsCancel";
            this.tsCancel.Size = new System.Drawing.Size(35, 22);
            this.tsCancel.Text = "取消";
            this.tsCancel.Click += new System.EventHandler(this.tsCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.elementHost1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(586, 500);
            this.splitContainer1.SplitterDistance = 368;
            this.splitContainer1.TabIndex = 1;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(586, 368);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(586, 128);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "No Errors";
            // 
            // tsImport
            // 
            this.tsImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsImport.Image = ((System.Drawing.Image)(resources.GetObject("tsImport.Image")));
            this.tsImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsImport.Name = "tsImport";
            this.tsImport.Size = new System.Drawing.Size(47, 22);
            this.tsImport.Text = "输入库";
            this.tsImport.Click += new System.EventHandler(this.tsImport_Click);
            // 
            // SharpCodeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 525);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SharpCodeDialog";
            this.Text = "代码编辑器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsCompile;
        private System.Windows.Forms.ToolStripButton tsSaveClose;
        private System.Windows.Forms.ToolStripButton tsCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripButton tsImport;

    }
}