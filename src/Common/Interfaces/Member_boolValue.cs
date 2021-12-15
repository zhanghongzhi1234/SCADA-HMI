using System;
namespace FreeSCADA.Interfaces
{
    public class Member_boolValue : ChannelMember
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
            return "boolValue";
        }
    }
}
