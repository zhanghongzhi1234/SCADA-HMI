using System;
namespace FreeSCADA.Interfaces
{
    public class Member_IsReadOnly : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(bool);
            }
        }
        public override string ToString()
        {
            return "IsReadOnly";
        }
    }
}
