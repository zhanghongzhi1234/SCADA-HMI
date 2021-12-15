using FreeSCADA.Common.Schema;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Media;
namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    internal class RangeBrushBindingPanelFactory : BaseBindingPanelFactory
    {
        public override string Name
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return StringResources.RangeSolidBrushBindingPanelName;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool CheckApplicability(object element, PropertyWrapper property)
        {
            System.Type propertyType = property.PropertyType;
            return propertyType.Equals(typeof(Brush));
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool CanWorkWithBinding(BindingBase binding)
        {
            if (binding != null && binding is Binding)
            {
                Binding binding2 = binding as Binding;
                return binding2.Source is ChannelDataProvider && binding2.Converter is RangeSolidBrushConverter;
            }
            return false;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override BaseBindingPanel CreateInstance()
        {
            return new RangeSolidBrushBindingPanel();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public RangeBrushBindingPanelFactory()
		{
		}
    }
}
