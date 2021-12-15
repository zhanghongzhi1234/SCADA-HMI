using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace FreeSCADA.Common.Dialog
{
    public class LoginForm : Form
    {
        private IContainer components;
        private GroupBox groupBox1;
        private Button okbutton;
        private Button cancelbutton;
        private Label label1;
        private TextBox passwordtextBox;
        private Label label2;
        private TextBox usernametextBox;
        private Button logoffbutton;
        public LoginForm()
        {
            this.InitializeComponent();
        }
        private void okbutton_Click(object sender, System.EventArgs e)
        {
            if (Env.Current.Project.DoLogin(this.usernametextBox.Text, this.passwordtextBox.Text))
            {
                base.Dispose();
                return;
            }
            base.DialogResult = DialogResult.None;
        }
        private void cancelbutton_Click(object sender, System.EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Dispose();
        }
        private void LoginForm_Load(object sender, System.EventArgs e)
        {
            string a = Env.Current.GetChannel("system.username").Value.ToString();
            if (a != "")
            {
                this.usernametextBox.Enabled = false;
                this.passwordtextBox.Enabled = false;
                this.logoffbutton.Visible = true;
                this.okbutton.Visible = false;
                return;
            }
            this.usernametextBox.Enabled = true;
            this.passwordtextBox.Enabled = true;
            this.logoffbutton.Visible = false;
            this.okbutton.Visible = true;
        }
        private void logoffbutton_Click(object sender, System.EventArgs e)
        {
            Env.Current.Project.DoLogoff();
            base.Dispose();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LoginForm));
            this.groupBox1 = new GroupBox();
            this.passwordtextBox = new TextBox();
            this.label2 = new Label();
            this.usernametextBox = new TextBox();
            this.label1 = new Label();
            this.okbutton = new Button();
            this.cancelbutton = new Button();
            this.logoffbutton = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.passwordtextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.usernametextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            componentResourceManager.ApplyResources(this.passwordtextBox, "passwordtextBox");
            this.passwordtextBox.Name = "passwordtextBox";
            componentResourceManager.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            componentResourceManager.ApplyResources(this.usernametextBox, "usernametextBox");
            this.usernametextBox.Name = "usernametextBox";
            componentResourceManager.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            componentResourceManager.ApplyResources(this.okbutton, "okbutton");
            this.okbutton.DialogResult = DialogResult.OK;
            this.okbutton.Name = "okbutton";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            componentResourceManager.ApplyResources(this.cancelbutton, "cancelbutton");
            this.cancelbutton.DialogResult = DialogResult.Cancel;
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            componentResourceManager.ApplyResources(this.logoffbutton, "logoffbutton");
            this.logoffbutton.DialogResult = DialogResult.OK;
            this.logoffbutton.Name = "logoffbutton";
            this.logoffbutton.UseVisualStyleBackColor = true;
            this.logoffbutton.Click += new System.EventHandler(this.logoffbutton_Click);
            base.AcceptButton = this.okbutton;
            componentResourceManager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.cancelbutton;
            base.Controls.Add(this.cancelbutton);
            base.Controls.Add(this.okbutton);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.logoffbutton);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "LoginForm";
            base.TopMost = true;
            base.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}
