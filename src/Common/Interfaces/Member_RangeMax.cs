using System;
namespace FreeSCADA.Interfaces
{
    public class Member_RangeMax : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(double);
            }
        }
        public override string ToString()
        {
            return "RangeMax";
        }
    }
}
