using FreeSCADA.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FreeSCADA.Common
{
    public partial class MethodDialog : Form
    {
        private IScriptHost host;
        private int state;
        private int inputcount = -1;
        private System.Reflection.MethodInfo[] methodInfos;

        public MethodDialog(IScriptHost sh)
        {
            if (!Env.Current.ScriptManager.ScriptHost.IsCompiled)
            {
                Env.Current.ScriptManager.ScriptHost.Compile(true);
            }
            this.InitializeComponent();
            this.host = sh;
            this.comboBox1.Items.AddRange(this.host.Types);
            this.comboBox1.SelectedIndex = 0;
        }
        public MethodDialog(string caption, int state, IScriptHost sh, string modulename, string current = "")
        {
            if (!Env.Current.ScriptManager.ScriptHost.IsCompiled)
            {
                Env.Current.ScriptManager.ScriptHost.Compile(true);
            }
            this.state = state;
            this.InitializeComponent();
            this.textBox2.Text = current;
            this.host = sh;
            this.Text = caption;
            this.comboBox1.Items.AddRange(this.host.Types);
            if (modulename != "")
            {
                this.comboBox1.Text = modulename;
            }
            else
            {
                this.comboBox1.SelectedIndex = 0;
            }
            if (current != "")
            {
                string[] array = current.Split(new char[]
				{
					';'
				});
                if (array.Length == 2)
                {
                    this.comboBox2.Text = array[1];
                }
            }
        }
        public MethodDialog(string caption, IScriptHost sh, string modulename, int inputcount)
        {
            if (!Env.Current.ScriptManager.ScriptHost.IsCompiled)
            {
                Env.Current.ScriptManager.ScriptHost.Compile(true);
            }
            this.InitializeComponent();
            this.Text = caption;
            this.host = sh;
            this.comboBox1.Items.Add(modulename);
            this.inputcount = inputcount;
            if (this.comboBox1.Items.Count > 0)
            {
                this.comboBox1.Text = "RunTime.Global";
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.comboBox2.Items.Clear();
            if (this.state == 0)
            {
                if (this.inputcount < 0)
                {
                    this.methodInfos = this.host.MethodInfos(this.comboBox1.Text);
                    System.Reflection.MethodInfo[] array = this.methodInfos;
                    for (int i = 0; i < array.Length; i++)
                    {
                        System.Reflection.MethodInfo methodInfo = array[i];
                        this.comboBox2.Items.Add(methodInfo.Name);
                    }
                }
                else
                {
                    System.Collections.Generic.List<System.Reflection.MethodInfo> list = new System.Collections.Generic.List<System.Reflection.MethodInfo>();
                    System.Reflection.MethodInfo[] array2 = this.host.MethodInfos(this.comboBox1.Text);
                    for (int j = 0; j < array2.Length; j++)
                    {
                        System.Reflection.MethodInfo methodInfo2 = array2[j];
                        if (methodInfo2.GetParameters().Length == this.inputcount)
                        {
                            list.Add(methodInfo2);
                            this.comboBox2.Items.Add(methodInfo2.Name);
                        }
                    }
                    this.methodInfos = list.ToArray();
                }
            }
            else
            {
                if (this.state == 1)
                {
                    System.Collections.Generic.List<System.Reflection.MethodInfo> list2 = new System.Collections.Generic.List<System.Reflection.MethodInfo>();
                    System.Reflection.MethodInfo[] array3 = this.host.MethodInfos(this.comboBox1.Text);
                    for (int k = 0; k < array3.Length; k++)
                    {
                        System.Reflection.MethodInfo methodInfo3 = array3[k];
                        if (methodInfo3.GetParameters().Length == 1)
                        {
                            System.Reflection.ParameterInfo[] parameters = methodInfo3.GetParameters();
                            if (parameters[0].ParameterType == typeof(IChannel))
                            {
                                list2.Add(methodInfo3);
                                this.comboBox2.Items.Add(methodInfo3.Name);
                            }
                        }
                    }
                    this.methodInfos = list2.ToArray();
                }
                else
                {
                    if (this.state == 2)
                    {
                        System.Collections.Generic.List<System.Reflection.MethodInfo> list3 = new System.Collections.Generic.List<System.Reflection.MethodInfo>();
                        System.Reflection.MethodInfo[] array4 = this.host.MethodInfos(this.comboBox1.Text);
                        for (int l = 0; l < array4.Length; l++)
                        {
                            System.Reflection.MethodInfo methodInfo4 = array4[l];
                            if (methodInfo4.GetParameters().Length == 1)
                            {
                                System.Reflection.ParameterInfo[] parameters2 = methodInfo4.GetParameters();
                                if (parameters2[0].ParameterType == typeof(object[]))
                                {
                                    list3.Add(methodInfo4);
                                    this.comboBox2.Items.Add(methodInfo4.Name);
                                }
                            }
                        }
                        this.methodInfos = list3.ToArray();
                    }
                }
            }
            if (this.comboBox2.Items.Count > 0)
            {
                this.comboBox2.SelectedIndex = 0;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.comboBox2.SelectedIndex >= 0)
            {
                System.Reflection.MethodInfo methodInfo = this.methodInfos[this.comboBox2.SelectedIndex];
                string text = methodInfo.ToString() + "\r\n名称:" + methodInfo.Name + "\r\n输入:";
                int num = 0;
                System.Reflection.ParameterInfo[] parameters = methodInfo.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    System.Reflection.ParameterInfo parameterInfo = parameters[i];
                    if (num == 0)
                    {
                        text += parameterInfo.ParameterType.ToString();
                    }
                    else
                    {
                        text = text + "," + parameterInfo.ParameterType.ToString();
                    }
                    num++;
                }
                text = text + "\r\n返回:" + methodInfo.ReturnType.ToString();
                this.textBox1.Text = text;
                return;
            }
            this.textBox1.Text = "";
        }
    }
}
