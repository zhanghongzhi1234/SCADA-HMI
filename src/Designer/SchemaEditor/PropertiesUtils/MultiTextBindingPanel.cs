using FreeSCADA.Common.Schema;
using FreeSCADA.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Data;


namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    internal partial class MultiTextBindingPanel : BaseBindingPanel
    {
        IChannel channel;
        public MultiTextBindingPanel()
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
            System.Type[] itemsType = new System.Type[]
			{
				typeof(string),
				typeof(double),
				typeof(double)
			};
            this.rangeListViewEx1.ItemsType = itemsType;
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
                MultiStringConverter multiStringConverter = binding2.Converter as MultiStringConverter;
                TextRangeValue[] ranges = multiStringConverter.Ranges;
                for (int i = 0; i < ranges.Length; i++)
                {
                    TextRangeValue textRangeValue = ranges[i];
                    System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem();
                    listViewItem.Text = textRangeValue.text;
                    listViewItem.SubItems.Add(textRangeValue.dstart.ToString());
                    listViewItem.SubItems.Add(textRangeValue.dend.ToString());
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
                MultiStringConverter multiStringConverter = new MultiStringConverter();
                System.Collections.Generic.List<TextRangeValue> list = new System.Collections.Generic.List<TextRangeValue>();
                foreach (System.Windows.Forms.ListViewItem listViewItem in this.rangeListViewEx1.Items)
                {
                    list.Add(new TextRangeValue
                    {
                        dstart = double.Parse(listViewItem.SubItems[1].Text),
                        dend = double.Parse(listViewItem.SubItems[2].Text),
                        text = listViewItem.Text
                    });
                }
                multiStringConverter.Ranges = list.ToArray();
                binding.Converter = multiStringConverter;
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
            System.Windows.Forms.ListViewItem listViewItem = this.rangeListViewEx1.Items.Add("文字");
            listViewItem.SubItems.Add("0");
            listViewItem.SubItems.Add("0");
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
        public override string ToString()
        {
            return StringResources.MultiTextBindingPanelName;
        }
    }
}
