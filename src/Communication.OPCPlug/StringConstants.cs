
namespace FreeSCADA.Communication.OPCPlug
{
	public abstract class StringConstants
	{
		public static string PluginName = "OPC Connection Plugin";
		public static string PluginId = "opc_connection_plug";

		public static string PropertyCommandName = "OPC properties...";
		public static string CommunicationGroupName = "Communication";
	}

    public enum OPCSERVERSTATE
    {
        OPC_STATUS_FAILED = 2,
        OPC_STATUS_NOCONFIG = 3,
        OPC_STATUS_RUNNING = 1,
        OPC_STATUS_SUSPENDED = 4,
        OPC_STATUS_TEST = 5
    }
}
