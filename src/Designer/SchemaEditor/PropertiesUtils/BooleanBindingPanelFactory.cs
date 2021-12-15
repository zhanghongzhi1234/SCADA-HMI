using FreeSCADA.Common.Schema;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    internal class BooleanBindingPanelFactory : BaseBindingPanelFactory
    {
        public override string Name
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return StringResources.BooleanBindingPanelName;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool CheckApplicability(object element, PropertyWrapper property)
        {
            System.Type propertyType = property.PropertyType;
            System.Collections.Generic.List<System.Type> list = new System.Collections.Generic.List<System.Type>(new System.Type[]
			{
				typeof(bool),
				typeof(Visibility)
			});
            return list.Contains(propertyType);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool CanWorkWithBinding(BindingBase binding)
        {
            if (binding != null && binding is Binding)
            {
                Binding binding2 = binding as Binding;
                return binding2.Source is ChannelDataProvider && binding2.Converter is ComposingConverter;
            }
            return false;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override BaseBindingPanel CreateInstance()
        {
            return new BooleanBindingPanel();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public BooleanBindingPanelFactory()
		{
		}
    }
}
