using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using FreeSCADA.Archiver;
using NPlot;

namespace FreeSCADA.RunTime.Views
{
	class ArchiverGraphView : DocumentView
	{
		struct ThreadData
		{
			public ArchiverGraphView view;
			public QueryInfo query;
		}
		private NPlot.Windows.PlotSurface2D graph;
		private Label label1;

		public ArchiverGraphView()
		{
			DocumentName = "Historical view [trends]";
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.graph = new NPlot.Windows.PlotSurface2D();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// graph
			// 
			this.graph.AutoScaleAutoGeneratedAxes = false;
			this.graph.AutoScaleTitle = false;
			this.graph.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.graph.DateTimeToolTip = false;
			this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graph.Legend = null;
			this.graph.LegendZOrder = -1;
			this.graph.Location = new System.Drawing.Point(0, 0);
			this.graph.Name = "graph";
			this.graph.RightMenu = null;
			this.graph.ShowCoordinates = true;
			this.graph.Size = new System.Drawing.Size(744, 400);
			this.graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			this.graph.TabIndex = 3;
			this.graph.Text = "plotSurface2D1";
			this.graph.Title = "";
			this.graph.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.graph.XAxis1 = null;
			this.graph.XAxis2 = null;
			this.graph.YAxis1 = null;
			this.graph.YAxis2 = null;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(744, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Loading data. Please wait...";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.Visible = false;
			// 
			// ArchiverGraphView
			// 
			this.ClientSize = new System.Drawing.Size(744, 400);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.graph);
			this.Name = "ArchiverGraphView";
			this.ResumeLayout(false);

		}

		static void DataLoadingThread(Object threadArgs)
		{
			ThreadData data = (ThreadData)threadArgs;

			Dictionary<ChannelInfo, DataTable> tables = new Dictionary<ChannelInfo, DataTable>();
			foreach (ChannelInfo info in data.query.Channels)
			{
				List<ChannelInfo> tmp = new List<ChannelInfo>();
				tmp.Add(info);
				DataTable dt = ArchiverMain.Current.GetChannelData(data.query.From, data.query.To, tmp);
				if(dt != null)
					tables[info] = dt;
			}

			if (tables.Count > 0)
			{
				object[] args = new object[1];
				args[0] = tables;
				data.view.BeginInvoke(new LoadingFinishedDelegate(data.view.OnLoadingFinished), args);
			}
		}

		public delegate void LoadingFinishedDelegate(Dictionary<ChannelInfo, DataTable> tables);
		public void OnLoadingFinished(Dictionary<ChannelInfo, DataTable> tables)
		{
			graph.Clear();

			Grid myGrid = new Grid();
			myGrid.VerticalGridType = Grid.GridType.Fine;
			myGrid.HorizontalGridType = Grid.GridType.Coarse;
			graph.Add(myGrid);

			Color[] availableColors = new Color[]
			{
				Color.Blue,
				Color.Red,
				Color.Violet,
				Color.Black,
				Color.Cyan,
				Color.Brown,
				Color.Yellow
			};

			int trendNum = 0;
			foreach (ChannelInfo channelInfo in tables.Keys)
			{
				int valueColumnIndex = tables[channelInfo].Columns.IndexOf("Value");
				int timeColumnIndex = tables[channelInfo].Columns.IndexOf("Time");
				int valueCount = tables[channelInfo].Rows.Count;
				double[] values = new double[valueCount];
				DateTime[] labels = new DateTime[valueCount];
				for(int i=0;i<valueCount;i++)
				{
					double.TryParse(tables[channelInfo].Rows[i].ItemArray[valueColumnIndex].ToString(), out values[i]);
					DateTime.TryParse(tables[channelInfo].Rows[i].ItemArray[timeColumnIndex].ToString(), out labels[i]);
				}

				LinePlot lp = new LinePlot();
				lp.DataSource = values;
				lp.AbscissaData = labels;
				lp.Color = availableColors[trendNum % availableColors.Length];
				lp.Label = channelInfo.ChannelName;

				graph.Add(lp);

				trendNum++;
			}

			Legend legend = new Legend();
			legend.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Right);
			legend.HorizontalEdgePlacement = Legend.Placement.Inside;
			legend.VerticalEdgePlacement = Legend.Placement.Outside;
			legend.XOffset = 10;
			legend.YOffset = -10;
			
			graph.Legend = legend;
			graph.LegendZOrder = 1;

			graph.Visible = true;
			label1.Visible = false;

			Cursor = Cursors.Default;
		}


		public bool Open(QueryInfo queryInfo)
		{
			graph.Visible = false;
			label1.Visible = true;
			Cursor = Cursors.WaitCursor;

			ThreadData args = new ThreadData();
			args.view = this;
			args.query = queryInfo;

			ThreadPool.QueueUserWorkItem(new WaitCallback(DataLoadingThread), args);

			return true;
		}
    }
}
