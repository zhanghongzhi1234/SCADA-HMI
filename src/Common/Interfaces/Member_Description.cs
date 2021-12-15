using System;
namespace FreeSCADA.Interfaces
{
    public class Member_Description : ChannelMember
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
            return "Description";
        }
    }
}
