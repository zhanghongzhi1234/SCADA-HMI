using FreeSCADA.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace FreeSCADA.Common.Dialog
{
    public class TagForm : Form
    {
        private delegate void SetValueDeltege(string value);
        private IContainer components;
        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private Button setbutton;
        private Label label6;
        private Label label5;
        private Button closebutton;
        public TextBox descriptiontextBox;
        public TextBox rangetextBox;
        public TextBox unittextBox;
        public TextBox tagnametextBox;
        public TextBox datasettextBox;
        public TextBox datatextBox;
        private CheckBox checkBox1;
        public TextBox typetextBox;
        private Label label7;
        private IChannel tag;
        private bool canset;
        private bool canUpdate;
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TagForm));
            this.groupBox1 = new GroupBox();
            this.typetextBox = new TextBox();
            this.label7 = new Label();
            this.descriptiontextBox = new TextBox();
            this.rangetextBox = new TextBox();
            this.unittextBox = new TextBox();
            this.tagnametextBox = new TextBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.setbutton = new Button();
            this.datasettextBox = new TextBox();
            this.datatextBox = new TextBox();
            this.label6 = new Label();
            this.label5 = new Label();
            this.closebutton = new Button();
            this.checkBox1 = new CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.typetextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.descriptiontextBox);
            this.groupBox1.Controls.Add(this.rangetextBox);
            this.groupBox1.Controls.Add(this.unittextBox);
            this.groupBox1.Controls.Add(this.tagnametextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            componentResourceManager.ApplyResources(this.typetextBox, "typetextBox");
            this.typetextBox.Name = "typetextBox";
            this.typetextBox.ReadOnly = true;
            componentResourceManager.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            componentResourceManager.ApplyResources(this.descriptiontextBox, "descriptiontextBox");
            this.descriptiontextBox.Name = "descriptiontextBox";
            this.descriptiontextBox.ReadOnly = true;
            componentResourceManager.ApplyResources(this.rangetextBox, "rangetextBox");
            this.rangetextBox.Name = "rangetextBox";
            this.rangetextBox.ReadOnly = true;
            componentResourceManager.ApplyResources(this.unittextBox, "unittextBox");
            this.unittextBox.Name = "unittextBox";
            this.unittextBox.ReadOnly = true;
            componentResourceManager.ApplyResources(this.tagnametextBox, "tagnametextBox");
            this.tagnametextBox.Name = "tagnametextBox";
            this.tagnametextBox.ReadOnly = true;
            componentResourceManager.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            componentResourceManager.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            componentResourceManager.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            componentResourceManager.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.groupBox2.Controls.Add(this.setbutton);
            this.groupBox2.Controls.Add(this.datasettextBox);
            this.groupBox2.Controls.Add(this.datatextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            componentResourceManager.ApplyResources(this.setbutton, "setbutton");
            this.setbutton.Name = "setbutton";
            this.setbutton.UseVisualStyleBackColor = true;
            this.setbutton.Click += new System.EventHandler(this.setbutton_Click);
            componentResourceManager.ApplyResources(this.datasettextBox, "datasettextBox");
            this.datasettextBox.Name = "datasettextBox";
            componentResourceManager.ApplyResources(this.datatextBox, "datatextBox");
            this.datatextBox.Name = "datatextBox";
            this.datatextBox.ReadOnly = true;
            componentResourceManager.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            componentResourceManager.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.closebutton.DialogResult = DialogResult.Cancel;
            componentResourceManager.ApplyResources(this.closebutton, "closebutton");
            this.closebutton.Name = "closebutton";
            this.closebutton.UseVisualStyleBackColor = true;
            this.closebutton.Click += new System.EventHandler(this.closebutton_Click);
            componentResourceManager.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            componentResourceManager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.closebutton;
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.closebutton);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TagForm";
            base.ShowInTaskbar = false;
            base.TopMost = true;
            base.Activated += new System.EventHandler(this.TagForm_Activated);
            base.FormClosing += new FormClosingEventHandler(this.TagForm_FormClosing);
            base.FormClosed += new FormClosedEventHandler(this.TagForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        public TagForm(string tagName, bool set)
        {
            this.InitializeComponent();
            base.Left = Cursor.Position.X;
            base.Top = Cursor.Position.Y;
            this.canset = set;
            if (tagName != "")
            {
                this.tag = Env.Current.GetChannel(tagName);
                if (this.tag != null)
                {
                    this.tagnametextBox.Text = this.tag.FullId;
                    this.unittextBox.Text = this.tag.Unit;
                    this.descriptiontextBox.Text = this.tag.Description;
                    this.rangetextBox.Text = this.tag.RangeMin.ToString() + " - " + this.tag.RangeMax.ToString();
                    this.datatextBox.Text = this.tag.Value.ToString();
                    this.datasettextBox.Text = this.tag.Value.ToString();
                    this.checkBox1.Checked = this.tag.IsReadOnly;
                    this.typetextBox.Text = this.tag.Type.ToString();
                    IChannel channel = Env.Current.GetChannel("system.userlevel");
                    bool flag = true;
                    if ((int)channel.Value < this.tag.Level)
                    {
                        flag = false;
                    }
                    if (!this.tag.IsReadOnly && this.canset && flag && this.tag.Type.IsValueType)
                    {
                        this.datasettextBox.Enabled = true;
                        this.setbutton.Enabled = true;
                    }
                    this.tag.ValueChanged += new System.EventHandler(this.OnValueChanged);
                }
            }
        }
        private void SetValue(string value)
        {
            this.datatextBox.Text = value;
        }
        private void OnValueChanged(object sender, System.EventArgs e)
        {
            if (this.canUpdate)
            {
                try
                {
                    base.Invoke(new TagForm.SetValueDeltege(this.SetValue), new object[]
					{
						(sender as BaseChannel).Value.ToString()
					});
                }
                catch (System.Exception)
                {
                }
            }
        }
        private void closebutton_Click(object sender, System.EventArgs e)
        {
            base.Close();
        }
        private void setbutton_Click(object sender, System.EventArgs e)
        {
            if (this.tag != null && !this.tag.IsReadOnly && this.canset)
            {
                try
                {
                    object obj = StringToValue.ToValue(this.tag.Type, this.datasettextBox.Text);
                    if (obj != null)
                    {
                        this.tag.Value = obj;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void TagForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
        }
        private void TagForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.tag != null)
            {
                this.tag.ValueChanged -= new System.EventHandler(this.OnValueChanged);
            }
        }
        private void TagForm_Activated(object sender, System.EventArgs e)
        {
            if (!this.canUpdate)
            {
                this.canUpdate = true;
            }
        }
    }
}
