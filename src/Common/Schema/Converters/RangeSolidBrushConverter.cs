using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
namespace FreeSCADA.Common.Schema
{
    [ContentProperty("Ranges")]
    public class RangeSolidBrushConverter : IValueConverter
    {
        private System.Collections.Generic.List<ColorRangeValue> range = new System.Collections.Generic.List<ColorRangeValue>();
        public ColorRangeValue[] Ranges
        {
            get
            {
                return this.range.ToArray();
            }
            set
            {
                this.range.Clear();
                this.range.AddRange(value);
            }
        }
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(Brush))
            {
                if (value == null)
                    return null;
                if (value.GetType().IsValueType)
                {
                    try
                    {
                        double num = (double)System.Convert.ChangeType(value, typeof(double));
                        ColorRangeValue[] ranges = this.Ranges;
                        for (int i = 0; i < ranges.Length; i++)
                        {
                            ColorRangeValue colorRangeValue = ranges[i];
                            if (num >= colorRangeValue.dstart && num < colorRangeValue.dend)
                            {
                                return colorRangeValue.bursh;
                            }
                        }
                        goto IL_75;
                    }
                    catch (System.Exception)
                    {
                        goto IL_75;
                    }
                }
                return null;
            }
        IL_75:
            return null;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.Exception("Can convert back");
        }
    }
}
