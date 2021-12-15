using FreeSCADA.Common.Dialog;
using FreeSCADA.Interfaces;
using FreeSCADA.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
namespace FreeSCADA.Common.Scripting
{
    public class FScadaApplication
    {
        public delegate void OpenEntityHandler(ProjectEntityType entity_type, string entityName, string extName);
        public delegate void DisplayTagDialog(string tagName, bool canset);
        private System.Collections.Generic.List<TagForm> listTagFrom = new System.Collections.Generic.List<TagForm>();
        public Color[] availableColors = new Color[]
		{
			System.Windows.Media.Colors.AliceBlue,
			System.Windows.Media.Colors.AntiqueWhite,
			System.Windows.Media.Colors.Aqua,
			System.Windows.Media.Colors.Aquamarine,
			System.Windows.Media.Colors.Azure,
			System.Windows.Media.Colors.Beige,
			System.Windows.Media.Colors.Bisque,
			System.Windows.Media.Colors.Black,
			System.Windows.Media.Colors.BlanchedAlmond,
			System.Windows.Media.Colors.Blue,
			System.Windows.Media.Colors.BlueViolet,
			System.Windows.Media.Colors.Brown,
			System.Windows.Media.Colors.BurlyWood,
			System.Windows.Media.Colors.CadetBlue,
			System.Windows.Media.Colors.Chartreuse,
			System.Windows.Media.Colors.Chocolate,
			System.Windows.Media.Colors.Coral,
			System.Windows.Media.Colors.CornflowerBlue,
			System.Windows.Media.Colors.Cornsilk,
			System.Windows.Media.Colors.Crimson,
			System.Windows.Media.Colors.Cyan,
			System.Windows.Media.Colors.DarkBlue,
			System.Windows.Media.Colors.DarkCyan,
			System.Windows.Media.Colors.DarkGoldenrod,
			System.Windows.Media.Colors.DarkGray,
			System.Windows.Media.Colors.DarkGreen,
			System.Windows.Media.Colors.DarkKhaki,
			System.Windows.Media.Colors.DarkMagenta,
			System.Windows.Media.Colors.DarkOliveGreen,
			System.Windows.Media.Colors.DarkOrange,
			System.Windows.Media.Colors.DarkOrchid,
			System.Windows.Media.Colors.DarkRed,
			System.Windows.Media.Colors.DarkSalmon,
			System.Windows.Media.Colors.DarkSeaGreen,
			System.Windows.Media.Colors.DarkSlateBlue,
			System.Windows.Media.Colors.DarkSlateGray,
			System.Windows.Media.Colors.DarkTurquoise,
			System.Windows.Media.Colors.DarkViolet,
			System.Windows.Media.Colors.DeepPink,
			System.Windows.Media.Colors.DeepSkyBlue,
			System.Windows.Media.Colors.DimGray,
			System.Windows.Media.Colors.DodgerBlue,
			System.Windows.Media.Colors.Firebrick,
			System.Windows.Media.Colors.FloralWhite,
			System.Windows.Media.Colors.ForestGreen,
			System.Windows.Media.Colors.Fuchsia,
			System.Windows.Media.Colors.Gainsboro,
			System.Windows.Media.Colors.GhostWhite,
			System.Windows.Media.Colors.Gold,
			System.Windows.Media.Colors.Goldenrod,
			System.Windows.Media.Colors.Gray,
			System.Windows.Media.Colors.Green,
			System.Windows.Media.Colors.GreenYellow,
			System.Windows.Media.Colors.Honeydew,
			System.Windows.Media.Colors.HotPink,
			System.Windows.Media.Colors.IndianRed,
			System.Windows.Media.Colors.Indigo,
			System.Windows.Media.Colors.Ivory,
			System.Windows.Media.Colors.Khaki,
			System.Windows.Media.Colors.Lavender,
			System.Windows.Media.Colors.LavenderBlush,
			System.Windows.Media.Colors.LawnGreen,
			System.Windows.Media.Colors.LemonChiffon,
			System.Windows.Media.Colors.LightBlue,
			System.Windows.Media.Colors.LightCoral,
			System.Windows.Media.Colors.LightCyan,
			System.Windows.Media.Colors.LightGoldenrodYellow,
			System.Windows.Media.Colors.LightGray,
			System.Windows.Media.Colors.LightGreen,
			System.Windows.Media.Colors.LightPink,
			System.Windows.Media.Colors.LightSalmon,
			System.Windows.Media.Colors.LightSeaGreen,
			System.Windows.Media.Colors.LightSkyBlue,
			System.Windows.Media.Colors.LightSlateGray,
			System.Windows.Media.Colors.LightSteelBlue,
			System.Windows.Media.Colors.LightYellow,
			System.Windows.Media.Colors.Lime,
			System.Windows.Media.Colors.LimeGreen,
			System.Windows.Media.Colors.Linen,
			System.Windows.Media.Colors.Magenta,
			System.Windows.Media.Colors.Maroon,
			System.Windows.Media.Colors.MediumAquamarine,
			System.Windows.Media.Colors.MediumBlue,
			System.Windows.Media.Colors.MediumOrchid,
			System.Windows.Media.Colors.MediumPurple,
			System.Windows.Media.Colors.MediumSeaGreen,
			System.Windows.Media.Colors.MediumSlateBlue,
			System.Windows.Media.Colors.MediumSpringGreen,
			System.Windows.Media.Colors.MediumTurquoise,
			System.Windows.Media.Colors.MediumVioletRed,
			System.Windows.Media.Colors.MidnightBlue,
			System.Windows.Media.Colors.MintCream,
			System.Windows.Media.Colors.MistyRose,
			System.Windows.Media.Colors.Moccasin,
			System.Windows.Media.Colors.NavajoWhite,
			System.Windows.Media.Colors.Navy,
			System.Windows.Media.Colors.OldLace,
			System.Windows.Media.Colors.Olive,
			System.Windows.Media.Colors.OliveDrab,
			System.Windows.Media.Colors.Orange,
			System.Windows.Media.Colors.OrangeRed,
			System.Windows.Media.Colors.Orchid,
			System.Windows.Media.Colors.PaleGoldenrod,
			System.Windows.Media.Colors.PaleGreen,
			System.Windows.Media.Colors.PaleTurquoise,
			System.Windows.Media.Colors.PaleVioletRed,
			System.Windows.Media.Colors.PapayaWhip,
			System.Windows.Media.Colors.PeachPuff,
			System.Windows.Media.Colors.Peru,
			System.Windows.Media.Colors.Pink,
			System.Windows.Media.Colors.Plum,
			System.Windows.Media.Colors.PowderBlue,
			System.Windows.Media.Colors.Purple,
			System.Windows.Media.Colors.Red,
			System.Windows.Media.Colors.RosyBrown,
			System.Windows.Media.Colors.RoyalBlue,
			System.Windows.Media.Colors.SaddleBrown,
			System.Windows.Media.Colors.Salmon,
			System.Windows.Media.Colors.SandyBrown,
			System.Windows.Media.Colors.SeaGreen,
			System.Windows.Media.Colors.SeaShell,
			System.Windows.Media.Colors.Sienna,
			System.Windows.Media.Colors.Silver,
			System.Windows.Media.Colors.SkyBlue,
			System.Windows.Media.Colors.SlateBlue,
			System.Windows.Media.Colors.SlateGray,
			System.Windows.Media.Colors.Snow,
			System.Windows.Media.Colors.SpringGreen,
			System.Windows.Media.Colors.SteelBlue,
			System.Windows.Media.Colors.Tan,
			System.Windows.Media.Colors.Teal,
			System.Windows.Media.Colors.Thistle,
			System.Windows.Media.Colors.Tomato,
			System.Windows.Media.Colors.Transparent,
			System.Windows.Media.Colors.Turquoise,
			System.Windows.Media.Colors.Violet,
			System.Windows.Media.Colors.Wheat,
			System.Windows.Media.Colors.White,
			System.Windows.Media.Colors.WhiteSmoke,
			System.Windows.Media.Colors.Yellow,
			System.Windows.Media.Colors.YellowGreen
		};
        public string[] availableColorNames = new string[]
		{
			"AliceBlue",
			"AntiqueWhite",
			"Aqua",
			"Aquamarine",
			"Azure",
			"Beige",
			"Bisque",
			"Black",
			"BlanchedAlmond",
			"Blue",
			"BlueViolet",
			"Brown",
			"BurlyWood",
			"CadetBlue",
			"Chartreuse",
			"Chocolate",
			"Coral",
			"CornflowerBlue",
			"Cornsilk",
			"Crimson",
			"Cyan",
			"DarkBlue",
			"DarkCyan",
			"DarkGoldenrod",
			"DarkGray",
			"DarkGreen",
			"DarkKhaki",
			"DarkMagenta",
			"DarkOliveGreen",
			"DarkOrange",
			"DarkOrchid",
			"DarkRed",
			"DarkSalmon",
			"DarkSeaGreen",
			"DarkSlateBlue",
			"DarkSlateGray",
			"DarkTurquoise",
			"DarkViolet",
			"DeepPink",
			"DeepSkyBlue",
			"DimGray",
			"DodgerBlue",
			"Firebrick",
			"FloralWhite",
			"ForestGreen",
			"Fuchsia",
			"Gainsboro",
			"GhostWhite",
			"Gold",
			"Goldenrod",
			"Gray",
			"Green",
			"GreenYellow",
			"Honeydew",
			"HotPink",
			"IndianRed",
			"Indigo",
			"Ivory",
			"Khaki",
			"Lavender",
			"LavenderBlush",
			"LawnGreen",
			"LemonChiffon",
			"LightBlue",
			"LightCoral",
			"LightCyan",
			"LightGoldenrodYellow",
			"LightGray",
			"LightGreen",
			"LightPink",
			"LightSalmon",
			"LightSeaGreen",
			"LightSkyBlue",
			"LightSlateGray",
			"LightSteelBlue",
			"LightYellow",
			"Lime",
			"LimeGreen",
			"Linen",
			"Magenta",
			"Maroon",
			"MediumAquamarine",
			"MediumBlue",
			"MediumOrchid",
			"MediumPurple",
			"MediumSeaGreen",
			"MediumSlateBlue",
			"MediumSpringGreen",
			"MediumTurquoise",
			"MediumVioletRed",
			"MidnightBlue",
			"MintCream",
			"MistyRose",
			"Moccasin",
			"NavajoWhite",
			"Navy",
			"OldLace",
			"Olive",
			"OliveDrab",
			"Orange",
			"OrangeRed",
			"Orchid",
			"PaleGoldenrod",
			"PaleGreen",
			"PaleTurquoise",
			"PaleVioletRed",
			"PapayaWhip",
			"PeachPuff",
			"Peru",
			"Pink",
			"Plum",
			"PowderBlue",
			"Purple",
			"Red",
			"RosyBrown",
			"RoyalBlue",
			"SaddleBrown",
			"Salmon",
			"SandyBrown",
			"SeaGreen",
			"SeaShell",
			"Sienna",
			"Silver",
			"SkyBlue",
			"SlateBlue",
			"SlateGray",
			"Snow",
			"SpringGreen",
			"SteelBlue",
			"Tan",
			"Teal",
			"Thistle",
			"Tomato",
			"Transparent",
			"Turquoise",
			"Violet",
			"Wheat",
			"White",
			"WhiteSmoke",
			"Yellow",
			"YellowGreen"
		};
        public event FScadaApplication.OpenEntityHandler OpenEntity;
        public event FScadaApplication.OpenEntityHandler CloseEntity;
        public event System.EventHandler OnLogin;
        public event System.EventHandler OnCommand;
        public event FScadaApplication.DisplayTagDialog OnDisplayTag;
        public string BasePath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        public string ProjectPath
        {
            get
            {
                string text = Env.Current.Project.FileName;
                if (text != "")
                {
                    int num = text.LastIndexOf('\\');
                    if (num > 0)
                    {
                        text = text.Substring(0, num + 1);
                    }
                }
                return text;
            }
        }
        public void PlusTag(string tagName, string inteval)
        {
            if (!Env.Current.IsRuning)
            {
                return;
            }
            int num = 0;
            if (int.TryParse(inteval, out num))
            {
                if (num < 100)
                {
                    num = 100;
                }
                this.SetTagValue(tagName, "1", 0);
                DispatcherTimer dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = System.TimeSpan.FromMilliseconds((double)num);
                dispatcherTimer.Tick += delegate(object sender, System.EventArgs e)
                {
                    if (sender is DispatcherTimer)
                    {
                        DispatcherTimer dispatcherTimer2 = sender as DispatcherTimer;
                        dispatcherTimer2.Stop();
                        this.SetTagValue(dispatcherTimer2.Tag.ToString(), "0", 0);
                        dispatcherTimer2.Tag = null;
                    }
                };
                dispatcherTimer.Tag = tagName;
                dispatcherTimer.Start();
            }
        }
        public void SetTagValue(string tagName, string value, int level = 0)
        {
            if (!Env.Current.IsRuning)
            {
                return;
            }
            if (tagName != "")
            {
                IChannel channel = Env.Current.GetChannel("system.userlevel");
                IChannel channel2 = this.GetChannel(tagName);
                if (channel2 != null)
                {
                    if ((int)channel.Value < channel2.Level && level < channel2.Level)
                    {
                        Env.Current.Logger.LogError("权限不够,不能设置数据:" + tagName);
                        return;
                    }
                    try
                    {
                        if (string.IsNullOrEmpty(value))
                        {
                            this.DisplayTag(tagName, true);
                        }
                        else
                        {
                            object obj = StringToValue.ToValue(channel2.Type, value);
                            if (obj != null)
                            {
                                channel2.Value = obj;
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Env.Current.Logger.LogError("设置:" + tagName + "失败," + ex.Message);
                    }
                }
            }
        }
        public void AddTagValue(string tagName, string value, int level = 0)
        {
            if (!Env.Current.IsRuning)
            {
                return;
            }
            if (tagName != "")
            {
                IChannel channel = Env.Current.GetChannel("system.userlevel");
                IChannel channel2 = this.GetChannel(tagName);
                if (channel2 != null)
                {
                    if ((int)channel.Value < channel2.Level && level < channel2.Level)
                    {
                        Env.Current.Logger.LogError("权限不够,不能设置数据" + tagName);
                        return;
                    }
                    if (channel2.Type.IsValueType)
                    {
                        try
                        {
                            object obj = StringToValue.AddValue(channel2.Value, value);
                            if (obj != null)
                            {
                                channel2.Value = obj;
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Env.Current.Logger.LogError("设置:" + tagName + "失败," + ex.Message);
                        }
                    }
                }
            }
        }
        public void ToogleTagValue(string tagName, int level = 0)
        {
            if (!Env.Current.IsRuning)
            {
                return;
            }
            if (tagName != "")
            {
                IChannel channel = this.GetChannel(tagName);
                IChannel channel2 = Env.Current.GetChannel("system.userlevel");
                if (channel != null)
                {
                    if ((int)channel2.Value < channel.Level && level < channel.Level)
                    {
                        Env.Current.Logger.LogError("权限不够,不能设置数据" + tagName);
                        return;
                    }
                    if (channel.Value is bool)
                    {
                        if ((bool)channel.Value)
                        {
                            channel.Value = false;
                            return;
                        }
                        channel.Value = true;
                        return;
                    }
                    else
                    {
                        if (channel.Value is int || channel.Value is short || channel.Value is ushort || channel.Value is uint || channel.Value is byte)
                        {
                            string a = channel.Value.ToString();
                            if (a == "1")
                            {
                                channel.Value = StringToValue.ToValue(channel.Type, "0");
                                return;
                            }
                            channel.Value = StringToValue.ToValue(channel.Type, "1");
                            return;
                        }
                        else
                        {
                            if (channel.Value is float || channel.Value is double)
                            {
                                if ((double)channel.Value > 0.0)
                                {
                                    channel.Value = StringToValue.ToValue(channel.Type, "0");
                                    return;
                                }
                                channel.Value = StringToValue.ToValue(channel.Type, "1");
                            }
                        }
                    }
                }
            }
        }
        public void FullScreen(bool val)
        {
            Env.Current.Project.FullScreen = val;
        }
        public void DisplayTag(string tagName)
        {
            if (tagName != "")
            {
                this.DisplayTag(tagName, false);
            }
        }
        public void DisplayTag(string tagName, bool set)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return;
            }
            lock (this.listTagFrom)
            {
                foreach (TagForm current in this.listTagFrom)
                {
                    if (current.Tag.ToString() == tagName)
                    {
                        current.Activate();
                        return;
                    }
                }
                TagForm tagForm = new TagForm(tagName, set);
                tagForm.FormClosed += delegate(object sender, FormClosedEventArgs e)
                {
                    lock (this.listTagFrom)
                    {
                        this.listTagFrom.Remove(sender as TagForm);
                    }
                };
                tagForm.Show();
                tagForm.Tag = tagName;
                this.listTagFrom.Add(tagForm);
            }
        }
        public void DisplayTag(Form parent, string tagName, bool set)
        {
            if (tagName == "")
            {
                return;
            }
            lock (this.listTagFrom)
            {
                foreach (TagForm current in this.listTagFrom)
                {
                    if (current.Tag.ToString() == tagName)
                    {
                        current.Activate();
                        return;
                    }
                }
                TagForm tagForm = new TagForm(tagName, set);
                tagForm.FormClosed += delegate(object sender, FormClosedEventArgs e)
                {
                    lock (this.listTagFrom)
                    {
                        this.listTagFrom.Remove(sender as TagForm);
                    }
                };
                tagForm.Show(parent);
                tagForm.Tag = tagName;
                this.listTagFrom.Add(tagForm);
            }
        }
        public IChannel GetChannel(string name)
        {
            return Env.Current.GetChannel(name);
        }
        public void ReplaceSchema(string name)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.ReplaceSchema, name, "");
            }
        }
        public void ReplaceSchema(string name, string param)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.ReplaceSchema, name, param);
            }
        }
        public void OpenSchema(string name)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.Schema, name, "");
            }
        }
        public void CloseSchema(string name)
        {
            if (this.CloseEntity != null)
            {
                this.CloseEntity(ProjectEntityType.Schema, name, "");
            }
        }
        public void OpenSchema(string name, string param)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.Schema, name, param);
            }
        }
        public void OpenDialogSchema(string name)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.Dialog, name, "");
            }
        }
        public void OpenDialogSchema(string name, string param)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.Dialog, name, param);
            }
        }
        public void OpenVariables()
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.VariableListView, "", "");
            }
        }
        public void OpenHisTrend()
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.HisTrend, "", "");
            }
        }
        public void OpenHisTrend(string groupname)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.HisTrend, groupname, "");
            }
        }
        public void OpenRealTrend()
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.RealTrend, "", "");
            }
        }
        public void OpenRealTrend(string groupname)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.RealTrend, groupname, "");
            }
        }
        public Color Colors(int i)
        {
            if (i >= 0 && i < this.availableColors.Length)
            {
                return this.availableColors[i];
            }
            return this.availableColors[0];
        }
        public Brush Brushs(int i)
        {
            if (i >= 0 && i < this.availableColors.Length)
            {
                return new SolidColorBrush(this.availableColors[i]);
            }
            return new SolidColorBrush(this.availableColors[0]);
        }
        public Brush BrushFromColor(byte r, byte g, byte b)
        {
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }
        public void ErrorMsgBox(string msg, string caption)
        {
            System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.Hand);
        }
        public void InfoMsgBox(string msg, string caption)
        {
            System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        public bool QuestionMsgBox(string msg, string caption)
        {
            bool result = false;
            if (System.Windows.MessageBox.Show(msg, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                result = true;
            }
            return result;
        }
        public void Exit()
        {
            if (this.CloseEntity != null)
            {
                this.CloseEntity(ProjectEntityType.Exit, "", "");
            }
        }
        public void ProjectStart()
        {
            if (this.CloseEntity != null)
            {
                this.CloseEntity(ProjectEntityType.Run, "", "");
            }
        }
        public void ProjectStop()
        {
            if (this.CloseEntity != null)
            {
                this.CloseEntity(ProjectEntityType.Stop, "", "");
            }
        }
        public bool Login(string username, string password)
        {
            return username != "" && password != "" && Env.Current.Project.DoLogin(username, password);
        }
        public void Login()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog(Env.Current.MainWindow as Form);
        }
        public void Logout()
        {
            Env.Current.Project.DoLogoff();
        }
        public void LoadRepice(string strfile)
        {
            if (!Env.Current.IsRuning)
            {
                return;
            }
            using (System.IO.Stream stream = Env.Current.Project["recipes\\" + strfile])
            {
                if (stream != null && stream.Length != 0L)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    try
                    {
                        xmlDocument.Load(stream);
                        XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("recipe");
                        if (elementsByTagName != null)
                        {
                            foreach (XmlElement xmlElement in elementsByTagName)
                            {
                                string value = xmlElement.Attributes["value"].Value;
                                IChannel channel = this.GetChannel(xmlElement.Attributes["tagname"].Value);
                                if (channel != null)
                                {
                                    try
                                    {
                                        System.Type type = channel.Type;
                                        object obj = StringToValue.ToValue(type, value);
                                        if (obj != null)
                                        {
                                            channel.Value = obj;
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Env.Current.Logger.LogError("配方设置:" + channel.FullId + "失败," + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        public void CommPlugMonitor(string strPlug)
        {
            ICommunicationPlug communicationPlug = Env.Current.CommunicationPlugins[strPlug];
            if (communicationPlug != null)
            {
                object monitoringView = communicationPlug.GetMonitoringView();
                if (monitoringView != null)
                {
                    (monitoringView as Form).Show(Env.Current.MainWindow as Form);
                }
            }
        }
        public void OpenReportFromFile(string repName, string param)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.FileReport, repName, param);
            }
        }
        public void OpenReport(string repName, string param)
        {
            if (this.OpenEntity != null)
            {
                this.OpenEntity(ProjectEntityType.ReportView, repName, param);
            }
        }
        public void ExtendSendCommand(string extName, string command)
        {
            IExtend extend = Env.Current.ExtendDlls[extName];
            if (extend != null)
            {
                try
                {
                    extend.DoCommand(command);
                }
                catch (System.Exception ex)
                {
                    Env.Current.Logger.LogError(ex.Message);
                }
            }
        }
    }
}
