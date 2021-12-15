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
    public partial class UserDLLDialog : Form
    {
        public UserDLLDialog()
        {
            InitializeComponent();
            this.UpdateDllList();
            if (this.dllList.SelectedIndices.Count == 0 && this.dllList.Items.Count > 0)
            {
                this.dllList.Items[0].Selected = true;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog.Filter = eYgJk0MPml23SOq7Fh.eyj01t0av(18324);
            //openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string[] fileNames = openFileDialog.FileNames;
            for (int i = 0; i < fileNames.Length; i++)
            {
                string path = fileNames[i];
                string fileName = System.IO.Path.GetFileName(path);
                if (Env.Current.Project.ContainsEntity(ProjectEntityType.Dll, fileName))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("文件名: {0}", fileName), "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                    return;
                }
            }
            string[] fileNames2 = openFileDialog.FileNames;
            for (int j = 0; j < fileNames2.Length; j++)
            {
                string path2 = fileNames2[j];
                string fileName2 = System.IO.Path.GetFileName(path2);
                using (System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(System.IO.File.Open(path2, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)))
                {
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        using (System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(memoryStream))
                        {
                            byte[] buffer = new byte[4096];
                            int num;
                            do
                            {
                                num = binaryReader.Read(buffer, 0, 4096);
                                if (num > 0)
                                {
                                    binaryWriter.Write(buffer, 0, num);
                                }
                            }
                            while (num > 0);
                            binaryWriter.Flush();
                            Env.Current.Project.SetData(ProjectEntityType.Dll, fileName2, memoryStream);
                        }
                    }
                }
                this.UpdateDllList();
            }
        }

        private void UpdateDllList()
        {
            System.Windows.Forms.ListView.SelectedIndexCollection arg_12_0 = this.dllList.SelectedIndices;
            this.dllList.Items.Clear();
            string[] entities = Env.Current.Project.GetEntities(ProjectEntityType.Dll);
            for (int i = 0; i < entities.Length; i++)
            {
                string text = entities[i];
                System.IO.Stream data = Env.Current.Project.GetData(ProjectEntityType.Dll, text);
                if (data != null)
                {
                    System.Windows.Forms.ListViewItem listViewItem = this.dllList.Items.Add(text);
                    listViewItem.SubItems.Add(data.Length.ToString());
                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.ListViewItem listViewItem in this.dllList.SelectedItems)
            {
                Env.Current.Project.RemoveEntity(ProjectEntityType.Dll, listViewItem.Text);
            }
            this.UpdateDllList();
        }

        private void imageList_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
        {
            string text = this.dllList.Items[e.Item].Text;
            string label = e.Label;
            if (label != null && label != text && label != "" && !Env.Current.Project.RenameEntity(ProjectEntityType.Dll, text, label))
            {
                e.CancelEdit = true;
            }
        }
    }
}
