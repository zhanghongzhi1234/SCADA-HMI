using System;
namespace FreeSCADA.Interfaces
{
    public class Member_Value : ChannelMember
    {
        public override System.Type T
        {
            get
            {
                return typeof(object);
            }
        }
        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public override string ToString()
        {
            return "Value";
        }
    }
}