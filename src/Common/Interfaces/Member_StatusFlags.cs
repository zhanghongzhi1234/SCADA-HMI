using System;
namespace FreeSCADA.Interfaces
{
    public class Member_StatusFlags : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(int);
            }
        }
        public override string ToString()
        {
            return "StatusFlags";
        }
    }
}
