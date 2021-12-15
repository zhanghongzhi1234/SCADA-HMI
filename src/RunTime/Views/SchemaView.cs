using System;
using System.Windows.Media;
using FreeSCADA.Common;
using FreeSCADA.Common.Schema;
using FreeSCADA.RunTime.DocumentCommands;
using System.Windows.Forms;
using System.Windows.Controls;

namespace FreeSCADA.RunTime.Views
{
	class SchemaView : DocumentView
	{
		private WPFShemaContainer wpfSchemaContainer;
        private ToolStripMenuItem FullScreenMenuItem1;
        private ToolStripMenuItem RestoreMenuItem1;
        private ContextMenuStrip contextMenu;
        private ScaleTransform SchemaScale = new ScaleTransform();
        private myHelpScrollViewer hostedComponent1;
        private System.Windows.Point SavedScrollPosition;
        

		public SchemaView()
		{
            this.contextMenu = new ContextMenuStrip();
            this.FullScreenMenuItem1 = new ToolStripMenuItem(StringResources.FullScreen, null, new EventHandler(this.FullScreenMenuItem_Click));
			InitializeComponent();
            //this.contextMenu.Items.Add(this.FullScreenMenuItem1);
            this.contextMenu.Items.Add("平移", null, new EventHandler(this.OnViewPan));
            this.contextMenu.Items.Add("恢复", null, new EventHandler(this.OnRestore));
            this.wpfSchemaContainer.ContextMenuStrip = this.contextMenu;
			DocumentCommands.Add(new CommandInfo(new NullCommand((int)CommandManager.Priorities.ViewCommands), CommandManager.viewContext));    // Separator
			DocumentCommands.Add(new CommandInfo(new ZoomLevelCommand(), CommandManager.viewContext));
			DocumentCommands.Add(new CommandInfo(new ZoomOutCommand(), CommandManager.viewContext));
			DocumentCommands.Add(new CommandInfo(new ZoomInCommand(), CommandManager.viewContext));
		}

