using System;
namespace FreeSCADA.Interfaces
{
    public class Member_FullId : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(string);
            }
        }
        public override string ToString()
        {
            return "FullId";
        }
    }
}
