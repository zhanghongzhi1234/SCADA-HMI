using FreeSCADA.RunTime.Properties;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace FreeSCADA.RunTime
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args )
		{
            if (Environment.Version < new Version(2, 0, 0))
            {
                MessageBox.Show("您的.net版本过低，至少需要.net3.5才能运行", "警告");
                return;
            }
            if (args != null && args.Length > 0 && args[0].ToLower() == "test")
            {
                return;
            }
            string language = Settings.Default.Language;
            if (!string.IsNullOrEmpty(language))
            {
                //string temp = Thread.CurrentThread.CurrentUICulture.Name;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string text = Settings.Default.StartApp;
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Demo\\NewDemo.fs2")))
            {
                text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Demo\\NewDemo.fs2");
            }
            else
            {
                if (text != "")
                {
                    text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, text);
                    if (!File.Exists(text))
                    {
                        text = "";
                    }
                }
            }
            if (args.Length > 0)
            {
                string text2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, args[0]);
                if (File.Exists(text2))
                {
                    text = text2;
                }
            }
            /*bool flag = true;
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Flash.jpg")))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "start.frm"));
                    Type type = assembly.GetType("StartForm");
                    if (type != null)
                    {
                        Form form = (Form)Activator.CreateInstance(type);
                        form.Show();
                        form.Update();
                        flag = false;
                    }
                }
                catch (Exception)
                {
                }
            }
            if (flag)
            {
                StartForm startForm = new StartForm();
                startForm.Show();
            }*/
            if (text != "")
            {
                Application.Run(new MainForm(text));
                return;
            }
            Application.Run(new MainForm());
		}

        
    }
}
