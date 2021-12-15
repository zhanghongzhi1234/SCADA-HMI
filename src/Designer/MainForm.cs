using System;
using System.Diagnostics;
using System.Windows.Forms;
using FreeSCADA.Archiver;
using FreeSCADA.Common;
using FreeSCADA.Designer.Dialogs;

namespace FreeSCADA.Designer
{
    /// <summary>
	/// Application main window
	/// </summary>
	public partial class MainForm : Form
	{
        WindowManager windowManager;

		/// <summary>
		/// Constructor
		/// </summary>
		public MainForm()
		{
            if (!this.Init())
            {
                System.Windows.Forms.MessageBox.Show("程序初始化失败", "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                System.Windows.Forms.Application.Exit();
            }
            Env.Current.Project.Initialize();       //Create default script of global and functions
		}

        public MainForm(string fileopen)
		{
            this.Init();
            base.Update();
            this.windowManager.LoadProject(fileopen);
		}
        private bool Init()
        {
            InitializeComponent();
            Env.Initialize(this, mnsMain, mainToolbar, FreeSCADA.Interfaces.EnvironmentMode.Designer);
            ArchiverMain.Initialize();

            CommandManager.fileContext = new BaseCommandContext(fileToolStripMenuItem.DropDown, mainToolbar);
            CommandManager.viewContext = new BaseCommandContext(viewSubMenu.DropDown, mainToolbar);
            CommandManager.documentContext = new BaseCommandContext(editSubMenu.DropDown, mainToolbar);

            ToolStripMenuItem newItem = new ToolStripMenuItem(StringResources.CommandContextHelp);
            mnsMain.Items.Add(newItem);
            CommandManager.helpContext = new BaseCommandContext(newItem.DropDown, null);
            CommandManager.helpContext.AddCommand(new CheckForUpdatesCommand());

            MRUManager mruManager = new MRUManager(mRU1ToolStripMenuItem, toolStripSeparator2);
            windowManager = new WindowManager(dockPanel, mruManager);

            Env.Current.Project.ProjectLoaded += new EventHandler(OnProjectLoaded);
            UpdateCaptionAndCommands();

            return false;
        }

		void OnProjectLoaded(object sender, EventArgs e)
		{
			UpdateCaptionAndCommands();
		}

		private void OnMenuVariables(object sender, System.EventArgs e)
		{
			VariablesDialog frm = new VariablesDialog();
			frm.ShowDialog(this);
		}

		private void OnMenuMediaContent(object sender, EventArgs e)
		{
			ProjectMediaDialog frm = new ProjectMediaDialog();
			frm.ShowDialog(this);
		}

		private void OnMenuExitClick(object sender, System.EventArgs e)
		{
			Close();
		}

		private void OnSchemaItemClick(object sender, System.EventArgs e)
		{
			windowManager.CreateNewSchema();
		}

		private void OnEventsItemClick(object sender, System.EventArgs e)
		{
			windowManager.ShowEvents();
		}

		private void OnVariablesSettingsClick(object sender, EventArgs e)
		{
            windowManager.ShowVariablesView();
		}

		private void OnSaveProjectClick(object sender, System.EventArgs e)
		{
			windowManager.SaveProject();
			UpdateCaptionAndCommands();
		}

		private void OnLoadProjectClick(object sender, System.EventArgs e)
		{
			windowManager.LoadProject();
			UpdateCaptionAndCommands();
		}

		private void OnSaveFileClick(object sender, System.EventArgs e)
		{
			//windowManager.SaveDocument();
            windowManager.SaveProject();
            UpdateCaptionAndCommands();
        }

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !windowManager.Close();
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			Env.Deinitialize();
		}

		void UpdateCaptionAndCommands()
		{
			if (string.IsNullOrEmpty(Env.Current.Project.FileName))
				Text = StringResources.MainWindowName;
			else
				Text = string.Format(StringResources.MainWindowNameEx, Env.Current.Project.FileName);

			if (string.IsNullOrEmpty(Env.Current.Project.FileName))
				runButton.Enabled = false;
			else
				runButton.Enabled = !Env.Current.Project.IsModified;
		}

		private void OnNewProjectClick(object sender, System.EventArgs e)
		{
            if (this.windowManager != null)
            {
                if (windowManager.Close())
                {
                    windowManager.ForceWindowsClose();
                    windowManager.Dispose();
                    Env.Current.CreateNewProject();

                    MRUManager mruManager = new MRUManager(mRU1ToolStripMenuItem, toolStripSeparator2);
                    windowManager = new WindowManager(dockPanel, mruManager);

                    //Env.Current.CreateNewProject();
                    UpdateCaptionAndCommands();
                    System.GC.Collect();
                }
            }
		}

