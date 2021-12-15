using FreeSCADA.Designer.SchemaEditor.Manipulators;
using FreeSCADA.Designer.SchemaEditor.Tools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FreeSCADA.Designer.SchemaEditor
{
    class SelectionManager
    {
        //Manipulators.BaseManipulator manipulator;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="el"></param>
        public delegate void SelectionChangedDelegate(UIElement el);
		private System.Collections.Generic.List<BaseManipulator> list_manipulator;
        /// <summary>
        /// 
        /// </summary>
        public event SelectionChangedDelegate SelectionChanged;
        Views.SchemaView _view;
        

        public List<UIElement> SelectedObjects
        {
            get;
            protected set;
        }

        public static SelectionManager GetSelectionManagerFor(UIElement el)
        {
            AdornerLayer al = AdornerLayer.GetAdornerLayer(el);
            Canvas c = Common.Schema.SchemaDocument.GetMainCanvas(el);
            return (c.Tag as Views.SchemaView).SelectionManager;
        }
        public SelectionManager(Views.SchemaView view)
        {
			this.list_manipulator = new System.Collections.Generic.List<BaseManipulator>();
            _view = view;
            SelectedObjects = new List<UIElement>();
        }
        public void AddObject(UIElement el)
        {
            if(el!=null)
                SelectedObjects.Insert(0,el);;
            UpdateManipulator();
            if (SelectionChanged != null)
                SelectionChanged(el);
            
        }
        public void DeleteObject(UIElement el)
        {
            SelectedObjects.Remove(el);
            UpdateManipulator();
            if (SelectionChanged != null)
                SelectionChanged(el);
        }

        public void SelectObject(UIElement el)
        {
            SelectedObjects.Clear();
            AddObject(el);
        }
        public Rect CalculateBounds()
        {
            if (SelectedObjects.Count > 0)
                return EditorHelper.CalculateBounds(SelectedObjects, _view.MainCanvas);
            else return Rect.Empty;
            
        }
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		private void ClearManipulator()
		{
			if (this.list_manipulator.Count > 0)
			{
                foreach (BaseManipulator current in this.list_manipulator)
				{
					current.Deactivate();
					AdornerLayer.GetAdornerLayer(this._view.MainCanvas).Remove(current);
				}
				this.list_manipulator.Clear();
			}
		}
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		public void UpdateManipulator()
		{
			this.ClearManipulator();
			if (this.SelectedObjects.Count > 0)
			{
				try
				{
					//bool mulitselected = this.SelectedObjects.Count > 20;
					BaseTool activeTool = this._view.ActiveTool;
					foreach (UIElement current in this.SelectedObjects)
					{
						//BaseManipulator baseManipulator = activeTool.CreateToolManipulator(current, mulitselected);
                        BaseManipulator baseManipulator = activeTool.CreateToolManipulator(current);
						this.list_manipulator.Add(baseManipulator);
						AdornerLayer.GetAdornerLayer(this._view.MainCanvas).Add(baseManipulator);
					}
					foreach (BaseManipulator current2 in this.list_manipulator)
					{
						current2.Activate();
					}
					if (this.list_manipulator.Count > 1)
					{
						this.list_manipulator[0].BlodActive();
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message);
					this.SelectObject(null);
				}
			}
			AdornerLayer.GetAdornerLayer(this._view.MainCanvas).Update();
		}

    }
}
