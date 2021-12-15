using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace FreeSCADA.Common.Scripting
{
    internal class SchemaEventProxy
    {
        private DependencyObject elem;
        private System.Reflection.MethodInfo method;
        private bool errorstop;
        private object runtimeclass;
        private bool init;
        public SchemaEventProxy(DependencyObject obj, System.Reflection.MethodInfo m)
        {
            this.elem = obj;
            this.method = m;
        }
        public static Canvas GetTopCanvas(DependencyObject el)
        {
            DependencyObject dependencyObject = el;
            Canvas result;
            try
            {
                while (VisualTreeHelper.GetParent(dependencyObject) != null)
                {
                    dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
                    if (dependencyObject is Canvas && (dependencyObject as Canvas).Tag != null)
                    {
                        result = (dependencyObject as Canvas);
                        return result;
                    }
                }
                result = null;
            }
            catch (System.Exception ex)
            {
                //Env.Current.Logger.LogError("RunTime", "EventScriptCollection GetTopCanvas失败:" + ex.Message);
                result = null;
            }
            return result;
        }
        public void OnEvent(System.Type EventType, object[] args)
        {
            if (!this.init)
            {
                this.init = true;
                if (!this.method.IsStatic)
                {
                    Canvas canvas = null;
                    UIElement topCanvas = SchemaEventProxy.GetTopCanvas(this.elem);
                    if (topCanvas != null && topCanvas is Canvas)
                    {
                        canvas = (topCanvas as Canvas);
                    }
                    if (canvas == null)
                    {
                        this.errorstop = true;
                    }
                    else
                    {
                        if (canvas.Tag == null)
                        {
                            this.errorstop = true;
                        }
                        else
                        {
                            this.runtimeclass = canvas.Tag;
                        }
                    }
                }
            }
            if (this.method != null && !this.errorstop)
            {
                try
                {
                    this.method.Invoke(this.runtimeclass, args);
                }
                catch (System.Exception ex)
                {
                    string message = "SchemaEventProxy:" + this.method.Name + " Error:" + ex.Message;
                    Trace.WriteLine(message);
                    //Env.Current.Logger.LogWarning("EventScriptCollection", message);
                }
            }
        }
    }
}
