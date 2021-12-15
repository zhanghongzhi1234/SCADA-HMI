using System.Xml;
using FreeSCADA.Interfaces;

namespace FreeSCADA.Communication.OPCPlug
{
	sealed class ChannelFactory
	{
		//Prevent class reation
		private ChannelFactory() { }

		public static IChannel CreateChannel(XmlElement node, Plugin plugin)
		{
			string name = node.Attributes["name"].Value;
            string hierarchy = null;
            if(node.Attributes["hierarchy"] != null)
                hierarchy = node.Attributes["hierarchy"].Value;
			string opcChannel = node.Attributes["opcChannel"].Value;
			string opcServer = node.Attributes["opcServer"].Value;
			string opcHost = node.Attributes["opcHost"].Value;

            return CreateChannel(name, hierarchy, plugin, opcChannel, opcServer, opcHost);
		}

		public static IChannel CreateChannel(string name, string hierarchy, Plugin plugin, string opcChannel, string opcServer, string opcHost)
		{
            return new OPCBaseChannel(name, hierarchy, plugin, opcChannel, opcServer, opcHost);
		}

		public static void SaveChannel(XmlElement node, IChannel channel)
		{
			OPCBaseChannel channelBase = (OPCBaseChannel)channel;
			node.SetAttribute("name", channelBase.Name);
            node.SetAttribute("hierarchy", channelBase.Hierarchy);
			node.SetAttribute("opcChannel", channelBase.OpcChannel);
			node.SetAttribute("opcServer", channelBase.OpcServer);
			node.SetAttribute("opcHost", channelBase.OpcHost);
		}
	}
}
