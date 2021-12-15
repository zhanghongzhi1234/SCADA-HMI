namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    partial class ScriptExpressionBindingPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.expressionEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorbutton = new System.Windows.Forms.Button();
            this.addcolorbutton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.channelsGrid = new ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "脚本函数";
            this.expressionEdit.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.expressionEdit.Location = new System.Drawing.Point(7, 19);
            this.expressionEdit.Name = "expressionEdit";
            this.expressionEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.expressionEdit.Size = new System.Drawing.Size(418, 21);
            this.expressionEdit.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "标签变量";
            this.colorbutton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.colorbutton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colorbutton.Location = new System.Drawing.Point(7, 223);
            this.colorbutton.Name = "colorbutton";
            this.colorbutton.Size = new System.Drawing.Size(52, 23);
            this.colorbutton.TabIndex = 3;
            this.colorbutton.UseVisualStyleBackColor = false;
            this.colorbutton.Click += new System.EventHandler(this.colorbutton_Click);
            this.addcolorbutton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.addcolorbutton.Location = new System.Drawing.Point(75, 223);
            this.addcolorbutton.Name = "addcolorbutton";
            this.addcolorbutton.Size = new System.Drawing.Size(75, 23);
            this.addcolorbutton.TabIndex = 4;
            this.addcolorbutton.Text = "添加颜色";
            this.addcolorbutton.UseVisualStyleBackColor = true;
            this.addcolorbutton.Click += new System.EventHandler(this.addcolorbutton_Click);
            this.checkBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(317, 227);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "修改模式";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.channelsGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.channelsGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3
			});
            this.channelsGrid.FullRowSelect = true;
            this.channelsGrid.GridLines = true;
            this.channelsGrid.Location = new System.Drawing.Point(7, 63);
            this.channelsGrid.MultiSelect = false;
            this.channelsGrid.Name = "channelsGrid";
            this.channelsGrid.OwnerDraw = true;
            this.channelsGrid.Size = new System.Drawing.Size(464, 157);
            this.channelsGrid.SmallImageList = this.imageList1;
            this.channelsGrid.TabIndex = 6;
            this.channelsGrid.UseCompatibleStateImageBehavior = false;
            this.channelsGrid.View = System.Windows.Forms.View.Details;
            this.channelsGrid.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.channelsGrid_DrawColumnHeader);
            this.channelsGrid.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.channelsGrid_DrawSubItem);
            this.channelsGrid.DoubleClick += new System.EventHandler(this.channelsGrid_DoubleClick);
            this.channelsGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.channelsGrid_KeyDown);
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 38;
            this.columnHeader2.Text = "名称";
            this.columnHeader2.Width = 120;
            this.columnHeader3.Text = "值域";
            this.columnHeader3.Width = 88;
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(1, 20);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.button1.Location = new System.Drawing.Point(394, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.button2.Location = new System.Drawing.Point(431, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.channelsGrid);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.addcolorbutton);
            base.Controls.Add(this.colorbutton);
            base.Controls.Add(this.expressionEdit);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ScriptExpressionBindingPanel";
            base.Size = new System.Drawing.Size(474, 249);
            base.ResumeLayout(false);
            base.PerformLayout();
		}

		#endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox expressionEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button colorbutton;
        private System.Windows.Forms.Button addcolorbutton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox checkBox1;
        private ListViewEx channelsGrid;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
	}
}
