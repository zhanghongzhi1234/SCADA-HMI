using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
namespace FreeSCADA.Common.Schema
{
    [ContentProperty("Ranges")]
    public class MultiStringConverter : IValueConverter
    {
        private System.Collections.Generic.List<TextRangeValue> range = new System.Collections.Generic.List<TextRangeValue>();
        public TextRangeValue[] Ranges
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
            if (value == null)
                return null;
            if (targetType == typeof(string) || targetType == typeof(object))
            {
                if (value.GetType().IsValueType)
                {
                    try
                    {
                        double num = (double)System.Convert.ChangeType(value, typeof(double));
                        TextRangeValue[] ranges = this.Ranges;
                        for (int i = 0; i < ranges.Length; i++)
                        {
                            TextRangeValue textRangeValue = ranges[i];
                            if (num >= textRangeValue.dstart && num < textRangeValue.dend)
                            {
                                return textRangeValue.text;
                            }
                        }
                        goto IL_87;
                    }
                    catch (System.Exception)
                    {
                        goto IL_87;
                    }
                }
                return null;
            }
        IL_87:
            return null;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.Exception("Can convert back");
        }
    }
}
