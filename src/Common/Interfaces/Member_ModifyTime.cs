using System;
namespace FreeSCADA.Interfaces
{
    public class Member_ModifyTime : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(System.DateTime);
            }
        }
        public override string ToString()
        {
            return "ModifyTime";
        }
    }
}
