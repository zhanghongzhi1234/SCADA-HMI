using System;
namespace FreeSCADA.Interfaces
{
    public class Member_RangeMin : ChannelMember
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
            return "RangeMin";
        }
    }
}
