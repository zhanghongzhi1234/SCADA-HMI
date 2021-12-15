using FreeSCADA.Common;
using System;
namespace FreeSCADA.Interfaces
{
    public interface IExtend
    {
        string Name
        {
            get;
        }
        bool IsSupportConfig
        {
            get;
        }
        string ConfigMenuName
        {
            get;
        }
        string Status
        {
            get;
        }
        BaseCommand Command
        {
            get;
        }
        string Description
        {
            get;
        }
        bool Inited
        {
            get;
        }
        bool Init(string exeName);
        bool Start();
        void Stop();
        void UnInit();
        void ShowAbout();
        void Config();
        bool DoCommand(string cmd);
    }
}
