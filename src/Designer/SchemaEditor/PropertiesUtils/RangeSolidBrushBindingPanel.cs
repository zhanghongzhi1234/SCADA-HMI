using FreeSCADA.Common.Schema;
using FreeSCADA.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    internal partial class RangeSolidBrushBindingPanel : BaseBindingPanel
    {
        private IChannel channel;
        public RangeSolidBrushBindingPanel()
        {
            InitializeComponent();
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label4.Text = "";
            ChannelMember[] members = MemberOfChannel.GetMembers();
            this.comboBox1.Items.Clear();
            ChannelMember[] array = members;
            for (int i = 0; i < array.Length; i++)
            {
                ChannelMember channelMember = array[i];
                if (channelMember.T.IsValueType || channelMember.T == typeof(object))
                {
                    this.comboBox1.Items.Add(channelMember);
                }
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void AddChannel(IChannel channel)
        {
            if (channel != null && (channel.Type.IsValueType || channel.Type == typeof(object)))
            {
                this.channel = channel;
                this.label3.ForeColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlText);
                this.label4.Text = channel.FullId;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void Initialize(object element, PropertyWrapper property, BindingBase binding)
        {
            base.Initialize(element, property, binding);
            this.comboBox1.Text = "Value";
            System.Windows.Data.Binding binding2 = binding as System.Windows.Data.Binding;
            if (binding2 != null)
            {
                ChannelDataProvider channelDataProvider = (ChannelDataProvider)binding2.Source;
                this.AddChannel(channelDataProvider.Channel);
                this.comboBox1.Text = channelDataProvider.BindPath;
                RangeSolidBrushConverter rangeSolidBrushConverter = binding2.Converter as RangeSolidBrushConverter;
                ColorRangeValue[] ranges = rangeSolidBrushConverter.Ranges;
                for (int i = 0; i < ranges.Length; i++)
                {
                    ColorRangeValue colorRangeValue = ranges[i];
                    System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem();
                    listViewItem.Text = "Color";
                    System.Drawing.Color color = System.Drawing.Color.FromArgb((int)colorRangeValue.bursh.Color.R, (int)colorRangeValue.bursh.Color.G, (int)colorRangeValue.bursh.Color.B);
                    listViewItem.BackColor = color;
                    listViewItem.Tag = color;
                    System.Windows.Forms.ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(colorRangeValue.dstart.ToString());
                    listViewSubItem.BackColor = this.rangeListViewEx1.BackColor;
                    listViewSubItem = listViewItem.SubItems.Add(colorRangeValue.dend.ToString());
                    listViewSubItem.BackColor = this.rangeListViewEx1.BackColor;
                    this.rangeListViewEx1.Items.Add(listViewItem);
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override BindingBase Save()
        {
            if (this.channel != null)
            {
                string text = this.comboBox1.Text;
                if (text == "")
                {
                    text = "Value";
                }
                System.Windows.Data.Binding binding = new System.Windows.Data.Binding("Value");
                ChannelDataProvider channelDataProvider = new ChannelDataProvider();
                channelDataProvider.ChannelName = this.channel.PluginId + "." + this.channel.Name;
                binding.Source = channelDataProvider;
                channelDataProvider.BindPath = text;
                channelDataProvider.Refresh();
                RangeSolidBrushConverter rangeSolidBrushConverter = new RangeSolidBrushConverter();
                System.Collections.Generic.List<ColorRangeValue> list = new System.Collections.Generic.List<ColorRangeValue>();
                foreach (System.Windows.Forms.ListViewItem listViewItem in this.rangeListViewEx1.Items)
                {
                    System.Drawing.Color color = (System.Drawing.Color)listViewItem.Tag;
                    list.Add(new ColorRangeValue
                    {
                        dstart = double.Parse(listViewItem.SubItems[1].Text),
                        dend = double.Parse(listViewItem.SubItems[2].Text),
                        bursh = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B))
                    });
                }
                rangeSolidBrushConverter.Ranges = list.ToArray();
                binding.Converter = rangeSolidBrushConverter;
                binding.Mode = BindingMode.Default;
                System.Windows.DependencyObject dependencyObject;
                System.Windows.DependencyProperty dp;
                if (base.Property.GetWpfObjects(out dependencyObject, out dp))
                {
                    binding.FallbackValue = dependencyObject.GetValue(dp);
                }
                return binding;
            }
            return base.Save();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void addbutton_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ListViewItem listViewItem = this.rangeListViewEx1.Items.Add("Color");
            listViewItem.Tag = System.Drawing.Color.Black;
            listViewItem.BackColor = System.Drawing.Color.Black;
            System.Windows.Forms.ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add("0");
            listViewSubItem.BackColor = this.rangeListViewEx1.BackColor;
            listViewSubItem = listViewItem.SubItems.Add("0");
            listViewSubItem.BackColor = this.rangeListViewEx1.BackColor;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (this.rangeListViewEx1.SelectedItems.Count > 0)
            {
                this.rangeListViewEx1.Items.Remove(this.rangeListViewEx1.SelectedItems[0]);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void rangeListViewEx1_DrawColumnHeader(object sender, System.Windows.Forms.DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void rangeListViewEx1_DrawSubItem(object sender, System.Windows.Forms.DrawListViewSubItemEventArgs e)
        {
            if (e.SubItem.Text == "Color")
            {
                e.Graphics.FillRectangle(new System.Drawing.SolidBrush(e.Item.BackColor), new System.Drawing.Rectangle(e.Item.Bounds.Left + 2, e.Item.Bounds.Top + 2, this.rangeListViewEx1.Columns[0].Width - 4, e.Item.Bounds.Height - 4));
                return;
            }
            e.DrawDefault = true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override string ToString()
        {
            return StringResources.RangeSolidBrushBindingPanelName;
        }
    }
}
