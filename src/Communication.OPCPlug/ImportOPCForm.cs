using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FreeSCADA.Common;
using OpcRcw.Comn;
using OpcRcw.Da;

namespace FreeSCADA.Communication.OPCPlug
{
	public partial class ImportOPCForm : Form
	{
		public struct OPCChannelInfo
		{
			public string progId;
			public string host;
			public string channel;
		}
        Dictionary<string, OPCChannelInfo> channels = new Dictionary<string, OPCChannelInfo>();

        public Dictionary<string, OPCChannelInfo> Channels
		{
			get { return channels; }
		}

		public ImportOPCForm()
		{
			InitializeComponent();
			FillServerList();
		}

		private void OnConnect(object sender, EventArgs e)
		{
			string hostName = localServerButton.Checked ? "localhost" : serverTextBox.Text;
			string serverName = serversComboBox.Text;

			Type t = Type.GetTypeFromProgID(serverName, hostName);
            object obj = null;
            try
            {
                obj = Activator.CreateInstance(t);
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("无法连接到" + hostName + "." + serverName, "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
			IOPCBrowseServerAddressSpace srv = (IOPCBrowseServerAddressSpace)obj;

			//IntPtr statusPtr;
			//server.GetStatus(out statusPtr);
			//OPCSERVERSTATUS status = (OPCSERVERSTATUS)Marshal.PtrToStructure(statusPtr, typeof(OPCSERVERSTATUS));
			//statusPtr = IntPtr.Zero;

			if (srv != null)
			{
				try
				{
					for (; ; )
						srv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_UP, "");
				}
				catch (COMException) { };
				channelsTree.Nodes.Clear();
				ImportOPCChannels(srv, channelsTree.Nodes);
			}

			groupBox1.Enabled = false;
			connectButton.Enabled = false;
		}

		void ImportOPCChannels(IOPCBrowseServerAddressSpace srv, TreeNodeCollection root)
		{
			OPCNAMESPACETYPE nsType;
			srv.QueryOrganization(out nsType);
			OpcRcw.Da.IEnumString es;
			if(nsType == OPCNAMESPACETYPE.OPC_NS_HIERARCHIAL)
			{				
				try{srv.BrowseOPCItemIDs(OPCBROWSETYPE.OPC_BRANCH, "", 0, 0, out es);}
				catch(COMException){return;}
				
				int fetched;
				do 
				{
					string[] tmp = new string[100];
					es.RemoteNext(tmp.Length, tmp, out fetched);
					for (int i = 0; i < fetched; i++)
					{
						try{srv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_DOWN, tmp[i]);}
						catch (Exception e) 
						{
							Env.Current.Logger.LogWarning(string.Format("OPC server failed to handle OPC_BROWSE_DOWN request for item '{0}' ({1})", tmp[i], e.Message));
							continue; 
						};
						TreeNode node = root.Add(tmp[i]);
						ImportOPCChannels(srv, node.Nodes);
						try { srv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_UP, ""); }
						catch (Exception e) 
						{
							Env.Current.Logger.LogWarning(string.Format("OPC server failed to handle OPC_BROWSE_UP request for item '{0}' ({1})", tmp[i], e.Message));
							continue; 
						};
					}
				} while(fetched>0);

				try{srv.BrowseOPCItemIDs(OPCBROWSETYPE.OPC_LEAF, "", 0, 0, out es);}
				catch(COMException){return;}
				IterateOPCItems(srv, root, es);
			}
			else if(nsType == OPCNAMESPACETYPE.OPC_NS_FLAT)
			{
				try { srv.BrowseOPCItemIDs(OPCBROWSETYPE.OPC_FLAT, "", 0, 0, out es); }
				catch (COMException) { return; }
				IterateOPCItems(srv, root, es);
			}
			es = null;
		}

		private void IterateOPCItems(IOPCBrowseServerAddressSpace srv, TreeNodeCollection root, OpcRcw.Da.IEnumString es)
		{
			int fetched;
			do
			{
				string[] tmp = new string[100];
				es.RemoteNext(tmp.Length, tmp, out fetched);
				for (int i = 0; i < fetched; i++)
					AddTreeNode(srv, root, tmp[i]);
			} while (fetched > 0);
		}

