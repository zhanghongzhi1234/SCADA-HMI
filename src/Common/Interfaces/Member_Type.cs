using System;
namespace FreeSCADA.Interfaces
{
    public class Member_Type : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(System.Type);
            }
        }
        public override string ToString()
        {
            return "Type";
        }
    }
}
