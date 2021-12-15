﻿using System;
using System.Collections.Generic;
using System.Windows.Data;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Windows;
using FreeSCADA.Interfaces;

namespace FreeSCADA.Common.Schema
{
    public class ExpressionScriptConverter : IMultiValueConverter
    {
        private string exp = "";
        private bool error;
        private System.Reflection.MethodInfo method;
        public string Expression
        {
            get
            {
                return this.exp;
            }
            set
            {
                this.exp = value;
                if (Env.Current.Mode >= EnvironmentMode.Designer && this.Expression != "")
                {
                    Env.Current.ScriptManager.ScriptHost.SourceText = exp;
                    if (Env.Current.ScriptManager.ScriptHost.Compile(true))
                    {
                        this.method = Env.Current.ScriptManager.ScriptHost.GetMethod("GetResult");
                        if (this.method == null)
                        {
                            //Env.Current.Logger.LogWarning("RunTime", "ScriptConver 未发现函数" + this.Expression);
                            this.error = true;
                            return;
                        }
                    }
                }
            }
        }
        public ExpressionScriptConverter()
        {
        }
        public ExpressionScriptConverter(string exp)
        {
            this.Expression = exp;
        }
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object obj = null;
            /*if (Env.Current.Mode == EnvironmentMode.Designer)
            {
                return parameter;
            }*/
            if (this.method != null && !this.error)
            {
                try
                {
                    object[] parameters = new object[]
					{
						values
					};
                    obj = this.method.Invoke(null, parameters);
                    System.Type type = obj.GetType();
                    if (type != targetType && !type.IsSubclassOf(targetType))
                    {
                        if (targetType == typeof(Visibility))
                        {
                            try
                            {
                                bool flag = (bool)System.Convert.ChangeType(obj, typeof(bool));
                                if (flag)
                                {
                                    obj = Visibility.Visible;
                                }
                                else
                                {
                                    obj = Visibility.Hidden;
                                }
                                goto IL_16B;
                            }
                            catch (System.Exception ex)
                            {
                                this.error = true;
                                //Env.Current.Logger.LogWarning("ScriptConver", this.Expression + " ChangeType Error:" + ex.Message);
                                goto IL_16B;
                            }
                        }
                        if (targetType.IsValueType)
                        {
                            try
                            {
                                obj = System.Convert.ChangeType(obj, targetType);
                                goto IL_16B;
                            }
                            catch (System.Exception ex2)
                            {
                                this.error = true;
                                //Env.Current.Logger.LogWarning("ScriptConver", this.Expression + " ChangeType Error:" + ex2.Message);
                                goto IL_16B;
                            }
                        }
                        try
                        {
                            obj = System.Convert.ChangeType(obj, targetType);
                        }
                        catch (System.Exception ex3)
                        {
                            this.error = true;
                            //Env.Current.Logger.LogWarning("ScriptConver", this.Expression + " ChangeType Error:" + ex3.Message);
                        }
                    }
                IL_16B: ;
                }
                catch (System.Exception ex4)
                {
                    this.error = true;
                    //Env.Current.Logger.LogWarning("ScriptConver", this.Expression + " InvokeError:" + ex4.Message);
                }
            }
            return obj;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
