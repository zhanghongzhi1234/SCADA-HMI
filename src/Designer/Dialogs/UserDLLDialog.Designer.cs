namespace FreeSCADA.Designer.Dialogs
{
    partial class UserDLLDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.ListView dllList;
        private System.Windows.Forms.ColumnHeader NamecolumnHeader;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
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
            this.components = new System.ComponentModel.Container();
            this.dllList = new System.Windows.Forms.ListView();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.okbutton = new System.Windows.Forms.Button();
            this.NamecolumnHeader = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // dllList
            // 
            this.dllList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.dllList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
			{
				this.NamecolumnHeader,
				this.columnHeader2
			});
            this.dllList.FullRowSelect = true;
            this.dllList.GridLines = true;
            this.dllList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dllList.LabelEdit = true;
            this.dllList.Location = new System.Drawing.Point(12, 12);
            this.dllList.Name = "dllList";
            this.dllList.Size = new System.Drawing.Size(363, 265);
            this.dllList.SmallImageList = this.imageList1;
            this.dllList.TabIndex = 0;
            this.dllList.UseCompatibleStateImageBehavior = false;
            this.dllList.View = System.Windows.Forms.View.Details;
            this.dllList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.imageList_AfterLabelEdit);
            this.NamecolumnHeader.Text = "名称";
            this.NamecolumnHeader.Width = 175;
            this.columnHeader2.Text = "尺寸";
            this.columnHeader2.Width = 112;
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(1, 20);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(11, 289);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "添加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(107, 289);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "删除";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // okbutton
            // 
            this.okbutton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbutton.Location = new System.Drawing.Point(299, 289);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 3;
            this.okbutton.Text = "关闭";
            this.okbutton.UseVisualStyleBackColor = true;
            // 
            // UserDLLDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 331);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.dllList);
            this.Name = "UserDLLDialog";
            this.Text = "用户DLL列表";
            this.ResumeLayout(false);

        }

        #endregion

    }
}