using System.Resources;
namespace FreeSCADA.Designer
{
	partial class MainForm
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

		#region Windows Form FreeSCADA.Designer generated code

		/// <summary>
		/// Required method for FreeSCADA.Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
        ResourceManager rm = new ResourceManager(typeof(MainForm));
		private void InitializeComponent()
		{
            System.Windows.Forms.ToolStripButton newProjectButton;
            System.Windows.Forms.ToolStripButton openProjectButton;
            System.Windows.Forms.ToolStripButton toolStripButton3;
            System.Windows.Forms.ToolStripButton toolStripButtonNewSchema;
            System.Windows.Forms.ToolStripButton toolStripButton5;
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportwebMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mRU1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSubMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.projectContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNewSchema = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEventItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appscriptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewScript = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArchiverSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVariablesView = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectSetStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.editSubMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.mainToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.runButton = new System.Windows.Forms.ToolStripButton();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            newProjectButton = new System.Windows.Forms.ToolStripButton();
            openProjectButton = new System.Windows.Forms.ToolStripButton();
            toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            toolStripButtonNewSchema = new System.Windows.Forms.ToolStripButton();
            toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.mnsMain.SuspendLayout();
            this.mainToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // newProjectButton
            // 
            newProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            newProjectButton.Image = global::FreeSCADA.Designer.Properties.Resources.new_file;
            newProjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            newProjectButton.Name = "newProjectButton";
            newProjectButton.Size = new System.Drawing.Size(23, 22);
            newProjectButton.ToolTipText = "新建项目";
            newProjectButton.Click += new System.EventHandler(this.OnNewProjectClick);
            // 
            // openProjectButton
            // 
            openProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            openProjectButton.Image = global::FreeSCADA.Designer.Properties.Resources.open_file;
            openProjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            openProjectButton.Name = "openProjectButton";
            openProjectButton.Size = new System.Drawing.Size(23, 22);
            openProjectButton.ToolTipText = "打开项目";
            openProjectButton.Click += new System.EventHandler(this.OnLoadProjectClick);
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = global::FreeSCADA.Designer.Properties.Resources.disk;
            toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new System.Drawing.Size(23, 22);
            toolStripButton3.ToolTipText = "保存项目";
            toolStripButton3.Click += new System.EventHandler(this.OnSaveFileClick);
            // 
            // toolStripButtonNewSchema
            // 
            toolStripButtonNewSchema.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonNewSchema.Image = global::FreeSCADA.Designer.Properties.Resources.page_white_add;
            toolStripButtonNewSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonNewSchema.Name = "toolStripButtonNewSchema";
            toolStripButtonNewSchema.Size = new System.Drawing.Size(23, 22);
            toolStripButtonNewSchema.ToolTipText = "新视图";
            toolStripButtonNewSchema.Click += new System.EventHandler(this.OnSchemaItemClick);
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = global::FreeSCADA.Designer.Properties.Resources.table_add;
            toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new System.Drawing.Size(23, 22);
            toolStripButton5.Text = "添加响应";
            toolStripButton5.Click += new System.EventHandler(this.OnEventsItemClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 386);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(699, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewProject,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportwebMenuItem,
            this.toolStripMenuItem1,
            this.mRU1ToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // mnuNewProject
            // 
            this.mnuNewProject.Image = global::FreeSCADA.Designer.Properties.Resources.new_file;
            this.mnuNewProject.Name = "mnuNewProject";
            this.mnuNewProject.Size = new System.Drawing.Size(158, 22);
            this.mnuNewProject.Text = "新建项目";
            this.mnuNewProject.Click += new System.EventHandler(this.OnNewProjectClick);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = global::FreeSCADA.Designer.Properties.Resources.open_file;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadToolStripMenuItem.Text = "打开项目";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.OnLoadProjectClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::FreeSCADA.Designer.Properties.Resources.save_file;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveToolStripMenuItem.Text = "保存项目";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveProjectClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::FreeSCADA.Designer.Properties.Resources.save_file;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveAsToolStripMenuItem.Text = "另存为...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportwebMenuItem
            // 
            this.exportwebMenuItem.Image = global::FreeSCADA.Designer.Properties.Resources.save_file;
            this.exportwebMenuItem.Name = "exportwebMenuItem";
            this.exportwebMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exportwebMenuItem.Text = "保存为Web版本";
            this.exportwebMenuItem.Click += new System.EventHandler(this.exportwebMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // mRU1ToolStripMenuItem
            // 
            this.mRU1ToolStripMenuItem.Name = "mRU1ToolStripMenuItem";
            this.mRU1ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.mRU1ToolStripMenuItem.Text = "MRU_start";
            this.mRU1ToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitToolStripMenuItem.Text = "退出";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnMenuExitClick);
            // 
            // viewSubMenu
            // 
            this.viewSubMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectContentToolStripMenuItem});
            this.viewSubMenu.Name = "viewSubMenu";
            this.viewSubMenu.Size = new System.Drawing.Size(43, 20);
            this.viewSubMenu.Text = "视图";
            // 
            // projectContentToolStripMenuItem
            // 
            this.projectContentToolStripMenuItem.Name = "projectContentToolStripMenuItem";
            this.projectContentToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.projectContentToolStripMenuItem.Text = "项目内容";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variablesToolStripMenuItem,
            this.mediaContentToolStripMenuItem,
            this.toolStripSeparator5,
            this.mnuNewSchema,
            this.mnuEventItem,
            this.appscriptMenuItem,
            this.mnuNewScript,
            this.mnuArchiverSetting,
            this.mnuVariablesView,
            this.ProjectSetStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.projectToolStripMenuItem.Text = "项目";
            // 
            // variablesToolStripMenuItem
            // 
            this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
            this.variablesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.variablesToolStripMenuItem.Text = "变量查看...";
            this.variablesToolStripMenuItem.Click += new System.EventHandler(this.OnMenuVariables);
            // 
            // mediaContentToolStripMenuItem
            // 
            this.mediaContentToolStripMenuItem.Name = "mediaContentToolStripMenuItem";
            this.mediaContentToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.mediaContentToolStripMenuItem.Text = "图形查看...";
            this.mediaContentToolStripMenuItem.Click += new System.EventHandler(this.OnMenuMediaContent);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(155, 6);
            // 
            // mnuNewSchema
            // 
            this.mnuNewSchema.Image = global::FreeSCADA.Designer.Properties.Resources.page_white_add;
            this.mnuNewSchema.Name = "mnuNewSchema";
            this.mnuNewSchema.Size = new System.Drawing.Size(158, 22);
            this.mnuNewSchema.Text = "新视图";
            this.mnuNewSchema.Click += new System.EventHandler(this.OnSchemaItemClick);
            // 
            // mnuEventItem
            // 
            this.mnuEventItem.Image = global::FreeSCADA.Designer.Properties.Resources.table_add;
            this.mnuEventItem.Name = "mnuEventItem";
            this.mnuEventItem.Size = new System.Drawing.Size(158, 22);
            this.mnuEventItem.Text = "添加响应";
            this.mnuEventItem.Click += new System.EventHandler(this.OnEventsItemClick);
            // 
            // appscriptMenuItem
            // 
            this.appscriptMenuItem.Name = "appscriptMenuItem";
            this.appscriptMenuItem.Size = new System.Drawing.Size(158, 22);
            this.appscriptMenuItem.Text = "应用程序脚本";
            this.appscriptMenuItem.Click += new System.EventHandler(this.appscriptMenuItem_Click);
            // 
            // mnuNewScript
            // 
            this.mnuNewScript.Image = global::FreeSCADA.Designer.Properties.Resources.script_add;
            this.mnuNewScript.Name = "mnuNewScript";
            this.mnuNewScript.Size = new System.Drawing.Size(158, 22);
            this.mnuNewScript.Text = "添加脚本";
            this.mnuNewScript.Click += new System.EventHandler(this.OnAddNewScriptClick);
            // 
            // mnuArchiverSetting
            // 
            this.mnuArchiverSetting.Image = global::FreeSCADA.Designer.Properties.Resources.db_settings;
            this.mnuArchiverSetting.Name = "mnuArchiverSetting";
            this.mnuArchiverSetting.Size = new System.Drawing.Size(158, 22);
            this.mnuArchiverSetting.Text = "归档设置";
            this.mnuArchiverSetting.Click += new System.EventHandler(this.OnArchiverSettingsClick);
            // 
            // mnuVariablesView
            // 
            this.mnuVariablesView.Image = global::FreeSCADA.Designer.Properties.Resources.tree_channels;
            this.mnuVariablesView.Name = "mnuVariablesView";
            this.mnuVariablesView.Size = new System.Drawing.Size(158, 22);
            this.mnuVariablesView.Text = "驱动及标签浏览";
            this.mnuVariablesView.Click += new System.EventHandler(this.OnVariablesViewClick);
            // 
            // ProjectSetStripMenuItem
            // 
            this.ProjectSetStripMenuItem.Name = "ProjectSetStripMenuItem";
            this.ProjectSetStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.ProjectSetStripMenuItem.Text = "项目设置";
            this.ProjectSetStripMenuItem.Click += new System.EventHandler(this.ProjectSetStripMenuItem_Click);
            // 
            // mnsMain
            // 
            this.mnsMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editSubMenu,
            this.viewSubMenu,
            this.projectToolStripMenuItem});
            this.mnsMain.Location = new System.Drawing.Point(0, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Size = new System.Drawing.Size(699, 25);
            this.mnsMain.TabIndex = 0;
            this.mnsMain.Text = "menuStrip1";
            // 
            // editSubMenu
            // 
            this.editSubMenu.Name = "editSubMenu";
            this.editSubMenu.Size = new System.Drawing.Size(43, 20);
            this.editSubMenu.Text = "编辑";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // mainToolbar
            // 
            this.mainToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            newProjectButton,
            openProjectButton,
            toolStripButton3,
            this.toolStripSeparator3,
            toolStripButtonNewSchema,
            toolStripButton5,
            this.toolStripButton2,
            this.toolStripSeparator4,
            this.toolStripButton1,
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.runButton});
            this.mainToolbar.Location = new System.Drawing.Point(0, 25);
            this.mainToolbar.Name = "mainToolbar";
            this.mainToolbar.Size = new System.Drawing.Size(699, 25);
            this.mainToolbar.TabIndex = 3;
            this.mainToolbar.Text = "mainToolbar";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::FreeSCADA.Designer.Properties.Resources.script_add;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "添加脚本";
            this.toolStripButton2.Click += new System.EventHandler(this.OnAddNewScriptClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::FreeSCADA.Designer.Properties.Resources.db_settings;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "归档设置";
            this.toolStripButton1.Click += new System.EventHandler(this.OnArchiverSettingsClick);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::FreeSCADA.Designer.Properties.Resources.tree_channels;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "驱动及标签浏览";
            this.toolStripButton4.Click += new System.EventHandler(this.OnVariablesViewClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // runButton
            // 
            this.runButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runButton.Image = global::FreeSCADA.Designer.Properties.Resources.run;
            this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(23, 22);
            this.runButton.Text = "运行";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(699, 383);
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.AllowDrop = true;
            this.dockPanel.DefaultFloatWindowSize = new System.Drawing.Size(200, 300);
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBottomPortion = 0.15D;
            this.dockPanel.DockLeftPortion = 0.15D;
            this.dockPanel.DockRightPortion = 0.15D;
            this.dockPanel.DockTopPortion = 0.15D;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Location = new System.Drawing.Point(0, 50);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(699, 336);
            this.dockPanel.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 408);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.mainToolbar);
            this.Controls.Add(this.mnsMain);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.mnsMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Design Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.mainToolbar.ResumeLayout(false);
            this.mainToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewSubMenu;
		private System.Windows.Forms.ToolStripMenuItem projectContentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem variablesToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mnsMain;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editSubMenu;
		private System.Windows.Forms.ToolStrip mainToolbar;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton runButton;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
		private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
		private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
		private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
		private System.Windows.Forms.ToolStripContentPanel ContentPanel;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripMenuItem mediaContentToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mRU1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuNewSchema;
        private System.Windows.Forms.ToolStripMenuItem mnuEventItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNewScript;
        private System.Windows.Forms.ToolStripMenuItem mnuArchiverSetting;
        private System.Windows.Forms.ToolStripMenuItem mnuVariablesView;
        private System.Windows.Forms.ToolStripMenuItem mnuNewProject;
        private System.Windows.Forms.ToolStripMenuItem ProjectSetStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appscriptMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportwebMenuItem;
	}
}
