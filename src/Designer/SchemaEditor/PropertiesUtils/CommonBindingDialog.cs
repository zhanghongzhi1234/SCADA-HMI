using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using FreeSCADA.Common;
using FreeSCADA.Interfaces;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
	/// <summary>
	/// Common dialog for bindings
	/// </summary>
	public partial class CommonBindingDialog : Form
	{
		object element;
		BaseBindingPanel bindingPanel;
        Dictionary<PropertyWrapper, BindingBase> activeBindings = new Dictionary<PropertyWrapper, BindingBase>();
        private System.Drawing.Font bindfont;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="element"></param>
		public CommonBindingDialog(object element)
		{
			this.element = element;
			InitializeComponent();
            this.bindfont = new System.Drawing.Font(this.propertyList.Font.FontFamily, this.propertyList.Font.Size, System.Drawing.FontStyle.Bold);
			FillChannels();
			FillProperties();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="element"></param>
		/// <param name="activeProperty"></param>
		internal CommonBindingDialog(object element, PropertyInfo activeProperty)
		{
			this.element = element;
			InitializeComponent();
            this.bindfont = new System.Drawing.Font(this.propertyList.Font.FontFamily, this.propertyList.Font.Size, System.Drawing.FontStyle.Bold);
			FillChannels();
			FillProperties();

			for(int i=0;i<propertyList.Items.Count;i++)
			{
                if ((propertyList.Items[i] as PropertyWrapper).PropertyInfo.SourceProperty == activeProperty.SourceProperty)
				{
					propertyList.SelectedIndex = i;
					break;
				}
			}
		}

		void FillProperties()
		{
			propertyList.Items.Clear();

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(element);
			foreach (PropertyDescriptor property in properties)
			{
                if (property is PropertyWrapper)
                {
                    propertyList.Items.Add(property);
                    PropertyWrapper propertyWrapper = property as PropertyWrapper;
                    System.Windows.Data.BindingBase binding = propertyWrapper.GetBinding();
                    if (binding != null)
                        activeBindings[propertyWrapper] = binding;
                }
			}

			if (propertyList.Items.Count > 0)
				propertyList.SelectedIndex = 0;
		}

		void FillChannels()
		{
			channelsTree.Nodes.Clear();
			foreach (string plugId in Env.Current.CommunicationPlugins.PluginIds)
			{
				TreeNode plugNode = channelsTree.Nodes.Add(Env.Current.CommunicationPlugins[plugId].Name);
                if (plugId == "opc_connection_plug")
                {   //use tree structure
                    foreach (FreeSCADA.Interfaces.IChannel ch in Env.Current.CommunicationPlugins[plugId].Channels)
                    {
                        if (ch.Hierarchy != null)
                        {
                            if (ch.Hierarchy != "")
                            {
                                string[] hierarchy = ch.Hierarchy.Split('.');
                                TreeNode parentNode = plugNode;
                                for (int i = 0; i < hierarchy.Length; i++)
                                {
                                    string nodeName = hierarchy[i];
                                    TreeNode[] nodes = parentNode.Nodes.Find(nodeName, false);
                                    if (nodes.Length >= 1)
                                    {
                                        parentNode = nodes[0];
                                    }
                                    else
                                    {
                                        TreeNode chNode1;           //use chNode1 to avoid name confilct with below blocks
                                        chNode1 = parentNode.Nodes.Add(hierarchy[i]);
                                        chNode1.Name = hierarchy[i];
                                        if (i == hierarchy.Length - 1)
                                            chNode1.Tag = ch;
                                        parentNode = chNode1;
                                    }
                                }
                                continue;
                            }
                        }
                        //This mean ch.Hierarchy is null or empty string
                        TreeNode chNode;
                        chNode = plugNode.Nodes.Add(ch.Name);
                        chNode.Tag = ch;
                        //OPCBaseChannel chOPC = (OPCBaseChannel)ch;
                    }
                }
                else
                {
                    foreach (FreeSCADA.Interfaces.IChannel ch in Env.Current.CommunicationPlugins[plugId].Channels)
                    {
                        TreeNode chNode;
                        chNode = plugNode.Nodes.Add(ch.Name);
                        chNode.Tag = ch;
                    }
                    plugNode.Expand();
                }
			}
		}

		void FillBindingTypes()
		{
			bindingTypes.Items.Clear();
			bindingTypes.Items.AddRange(GetAvailableBindingPanels().ToArray());

			if (bindingTypes.Items.Count > 0)
				bindingTypes.SelectedIndex = 0;
		}

		private void CreateAssociationButton_Click(object sender, EventArgs e)
		{
			SavePanelStateAndClose();

			if (propertyList.SelectedIndex >= 0 && bindingTypes.SelectedIndex >= 0)
			{
				BaseBindingPanelFactory factory = (BaseBindingPanelFactory)bindingTypes.SelectedItem;
				bindingPanel = factory.CreateInstance();
				bindingPanel.Initialize(element, propertyList.SelectedItem as PropertyWrapper, null);
				bindingPanel.Parent = panel1;
				bindingPanel.Dock = DockStyle.Fill;
				CreateAssociationButton.Enabled = false;
				bindingTypes.Enabled = false;
			}
		}

		void ShowBindingPanel(bool save=true)
		{
            if(save == true)
			    SavePanelStateAndClose();

            RemoveAssociationButton.Enabled = false;
			if (propertyList.SelectedIndex >= 0)
			{
                System.Windows.Data.BindingBase binding = GetExistingBinding(propertyList.SelectedItem as PropertyWrapper);
				if (binding != null)
				{
					PropertyInfo propInfo = propertyList.SelectedItem as PropertyInfo;
					List<BaseBindingPanelFactory> panels = GetAvailableBindingPanels();
					foreach (BaseBindingPanelFactory panel in panels)
					{
						if (panel.CanWorkWithBinding(binding))
						{
							bindingPanel = panel.CreateInstance();
							break;
						}
					}

					if (bindingPanel != null)
					{
                        bindingPanel.Initialize(element, propertyList.SelectedItem as PropertyWrapper, binding);
						bindingPanel.Parent = panel1;
						bindingPanel.Dock = DockStyle.Fill;
                        this.bindinglabel.Text = this.bindingPanel.ToString();
						enableInDesignerCheckbox.Checked = bindingPanel.EnableInDesigner;

                        RemoveAssociationButton.Enabled = true;
					}
					CreateAssociationButton.Enabled = false;
                    this.bindingTypes.Enabled = false;
                    this.bindingTypes.Visible = false;
                    this.bindinglabel.Visible = true;
                    return;
				}
                this.bindinglabel.Visible = false;
			}
		}

		private void SavePanelStateAndClose()
		{
			if (bindingPanel != null)
			{
				BindingBase binding = bindingPanel.Save();
				if (binding != null)
					activeBindings[bindingPanel.Property] = binding;

				bindingPanel.Dispose();
				bindingPanel = null;
			}
		}

        private void RemoveBindingPanelAndClose()
        {
            if (bindingPanel != null)
            {
                activeBindings.Remove(bindingPanel.Property);
                bindingPanel.Dispose();
                bindingPanel = null;
            }
        }

		System.Windows.Data.BindingBase GetExistingBinding(PropertyWrapper property)
		{
			if (activeBindings.ContainsKey(property))
				return activeBindings[property];
			else
			{
                return property.GetBinding();
			}
		}

		List<BaseBindingPanelFactory> GetAvailableBindingPanels()
		{
			PropertyWrapper property = null;
			List<BaseBindingPanelFactory> result = new List<BaseBindingPanelFactory>();

			if (propertyList.SelectedIndex >= 0)
				property = propertyList.Items[propertyList.SelectedIndex] as PropertyWrapper;

			if (property == null)
				return result;

			Assembly archiverAssembly = this.GetType().Assembly;
			foreach (Type type in archiverAssembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof(BaseBindingPanelFactory)))
				{
					BaseBindingPanelFactory factory = (BaseBindingPanelFactory)Activator.CreateInstance(type);
					if (factory != null && factory.CheckApplicability(element, property))
						result.Add(factory);
				}
			}

			return result;
		}

		void UpdateControlsState()
		{
			if (GetAvailableBindingPanels().Count > 0)
			{
				CreateAssociationButton.Enabled = true;
                //RemoveAssociationButton.Enabled = false;
				enableInDesignerCheckbox.Enabled = true;
				bindingTypes.Enabled = true;
                this.bindingTypes.Visible = true;
			}
			else
			{
				CreateAssociationButton.Enabled = false;
                //RemoveAssociationButton.Enabled = true;
				enableInDesignerCheckbox.Enabled = false;
				bindingTypes.Enabled = false;
                this.bindinglabel.Visible = true;
			}

            if (propertyList.SelectedIndex >= 0)
            {
                System.Windows.Data.BindingBase binding = GetExistingBinding(propertyList.SelectedItem as PropertyWrapper);
                if (binding != null)
                {
                }
            }
		}

		private void propertyList_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillBindingTypes();
			UpdateControlsState();
			ShowBindingPanel();
		}

		private void channelsTree_DoubleClick(object sender, EventArgs e)
		{
			if (channelsTree.SelectedNode != null && bindingPanel != null)
			{
				bindingPanel.AddChannel(channelsTree.SelectedNode.Tag as IChannel);
			}
		}

		private void enableInDesignerCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if (bindingPanel != null)
				bindingPanel.EnableInDesigner = enableInDesignerCheckbox.Checked;
		}

        private void saveButton_Click(object sender, EventArgs e)
        {
			SavePanelStateAndClose();
            try
            {
                if (activeBindings.Count > 0)
                {
                    foreach (PropertyWrapper key in activeBindings.Keys)
                    {
                        DependencyObject depObj;
                        DependencyProperty depProp;
                        System.Windows.Data.BindingBase binding = activeBindings[key];
                        if (key.GetWpfObjects(out depObj, out depProp))
                        {
                            BindingOperations.ClearBinding(depObj, depProp);
                            if(binding != null)
                                BindingOperations.SetBinding(depObj, depProp, binding);
                        }
                    }
                }
            }
            catch(SystemException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
			DialogResult = DialogResult.OK;
			Close();
        }

        private void RemoveAssociationButton_Click(object sender, EventArgs e)
        {
            DependencyObject depObj;
            DependencyProperty depProp;
            if (bindingPanel.Property.GetWpfObjects(out depObj, out depProp) != null)
                BindingOperations.ClearBinding(depObj, depProp);
            /*activeBindings.Remove(bindingPanel.Property);
            UpdateControlsState();
            ShowBindingPanel();*/
            RemoveBindingPanelAndClose();

            FillBindingTypes();
            UpdateControlsState();
            ShowBindingPanel();
            propertyList.Refresh();
        }

        private void propertyList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.State == System.Windows.Forms.DrawItemState.Selected)
            {
                e.DrawFocusRectangle();
            }
            else
            {
                e.DrawBackground();
            }
            if (e.Index < 0)
            {
                return;
            }
            BindingBase binding = (this.propertyList.Items[e.Index] as PropertyWrapper).GetBinding();
            if (binding != null)
            {
                e.Graphics.DrawString(this.propertyList.Items[e.Index].ToString(), this.bindfont, System.Drawing.Brushes.Red, e.Bounds);
                return;
            }
            e.Graphics.DrawString(this.propertyList.Items[e.Index].ToString(), this.propertyList.Font, System.Drawing.Brushes.Black, e.Bounds);
        }

        private void CommonBindingDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }
	}
}