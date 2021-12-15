using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace FreeSCADA.Common.Schema
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object o, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                if (o.GetType() == typeof(bool))
                {
                    if ((bool)o)
                    {
                        return Visibility.Visible;
                    }
                    return Visibility.Hidden;
                }
                else
                {
                    try
                    {
                        double num = (double)System.Convert.ChangeType(o, typeof(double));
                        object result;
                        if (num > 0.0)
                        {
                            result = Visibility.Visible;
                            return result;
                        }
                        result = Visibility.Hidden;
                        return result;
                    }
                    catch
                    {
                    }
                }
            }
            return true;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
