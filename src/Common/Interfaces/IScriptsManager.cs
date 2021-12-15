using FreeSCADA.Common.Scripting;
using System;
using System.Reflection;
namespace FreeSCADA.Interfaces
{
    public interface IScriptsManager
    {
        event NewScriptCreatedHandler NewScriptCreated;
        event System.EventHandler ScriptsUpdated;
        IChannelsScriptHandlers ChannelsHandlers
        {
            get;
        }
        FScadaApplication ScriptApplication
        {
            get;
        }
        IScriptHost ScriptHost
        {
            get;
        }
        string[] Refrences
        {
            get;
            set;
        }
        string[] Sources
        {
            get;
        }
        string[] ScriptNames
        {
            get;
        }
        void Initialize();
        void SaveRefrence();
        void Clear();
        void CreateNewScript(string name, bool show = true, bool schemaevent = false);
        bool ContainsScript(string name);
        string GetScriptText(string name);
        System.Reflection.MethodInfo GetMethod(string strType, string strMethod);
        System.Type GetClassByName(string strType);
        void InvokeMethod(string strType, string strMethod);
        void Save(string strName, string source);
        void Save();
        void Remove(string strName);
    }
}
