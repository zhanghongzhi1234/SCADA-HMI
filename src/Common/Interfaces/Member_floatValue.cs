using System;
namespace FreeSCADA.Interfaces
{
    public class Member_floatValue : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(float);
            }
        }
        public override string ToString()
        {
            return "floatValue";
        }
    }
}
