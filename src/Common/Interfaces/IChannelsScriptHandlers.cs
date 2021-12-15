using FreeSCADA.Common.Scripting;
using System;
namespace FreeSCADA.Interfaces
{
    public interface IChannelsScriptHandlers
    {
        void AddAssociation(string evnt, IChannel ch, ScriptCallInfo info);
        void Clear();
        void RemoveAssociation(string evnt, IChannel ch);
        ScriptCallInfo GetAssosiation(string evnt, IChannel ch);
        void InstallHandlers();
    }
}
