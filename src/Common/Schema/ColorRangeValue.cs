using System;
using System.Windows.Media;
namespace FreeSCADA.Common.Schema
{
    public class ColorRangeValue
    {
        public double dstart
        {
            get;
            set;
        }
        public double dend
        {
            get;
            set;
        }
        public SolidColorBrush bursh
        {
            get;
            set;
        }
    }
}
