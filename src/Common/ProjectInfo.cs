using System;
using System.Xml.Serialization;
namespace FreeSCADA.Common
{
	[XmlInclude(typeof(SoundInfo))]
	[System.Serializable]
	public class ProjectInfo
	{
		public string ProjectName = "";
		public int AutoStart;
		public string StartPage = "";
		public string UserName_Open = "";
		public string Password_Open = "";
		public string StartPage_Param = "";
		public string StartScript = "";
		public string StopScript = "";
		public string Design = "";
		public string Memo = "";
		public string other = "";
		public int FullScreen;
		public string Language = "cs";
		public string LoginUserName = "";
		public string UserRunTimeCodeDll = "";
		public bool ShowChannelInNode = true;
		public SoundInfo sndInfo = new SoundInfo();
		public string WcfDatabaseAdress = "";
		public bool ShowAlarm = false;
        public int originX = 0;
        public int originY = 0;
        public ProjectInfo()
		{
		}
		public void Clear()
		{
			this.ProjectName = "";
			this.AutoStart = 0;
			this.StartPage = "";
			this.UserName_Open = "";
			this.Password_Open = "";
			this.ShowAlarm = true;
			this.StartPage_Param = "";
			this.StartScript = "";
			this.StopScript = "";
			this.WcfDatabaseAdress = "";
			this.Design = "";
			this.Memo = "";
			this.FullScreen = 0;
			this.LoginUserName = "";
            this.originX = 0;
            this.originY = 0;
		}
	}
}
