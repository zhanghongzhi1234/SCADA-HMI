using FreeSCADA.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FreeSCADA.Designer.Dialogs
{
    public partial class ReferanceForm : Form
    {
        public ReferanceForm()
        {
            InitializeComponent();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void listBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                this.button3.PerformClick();
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void addbutton_Click(object sender, System.EventArgs e)
        {
            if (this.libtextBox.Text != "")
            {
                this.liblistBox.Items.Add(this.libtextBox.Text);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private string ReplacePath(string path)
        {
            return path;/*
            string text = path.ToLower();
            string text2 = System.AppDomain.CurrentDomain.BaseDirectory.ToLower();
            string text3 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows, System.Environment.SpecialFolderOption.None) + eYgJk0MPml23SOq7Fh.eyj01t0av(14756);
            text3 = text3.ToLower();
            string value = System.IO.Path.Combine(new string[]
			{
				text3 + eYgJk0MPml23SOq7Fh.eyj01t0av(14762).ToLower()
			});
            string text4 = Env.Current.Application.ProjectPath.ToLower();
            if (text.Contains(value))
            {
                text = text.Replace(text2, eYgJk0MPml23SOq7Fh.eyj01t0av(14844));
            }
            else
            {
                if (text.Contains(text2))
                {
                    text = text.Replace(text2, eYgJk0MPml23SOq7Fh.eyj01t0av(14858));
                }
                else
                {
                    if (text.Contains(text3))
                    {
                        text = text.Replace(text3, eYgJk0MPml23SOq7Fh.eyj01t0av(14884));
                    }
                    else
                    {
                        if (text4 != "" && text.Contains(text4))
                        {
                            text = text.Replace(text2, eYgJk0MPml23SOq7Fh.eyj01t0av(14904));
                        }
                    }
                }
            }
            return text;*/
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void libtextBox_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && this.openFileDialog1.FileNames.Length > 0)
            {
                string[] fileNames = this.openFileDialog1.FileNames;
                for (int i = 0; i < fileNames.Length; i++)
                {
                    string path = fileNames[i];
                    string item = this.ReplacePath(path);
                    this.liblistBox.Items.Add(item);
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void button2_Click(object sender, System.EventArgs e)
        {
            this.liblistBox.Items.Clear();
            this.liblistBox.Items.Add("System.dll");
            this.liblistBox.Items.Add("System.Data.dll");
            this.liblistBox.Items.Add("System.Drawing.dll");
            this.liblistBox.Items.Add("System.Xml.dll");
            this.liblistBox.Items.Add("System.Windows.Forms.dll");
            this.liblistBox.Items.Add("System.Xaml.dll");
            this.liblistBox.Items.Add("Common.dll");
            this.liblistBox.Items.Add("%wpf%PresentationCore.dll");
            this.liblistBox.Items.Add("%wpf%PresentationFramework.dll");
            this.liblistBox.Items.Add("%wpf%WindowsBase.dll");
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void OnDeleteItem(object sender, System.EventArgs e)
        {
            int num = this.liblistBox.SelectedIndex;
            if (num >= 0)
            {
                this.liblistBox.Items.RemoveAt(num);
                if (this.liblistBox.Items.Count > 0)
                {
                    if (num >= this.liblistBox.Items.Count)
                    {
                        num--;
                    }
                    this.liblistBox.SelectedIndex = num;
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void liblistBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.liblistBox.SelectedIndex >= 0)
            {
                this.libtextBox.Text = this.liblistBox.Items[this.liblistBox.SelectedIndex].ToString();
                this.button3.Enabled = true;
                this.button4.Enabled = true;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void button4_Click(object sender, System.EventArgs e)
        {
            if (this.liblistBox.SelectedIndex >= 0 && this.libtextBox.Text != "")
            {
                this.liblistBox.Items[this.liblistBox.SelectedIndex] = this.libtextBox.Text;
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void libtextBox_TextChanged(object sender, System.EventArgs e)
        {
            this.addbutton.Enabled = (this.libtextBox.Text != "");
        }
    }
}
