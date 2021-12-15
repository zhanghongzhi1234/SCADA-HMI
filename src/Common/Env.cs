using System.Windows.Forms;
using FreeSCADA.Common.Scripting;
using FreeSCADA.Interfaces;
using System;

namespace FreeSCADA.Common
{
	public class Env : IEnvironment
	{
		const string version = "2.0.0.9";
        public const string WPFPath = "Microsoft.NET\\Framework\\v4.0.30319\\WPF\\";
		Commands commands;
		Control mainWindow;
        private IExtendsManager extendsManager;
        private ICommunationPlugsManager communicationPluginsManager;
        //CommunationPlugs communicationPlugins;
        VisualControlsPlugs visualPlugins;
        FreeSCADA.Common.Project project;
		EnvironmentMode mode;
		Logger logger;
        IScriptsManager scriptManager;
        private bool isruning;
        public event System.EventHandler RunStateChanged;

		#region Initialization and singleton implementation
		static Env environmentInstance = null;
		public static void Initialize(Control mainWindow, MenuStrip mainMenu, ToolStrip mainToolbar,  EnvironmentMode mode)
		{
			if (environmentInstance == null)
			{
				environmentInstance = new Env();

				environmentInstance.mode = mode;
				environmentInstance.logger = new Logger();
                environmentInstance.CreateNewProject(false);
				environmentInstance.commands = new Commands(mainMenu, mainToolbar);
				environmentInstance.mainWindow = mainWindow;
                //Type t = new Type("communicationPlugins");
                //environmentInstance.communicationPluginsManager = (ICommunationPlugsManager)System.Activator.CreateInstance(type);
                //environmentInstance.communicationPlugins = new CommunationPlugs();
                environmentInstance.communicationPluginsManager = new CommunationPlugs();
                environmentInstance.scriptManager = new ScriptManager();
                environmentInstance.visualPlugins = new VisualControlsPlugs();
                //environmentInstance.communicationPlugins.Load();
                environmentInstance.communicationPluginsManager.Load();
                environmentInstance.visualPlugins.Load();
				//Should be called after all plugins
				environmentInstance.scriptManager.Initialize();
                //environmentInstance.project.Initialize();     //it will initialize script in MainForm
			}
		}
        
		public static void Deinitialize()
		{
			environmentInstance = null;
		}

		public static Env Current
		{
			get
			{
				if (environmentInstance == null)
					throw new System.NullReferenceException();

				return environmentInstance;
			}
		}

		Env() { }

        public IChannel GetChannel(string channelName)
        {
            if (this.communicationPluginsManager != null)
            {
                return this.communicationPluginsManager.GetChannel(channelName);
            }
            return null;
        }

        public bool IsRuning
        {
            get
            {
                return this.isruning;
            }
            set
            {
                if (this.isruning != value)
                {
                    this.isruning = value;
                    if (this.RunStateChanged != null)
                    {
                        this.RunStateChanged(this, null);
                    }
                }
            }
        }
		#endregion

		#region IEnvironment Members

		public string Version
		{
			get { return version; }
		}

		public ICommands Commands
		{
			get { return commands; }
		}

		public Control MainWindow
		{
			get	{ return mainWindow;	}
		}

		public FreeSCADA.Common.Project Project
		{
			get { return project; }
		}

		public EnvironmentMode Mode
		{
			get { return mode; }
		}

		#endregion

        public ICommunationPlugsManager CommunicationPlugins
        {
            get
            {
                return this.communicationPluginsManager;
            }
        }
        /*
		public CommunationPlugs CommunicationPlugins
		{
			get { return communicationPlugins; }
		}*/

		public VisualControlsPlugs VisualPlugins
		{
			get { return visualPlugins; }
		}

        public IExtendsManager ExtendDlls
        {
            get
            {
                return this.extendsManager;
            }
        }

		public IScriptsManager ScriptManager
		{
			get { return scriptManager; }
		}

		public void CreateNewProject(bool initScript = true)
		{
            if (project != null)            //reuse project, no need create again.
            {
                project.Clear();
                scriptManager.Clear();
                if (initScript == true)
                    project.Initialize();
            }
            else
            {
                project = new FreeSCADA.Common.Project();
                //Env.Current.ScriptManager.CreateNewScript("global", false, false);
            }
		}

		public Logger Logger
		{
			get { return logger; }
			set { logger = value; }
		}
	}
}