		private void InitializeComponent()
		{
            this.wpfSchemaContainer = new FreeSCADA.Common.Schema.WPFShemaContainer();
            //this.hostedComponent1 = new FreeSCADA.Common.Schema.myHelpScrollViewer();     //it will be created in wpfSchemaContainer class, so comment here
            this.SuspendLayout();
            // 
            // wpfSchemaContainer
            // 
            this.wpfSchemaContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpfSchemaContainer.Location = new System.Drawing.Point(0, 0);
            this.wpfSchemaContainer.Name = "wpfSchemaContainer";
            this.wpfSchemaContainer.Size = new System.Drawing.Size(292, 273);
            this.wpfSchemaContainer.TabIndex = 0;
            this.wpfSchemaContainer.Text = "WPFSchemaContainer";
            this.wpfSchemaContainer.ZoomInEvent += new FreeSCADA.Common.Schema.WPFShemaContainer.ZoomDelegate(this.ZoomIn);
            this.wpfSchemaContainer.ZoomOutEvent += new FreeSCADA.Common.Schema.WPFShemaContainer.ZoomDelegate(this.ZoomOut);
            //this.wpfSchemaContainer.Child = this.hostedComponent1;
            //this.wpfSchemaContainer.SetScrollBar(System.Windows.Controls.ScrollBarVisibility.Hidden);
            //this.wpfSchemaContainer.Visible = false;
            // 
            // SchemaView
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.wpfSchemaContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.VerticalScroll.Visible = false;
            this.HorizontalScroll.Visible = false;
            this.Name = "SchemaView";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        private void FullScreenMenuItem_Click(object sender, System.EventArgs e)
        {
            MainForm mainForm = (MainForm)this.MdiParent;
            mainForm.FullScreenMenuItem_Click(null, EventArgs.Empty);
        }

        private void OnViewPan(object sender, EventArgs e)
        {
            this.wpfSchemaContainer.PanMove = true;
            this.wpfSchemaContainer.Cursor = Cursors.NoMove2D;
        }

        private void OnRestore(object sender, EventArgs e)
        {
            //System.Windows.Controls.ScrollViewer msv = (System.Windows.Controls.ScrollViewer)wpfSchemaContainer.Child;
            SchemaScale.ScaleX = 1;
            SchemaScale.ScaleY = 1;
            MainCanvas.LayoutTransform = SchemaScale;
            //msv.ScrollToVerticalOffset(msv.VerticalOffset * 1.05 + center.Y * 0.05);
            //msv.ScrollToHorizontalOffset(msv.HorizontalOffset * 1.05 + center.X * 0.05);

            this.wpfSchemaContainer.Reset();
        }

		public System.Windows.Controls.Canvas MainCanvas
		{
            get { return wpfSchemaContainer.View as System.Windows.Controls.Canvas; }
			set
			{
				
                if (wpfSchemaContainer.View == null)
                    wpfSchemaContainer.View = value;
                else
                    throw new Exception("View has already attached canvas");
      		}
		}

		public bool LoadDocument(string name)
		{

			System.Windows.Controls.Canvas canvas = SchemaDocument.LoadSchema(name);
			if (canvas == null)
				return false;

			MainCanvas = canvas;
            DocumentName = name;
            string text = "RunTime.Schema_" + name;
            Type classByName = Env.Current.ScriptManager.GetClassByName(text);
            if (classByName != null)
            {
                try
                {
                    object tag = Activator.CreateInstance(classByName, new object[]
						{
							this.MainCanvas
						});
                    this.MainCanvas.Tag = tag;
                }
                catch (Exception ex)
                {
                    //Env.Current.Logger.LogError("Error", "Create " + text + "Failed, " + ex.Message);
                }
            }
			//schema.LinkActions();
			return true;
		}

        public override void OnActivated()
        {
			base.OnActivated();

			foreach (CommandInfo cmdInfo in DocumentCommands)
			{
				if (cmdInfo.command is BaseDocumentCommand)
				{
					BaseDocumentCommand cmd = (BaseDocumentCommand)cmdInfo.command;
					cmd.ControlledObject = this;
				}
			}

            // Scroll to saved position
            System.Windows.Controls.ScrollViewer msv = (System.Windows.Controls.ScrollViewer)wpfSchemaContainer.Child;
            msv.ScrollToVerticalOffset(SavedScrollPosition.Y);
            msv.ScrollToHorizontalOffset(SavedScrollPosition.X);
        }

		public override void OnDeactivated()
        {
			base.OnDeactivated();
            // Save scroll position
            if (wpfSchemaContainer != null)
            {
                System.Windows.Controls.ScrollViewer msv = (System.Windows.Controls.ScrollViewer)wpfSchemaContainer.Child;
                if (msv != null)
                {
                    SavedScrollPosition.Y = msv.VerticalOffset;
                    SavedScrollPosition.X = msv.HorizontalOffset;
                }
            }
        }
        
        protected override void OnClosed(EventArgs e)
		{
			wpfSchemaContainer.Dispose();
			wpfSchemaContainer = null;

			base.OnClosed(e);
		}

        public void ZoomIn()
        {
            ZoomIn(new System.Windows.Point (0,0));
        }

        public void ZoomIn(System.Windows.Point center)
        {
            System.Windows.Controls.ScrollViewer msv = (System.Windows.Controls.ScrollViewer)wpfSchemaContainer.Child;
            SchemaScale.ScaleX *= 1.05;
            SchemaScale.ScaleY *= 1.05;
            MainCanvas.LayoutTransform = SchemaScale;
            msv.ScrollToVerticalOffset(msv.VerticalOffset * 1.05 + center.Y* 0.05);
            msv.ScrollToHorizontalOffset(msv.HorizontalOffset * 1.05 + center.X * 0.05);

			UpdateZoomLevel();
        }

        public void ZoomOut()
        {
            ZoomOut(new System.Windows.Point(0, 0));
        }

        public void ZoomOut(System.Windows.Point center)
        {
            System.Windows.Controls.ScrollViewer msv = (System.Windows.Controls.ScrollViewer)wpfSchemaContainer.Child;
            SchemaScale.ScaleX /= 1.05;
            SchemaScale.ScaleY /= 1.05;
            MainCanvas.LayoutTransform = SchemaScale;
            msv.ScrollToVerticalOffset(msv.VerticalOffset / 1.05 - center.Y * 0.05);
            msv.ScrollToHorizontalOffset(msv.HorizontalOffset / 1.05 - center.X* 0.05);

			UpdateZoomLevel();
        }

		private void UpdateZoomLevel()
		{
			foreach (CommandInfo cmdInfo in DocumentCommands)
			{
				if (cmdInfo.command is ZoomLevelCommand)
					(cmdInfo.command as ZoomLevelCommand).Level = SchemaScale.ScaleX;
			}
		}

        public double ZoomLevel
        {
            get
            {
                return SchemaScale.ScaleX;
            }
            set
            {
                SchemaScale.ScaleX = value;
                SchemaScale.ScaleY = value;
                MainCanvas.LayoutTransform = SchemaScale;
            }
        }

        public void SetScrollBar(System.Windows.Controls.ScrollBarVisibility v)
        {
            wpfSchemaContainer.SetScrollBar(v);
        }
    }
}
