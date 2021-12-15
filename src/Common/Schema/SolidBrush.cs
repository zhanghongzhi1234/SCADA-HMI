using System;
using System.Windows.Media;
namespace FreeSCADA.Common.Schema
{
    internal class SolidBrush
    {
        private Brush _Brush;
        public Brush Brush
        {
            get
            {
                return this._Brush;
            }
        }
        public SolidBrush(Color c)
        {
            this._Brush = new SolidColorBrush(c);
        }
    }
}
