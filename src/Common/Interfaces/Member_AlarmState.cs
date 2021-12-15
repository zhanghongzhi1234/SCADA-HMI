using System;
namespace FreeSCADA.Interfaces
{
    public class Member_AlarmState : ChannelMember
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
            return "AlarmState";
        }
    }
}
