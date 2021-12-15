using System;
namespace FreeSCADA.Interfaces
{
    public class ChannelMember
    {
        public virtual System.Type T
        {
            get
            {
                return typeof(object);
            }
        }
        public virtual bool IsReadOnly
        {
            get
            {
                return true;
            }
        }
    }
}