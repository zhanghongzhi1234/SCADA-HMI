using System;
namespace FreeSCADA.Common
{
    public static class StringToValue
    {
        public static object ToValue(System.Type T, string strvalue)
        {
            object result = null;
            if (T == typeof(bool))
            {
                if (strvalue == null || strvalue == "0" || strvalue == "false" || strvalue == "False" || strvalue == "FALSE")
                {
                    result = false;
                }
                else
                {
                    result = true;
                    /*
                    if (strvalue == "0")
                    {
                        result = false;
                    }*/
                }
            }
            else
            {
                result = System.Convert.ChangeType(strvalue, T);
            }
            return result;
        }
        public static object AddValue(object value, string strvalue)
        {
            object result = null;
            System.Type type = value.GetType();
            if (type == typeof(byte))
            {
                try
                {
                    byte b = (byte)StringToValue.ToValue(type, strvalue);
                    result = (int)((byte)value + b);
                    return result;
                }
                catch
                {
                    result = null;
                    return result;
                }
            }
            if (type == typeof(int))
            {
                try
                {
                    int num = (int)StringToValue.ToValue(type, strvalue);
                    result = (int)value + num;
                    return result;
                }
                catch
                {
                    result = null;
                    return result;
                }
            }
            if (type == typeof(short))
            {
                try
                {
                    short num2 = (short)StringToValue.ToValue(type, strvalue);
                    result = (int)((short)value + num2);
                    return result;
                }
                catch
                {
                    result = null;
                    return result;
                }
            }
            if (type == typeof(uint))
            {
                try
                {
                    uint num3 = (uint)StringToValue.ToValue(type, strvalue);
                    result = (uint)value + num3;
                    return result;
                }
                catch
                {
                    result = null;
                    return result;
                }
            }
            if (type == typeof(ushort))
            {
                try
                {
                    ushort num4 = (ushort)StringToValue.ToValue(type, strvalue);
                    result = (int)((ushort)value + num4);
                    return result;
                }
                catch
                {
                    result = null;
                    return result;
                }
            }
            if (type == typeof(float))
            {
                try
                {
                    float num5 = (float)StringToValue.ToValue(type, strvalue);
                    result = (float)value + num5;
                    return result;
                }
                catch
                {
                    result = null;
                    return result;
                }
            }
            if (type == typeof(double))
            {
                try
                {
                    double num6 = (double)StringToValue.ToValue(type, strvalue);
                    result = (double)value + num6;
                    return result;
                }
                catch
                {
                    result = null;
                    return result;
                }
            }
            if (type == typeof(string))
            {
                result = value.ToString() + strvalue;
            }
            return result;
        }
    }
}
