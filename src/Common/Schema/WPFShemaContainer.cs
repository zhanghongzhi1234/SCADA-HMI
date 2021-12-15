using FreeSCADA.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
namespace FreeSCADA.Common.Schema
{
    public class WPFShemaContainer : ElementHost
    {
        public delegate void ZoomDelegate(Point pt);
        private FrameworkElement view;
        private myHelpScrollViewer scroll;
        private Point origPanPoint;
        private Point offset = new Point(0, 0);
        private bool StartDrag;
        public event WPFShemaContainer.ZoomDelegate ZoomInEvent;
        public event WPFShemaContainer.ZoomDelegate ZoomOutEvent;
        public event System.EventHandler LeftMouseDoubleClient;
        public FrameworkElement View
        {
            get
            {
                return (base.Child as ScrollViewer).Content as FrameworkElement;
            }
            set
            {
                ContentControl arg_15_0 = base.Child as ScrollViewer;
                this.view = value;
                arg_15_0.Content = value;
                if (!(this.view.RenderTransform is ScaleTransform))
                {
                    this.view.RenderTransform = new ScaleTransform();
                }
            }
        }
        public ScrollBarVisibility HScroll
        {
            get
            {
                return this.scroll.HorizontalScrollBarVisibility;
            }
        }
        public bool PanMove
        {
            get;
            set;
        }
        public WPFShemaContainer()
        {
            this.Initialize();
        }
        public void SetScrollBar(ScrollBarVisibility v)
        {
            this.scroll.HorizontalScrollBarVisibility = v;
            this.scroll.VerticalScrollBarVisibility = v;
        }
        private void Initialize()
        {
            base.Child = (this.scroll = new myHelpScrollViewer());
            if (Env.Current.Mode > EnvironmentMode.Designer)
            {
                this.scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                this.scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            else
            {
                this.scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                this.scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            AutomationProperties.SetAutomationId(base.Child, "SchemaCanvas");
            base.Child.SnapsToDevicePixels = true;
            base.Child.PreviewMouseWheel += new MouseWheelEventHandler(this.Child_MouseWheel);
            base.Child.PreviewMouseDown += new MouseButtonEventHandler(this.Child_MouseDown);
            base.Child.PreviewMouseUp += new MouseButtonEventHandler(this.Child_MouseUp);
            base.Child.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(this.Child_MouseMove);
        }
        private void Child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                Point position = e.GetPosition(base.Child);
                if (e.Delta > 0)
                {
                    this.NotifyZoomInEvent(position);
                }
                else
                {
                    this.NotifyZoomOutEvent(position);
                }
                e.Handled = true;
            }
        }
        private void Child_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                Trace.WriteLine("");
                if (this.LeftMouseDoubleClient != null)
                {
                    this.LeftMouseDoubleClient(sender, null);
                }
                e.Handled = true;
                return;
            }
            if (this.PanMove)
            {
                this.StartDrag = true;
            }
            if (e.MiddleButton == MouseButtonState.Pressed || this.StartDrag)
            {
                this.origPanPoint = e.GetPosition(base.Child);
                this.Cursor = System.Windows.Forms.Cursors.NoMove2D;
                return;
            }
            e.Handled = false;
        }
        private void Child_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Released || this.StartDrag)
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                e.Handled = false;
            }
            this.StartDrag = false;
            this.PanMove = false;
        }
        private void Child_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            e.GetPosition(base.Child);
            if (e.MiddleButton == MouseButtonState.Pressed || this.StartDrag)
            {
                Point position = e.GetPosition(base.Child);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.NoMove2D;
                if (this.HScroll == ScrollBarVisibility.Disabled)
                {
                    Canvas canvas = this.view as Canvas;
                    double num = this.origPanPoint.X - position.X;
                    double num2 = this.origPanPoint.Y - position.Y;
                    System.Collections.IEnumerator enumerator = canvas.Children.GetEnumerator();
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            UIElement element = (UIElement)enumerator.Current;
                            Canvas.SetLeft(element, Canvas.GetLeft(element) - num);
                            Canvas.SetTop(element, Canvas.GetTop(element) - num2);
                        }
                        offset.X -= num;
                        offset.Y -= num2;
                        goto IL_128;
                    }
                    finally
                    {
                        System.IDisposable disposable = enumerator as System.IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
                this.scroll.ScrollToVerticalOffset(this.scroll.VerticalOffset + this.origPanPoint.Y - position.Y);
                this.scroll.ScrollToHorizontalOffset(this.scroll.HorizontalOffset + this.origPanPoint.X - position.X);
                offset.X += this.origPanPoint.X - position.X;
                offset.Y += this.origPanPoint.Y - position.Y;
            IL_128:
                this.origPanPoint.X = position.X;
                this.origPanPoint.Y = position.Y;
                return;
            }
            e.Handled = false;
        }
        protected void NotifyZoomInEvent(Point pt)
        {
            if (this.ZoomInEvent != null)
            {
                this.ZoomInEvent(pt);
            }
        }
        protected void NotifyZoomOutEvent(Point pt)
        {
            if (this.ZoomOutEvent != null)
            {
                this.ZoomOutEvent(pt);
            }
        }
        public void Reset()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.NoMove2D;
            if (this.HScroll == ScrollBarVisibility.Disabled)
            {
                Canvas canvas = this.view as Canvas;
                System.Collections.IEnumerator enumerator = canvas.Children.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        UIElement element = (UIElement)enumerator.Current;
                        Canvas.SetLeft(element, Canvas.GetLeft(element) - offset.X);
                        Canvas.SetTop(element, Canvas.GetTop(element) - offset.Y);
                    }
                    offset.X = 0;
                    offset.Y = 0;
                    goto IL_129;
                }
                finally
                {
                    System.IDisposable disposable = enumerator as System.IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
            this.scroll.ScrollToVerticalOffset(this.scroll.VerticalOffset - offset.Y);
            this.scroll.ScrollToHorizontalOffset(this.scroll.HorizontalOffset - offset.X);
            offset.X = 0;
            offset.Y = 0;
        IL_129:
            return;
        }
    }
}
