using FreeSCADA.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
namespace FreeSCADA.Common.Scripting
{
    public class EventScriptCollection
    {
        private System.Collections.Generic.Dictionary<string, ScriptCallInfo> associations = new System.Collections.Generic.Dictionary<string, ScriptCallInfo>();
        public static readonly DependencyProperty EventScriptCollectionProperty = DependencyProperty.RegisterAttached(typeof(EventScriptCollection).Name, typeof(EventScriptCollection), typeof(EventScriptCollection), new FrameworkPropertyMetadata(new EventScriptCollection(), new PropertyChangedCallback(EventScriptCollection.PropertyChangedCallback)));
        public System.Collections.Generic.Dictionary<string, ScriptCallInfo> Associations
        {
            get
            {
                return this.associations;
            }
            set
            {
                this.associations = value;
            }
        }
        public static void SetEventScriptCollection(DependencyObject obj, EventScriptCollection value)
        {
            obj.SetValue(EventScriptCollection.EventScriptCollectionProperty, value);
        }
        public static EventScriptCollection GetEventScriptCollection(DependencyObject obj)
        {
            if (obj.ReadLocalValue(EventScriptCollection.EventScriptCollectionProperty) == DependencyProperty.UnsetValue)
            {
                return new EventScriptCollection();
            }
            return (EventScriptCollection)obj.GetValue(EventScriptCollection.EventScriptCollectionProperty);
        }
        public static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is EventScriptCollection)
            {
                (e.NewValue as EventScriptCollection).InvalidateEvents(d);
            }
        }
        private void InvalidateEvents(DependencyObject obj)
        {
            if (Env.Current.Mode > EnvironmentMode.Designer)
            {
                foreach (string current in this.associations.Keys)
                {
                    System.Reflection.EventInfo @event = obj.GetType().GetEvent(current);
                    if (!(@event == null))
                    {
                        object eventHandler = EventHandlerFactory.GetEventHandler(@event);
                        System.Delegate handler = System.Delegate.CreateDelegate(@event.EventHandlerType, eventHandler, "CustomEventHandler");
                        @event.AddEventHandler(obj, handler);
                        System.Reflection.MethodInfo method = Env.Current.ScriptManager.GetMethod("RunTime.Schema_" + this.associations[current].ClassName, this.associations[current].HandlerName);
                        if (method == null)
                        {
                            //Env.Current.Logger.LogError("RunTime", "EventScript:" + this.associations[current].HandlerName + "不存在");
                        }
                        else
                        {
                            SchemaEventProxy target = new SchemaEventProxy(obj, method);
                            System.Reflection.EventInfo event2 = eventHandler.GetType().GetEvent("CommonEvent");
                            System.Delegate handler2 = System.Delegate.CreateDelegate(event2.EventHandlerType, target, "OnEvent");
                            event2.AddEventHandler(eventHandler, handler2);
                        }
                    }
                }
            }
        }
        public void AddAssociation(string evnt, ScriptCallInfo info)
        {
            this.associations[evnt] = info;
        }
        public void RemoveAssociation(string evnt)
        {
            if (this.associations.ContainsKey(evnt))
            {
                this.associations.Remove(evnt);
            }
        }
        public ScriptCallInfo GetAssosiation(string evnt)
        {
            if (this.associations.ContainsKey(evnt))
            {
                return this.associations[evnt];
            }
            return null;
        }
    }
}
