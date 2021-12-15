using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    public class MultiTextListViewEx : System.Windows.Forms.ListView
    {
        private System.Windows.Forms.TextBox textEditBox;
        private System.Collections.Generic.List<System.Type> subitemsType;
        private System.Type currentType;
        [method: System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public event System.EventHandler OnItemChanged;
        public System.Type[] ItemsType
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                return this.subitemsType.ToArray();
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                this.subitemsType.Clear();
                this.subitemsType.AddRange(value);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public MultiTextListViewEx()
		{
			this.subitemsType = new System.Collections.Generic.List<System.Type>();
			this.textEditBox = new System.Windows.Forms.TextBox();
			this.textEditBox.Multiline = false;
			this.textEditBox.Visible = false;
			base.GridLines = true;
			base.FullRowSelect = true;
			base.Controls.Add(this.textEditBox);
			this.textEditBox.Leave += new System.EventHandler(this.tb_Leave);
			this.textEditBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditBox_KeyDown);
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
                this.textEditBox.Bounds = bounds;
            }
            else
            {
                this.textEditBox.Bounds = rt;
            }
            this.textEditBox.BringToFront();
            this.textEditBox.Text = subItem.Text;
            this.textEditBox.Visible = true;
            this.textEditBox.Tag = subItem;
            this.textEditBox.Select();
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected void textEditBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Return)
            {
                if ((sender as System.Windows.Forms.TextBox).Tag is System.Windows.Forms.ListViewItem.ListViewSubItem)
                {
                    if (this.currentType == typeof(string))
                    {
                        (this.textEditBox.Tag as System.Windows.Forms.ListViewItem.ListViewSubItem).Text = this.textEditBox.Text;
                    }
                    else
                    {
                        try
                        {
                            System.Convert.ChangeType(this.textEditBox.Text, this.currentType);
                            (this.textEditBox.Tag as System.Windows.Forms.ListViewItem.ListViewSubItem).Text = this.textEditBox.Text;
                        }
                        catch
                        {
                            return;
                        }
                    }
                    this.tb_Leave(sender, null);
                    if (this.OnItemChanged != null)
                    {
                        this.OnItemChanged(this, null);
                        return;
                    }
                }
            }
            else
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Escape)
                {
                    this.tb_Leave(sender, null);
                }
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected override void OnSelectedIndexChanged(System.EventArgs e)
        {
            this.textEditBox.Visible = false;
            this.textEditBox.Tag = null;
            base.OnSelectedIndexChanged(e);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected override void OnDoubleClick(System.EventArgs e)
        {
            System.Drawing.Point point = base.PointToClient(System.Windows.Forms.Cursor.Position);
            System.Windows.Forms.ListViewItem.ListViewSubItem subItem = base.HitTest(point).SubItem;
            System.Windows.Forms.ListViewItem item = base.HitTest(point).Item;
            if (subItem != null)
            {
                if (item.SubItems[0].Equals(subItem))
                {
                    if (this.subitemsType.Count == 0)
                    {
                        this.currentType = typeof(string);
                    }
                    else
                    {
                        this.currentType = this.subitemsType[0];
                    }
                    this.EditItem(subItem, new System.Drawing.Rectangle(item.Bounds.Left, item.Bounds.Top, base.Columns[0].Width, item.Bounds.Height - 2));
                }
                else
                {
                    if (this.subitemsType.Count == 0)
                    {
                        this.currentType = typeof(string);
                    }
                    else
                    {
                        int index = item.SubItems.IndexOf(subItem);
                        this.currentType = this.subitemsType[index];
                    }
                    System.Drawing.Rectangle empty = System.Drawing.Rectangle.Empty;
                    this.EditItem(subItem, empty);
                }
            }
            base.OnDoubleClick(e);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 277 || m.Msg == 276)
            {
                this.textEditBox.Visible = false;
            }
            base.WndProc(ref m);
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void tb_Leave(object sender, System.EventArgs e)
        {
            this.textEditBox.Visible = false;
            this.textEditBox.Tag = null;
        }
    }
}
