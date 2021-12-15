using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;
namespace FreeSCADA.Common.Schema
{
    public class BrushProvider : DataSourceProvider
    {
        private Color _Color = Colors.White;
        private SolidBrush _Brush;
        public bool IsBindInDesign
        {
            get;
            set;
        }
        public Color Color
        {
            get
            {
                base.Refresh();
                return this._Color;
            }
            set
            {
                if (this._Color == value)
                {
                    return;
                }
                this._Color = value;
                this._Brush = new SolidBrush(this._Color);
                base.OnPropertyChanged(new PropertyChangedEventArgs("Color"));
            }
        }
        protected override void BeginQuery()
        {
            if (this._Brush == null)
            {
                this._Brush = new SolidBrush(this._Color);
            }
            base.OnQueryFinished(this._Brush);
        }
    }
}
