using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    public class ListViewEx : System.Windows.Forms.ListView
    {
        private System.Windows.Forms.ComboBox comboBox;
        private System.Collections.Generic.List<System.Type> subitemsType;
        [method: System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public event System.EventHandler OnItemChanged;
        public string[] StrItems
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                this.comboBox.Items.Clear();
                this.comboBox.Items.AddRange(value);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public ListViewEx()
		{
			this.subitemsType = new System.Collections.Generic.List<System.Type>();
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox.Visible = false;
			base.GridLines = true;
			base.FullRowSelect = true;
			base.Controls.Add(this.comboBox);
			this.comboBox.Leave += new System.EventHandler(this.tb_Leave);
		}
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void EditItem(System.Windows.Forms.ListViewItem.ListViewSubItem subItem, System.Drawing.Rectangle rt)
        {
            if (base.SelectedItems.Count <= 0)
            {
                return;
            }
            if (rt.IsEmpty)
            {
                System.Drawing.Rectangle bounds = subItem.Bounds;
                this.comboBox.Bounds = bounds;
            }
            else
            {
                this.comboBox.Bounds = rt;
            }
            this.comboBox.BringToFront();
            this.comboBox.Text = subItem.Text;
            this.comboBox.Visible = true;
            this.comboBox.Tag = subItem;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox.Focus();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void comboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.comboBox.Tag != null && (sender as System.Windows.Forms.ComboBox).Tag is System.Windows.Forms.ListViewItem.ListViewSubItem)
            {
                (this.comboBox.Tag as System.Windows.Forms.ListViewItem.ListViewSubItem).Text = this.comboBox.Text;
                if (this.OnItemChanged != null)
                {
                    this.OnItemChanged(this.comboBox.Tag, null);
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected override void OnSelectedIndexChanged(System.EventArgs e)
        {
            this.comboBox.Visible = false;
            this.comboBox.Tag = null;
            base.OnSelectedIndexChanged(e);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected override void OnDoubleClick(System.EventArgs e)
        {
            System.Drawing.Point point = base.PointToClient(System.Windows.Forms.Cursor.Position);
            System.Windows.Forms.ListViewItem.ListViewSubItem subItem = base.HitTest(point).SubItem;
            System.Windows.Forms.ListViewItem item = base.HitTest(point).Item;
            if (subItem != null && item.SubItems[2].Equals(subItem) && subItem.Text != "")
            {
                System.Drawing.Rectangle empty = System.Drawing.Rectangle.Empty;
                this.EditItem(subItem, empty);
            }
            base.OnDoubleClick(e);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 277 || m.Msg == 276)
            {
                this.comboBox.Visible = false;
            }
            base.WndProc(ref m);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void tb_Leave(object sender, System.EventArgs e)
        {
            (sender as System.Windows.Forms.Control).Visible = false;
            this.comboBox.Tag = null;
        }
    }
}
