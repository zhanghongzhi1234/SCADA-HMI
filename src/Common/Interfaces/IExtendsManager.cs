using System;
using System.Collections.Generic;
namespace FreeSCADA.Interfaces
{
    public interface IExtendsManager
    {
        IExtend[] Extends
        {
            get;
        }
        System.Collections.Generic.List<string> ExtendNames
        {
            get;
        }
        IExtend this[string extendName]
        {
            get;
        }
        void Load(string password);
    }
}