		private void AddTreeNode(IOPCBrowseServerAddressSpace srv, TreeNodeCollection root, string tag)
		{
			TreeNode item = root.Add(tag);
			OPCChannelInfo channel = new OPCChannelInfo();
			channel.progId = serversComboBox.Text;
			channel.host = localServerButton.Checked ? "localhost" : serverTextBox.Text;
			srv.GetItemID(tag, out channel.channel);
			item.Tag = channel;
		}

		void FillServerList()
		{
			string hostName = localServerButton.Checked ? "localhost" : serverTextBox.Text;
			serversComboBox.Items.Clear();

			try
			{
				Type serverListType = Type.GetTypeFromProgID("OPC.ServerList", hostName);
				IOPCServerList serverList = (IOPCServerList)Activator.CreateInstance(serverListType);
				Guid[] categories = { typeof(CATID_OPCDAServer20).GUID };
				IEnumGUID enumGuids;
				serverList.EnumClassesOfCategories(categories.Length, categories, 0, null, out enumGuids);
				int fetched;
				do
				{
					Guid[] ids = new Guid[10];
					enumGuids.Next(ids.Length, ids, out fetched);
					for (int i = 0; i < fetched; i++)
					{
						string progId;
						string name;
						serverList.GetClassDetails(ref ids[i], out progId, out name);
						serversComboBox.Items.Add(progId);
					}
				} while (fetched > 0);

				if (serversComboBox.Items.Count > 0)
					serversComboBox.Text = serversComboBox.Items[0].ToString();
			}
			catch(System.Exception ex)
			{
                //MessageBox.Show(ex.ToString());
			}
		}

		private void OnServerClick(object sender, EventArgs e)
		{
			serverTextBox.Enabled = remoteServerButton.Checked;
		}

		private void OnRefreshServersClick(object sender, EventArgs e)
		{
			FillServerList();
		}

		private void OnServerChanged(object sender, EventArgs e)
		{
			connectButton.Enabled = serversComboBox.Text.Length > 0;
		}

		private void OnCancelClick(object sender, EventArgs e)
		{
			Close();
		}

		void SaveOPCChannels(TreeNodeCollection root)
		{
			foreach (TreeNode node in root)
			{
				if (node.Nodes.Count > 0)
					SaveOPCChannels(node.Nodes);
                else if (node.Checked && node.Tag!=null)
                {
                    //channels.Add((OPCChannelInfo)node.Tag);
                    OPCChannelInfo temp = (OPCChannelInfo)node.Tag;
                    channels[temp.channel] = temp;
                }
			}
		}

		private void OnOkClick(object sender, EventArgs e)
		{
			//channels.Clear();
			//SaveOPCChannels(channelsTree.Nodes);
			Close();
		}

        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                /*if (node.Tag != null)
                {
                    OPCChannelInfo info = (OPCChannelInfo)node.Tag;
                    UpdateChannel(info, node.Checked);
                }*/
                UpdateChannel(node);
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void channelsTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                /*if (e.Node.Tag != null)
                {
                    OPCChannelInfo info = (OPCChannelInfo)e.Node.Tag;
                    UpdateChannel(info, e.Node.Checked);
                }*/
                UpdateChannel(e.Node);
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        //add to avoid slow in adding big data.
        private void UpdateChannel(OPCChannelInfo info, bool isChecked)
        {
            if (isChecked)
                channels[info.channel] = info;
            else
                channels.Remove(info.channel);
        }

        //add to avoid slow in adding big data.
        private void UpdateChannel(TreeNode node)
        {
            if (node.Tag != null)
            {
                OPCChannelInfo info = (OPCChannelInfo)node.Tag;
                if (node.Checked)
                    channels[info.channel] = info;
                else
                    channels.Remove(info.channel);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in channelsTree.Nodes)
            {
                node.Checked = true;
                UpdateChannel(node);
                this.CheckAllChildNodes(node, true);
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in channelsTree.Nodes)
            {
                node.Checked = false;
                UpdateChannel(node);
                this.CheckAllChildNodes(node, false);
            }
        }
	}
}
