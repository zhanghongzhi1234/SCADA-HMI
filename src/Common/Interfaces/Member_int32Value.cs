using System;
namespace FreeSCADA.Interfaces
{
    public class Member_int32Value : ChannelMember
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
            return "int32Value";
        }
    }
}
