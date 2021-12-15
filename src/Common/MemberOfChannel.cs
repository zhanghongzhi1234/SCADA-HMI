using System;
using System.Collections.Generic;
using System.Reflection;
namespace FreeSCADA.Interfaces
{
    public class MemberOfChannel
    {
        public static ChannelMember[] GetMembers()
        {
            Member_Value member_Value = new Member_Value();
            System.Collections.Generic.List<ChannelMember> list = new System.Collections.Generic.List<ChannelMember>();
            System.Reflection.Assembly assembly = member_Value.GetType().Assembly;
            System.Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                System.Type type = types[i];
                if (type.IsSubclassOf(typeof(ChannelMember)))
                {
                    ChannelMember item = (ChannelMember)System.Activator.CreateInstance(type);
                    list.Add(item);
                }
            }
            return list.ToArray();
        }
    }
}