        private void runButton_Click(object sender, EventArgs e)
        {

            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + @"\\RunTime.exe");
            psi.Arguments = "\""+Env.Current.Project.FileName+"\"";
            Process.Start(psi);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = Env.Current.Project.FileName;
			sfd.Filter = StringResources.FileOpenDialogFilter;
			sfd.FilterIndex = 0;
			sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Env.Current.Project.SaveAsFileName = sfd.FileName;
                windowManager.SaveProject();
                UpdateCaptionAndCommands();
            }
        }

        private void OnArchiverSettingsClick(object sender, EventArgs e)
        {
            windowManager.ShowArchiverSettings();
        }

        private void appscriptMenuItem_Click(object sender, EventArgs e)
        {
            if (Env.Current.Project.GetData(ProjectEntityType.Script, "global") == null)
            {
                Env.Current.ScriptManager.CreateNewScript("global", true, false);
                return;
            }
            this.windowManager.OnOpenProjectEntity(ProjectEntityType.Script, "global");
        }

		private void OnAddNewScriptClick(object sender, EventArgs e)
		{
			string newScriptName = Env.Current.Project.GenerateUniqueName(ProjectEntityType.Script, StringResources.UntitledScript);
			Env.Current.ScriptManager.CreateNewScript(newScriptName);
		}

        private void OnVariablesViewClick(object sender, EventArgs e)
        {
            windowManager.ShowVariablesView();
        }

        private void ProjectSetStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectInfoDialog projectInfoDialog = new ProjectInfoDialog();
            projectInfoDialog.textBox1.Text = Env.Current.Project.projectInfo.ProjectName;
            if (Env.Current.Project.projectInfo.WcfDatabaseAdress != "")
            {
                projectInfoDialog.wcfDataTextBox.Text = Env.Current.Project.projectInfo.WcfDatabaseAdress;
            }
            projectInfoDialog.alarmcheckBox.Checked = Env.Current.Project.projectInfo.ShowAlarm;
            projectInfoDialog.comboBox1.Text = Env.Current.Project.projectInfo.StartPage;
            projectInfoDialog.textBox3.Text = Env.Current.Project.projectInfo.Design;
            projectInfoDialog.textBox4.Text = Env.Current.Project.projectInfo.Memo;
            projectInfoDialog.comboBox2.Text = Env.Current.Project.projectInfo.StartPage_Param;
            projectInfoDialog.textBox6.Text = Env.Current.Project.projectInfo.StartScript;
            projectInfoDialog.textBox7.Text = Env.Current.Project.projectInfo.StopScript;
            projectInfoDialog.checkBox1.Checked = (Env.Current.Project.projectInfo.AutoStart > 0);
            projectInfoDialog.usertextBox.Text = Env.Current.Project.projectInfo.UserName_Open;
            projectInfoDialog.pwdtextBox.Text = Env.Current.Project.projectInfo.Password_Open;
            projectInfoDialog.fullscreencheckBox.Checked = (Env.Current.Project.projectInfo.FullScreen > 0);
            projectInfoDialog.userCodecomboBox.Text = Env.Current.Project.projectInfo.UserRunTimeCodeDll;
            projectInfoDialog.autoLogincomboBox.Text = Env.Current.Project.projectInfo.LoginUserName;
            projectInfoDialog.txtOriginX.Text = Env.Current.Project.projectInfo.originX.ToString();
            projectInfoDialog.txtOriginY.Text = Env.Current.Project.projectInfo.originY.ToString();
            if (projectInfoDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Env.Current.Project.projectInfo.ProjectName = projectInfoDialog.textBox1.Text;
                Env.Current.Project.projectInfo.StartPage = projectInfoDialog.comboBox1.Text;
                Env.Current.Project.projectInfo.Design = projectInfoDialog.textBox3.Text;
                Env.Current.Project.projectInfo.Memo = projectInfoDialog.textBox4.Text;
                Env.Current.Project.projectInfo.AutoStart = (projectInfoDialog.checkBox1.Checked ? 1 : 0);
                Env.Current.Project.projectInfo.FullScreen = (projectInfoDialog.fullscreencheckBox.Checked ? 1 : 0);
                Env.Current.Project.projectInfo.StartPage_Param = projectInfoDialog.comboBox2.Text;
                Env.Current.Project.projectInfo.StartScript = projectInfoDialog.textBox6.Text;
                Env.Current.Project.projectInfo.StopScript = projectInfoDialog.textBox7.Text;
                Env.Current.Project.projectInfo.UserName_Open = projectInfoDialog.usertextBox.Text;
                Env.Current.Project.projectInfo.Password_Open = projectInfoDialog.pwdtextBox.Text;
                Env.Current.Project.projectInfo.LoginUserName = projectInfoDialog.autoLogincomboBox.Text;
                Env.Current.Project.projectInfo.WcfDatabaseAdress = projectInfoDialog.wcfDataTextBox.Text;
                Env.Current.Project.projectInfo.UserRunTimeCodeDll = projectInfoDialog.userCodecomboBox.Text;
                Env.Current.Project.projectInfo.ShowAlarm = projectInfoDialog.alarmcheckBox.Checked;
                Env.Current.Project.projectInfo.originX = Convert.ToInt32(projectInfoDialog.txtOriginX.Text);
                Env.Current.Project.projectInfo.originY = Convert.ToInt32(projectInfoDialog.txtOriginY.Text);
                Env.Current.Project.SetModify(true);
            }
        }

        private void exportwebMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "导出到Web版本";
            saveFileDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(Env.Current.Project.FileName);
            saveFileDialog.Filter = "FreeSCADA WebBrowser Project(*.zip)";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Env.Current.Project.ExportWeb(saveFileDialog.FileName, WindowManager.password);
            }
        }
   
    }
}
