using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using OpcRcw.Da;

namespace FreeSCADA.Communication.OPCPlug
{
	class ConnectionGroup
	{
		OPCDataCallback callback;
		IOPCItemMgt group;
		int callbackCookie;
		public IOPCServer server = null;

		const int OPC_READABLE = 1;
		const int OPC_WRITEABLE = 2;

        public string opcServer;
        public string opcHost;
        public List<OPCBaseChannel> channels = new List<OPCBaseChannel>();

		public ConnectionGroup(string opcServer, string opcHost, List<OPCBaseChannel> channels)
		{
            this.opcServer = opcServer;
            this.opcHost = opcHost;
            this.channels.AddRange(channels);

			Type t = Type.GetTypeFromProgID(opcServer, opcHost);
            if (t == null)
            {
                System.Windows.Forms.MessageBox.Show(null, "opcServer=" + opcServer + ", opcHost=" + opcHost + "不存在!", "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                return;
            }
            try
            {
                server = (IOPCServer)Activator.CreateInstance(t);
            }
            catch (Exception ex)
            {
                return;
            }
			int groupClientId = 1;
			int groupId;
			int updateRate = 0;
			object group_obj;
			Guid tmp_guid = typeof(IOPCItemMgt).GUID;
			server.AddGroup("", 1, updateRate, groupClientId, new IntPtr(), new IntPtr(), 0, out groupId, out updateRate, ref tmp_guid, out group_obj);
    		group = (IOPCItemMgt)group_obj;
            
			OPCITEMDEF[] items = new OPCITEMDEF[channels.Count];
			for (int i = 0; i < channels.Count; i++)
			{
				items[i].bActive = 1;
				items[i].szItemID = channels[i].OpcChannel;
				items[i].hClient = channels[i].GetHashCode();
			}
			IntPtr addResult;
			IntPtr addErrors;
			group.AddItems(items.Length, items, out addResult, out addErrors);
			for (int i = 0; i < channels.Count; i++)
			{
				IntPtr pos = new IntPtr(addResult.ToInt32() + Marshal.SizeOf(typeof(OPCITEMRESULT)) * i);
				OPCITEMRESULT res = (OPCITEMRESULT)Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));

				bool readOnly = (res.dwAccessRights & OPC_WRITEABLE) != OPC_WRITEABLE;
				channels[i].Connect(this, res.hServer, readOnly);
			}
			Marshal.FreeCoTaskMem(addResult);
			Marshal.FreeCoTaskMem(addErrors);            
			addResult = IntPtr.Zero;
			addErrors = IntPtr.Zero;

			IConnectionPointContainer cpc = (IConnectionPointContainer)group_obj;
			IConnectionPoint cp;
			Guid dataCallbackGuid = typeof(IOPCDataCallback).GUID;
			cpc.FindConnectionPoint(ref dataCallbackGuid, out cp);

			callback = new OPCDataCallback(channels);
			cp.Advise(callback, out callbackCookie);
		}

		~ConnectionGroup()
		{
			try
			{
				IConnectionPointContainer cpc = (IConnectionPointContainer)group;
                if (cpc != null)        //do it only when the group is successfully created
                {
                    IConnectionPoint cp;
                    Guid dataCallbackGuid = typeof(IOPCDataCallback).GUID;
                    cpc.FindConnectionPoint(ref dataCallbackGuid, out cp);

                    cp.Unadvise(callbackCookie);
                }
			}
			catch (System.Runtime.InteropServices.COMException)
			{
			}
            catch (Exception)
            {
            }

			group = null;
			callback = null;
			server = null;
		}

		public bool WriteChannel(int channelHandle, object value)
		{
			IOPCAsyncIO2 asyncIO = null;
			IOPCSyncIO syncIO = null;
			try
			{
				asyncIO = (IOPCAsyncIO2)group;
			}
			catch (System.Runtime.InteropServices.COMException)
			{
			}

			if (asyncIO == null)
			{
				try
				{
					syncIO = (IOPCSyncIO)group;
				}
				catch (System.Runtime.InteropServices.COMException)
				{
				}
			}

			if (asyncIO == null && syncIO == null)
				return false;

			if (asyncIO != null)
			{
				int cancelID;
				IntPtr ppErrors;
				asyncIO.Write(1, new int[]{channelHandle}, new object[]{value}, 0, out cancelID, out ppErrors);
				Marshal.FreeCoTaskMem(ppErrors);
			}
			else if (syncIO != null)
			{
				IntPtr ppErrors;
				syncIO.Write(1, new int[] { channelHandle }, new object[] { value }, out ppErrors);
				Marshal.FreeCoTaskMem(ppErrors);
			}

			return true;
		}

        public bool IsOPCServerConnected()
        {
            bool ret = false;
            if (server == null)
                return false;

            IntPtr statusPtr = IntPtr.Zero;
            try
            {
                server.GetStatus(out statusPtr);
                OPCSERVERSTATUS status = (OPCSERVERSTATUS)Marshal.PtrToStructure(statusPtr, typeof(OPCSERVERSTATUS));
                if (status.dwServerState == OpcRcw.Da.OPCSERVERSTATE.OPC_STATUS_RUNNING)
                    ret = true;
                else
                    ret = false;

                statusPtr = IntPtr.Zero;
            }
            catch (Exception localException)
            {
                //LogHelper.Error(CLASS_NAME, Function_Name, localException);
                ret = false;
            }

            return ret;
        }
	}
}
