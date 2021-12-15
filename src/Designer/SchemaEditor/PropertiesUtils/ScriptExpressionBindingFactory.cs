using FreeSCADA.Common.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    internal class ScriptExpressionBindingFactory : BaseBindingPanelFactory
    {
        public override string Name
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return StringResources.ScriptExpressionBindingPanelName;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool CheckApplicability(object element, PropertyWrapper property)
        {
            return true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool CanWorkWithBinding(BindingBase binding)
        {
            if (binding != null && binding is MultiBinding)
            {
                MultiBinding multiBinding = binding as MultiBinding;
                return multiBinding.Converter is ScriptConverter;
            }
            return false;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override BaseBindingPanel CreateInstance()
        {
            return new ScriptExpressionBindingPanel();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ScriptExpressionBindingFactory()
		{
		}
    }
}
