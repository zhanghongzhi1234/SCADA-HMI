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
    class EventsTab : System.Windows.Forms.Design.PropertyTab
    {
        private System.Collections.Generic.List<string> expArray;
        public override System.ComponentModel.PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
        {
            //Return list of known events
            EventDescriptorCollection events;
            if (component is PropProxy)
                events = (component as PropProxy).GetEvents();
            else
                events = TypeDescriptor.GetEvents(component, true);

            System.Collections.Generic.List<PropertyDescriptor> list = new System.Collections.Generic.List<PropertyDescriptor>();
            for (int i = 0; i < events.Count; i++)
            {
                if (!this.expArray.Contains(events[i].Name))
                {
                    list.Add(new EventWrapper(events[i].Name, events[i].Description, events[i].DisplayName));
                }
            }
            return new PropertyDescriptorCollection(list.ToArray());
            /*
            PropertyDescriptor[] events = new PropertyDescriptor[events_info.Count];
            for (int i = 0; i < events_info.Count; i++)
                events[i] = new EventWrapper(events_info[i].Name);

            return new PropertyDescriptorCollection(events);
            */
        }

        public override string TabName
        {
            get { return "Events"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return Properties.Resources.open_events; }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		public EventsTab()
		{
			this.expArray = new System.Collections.Generic.List<string>(new string[]
			{
				
			});
		}
    }
}
