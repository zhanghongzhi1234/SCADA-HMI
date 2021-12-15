using FreeSCADA.Common;
using FreeSCADA.Common.Schema;
using FreeSCADA.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
	internal partial class ScriptExpressionBindingPanel: BaseBindingPanel
	{
        private System.Collections.Generic.List<ChannelDataProvider> channels;
        private System.Collections.Generic.List<SolidColorBrush> colors;

        public ScriptExpressionBindingPanel()
		{
			this.channels = new System.Collections.Generic.List<ChannelDataProvider>();
			this.colors = new System.Collections.Generic.List<SolidColorBrush>();
			ChannelMember[] members = MemberOfChannel.GetMembers();
			this.channels.Clear();
			this.colors.Clear();
			this.InitializeComponent();
			string[] array = new string[members.Length];
			int num = 0;
			ChannelMember[] array2 = members;
			for (int i = 0; i < array2.Length; i++)
			{
				ChannelMember channelMember = array2[i];
				array[num++] = channelMember.ToString();
			}
			this.channelsGrid.StrItems = array;
			this.channelsGrid.OnItemChanged += new System.EventHandler(this.channelsGrid_OnItemChanged);
		}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void channelsGrid_OnItemChanged(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ListViewItem.ListViewSubItem listViewSubItem = (System.Windows.Forms.ListViewItem.ListViewSubItem)sender;
            (this.channelsGrid.SelectedItems[0].Tag as ChannelDataProvider).BindPath = listViewSubItem.Text;
            this.Save();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void Initialize(object element, PropertyWrapper property, BindingBase binding)
        {
            base.Initialize(element, property, binding);
            if (property.PropertyType == typeof(System.Windows.Media.Brush))
            {
                this.colorbutton.Visible = true;
                this.addcolorbutton.Visible = true;
            }
            else
            {
                this.colorbutton.Visible = false;
                this.addcolorbutton.Visible = false;
            }
            MultiBinding multiBinding = binding as MultiBinding;
            if (multiBinding != null)
            {
                this.expressionEdit.Text = (multiBinding.Converter as ScriptConverter).Expression;
                this.channels.Clear();
                foreach (BindingBase current in multiBinding.Bindings)
                {
                    if (current is System.Windows.Data.Binding)
                    {
                        System.Windows.Data.Binding binding2 = current as System.Windows.Data.Binding;
                        if (binding2.Source is ChannelDataProvider)
                        {
                            ChannelDataProvider item = (ChannelDataProvider)binding2.Source;
                            this.channels.Add(item);
                        }
                        else
                        {
                            if (binding2.Source is BrushProvider)
                            {
                                BrushProvider brushProvider = (BrushProvider)binding2.Source;
                                this.colors.Add(new SolidColorBrush(brushProvider.Color));
                            }
                        }
                    }
                }
                this.FillChannelsGrid();
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override BindingBase Save()
        {
            System.Type arg_12_0 = base.Property.PropertyType;
            if (this.channels.Count > 0 || this.colors.Count > 0)
            {
                MultiBinding multiBinding = new MultiBinding();
                foreach (ChannelDataProvider current in this.channels)
                {
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(current.BindPath);
                    binding.Source = current;
                    multiBinding.Bindings.Add(binding);
                }
                foreach (SolidColorBrush current2 in this.colors)
                {
                    System.Windows.Data.Binding binding2 = new System.Windows.Data.Binding("Value");
                    binding2.Source = new BrushProvider
                    {
                        Color = current2.Color
                    };
                    multiBinding.Bindings.Add(binding2);
                }
                System.Windows.DependencyObject dependencyObject;
                System.Windows.DependencyProperty dp;
                if (base.Property.GetWpfObjects(out dependencyObject, out dp))
                {
                    multiBinding.FallbackValue = dependencyObject.GetValue(dp);
                }
                multiBinding.ConverterParameter = multiBinding.FallbackValue;
                multiBinding.Mode = BindingMode.Default;
                ScriptConverter converter = new ScriptConverter(this.expressionEdit.Text);
                multiBinding.Converter = converter;
                return multiBinding;
            }
            return base.Save();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void AddChannel(IChannel channel)
        {
            bool flag = true;
            if (channel != null)
            {
                if (this.checkBox1.Checked && this.channelsGrid.SelectedItems.Count == 1)
                {
                    System.Windows.Forms.ListViewItem listViewItem = this.channelsGrid.SelectedItems[0];
                    flag = false;
                    ChannelDataProvider channelDataProvider = (ChannelDataProvider)listViewItem.Tag;
                    channelDataProvider.ChannelName = channel.FullId;
                    listViewItem.SubItems[1].Text = channel.FullId;
                }
                if (flag)
                {
                    ChannelDataProvider channelDataProvider2 = new ChannelDataProvider();
                    channelDataProvider2.ChannelName = channel.FullId;
                    this.channels.Add(channelDataProvider2);
                    this.FillChannelsGrid();
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void FillChannelsGrid()
        {
            this.channelsGrid.Items.Clear();
            int num = 0;
            foreach (ChannelDataProvider current in this.channels)
            {
                System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem();
                listViewItem.Text = num.ToString();
                listViewItem.SubItems.Add(current.ChannelName);
                listViewItem.SubItems.Add(current.BindPath);
                listViewItem.Tag = current;
                this.channelsGrid.Items.Add(listViewItem);
                num++;
            }
            foreach (SolidColorBrush current2 in this.colors)
            {
                System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
                listViewItem2.Text = num.ToString();
                listViewItem2.SubItems.Add(current2.Color.ToString());
                listViewItem2.SubItems.Add("颜色");
                listViewItem2.Tag = current2;
                listViewItem2.SubItems[2].BackColor = System.Drawing.Color.FromArgb((int)current2.Color.R, (int)current2.Color.G, (int)current2.Color.B);
                this.channelsGrid.Items.Add(listViewItem2);
                num++;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (this.channelsGrid.SelectedItems.Count == 1)
            {
                System.Windows.Forms.ListViewItem listViewItem = this.channelsGrid.SelectedItems[0];
                if (listViewItem.Tag is ChannelDataProvider)
                {
                    ChannelDataProvider item = listViewItem.Tag as ChannelDataProvider;
                    this.channels.Remove(item);
                }
                else
                {
                    if (listViewItem.Tag is SolidColorBrush)
                    {
                        SolidColorBrush item2 = (SolidColorBrush)listViewItem.Tag;
                        this.colors.Remove(item2);
                    }
                }
                this.FillChannelsGrid();
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void colorbutton_Click(object sender, System.EventArgs e)
        {
            this.colorDialog.Color = this.colorbutton.BackColor;
            if (this.colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.colorbutton.BackColor = this.colorDialog.Color;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void addcolorbutton_Click(object sender, System.EventArgs e)
        {
            SolidColorBrush item = new SolidColorBrush(System.Windows.Media.Color.FromRgb(this.colorbutton.BackColor.R, this.colorbutton.BackColor.G, this.colorbutton.BackColor.B));
            this.colors.Add(item);
            this.FillChannelsGrid();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void channelsGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.channelsGrid.SelectedItems.Count == 1 && e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                this.button1.PerformClick();
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void channelsGrid_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.channelsGrid.SelectedItems.Count == 1)
            {
                System.Windows.Forms.ListViewItem listViewItem = this.channelsGrid.SelectedItems[0];
                if (listViewItem.Tag is SolidColorBrush)
                {
                    SolidColorBrush solidColorBrush = (SolidColorBrush)listViewItem.Tag;
                    this.colorDialog.Color = System.Drawing.Color.FromArgb((int)solidColorBrush.Color.R, (int)solidColorBrush.Color.G, (int)solidColorBrush.Color.B);
                    if (this.colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        for (int i = 0; i < this.colors.Count; i++)
                        {
                            if (solidColorBrush == this.colors[i])
                            {
                                this.colors[i] = new SolidColorBrush(System.Windows.Media.Color.FromRgb(this.colorDialog.Color.R, this.colorDialog.Color.G, this.colorDialog.Color.B));
                                listViewItem.Tag = this.colors[i];
                                listViewItem.SubItems[1].Text = this.colors[i].Color.ToString();
                                listViewItem.SubItems[2].BackColor = System.Drawing.Color.FromArgb((int)this.colors[i].Color.R, (int)this.colors[i].Color.G, (int)this.colors[i].Color.B);
                                return;
                            }
                        }
                    }
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void channelsGrid_DrawSubItem(object sender, System.Windows.Forms.DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void channelsGrid_DrawColumnHeader(object sender, System.Windows.Forms.DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void button2_Click(object sender, System.EventArgs e)
        {
            MethodDialog methodDialog = new MethodDialog("选择函数", 2, Env.Current.ScriptManager.ScriptHost, "RunTime.Functions", "");
            if (methodDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.expressionEdit.Text = methodDialog.comboBox1.Text + ";" + methodDialog.comboBox2.Text;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override string ToString()
        {
            return StringResources.ScriptExpressionBindingPanelName;
        }
	}
}
