using FreeSCADA.Common.Schema;
using FreeSCADA.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Data;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    internal partial class BooleanBindingPanel : BaseBindingPanel
    {
        private IChannel channel;

        public BooleanBindingPanel()
        {
            InitializeComponent();
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Text = "";
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void AddChannel(IChannel channel)
        {
            if (channel != null && (channel.Type.IsValueType || channel.Type == typeof(object)))
            {
                this.channel = channel;
                this.label3.ForeColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlText);
                this.textBox1.Text = channel.FullId;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void Initialize(object element, PropertyWrapper property, BindingBase binding)
        {
            base.Initialize(element, property, binding);
            System.Windows.Data.Binding binding2 = binding as System.Windows.Data.Binding;
            if (binding2 != null)
            {
                ChannelDataProvider channelDataProvider = (ChannelDataProvider)binding2.Source;
                this.AddChannel(channelDataProvider.Channel);
                ComposingConverter composingConverter = binding2.Converter as ComposingConverter;
                foreach (IValueConverter current in composingConverter.Converters)
                {
                    if (current is FreeSCADA.Common.Schema.BooleanConverter)
                    {
                        FreeSCADA.Common.Schema.BooleanConverter booleanConverter = current as FreeSCADA.Common.Schema.BooleanConverter;
                        this.checkBox1.Checked = booleanConverter.Not;
                    }
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override BindingBase Save()
        {
            if (this.channel != null)
            {
                System.Windows.Data.Binding binding = new System.Windows.Data.Binding("Value");
                ChannelDataProvider channelDataProvider = new ChannelDataProvider();
                channelDataProvider.ChannelName = this.channel.PluginId + "." + this.channel.Name;
                binding.Source = channelDataProvider;
                channelDataProvider.Refresh();
                ComposingConverter composingConverter = new ComposingConverter();
                FreeSCADA.Common.Schema.BooleanConverter booleanConverter = new FreeSCADA.Common.Schema.BooleanConverter();
                booleanConverter.Not = this.checkBox1.Checked;
                composingConverter.Converters.Add(booleanConverter);
                if (base.Property.PropertyType == typeof(Visibility))
                {
                    composingConverter.Converters.Add(new VisibilityConverter());
                    binding.Mode = BindingMode.Default;
                }
                else
                {
                    binding.Mode = BindingMode.TwoWay;
                }
                binding.Converter = composingConverter;
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
        public override string ToString()
        {
            return StringResources.BooleanBindingPanelName;
        }
    }
}
