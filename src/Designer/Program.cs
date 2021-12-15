using FreeSCADA.Designer.Dialogs;
using FreeSCADA.Designer.Properties;
using System;
using System.Windows.Forms;

namespace FreeSCADA.Designer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main(string[] args)
		{
            if (System.Environment.Version < new System.Version(2, 0, 0))
            {
                System.Windows.Forms.MessageBox.Show("您的.net版本过低，至少需要.net3.5才能运行", "警告");
                return;
            }
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            bool flag = true;
            if (System.IO.File.Exists(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Flash.jpg")))
            {
                try
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "start.frm"));
                    System.Type type = assembly.GetType("StartForm");
                    if (type != null)
                    {
                        System.Windows.Forms.Form form = (System.Windows.Forms.Form)System.Activator.CreateInstance(type);
                        form.Show();
                        form.Update();
                        flag = false;
                    }
                }
                catch
                {
                }
            }
            if (flag)
            {
                StartForm startForm = new StartForm();
                startForm.Show();
                startForm.Update();
            }
            string text = Settings.Default.AppStart;
            if (System.IO.File.Exists(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Demo\\NewDemo.fs2")))
            {
                text = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Demo\\NewDemo.fs2");
            }
            else
            {
                if (args.Length > 0)
                {
                    string text2 = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, args[0]);
                    if (System.IO.File.Exists(text2))
                    {
                        text = text2;
                    }
                }
                else
                {
                    if (text != "")
                    {
                        text = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, text);
                        if (!System.IO.File.Exists(text))
                        {
                            text = "";
                        }
                    }
                }
            }
            if (text != "")
            {
                System.Windows.Forms.Application.Run(new MainForm(text));
                return;
            }
            System.Windows.Forms.Application.Run(new MainForm());
		}
    }
}
