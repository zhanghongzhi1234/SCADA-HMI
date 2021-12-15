using System;
using System.Collections.Generic;
using System.Xml;
using FreeSCADA.Interfaces;
using FreeSCADA.Interfaces.Plugins;

namespace FreeSCADA.Communication.OPCPlug
{
	public class Plugin: ICommunicationPlug
	{
		private IEnvironment environment;
		List<IChannel> channels = new List<IChannel>();
		List<ConnectionGroup> connectionGroups = new List<ConnectionGroup>();
		bool connectedFlag = false;
        private System.Timers.Timer aTimer;

		~Plugin()
		{
			if (IsConnected)
				Disconnect();
		}

		#region ICommunicationPlug Members
		public event EventHandler ChannelsChanged;

		public string Name
		{
			get { return StringConstants.PluginName; }
		}

		public IChannel[] Channels
		{
			get { return channels.ToArray(); }
			set 
			{ 
				channels.Clear(); 
				channels.AddRange(value);
				channels.RemoveAll( delegate(IChannel ch) { return ch == null; } );
				FireChannelChangedEvent();
			}
		}

		public string PluginId
		{
			get { return StringConstants.PluginId; }
		}

		public void Initialize(IEnvironment environment)
		{
			this.environment = environment;
			environment.Project.ProjectLoaded += new System.EventHandler(OnProjectLoad);

			LoadSettings();

            if (environment.Mode == EnvironmentMode.Designer)
            {
                ICommandContext context = environment.Commands.GetPredefinedContext(PredefinedContexts.Communication);
                environment.Commands.AddCommand(context, new PropertyCommand(this));
            }
            else
            {
                aTimer = new System.Timers.Timer();
                aTimer.Interval = 2000;

                // Alternate method: create a Timer with an interval argument to the constructor.
                //aTimer = new System.Timers.Timer(2000);

                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer(2000);

                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;

                // Have the timer fire repeated events (true is the default)
                aTimer.AutoReset = true;
            }
		}

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < connectionGroups.Count; i++)
            {
                ConnectionGroup group = connectionGroups[i];
                if (group.IsOPCServerConnected() == false)
                {
                    //aTimer.Enabled = false;
                    string opcServer = group.opcServer;
                    string opcHost = group.opcHost;
                    List<OPCBaseChannel> groupChannels = new List<OPCBaseChannel>();
                    groupChannels.AddRange(group.channels);
                    ConnectionGroup connectionGroup = null;
                    try
                    {
                        connectionGroup = new ConnectionGroup(opcServer, opcHost, groupChannels);
                        //if no exception
                        if (connectionGroup.server != null)
                        {
                            connectionGroups.RemoveAt(i);
                            connectionGroups.Insert(i, connectionGroup);
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
        }

		public bool IsConnected
		{
			get { return connectedFlag; }
		}

		public bool Connect()
		{
			if (IsConnected)
				return false;

			connectionGroups.Clear();
			System.GC.Collect();

			if (channels.Count > 0)
			{
				List<IChannel> originalChannels = new List<IChannel>();
				originalChannels.AddRange(channels);
				do
				{
					List<OPCBaseChannel> groupChannels = new List<OPCBaseChannel>();
					OPCBaseChannel lhc = (OPCBaseChannel)originalChannels[0];
					groupChannels.Add(lhc);
					originalChannels.RemoveAt(0);
					for (int i = originalChannels.Count - 1; i >= 0; i--)
					{
						OPCBaseChannel rhc = (OPCBaseChannel)originalChannels[i];
						if (lhc.OpcServer == rhc.OpcServer && lhc.OpcHost == rhc.OpcHost)
						{
							groupChannels.Add(rhc);
							originalChannels.RemoveAt(i);
						}
					}
                    ConnectionGroup connectionGroup = null;
                    try
                    {
                        connectionGroup = new ConnectionGroup(lhc.OpcServer, lhc.OpcHost, groupChannels);
                        connectionGroups.Add(connectionGroup);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    //if(connectionGroup != null)
				} while (originalChannels.Count > 0);

                // Start the timer
                aTimer.Enabled = true;
			}

			connectedFlag = true;
			return IsConnected;
		}

		public void Disconnect()
		{
			connectedFlag = false;

			foreach (OPCBaseChannel ch in channels)
				ch.Disconnect();
			
			connectionGroups.Clear();

			System.GC.Collect();

            // Stop the timer
            aTimer.Enabled = false;
		}

		#endregion

		public IEnvironment Environment
		{
			get { return environment; }
			set { Initialize(value); }
		}

		public void SaveSettings()
		{
            string name = "settings\\" + StringConstants.PluginId + "_channels";
            /*if (channels.Count == 0)
            {
                environment.Project.RemoveData(name);
                return;
            }*/
			using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
			{
				if (ms.Length != 0)
				{
					ms.SetLength(0);
					ms.Seek(0, System.IO.SeekOrigin.Begin);
				}

				XmlDocument doc = new System.Xml.XmlDocument();
				XmlElement root_elem = doc.CreateElement("root");
				foreach (IChannel ch in channels)
				{
					XmlElement elem = doc.CreateElement("channel");
					ChannelFactory.SaveChannel(elem, ch);
					root_elem.AppendChild(elem);
				}
				doc.AppendChild(root_elem);
				doc.Save(ms);
				environment.Project.SetData(name, ms);
			}
		}

        public object GetMonitoringView()
        {
            return null;
        }

		void LoadSettings()
		{
			channels.Clear();
			using (System.IO.Stream ms = environment.Project["settings/" + StringConstants.PluginId + "_channels"])
			{
				if (ms == null || ms.Length == 0)
					return;
				XmlDocument doc = new System.Xml.XmlDocument();
				try
				{
					doc.Load(ms);
				}
				catch
				{
					return;
				}
				XmlNodeList nodes = doc.GetElementsByTagName("channel");
				foreach (XmlElement node in nodes)
					channels.Add(ChannelFactory.CreateChannel(node, this));
			}
			FireChannelChangedEvent();
		}

		void OnProjectLoad(object sender, System.EventArgs e)
		{
			LoadSettings();
		}

		void FireChannelChangedEvent()
		{
			if (ChannelsChanged != null)
				ChannelsChanged(this, new EventArgs());
		}
	}
}
