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
    public partial class ProjectInfoDialog : Form
    {
        public ProjectInfoDialog()
        {
            InitializeComponent();
            this.comboBox1.Items.AddRange(Env.Current.Project.GetEntities(ProjectEntityType.Schema));
            this.comboBox2.Items.AddRange(Env.Current.Project.GetEntities(ProjectEntityType.Param));
            this.userCodecomboBox.Items.AddRange(Env.Current.Project.GetEntities(ProjectEntityType.Dll));
            if (Env.Current.Project.Users != null)
                this.autoLogincomboBox.Items.AddRange(Env.Current.Project.Users);
        }

        private void GetMethod(System.Windows.Forms.TextBox editBox)
        {
            if (Env.Current.ScriptManager.ScriptHost.IsCompiled)
            {
                MethodDialog methodDialog = new MethodDialog("选择函数", Env.Current.ScriptManager.ScriptHost, "RunTime.Global", 0);
                if (methodDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    editBox.Text = methodDialog.comboBox2.Text;
                    return;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("脚本还没有被编译完成，请打开脚本执行编译操作！", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserDLLDialog userDLLDialog = new UserDLLDialog();
            userDLLDialog.ShowDialog();
            this.userCodecomboBox.Items.Clear();
            this.userCodecomboBox.Items.AddRange(Env.Current.Project.GetEntities(ProjectEntityType.Dll));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.GetMethod(this.textBox6);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.GetMethod(this.textBox7);
        }

        private void okbutton_Click(object sender, EventArgs e)
        {

        }

        private void txtOriginX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("只能输入整数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
