using System;
using System.Globalization;
using System.Windows.Data;
namespace FreeSCADA.Common.Schema
{
    public class BooleanConverter : IValueConverter
    {
        private bool _not;
        public bool Not
        {
            get
            {
                return this._not;
            }
            set
            {
                this._not = value;
            }
        }
        public object Convert(object o, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Type type = o.GetType();
            bool flag;
            if (type == typeof(bool))
            {
                flag = (bool)o;
            }
            else
            {
                flag = (bool)StringToValue.ToValue(typeof(bool), o.ToString());
            }
            if (this.Not)
            {
                flag = !flag;
            }
            return flag;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
