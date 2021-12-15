using FreeSCADA.Common;
using FreeSCADA.Designer.Dialogs;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Indentation.CSharp;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
namespace FreeSCADA.Designer.Views
{
    internal class SharpCodeView : DocumentView
    {
        private IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ElementHost elementHost1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘帖ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox lineNumberTextBox;
        private System.Windows.Forms.ToolStripButton undotoolStripButton;
        private System.Windows.Forms.ToolStripButton redotoolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private TextEditor textedior;
        private SearchPanel searchpanel;
        private string striptname;
        private AppCompletionDatas appcomdata;
        private FoldingManager foldingManager;
        private AbstractFoldingStrategy foldingStrategy;
        private CompletionWindow completionWindow;
        public override bool IsModified
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return base.IsModified;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                if (value != this.IsModified)
                {
                    base.IsModified = value;
                    if (value)
                    {
                        this.saveButton.Enabled = true;
                    }
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.undotoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.redotoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.lineNumberTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘帖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveButton,
            this.toolStripButton1,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripButton10,
            this.undotoolStripButton,
            this.redotoolStripButton,
            this.toolStripSeparator1,
            this.toolStripButton5,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton,
            this.toolStripButton6,
            this.toolStripButton7,
            this.lineNumberTextBox});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(728, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::FreeSCADA.Designer.Properties.Resources.save_file;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "保存";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::FreeSCADA.Designer.Properties.Resources.page_copy;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "复制";
            this.toolStripButton1.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::FreeSCADA.Designer.Properties.Resources.cut;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "toolStripButton8";
            this.toolStripButton8.ToolTipText = "剪切";
            this.toolStripButton8.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::FreeSCADA.Designer.Properties.Resources.paste_plain;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "toolStripButton9";
            this.toolStripButton9.ToolTipText = "粘贴";
            this.toolStripButton9.Click += new System.EventHandler(this.粘帖ToolStripMenuItem_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = global::FreeSCADA.Designer.Properties.Resources.delete;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "toolStripButton10";
            this.toolStripButton10.ToolTipText = "删除";
            this.toolStripButton10.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
            // 
            // undotoolStripButton
            // 
            this.undotoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undotoolStripButton.Image = global::FreeSCADA.Designer.Properties.Resources.arrow_undo;
            this.undotoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undotoolStripButton.Name = "undotoolStripButton";
            this.undotoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.undotoolStripButton.Text = "undotoolStripButton";
            this.undotoolStripButton.ToolTipText = "撤销";
            this.undotoolStripButton.Click += new System.EventHandler(this.undotoolStripButton_Click);
            // 
            // redotoolStripButton
            // 
            this.redotoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redotoolStripButton.Image = global::FreeSCADA.Designer.Properties.Resources.arrow_redo;
            this.redotoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redotoolStripButton.Name = "redotoolStripButton";
            this.redotoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.redotoolStripButton.Text = "redotoolStripButton";
            this.redotoolStripButton.ToolTipText = "重做";
            this.redotoolStripButton.Click += new System.EventHandler(this.redotoolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(47, 22);
            this.toolStripButton5.Text = "输入库";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton2.Text = "编译";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton3.Text = "信息";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton4.Text = "测试";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton
            // 
            this.toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton.Name = "toolStripButton";
            this.toolStripButton.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton.Text = "查找";
            this.toolStripButton.Click += new System.EventHandler(this.toolStripButton_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton6.Text = "替换";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton7.Text = "到行";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // lineNumberTextBox
            // 
            this.lineNumberTextBox.Name = "lineNumberTextBox";
            this.lineNumberTextBox.Size = new System.Drawing.Size(40, 25);
            this.lineNumberTextBox.Text = "1";
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
            this.splitContainer1.Size = new System.Drawing.Size(728, 405);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 4;
            // 
            // elementHost1
            // 
            this.elementHost1.ContextMenuStrip = this.contextMenuStrip;
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(728, 338);
            this.elementHost1.TabIndex = 1;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.粘帖ToolStripMenuItem,
            this.全选ToolStripMenuItem,
            this.清除ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.取消ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(99, 142);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 粘帖ToolStripMenuItem
            // 
            this.粘帖ToolStripMenuItem.Name = "粘帖ToolStripMenuItem";
            this.粘帖ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.粘帖ToolStripMenuItem.Text = "粘帖";
            this.粘帖ToolStripMenuItem.Click += new System.EventHandler(this.粘帖ToolStripMenuItem_Click);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 清除ToolStripMenuItem
            // 
            this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
            this.清除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.清除ToolStripMenuItem.Text = "清除";
            this.清除ToolStripMenuItem.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(95, 6);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.取消ToolStripMenuItem.Text = "取消";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(728, 63);
            this.textBox1.TabIndex = 1;
            // 
            // SharpCodeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 430);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = global::FreeSCADA.Designer.Properties.Resources.csharp;
            this.Name = "SharpCodeView";
            this.Text = "代码编辑器";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public SharpCodeView(string strName)
		{
			this.striptname = "";
			this.appcomdata = new AppCompletionDatas();
			this.striptname = strName;
			this.DocumentName = strName;
			this.DoubleBuffered = true;
			this.InitializeComponent();
			this.textedior = new TextEditor();
			this.textedior.ShowLineNumbers = true;
			this.textedior.WordWrap = true;
			this.textedior.FontFamily = new System.Windows.Media.FontFamily("Arial");
			this.textedior.FontSize = 12.0;
			this.textedior.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
			this.textedior.TextArea.TextEntering += new TextCompositionEventHandler(this.textEditor_TextArea_TextEntering);
			this.textedior.TextArea.TextEntered += new TextCompositionEventHandler(this.textEditor_TextArea_TextEntered);
			this.textedior.KeyDown += new System.Windows.Input.KeyEventHandler(this.textedior_KeyDown);
			this.elementHost1.Child = this.textedior;
			this.textedior.TextArea.IndentationStrategy = new CSharpIndentationStrategy(this.textedior.Options);
			this.foldingStrategy = new BraceFoldingStrategy();
			if (this.foldingStrategy != null)
			{
				if (this.foldingManager == null)
				{
					this.foldingManager = FoldingManager.Install(this.textedior.TextArea);
				}
				this.foldingStrategy.UpdateFoldings(this.foldingManager, this.textedior.Document);
			}
			else
			{
				if (this.foldingManager != null)
				{
					FoldingManager.Uninstall(this.foldingManager);
					this.foldingManager = null;
				}
			}
			if (Env.Current.ScriptManager.ContainsScript(strName))
			{
				this.textedior.Text = Env.Current.ScriptManager.GetScriptText(strName);
			}
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 2000;
			timer.Tick += new System.EventHandler(this.foldingUpdateTimer_Tick);
			timer.Start();
			this.searchpanel = new SearchPanel();
			this.searchpanel.Attach(this.textedior.TextArea);
			this.textedior.TextChanged += new System.EventHandler(this.textedior_TextChanged);
		}
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool SaveDocument()
        {
            Env.Current.ScriptManager.Save(this.DocumentName, this.textedior.Text);
            Env.Current.ScriptManager.SaveRefrence();
            this.IsModified = false;
            return true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textedior_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F && (Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != System.Windows.Input.ModifierKeys.None && this.searchpanel.IsClosed)
            {
                this.searchpanel.Open();
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void foldingUpdateTimer_Tick(object sender, System.EventArgs e)
        {
            if (this.foldingStrategy != null)
            {
                this.foldingStrategy.UpdateFoldings(this.foldingManager, this.textedior.Document);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {/*
            if (e.Text == eYgJk0MPml23SOq7Fh.eyj01t0av(2114))
            {
                int offset = this.textedior.TextArea.Caret.Offset;
                DocumentLine lineByOffset = this.textedior.Document.GetLineByOffset(offset);
                Trace.WriteLine(eYgJk0MPml23SOq7Fh.eyj01t0av(2120) + this.textedior.Document.GetText(lineByOffset.Offset, offset - lineByOffset.Offset));
                this.completionWindow = new CompletionWindow(this.textedior.TextArea);
                System.Collections.Generic.IList<ICompletionData> completionData = this.completionWindow.CompletionList.CompletionData;
                MyCompletionData[] list = this.appcomdata.List;
                for (int i = 0; i < list.Length; i++)
                {
                    MyCompletionData item = list[i];
                    completionData.Add(item);
                }
                this.completionWindow.Show();
                this.completionWindow.Closed += delegate
                {
                    this.completionWindow = null;
                };
            }*/
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textedior_TextChanged(object sender, System.EventArgs e)
        {
            this.IsModified = true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && this.completionWindow != null && !char.IsLetterOrDigit(e.Text[0]))
            {
                this.completionWindow.CompletionList.RequestInsertion(e);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void saveButton_Click(object sender, System.EventArgs e)
        {
            this.SaveDocument();
            Env.Current.ScriptManager.Refrences = Env.Current.ScriptManager.ScriptHost.References;
            this.saveButton.Enabled = false;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void toolStripButton5_Click(object sender, System.EventArgs e)
        {
            ReferanceForm referanceForm = new ReferanceForm();
            string[] references = Env.Current.ScriptManager.ScriptHost.References;
            referanceForm.liblistBox.Items.AddRange(references);
            if (referanceForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string[] array = new string[referanceForm.liblistBox.Items.Count];
                referanceForm.liblistBox.Items.CopyTo(array, 0);
                Env.Current.ScriptManager.ScriptHost.References = array;
                this.IsModified = true;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void toolStripButton2_Click(object sender, System.EventArgs e)
        {
            this.saveButton.PerformClick();
            Env.Current.ScriptManager.ScriptHost.SourceText = this.textedior.Text;
            //if (!Env.Current.ScriptManager.ScriptHost.IsCompiled)         //force compile in any condition
            {
                this.textBox1.Text = "";
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                if (Env.Current.ScriptManager.ScriptHost.Compile(true))
                {
                    string text = stopwatch.ElapsedMilliseconds.ToString();
                    this.textBox1.Text = string.Concat(new string[]
					{
						"[",
						System.DateTime.Now.ToString(),
						"] 编译成功，耗时",
						text,
						"ms"
					});
                }
                else
                {
                    this.textBox1.Text = "[" + System.DateTime.Now.ToString() + "] 编译失败.\r\n";
                }
            }
            System.Windows.Forms.TextBox expr_12C = this.textBox1;
            expr_12C.Text += Env.Current.ScriptManager.ScriptHost.CompilerInfo;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void toolStripButton3_Click(object sender, System.EventArgs e)
        {
            if (!Env.Current.ScriptManager.ScriptHost.IsCompiled)
            {
                this.toolStripButton2.PerformClick();
            }
            this.textBox1.Text = "";
            System.Reflection.Assembly assembly = Env.Current.ScriptManager.ScriptHost.GetAssembly();
            if (assembly != null)
            {
                this.textBox1.AppendText(string.Format("CodeBase: {0}", assembly.CodeBase));
                this.textBox1.AppendText(string.Format("\r\nFullName: {0}", assembly.FullName));
                this.textBox1.AppendText(string.Format("\r\nImageRuntimeVersion: {0}", assembly.ImageRuntimeVersion));
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                System.Type[] types = assembly.GetTypes();
                System.Type[] array = types;
                for (int i = 0; i < array.Length; i++)
                {
                    System.Type type = array[i];
                    this.textBox1.AppendText(string.Format("\r\nType: {0}", type.ToString()));
                    System.Reflection.MethodInfo[] methods = type.GetMethods();
                    System.Reflection.MethodInfo[] array2 = methods;
                    for (int j = 0; j < array2.Length; j++)
                    {
                        System.Reflection.MethodInfo methodInfo = array2[j];
                        this.textBox1.AppendText(string.Format("\r\n\tMethod: {0}", methodInfo.ToString()));
                    }
                }
                Trace.WriteLine("ElapsedMilliseconds: " + stopwatch.ElapsedMilliseconds.ToString());
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void toolStripButton4_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.toolStripButton2_Click(sender, e);
                if (Env.Current.ScriptManager.ScriptHost.IsCompiled)
                {
                    MethodDialog methodDialog = new MethodDialog(Env.Current.ScriptManager.ScriptHost);
                    if (methodDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        System.Reflection.MethodInfo method = Env.Current.ScriptManager.ScriptHost.GetMethod(methodDialog.comboBox1.Text, methodDialog.comboBox2.Text);
                        if (method != null && method.GetParameters().Length == 0)
                        {
                            Env.Current.ScriptManager.ScriptHost.Invoke(methodDialog.comboBox1.Text, methodDialog.comboBox2.Text);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.TextBox expr_C4 = this.textBox1;
                expr_C4.Text = expr_C4.Text + "[" + ex.ToString() + "]";
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void toolStripButton_Click(object sender, System.EventArgs e)
        {
            if (this.searchpanel.IsClosed)
            {
                this.searchpanel.Open();
                return;
            }
            this.searchpanel.Close();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void toolStripButton6_Click(object sender, System.EventArgs e)
        {
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void toolStripButton7_Click(object sender, System.EventArgs e)
        {
            if (this.lineNumberTextBox.Text != "")
            {
                int line = -1;
                if (int.TryParse(this.lineNumberTextBox.Text, out line))
                {
                    this.textedior.TextArea.Focus();
                    this.textedior.ScrollToLine(line);
                    this.textedior.TextArea.Caret.Line = line;
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void 复制ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.textedior.Copy();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void 剪切ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.textedior.Cut();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void 粘帖ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.textedior.Paste();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void 全选ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.textedior.SelectAll();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void 清除ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.textedior.Delete();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void undotoolStripButton_Click(object sender, System.EventArgs e)
        {
            this.textedior.Undo();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void redotoolStripButton_Click(object sender, System.EventArgs e)
        {
            this.textedior.Redo();
        }
    }
}
