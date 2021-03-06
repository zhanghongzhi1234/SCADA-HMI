using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using FreeSCADA.Designer.SchemaEditor.Manipulators.Controls;

namespace FreeSCADA.Designer.SchemaEditor.Manipulators
{
              
    /// <summary>
    /// Base Class for manipulators
    /// 
    /// /// </summary>
        
    public class BaseManipulator :Adorner
    {
        
        /// <summary>
        /// Container for manipulator controlls
        /// </summary>
        protected VisualCollection visualChildren;
        /// <summary>
        /// 
        /// </summary>
        protected System.Windows.Controls.Canvas mainCanvas;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public BaseManipulator(UIElement element)
            : base(element)
        {
            if (!(AdornedElement.RenderTransform is TransformGroup))
            {
                TransformGroup t = new TransformGroup();
                t.Children.Add(new MatrixTransform());
                t.Children.Add(new RotateTransform());
                AdornedElement.RenderTransform = t;
                AdornedElement.RenderTransformOrigin = new Point(0.5, 0.5);
                
            }
            ThumbsResources tr = new ThumbsResources();
            tr.InitializeComponent();
            Resources = tr;
            this.Visibility = Visibility.Collapsed;                       
            visualChildren = new VisualCollection(this);
            mainCanvas=Common.Schema.SchemaDocument.GetMainCanvas(AdornedElement);
       
    }
        /// <summary>
        /// 
        /// </summary>
        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Activate()
        {
            this.Visibility = Visibility.Visible;
            InvalidateVisual();
        }
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		public void BlodActive()
		{
			VisualCollection.Enumerator enumerator = this.visualChildren.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Visual current = enumerator.Current;
				if (current is Rectangle)
				{
					(current as Rectangle).StrokeThickness = 2.0;
				}
			}
			base.InvalidateVisual();
		}
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		public virtual void Deactivate()
        {
            this.Visibility = Visibility.Collapsed;
            InvalidateVisual();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public virtual bool IsSelactable(UIElement el)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            Matrix m = new Matrix();
            m.OffsetX = ((MatrixTransform)transform).Matrix.OffsetX;
            m.OffsetY = ((MatrixTransform)transform).Matrix.OffsetY;

            return new MatrixTransform();//new MatrixTransform(m); ;// //this code neded for right manipulators zooming
            
        }
     

    }

 }
