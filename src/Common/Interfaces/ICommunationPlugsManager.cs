using FreeSCADA.Interfaces.Plugins;
using System;
using System.Collections.Generic;
namespace FreeSCADA.Interfaces
{
    public interface ICommunationPlugsManager
    {
        event ChannelsChangedHandler ChannelsChanged;
        ICommunicationPlug[] Plugs
        {
            get;
        }
        System.Collections.Generic.List<string> PluginIds
        {
            get;
        }
        ICommunicationPlug this[string pluginId]
        {
            get;
        }
        IChannel[] ChannelArray
        {
            get;
        }
        bool IsConnected
        {
            get;
        }
        //void Load(string password);
        void Load();
        void Clear();
        void CreateRuntimeTag();
        bool Connect();
        void Disconnect();
        IChannel GetChannel(string name);
    }
}
