using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FreeSCADA.Communication.OPCPlug
{
	public partial class SettingsForm : Form
	{
		Plugin plugin;

		public SettingsForm(Plugin plugin)
		{
			InitializeComponent();
			this.plugin = plugin;

			grid.SelectionMode = SourceGrid.GridSelectionMode.Row;
			grid.Selection.EnableMultiSelection = false;

			DevAge.Drawing.RectangleBorder b = grid.Selection.Border;
			b.SetWidth(0);
			grid.Selection.Border = b;
			grid.Selection.FocusBackColor = grid.Selection.BackColor;

			grid.ColumnsCount = 5;
			grid.RowsCount = 1;
			grid[0, 0] = new SourceGrid.Cells.ColumnHeader("Channel name");
			grid[0, 1] = new SourceGrid.Cells.ColumnHeader("OPC channel");
			grid[0, 2] = new SourceGrid.Cells.ColumnHeader("Server");
			grid[0, 3] = new SourceGrid.Cells.ColumnHeader("Host");
            grid[0, 4] = new SourceGrid.Cells.ColumnHeader("Hierarchy");

			LoadChannels();

			grid.AutoStretchColumnsToFitWidth = true;
			grid.AutoSizeCells();
            grid.Selection.EnableMultiSelection = true;
            UpdateTitle();
		}

		private void OnImport(object sender, EventArgs e)
		{
			ImportOPCForm form = new ImportOPCForm();
			form.ShowDialog(this);
			
			foreach (KeyValuePair<string, ImportOPCForm.OPCChannelInfo> item in form.Channels)
			{
				//string variableName = GetUniqueVariableName();
                ImportOPCForm.OPCChannelInfo ch = item.Value;
                string variableName = ch.channel;       //use channel tag name as name
                variableName = variableName.Replace(".","_");
                string hierarchy = ch.channel;       //use channel tag name as name
                hierarchy = hierarchy.Replace(".", ".");
                //check if exist same name. now ignore
                AddVariable(variableName, hierarchy, ch.channel, ch.progId, ch.host);
			}
            UpdateTitle();
		}

        private void UpdateTitle()
        {
            this.Text = "OPC驱动设置(共" + grid.RowsCount + "个标签)";
        }

		private void AddVariable(string variableName, string hierarchy, string opcChannel, string server, string host)
		{
			int row = grid.RowsCount;
			grid.RowsCount++;

			grid[row, 0] = new SourceGrid.Cells.Cell(variableName, typeof(string));
			grid[row, 1] = new SourceGrid.Cells.Cell(opcChannel, typeof(string));
			grid[row, 2] = new SourceGrid.Cells.Cell(server, typeof(string));
			grid[row, 3] = new SourceGrid.Cells.Cell(host, typeof(string));
            grid[row, 4] = new SourceGrid.Cells.Cell(hierarchy, typeof(string));
            grid.Columns[4].Visible = false;
			grid.Selection.ResetSelection(true);
			grid.Selection.SelectRow(row, true);
		}

		private void OnRemoveRow(object sender, EventArgs e)
		{
            int rowsRemoved = 0;
            foreach (int row in grid.Selection.GetSelectionRegion().GetRowsIndex())
            {
                int currentRow = row - rowsRemoved;     //whole grid will shift up so we decrease the index of seleted row
                grid.Rows.Remove(currentRow);
                rowsRemoved++;
            }
            UpdateTitle();
		}

		private string GetUniqueVariableName()
		{
			string baseName = "variable_";
			int baseNumber = 1;
			for(;;)
			{
				string newName = string.Format(System.Globalization.CultureInfo.CurrentUICulture, "{0}{1}", baseName, baseNumber);
				bool exists = false;
				for (int i = 1; i < grid.RowsCount; i++)
				{
					if (grid[i, 0].DisplayText == newName)
					{
						exists = true;
						break;
					}
				}
				if (exists == false)
					return newName;

				baseNumber++;
			}
		}

		private void OnOkClick(object sender, EventArgs e)
		{
			SaveChannels();
			Close();
		}

		private void OnCancelClick(object sender, EventArgs e)
		{
			Close();
		}

		private void LoadChannels()
		{
			foreach (OPCBaseChannel channel in plugin.Channels)
				AddVariable(channel.Name, channel.Hierarchy, channel.OpcChannel, channel.OpcServer, channel.OpcHost);
		}

		private void SaveChannels()
		{
			Interfaces.IChannel[] channels = new Interfaces.IChannel[grid.RowsCount - 1];
			for (int i = 1; i < grid.RowsCount; i++)
			{
				channels[i-1] = ChannelFactory.CreateChannel(	grid[i, 0].DisplayText,
                                                                grid[i, 4].DisplayText, 
																plugin, 
																grid[i, 1].DisplayText, 
																grid[i, 2].DisplayText,
																grid[i, 3].DisplayText);
			}
			plugin.Channels = channels;
			plugin.SaveSettings();
		}
	}
}
