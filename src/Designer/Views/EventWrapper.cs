using FreeSCADA.Common;
using FreeSCADA.Common.Schema;
using FreeSCADA.Common.Scripting;
using FreeSCADA.Designer.SchemaEditor.PropertiesUtils;
using FreeSCADA.Designer.SchemaEditor.UndoRedo;
using FreeSCADA.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
namespace FreeSCADA.Designer.Views
{
    internal class EventWrapper : PropertyDescriptor
    {
        private string name;
        private string descripton;
        private string dispname;
        public override string DisplayName
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return this.dispname;
            }
        }
        public override string Description
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return this.descripton;
            }
        }
        public override bool IsReadOnly
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return false;
            }
        }
        public override string Name
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return "fs2_" + this.name;
            }
        }
        public override System.Type ComponentType
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                throw new System.NotImplementedException();
            }
        }
        public override System.Type PropertyType
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return typeof(string);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public EventWrapper(string name, string desc, string displayName)
            : base(name, null)
		{
			this.name = name;
			this.descripton = desc;
			this.dispname = displayName;
		}
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool CanResetValue(object component)
        {
            return true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override object GetValue(object component)
        {
            if (component is PropProxy)
            {
                object controlledObject = (component as PropProxy).ControlledObject;
                if (controlledObject is System.Windows.DependencyObject)
                {
                    EventScriptCollection eventScriptCollection = EventScriptCollection.GetEventScriptCollection(controlledObject as System.Windows.DependencyObject);
                    ScriptCallInfo assosiation = eventScriptCollection.GetAssosiation(this.name);
                    if (assosiation == null)
                    {
                        return "";
                    }
                    return assosiation.HandlerName;
                }
            }
            if (!(component is IChannel))
            {
                return null;
            }
            ScriptCallInfo assosiation2 = Env.Current.ScriptManager.ChannelsHandlers.GetAssosiation(this.name, component as IChannel);
            if (assosiation2 == null)
            {
                return "";
            }
            return assosiation2.HandlerName;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void ResetValue(object component)
        {
            throw new System.NotImplementedException();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void SetValue(object component, object value)
        {
            if (component is PropProxy)
            {
                object controlledObject = (component as PropProxy).ControlledObject;
                if (controlledObject is System.Windows.DependencyObject)
                {
                    BasicUndoBuffer undoBufferFor = UndoRedoManager.GetUndoBufferFor(controlledObject as UIElement);
                    undoBufferFor.AddCommand(new ModifyGraphicsObject(controlledObject as UIElement));
                    if (string.IsNullOrEmpty(value as string))
                    {
                        EventScriptCollection eventScriptCollection = EventScriptCollection.GetEventScriptCollection(controlledObject as System.Windows.DependencyObject);
                        eventScriptCollection.RemoveAssociation(this.name);
                        (controlledObject as System.Windows.DependencyObject).ClearValue(EventScriptCollection.EventScriptCollectionProperty);
                        if (eventScriptCollection.Associations.Count > 0)
                        {
                            EventScriptCollection.SetEventScriptCollection(controlledObject as System.Windows.DependencyObject, eventScriptCollection);
                        }
                    }
                    else
                    {
                        Canvas mainCanvas = SchemaDocument.GetMainCanvas(controlledObject as System.Windows.DependencyObject);
                        ScriptCallInfo scriptCallInfo = new ScriptCallInfo();
                        if (mainCanvas != null)
                        {
                            /*if ((mainCanvas.Tag as DocumentView).IsFileDocucment)
                            {
                                return;
                            }*/
                            scriptCallInfo.ClassName = (mainCanvas.Tag as DocumentView).DocumentName;
                        }
                        else
                        {
                            scriptCallInfo.ClassName = "Usercode";
                        }
                        scriptCallInfo.HandlerName = (value as string);
                        EventScriptCollection eventScriptCollection2 = EventScriptCollection.GetEventScriptCollection(controlledObject as System.Windows.DependencyObject);
                        eventScriptCollection2.AddAssociation(this.name, scriptCallInfo);
                        EventScriptCollection.SetEventScriptCollection(controlledObject as System.Windows.DependencyObject, eventScriptCollection2);
                        if (Env.Current.Project.GetData(ProjectEntityType.Script, scriptCallInfo.ClassName) == null)
                        {
                            Env.Current.ScriptManager.CreateNewScript(scriptCallInfo.ClassName, true, true);
                        }
                    }
                }
            }
            if (component is IChannel)
            {
                if (string.IsNullOrEmpty(value as string))
                {
                    Env.Current.ScriptManager.ChannelsHandlers.RemoveAssociation(this.name, component as IChannel);
                    return;
                }
                ScriptCallInfo scriptCallInfo2 = new ScriptCallInfo();
                scriptCallInfo2.HandlerName = (value as string);
                scriptCallInfo2.ClassName = "ChannelHandlers";
                Env.Current.ScriptManager.ChannelsHandlers.AddAssociation(this.name, component as IChannel, scriptCallInfo2);
                if (Env.Current.Project.GetData(ProjectEntityType.Script, "ChannelHandlers") == null)
                {
                    Env.Current.ScriptManager.CreateNewScript("ChannelHandlers", true, false);
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}
