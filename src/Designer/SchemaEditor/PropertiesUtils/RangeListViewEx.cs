using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    public class RangeListViewEx : System.Windows.Forms.ListView
    {
        private System.Windows.Forms.TextBox textEditBox;
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public RangeListViewEx()
		{
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
            this.textEditBox.TextChanged += new System.EventHandler(this.textEditBox_TextChanged);
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
                    double num = 0.0;
                    if (double.TryParse(this.textEditBox.Text, out num))
                    {
                        (this.textEditBox.Tag as System.Windows.Forms.ListViewItem.ListViewSubItem).Text = this.textEditBox.Text;
                        this.tb_Leave(sender, null);
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
                    System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
                    colorDialog.Color = item.BackColor;
                    if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        item.BackColor = colorDialog.Color;
                        item.Tag = colorDialog.Color;
                    }
                }
                else
                {
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
            this.textEditBox.TextChanged -= new System.EventHandler(this.textEditBox_TextChanged);
            (sender as System.Windows.Forms.TextBox).Visible = false;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textEditBox_TextChanged(object sender, System.EventArgs e)
        {
        }
    }
}
