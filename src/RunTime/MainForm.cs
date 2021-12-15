using System.Windows.Forms;
using FreeSCADA.Archiver;
using FreeSCADA.Common;
using System;
using Microsoft.Win32;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using System.Collections.Generic;

namespace FreeSCADA.RunTime
{
	public partial class MainForm : Form
	{
		WindowManager windowManager;
        private MRUManager mruManager;
        //private ToolStripMenuItem MainMenuItem;
        private ToolStripMenuItem FullScreenMenuItem1;
        private ContextMenuStrip contextMenu;
        private string strProject;
        NamedPipeServerStream pipeServer;
        private string paramName = "ChangeSchema";
		public MainForm()
		{
            this.contextMenu = new ContextMenuStrip();
			this.strProject = "";
            try
            {
                this.Init();
                StartPipeServer("Schema_");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

        public MainForm(string fileToLoad)
        {
			this.contextMenu = new ContextMenuStrip();
			this.strProject = "";
			try
			{
				this.Init();
                this.strProject = fileToLoad;
                string filename = Path.GetFileNameWithoutExtension(fileToLoad);
                StartPipeServer("Schema_" + filename);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
            
        }

        private void StartPipeServer(string pipeName)
        {
            pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous); 
            ThreadPool.QueueUserWorkItem(delegate
                { 
                   pipeServer.BeginWaitForConnection((o) =>
                    { 
                        NamedPipeServerStream server = (NamedPipeServerStream)o.AsyncState;
                        server.EndWaitForConnection(o);
                        StreamReader sr = new StreamReader(server);
                        StreamWriter sw = new StreamWriter(server);
                        string result = null;
                        //string clientName = server.GetImpersonationUserName();
                        //string clientName = "haha";
                        while (true)
                        {
                            result = sr.ReadLine();
                            if (result == null)  
                                break;
                            //this.Invoke((MethodInvoker)delegate { lsbMsg.Items.Add(clientName+" : "+result); });
                            ParseCommand(result);
                        }
                    }, pipeServer);
                });
        }

        private void ParseCommand(string command)
        {
            if (command.Length > 0)
            {
                Dictionary<string, string> commandDict = ParseCommandLine(command);
                if (commandDict != null && commandDict.ContainsKey(paramName))
                {
                    string schemaName = commandDict[paramName];
                    this.Invoke((MethodInvoker)delegate
                    {
                        try
                        {
                            Env.Current.ScriptManager.ScriptApplication.OpenSchema(schemaName);
                        }
                        catch(Exception)
                        {
                        }
                    });
                }
            }
        }

        //解析命令行参数
        public Dictionary<string, string> ParseCommandLine(string s)
        {       //--diagramkey=1,2,3,4
            Dictionary<string, string> ret = null;
            string prefix = "--" + paramName + "=";
            bool bError = false;
            string[] split = s.Split(new string[] { "--", "=", "," }, StringSplitOptions.None);
            if (split.Length >= 3 && split[1] == paramName)
            {
                for (int i = 2; i < split.Length - 1; i++)
                {
                    try
                    {
                        Convert.ToInt32(split[i]);
                    }
                    catch (System.Exception ex)
                    {
                        bError = true;
                        break;
                    }
                }
                if (bError == true)
                {   //继续外循环
 
                }
                else
                {   //找到了正确的值，退出外循环
                    ret = new Dictionary<string, string>();
                    string temp = s.Substring(paramName.Length + 3);
                    ret[paramName] = s.Substring(paramName.Length + 3);
                }
            }
            return ret;
        }

        private void Init()
        {
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.MainMenuItem = new ToolStripMenuItem(StringResources.ViewMainMenu, null, new EventHandler(this.MainMenuItem_Click));
            this.FullScreenMenuItem1 = new ToolStripMenuItem(StringResources.FullScreen, null, new EventHandler(this.FullScreenMenuItem_Click));
            this.InitializeComponent();
            /*this.contextMenu.Items.Add(this.MainMenuItem);
            this.contextMenu.Items.Add(StringResources.OpenPicture, null, new EventHandler(this.SelectSchemaMenuItem_Click));
            this.contextMenu.Items.Add(StringResources.ViewMain, null, new EventHandler(this.OnViewMain));
            this.contextMenu.Items.Add(eYgJk0MPml23SOq7Fh.eyj01t0av(3616));
            this.contextMenu.Items.Add(StringResources.CloseCurrent, null, new EventHandler(this.OnCloseCurrent));*/

            this.contextMenu.Items.Add(this.FullScreenMenuItem1);
            this.FullScreenMenuItem1.Visible = false;
            this.ContextMenuStrip = this.contextMenu;
            try
            {
                Env.Initialize(this, this.mainMenu, this.mainToolbar, FreeSCADA.Interfaces.EnvironmentMode.Runtime);
                ArchiverMain.Initialize();
                //ArchiverMain.Current.OnShowTrend += new ArchiverMain.ShowTrend(this.OnShowTrend);
                CommandManager.viewContext = new BaseCommandContext(this.viewSubMenu.DropDown, null);
                this.mruManager = new MRUManager(this.mRUstartToolStripMenuItem, this.toolStripSeparator3);
                this.windowManager = new WindowManager(this, this.mruManager);
                //this.windowManager = new WindowManager(this.dockPanel, this.mruManager);
                //this.windowManager.OnCommand += new WindowManager.CommandDeltage(this.windowManager_OnCommand);
               /* IExtend[] extends = Env.Current.ExtendDlls.Extends;
                for (int i = 0; i < extends.Length; i++)
                {
                    IExtend extend = extends[i];
                    if (!extend.Init(Application.ExecutablePath))
                    {
                        Env.Current.Logger.LogError(eYgJk0MPml23SOq7Fh.eyj01t0av(3622), extend.Name + eYgJk0MPml23SOq7Fh.eyj01t0av(3640));
                    }
                    else
                    {
                        Env.Current.Logger.LogInfo(eYgJk0MPml23SOq7Fh.eyj01t0av(3658), eYgJk0MPml23SOq7Fh.eyj01t0av(3676) + extend.Name);
                    }
                }*/
                Env.Current.Project.OnFullScreen += new EventHandler(this.Project_OnFullScreen);
                //Env.Current.Project.ProjectClosed += new EventHandler(this.Project_ProjectClosed);
                //this.UpdateCaption(true);
                this.UpdateCaption();
            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
        }

        private void CheckAutoRun()
        {
            if (Env.Current.Project.projectInfo.AutoStart == 1)
            {
                this.OnRunClick(this, null);
            }
            else
            {
                this.OnStopClick(this, null);
            }
            /*if (Env.Current.Project.projectInfo.FullScreen == 1)
            {
                FullScreenMenuItem_Click(null, EventArgs.Empty);
            }*/
            this.Cursor = Cursors.Arrow;
            this.UpdateCaption();
        }

		void UpdateCaption()
		{
			if (Env.Current.Project.FileName == "")
				Text = StringResources.MainWindowName;
			else
				Text = string.Format(StringResources.MainWindowNameEx, Env.Current.Project.FileName);

			showTableButton.Enabled = ArchiverMain.Current.DatabaseSettings.EnableArchiving;
		}

		private void OnLoadProjectClick(object sender, System.EventArgs e)
		{
			windowManager.LoadProject();
            CheckAutoRun();
			UpdateCaption();
		}

		private void OnMenuExitClick(object sender, System.EventArgs e)
		{
			Close();
		}

		private void OnRunClick(object sender, System.EventArgs e)
		{
            
            if (Env.Current.Project.FileName != "" && windowManager.StartRuntime())
            {   
                runButton.Enabled = false;
                refreshButton.Enabled = false;
                stopButton.Enabled = true;

                this.windowManager.OnOpenProjectEntity(ProjectEntityType.Schema, Env.Current.Project.projectInfo.StartPage, "");
            }

            if (Env.Current.Project.projectInfo.FullScreen == 1)
            {
                FullScreenMenuItem_Click(null, EventArgs.Empty);
            }
		}

		private void OnStopClick(object sender, System.EventArgs e)
		{
            windowManager.StopRuntime();
            runButton.Enabled = true;
            refreshButton.Enabled = true;
            stopButton.Enabled = false;
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			ArchiverMain.Current.Stop();
			Env.Current.CommunicationPlugins.Disconnect();
            Env.Current.Project.OnFullScreen -= new EventHandler(this.Project_OnFullScreen);
		}

        private void refreshButton_Click(object sender, System.EventArgs e)
        {
            windowManager.LoadProject(Env.Current.Project.FileName);
            CheckAutoRun();
            UpdateCaption();
        }

		private void showTableButton_Click(object sender, System.EventArgs e)
		{
			//windowManager.ShowQueryView();
		}

        public void FullScreenMenuItem_Click(object sender, System.EventArgs e)
        {
            this.FullScreenMenuItem.Checked = !this.FullScreenMenuItem.Checked;
            Env.Current.Project.FullScreen = !Env.Current.Project.FullScreen;
            Project_OnFullScreen(this, EventArgs.Empty);
        }

        public void Project_OnFullScreen(object sender, EventArgs e)
        {
            bool fullScreen = Env.Current.Project.FullScreen;
            this.FullScreenMenuItem.Checked = fullScreen;
            if(FullScreenMenuItem1 != null)
                this.FullScreenMenuItem1.Checked = fullScreen;
            if (fullScreen)
            {
                /*if (base.WindowState != FormWindowState.Maximized)
                {
                    base.WindowState = FormWindowState.Maximized;
                }*/
                /*base.MainMenuStrip.Visible = false;*/
                this.mainMenu.Visible = false;
                this.mainToolbar.Visible = false;
                this.statusStrip1.Visible = false;

                //this.windowManager.HideLogView();
                if (base.FormBorderStyle != FormBorderStyle.None)
                {
                    base.FormBorderStyle = FormBorderStyle.None;
                }
                this.Left = Env.Current.Project.projectInfo.originX;
                this.Top = Env.Current.Project.projectInfo.originY;
                this.Width = Convert.ToInt32(windowManager.currentSchema.MainCanvas.Width);
                this.Height = Convert.ToInt32(windowManager.currentSchema.MainCanvas.Height);
                foreach(Form frm in this.MdiChildren)
                {
                    //if(frm.WindowState==FormWindowState.Normal)
                        //frm.WindowState = FormWindowState.Maximized; //变为最小化，如果想写最大化可以做相应更改
                }
                this.SetBevel(false);
            }
            else
            {
                //base.MainMenuStrip.Visible = true;
                this.mainMenu.Visible = true;
                this.mainToolbar.Visible = true;
                this.statusStrip1.Visible = true;
                this.ClientSize = new System.Drawing.Size(800, 600);
                this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
                //this.windowManager.ShowLogView();
                base.FormBorderStyle = FormBorderStyle.Sizable;
                this.SetBevel(true);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RegFile("hongzhi4.fs2", "fs2", "logo.ico");
            if (strProject != "")
            {
                windowManager.LoadProject(strProject);
                CheckAutoRun();
            }
        }

        public void RegFile(string fileTypeName, string fileExt, string fileIcon)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("." + fileExt);
            if (key == null)
            {
                key = Registry.ClassesRoot.CreateSubKey("." + fileExt);
                key.SetValue("", fileTypeName + "." + fileExt);
                key.SetValue("Content Type", "application/" + fileExt);

                key = Registry.ClassesRoot.CreateSubKey(fileTypeName + "." + fileExt);
                key.SetValue("", fileTypeName);

                RegistryKey keySub = key.CreateSubKey("DefaultIcon");
                keySub.SetValue("", System.Windows.Forms.Application.StartupPath + "\\" + fileIcon);
                keySub = key.CreateSubKey("shell\\open\\command");
                keySub.SetValue("", "\"" + System.Windows.Forms.Application.ExecutablePath + "\" \"%1\"");
            }

        }
    }
}
