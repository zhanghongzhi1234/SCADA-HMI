using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using FreeSCADA.Common;
using FreeSCADA.Common.Schema;
using FreeSCADA.Common.Scripting;
using FreeSCADA.Designer.SchemaEditor.PropertiesUtils;
using FreeSCADA.Interfaces;

namespace FreeSCADA.Designer.Views
{
    class PropertyBrowserView : ToolWindow
    {
        private System.Windows.Forms.ComboBox objcomboBox;
        private System.Windows.Forms.PropertyGrid propertyGrid;

        public PropertyBrowserView()
		{
			TabText = "属性窗口";
            InitializeComponent();
		}

        private void InitializeComponent()
        {
            this.objcomboBox = new System.Windows.Forms.ComboBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // ComboBox
            // 
            this.objcomboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.objcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objcomboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            this.objcomboBox.FormattingEnabled = true;
            this.objcomboBox.ItemHeight = 14;
            this.objcomboBox.Location = new System.Drawing.Point(0, 0);
            this.objcomboBox.Name = "Name";
            this.objcomboBox.Size = new System.Drawing.Size(275, 22);
            this.objcomboBox.TabIndex = 3;
            //this.objcomboBox.SelectedIndexChanged += new System.EventHandler(this.objcomboBox_SelectedIndexChanged);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(292, 273);
            this.propertyGrid.TabIndex = 0;
            // 
            // PropertyBrowserView
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.objcomboBox);
            this.Name = "PropertyBrowserView";
            this.ResumeLayout(false);

        }

        private string GetObjectName(object obj)
        {
            if (obj is PropProxy)
            {
                string text = (obj as PropProxy).ControlledObject.ToString();
                int num = text.LastIndexOf('.') + 1;
                if (num > 0)
                {
                    text = text.Substring(num, text.Length - num);
                }
                //return text + "[" + (obj as PropProxy).ControlledObject.GetType().Name + "]";
                /*AttributeCollection attributeCollection = (obj as PropProxy).GetAttributes();
                string ObjectName = attributeCollection.Attribute["Name"];*/
                
                PropertyDescriptorCollection pdc = (obj as PropProxy).GetProperties();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
                PropertyDescriptor property = properties.Find("Name", true);
                property.GetValue(obj);
                
                return text + "[" + property.GetValue(obj) + "]";
            }
            string text2 = obj.ToString();
            int num2 = text2.LastIndexOf('.') + 1;
            if (num2 > 0)
            {
                text2 = text2.Substring(num2, text2.Length - num2);
            }
            return text2 + "[" + obj.GetType().Name + "]";
        }

        /// <summary>
        /// 获取一个类指定的属性值
        /// </summary>
        /// <param name="info">object对象</param>
        /// <param name="field">属性名称</param>
        /// <returns></returns>
        /*public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }*/

        public void ShowProperties(object obj)
        {
            this.objcomboBox.Items.Clear();
            //this.objcomboBox.SelectedIndexChanged -= new System.EventHandler(this.objcomboBox_SelectedIndexChanged);
            try
            {
                if (propertyGrid.SelectedObject is IDisposable)
                    (propertyGrid.SelectedObject as IDisposable).Dispose();

				if (obj != null)
				{
                    string objectName = this.GetObjectName(obj);
                    this.objcomboBox.Items.Add(objectName);
					propertyGrid.SelectedObject = obj;
					propertyGrid.PropertyTabs.AddTabType(typeof(EventsTab));
                    this.objcomboBox.SelectedIndex = 0;
                    //this.objcomboBox.SelectedIndexChanged += new System.EventHandler(this.objcomboBox_SelectedIndexChanged);
				}
            /*if(obj is CommonShortProp)
                (obj as CommonShortProp).PropertiesChanged += new CommonShortProp.PropertiesChangedDelegate(PropertyBrowserView_PropertiesChanged);*/
			}
            catch { };
        }
        
		delegate void InvokeDelegate();
        void PropertyBrowserView_PropertiesChanged()
        {
			propertyGrid.BeginInvoke(new InvokeDelegate(delegate() { propertyGrid.Refresh(); }));
        }
	}	
}
