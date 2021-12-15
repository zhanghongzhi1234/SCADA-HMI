using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FreeSCADA.Archiver;
using FreeSCADA.Common;
using FreeSCADA.RunTime.Views;
using WeifenLuo.WinFormsUI.Docking;

namespace FreeSCADA.RunTime
{
    class WindowManager : IDisposable
    {
        //WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        MRUManager mruManager;
        private Form MDIParant;
        List<DocumentView> documentViews = new List<DocumentView>();
        //ProjectContentView projectContentView;
        //LogConsoleView logConsoleView;

        DocumentView currentDocument;
        public SchemaView currentSchema;
        /*
        public WindowManager(DockPanel dockPanel, MRUManager mruManager)
        {
            this.dockPanel = dockPanel;
            this.mruManager = mruManager;
            mruManager.ItemClicked += new MRUManager.ItemClickedDelegate(OnMRUItemClicked);

            //Create toolwindows
            dockPanel.SuspendLayout();
            //projectContentView = new ProjectContentView();
            //projectContentView.Show(dockPanel, DockState.DockLeft);
            //projectContentView.OpenEntity += new ProjectContentView.OpenEntityHandler(OnOpenProjectEntity);

            //logConsoleView = new LogConsoleView();
            //logConsoleView.Show(dockPanel, DockState.DockBottomAutoHide);
            dockPanel.ActiveAutoHideContent = null;
            
            //projectContentView.Activate();
            dockPanel.ResumeLayout(true);

            //Connect Windows Manager to heleper events
            dockPanel.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);

			Env.Current.ScriptManager.ScriptApplication.OpenEntity += new FreeSCADA.Common.Scripting.Application.OpenEntityHandler(OnOpenProjectEntity);
        }*/

        public WindowManager(Form mdiParant, MRUManager mruManager)
        {
            //this.dockPanel = dockPanel;
            this.MDIParant = mdiParant;
            this.mruManager = mruManager;
            mruManager.ItemClicked += new MRUManager.ItemClickedDelegate(OnMRUItemClicked);

            //Create toolwindows
            //dockPanel.SuspendLayout();
            //projectContentView = new ProjectContentView();
            //projectContentView.Show(dockPanel, DockState.DockLeft);
            //projectContentView.OpenEntity += new ProjectContentView.OpenEntityHandler(OnOpenProjectEntity);

            //logConsoleView = new LogConsoleView();
            //logConsoleView.Show();
            //logConsoleView.Show(dockPanel, DockState.DockBottomAutoHide);
            /*dockPanel.ActiveAutoHideContent = null;
            
            //projectContentView.Activate();
            dockPanel.ResumeLayout(true);

            //Connect Windows Manager to heleper events
            dockPanel.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);
            */
            Env.Current.ScriptManager.ScriptApplication.OpenEntity += new FreeSCADA.Common.Scripting.FScadaApplication.OpenEntityHandler(OnOpenProjectEntity);
        }

        public void Close()
        {
            while (documentViews.Count > 0)
            {
                DocumentView doc = documentViews[0];
                doc.Close();
                documentViews.Remove(doc);
            }
            documentViews.Clear();

            //projectContentView.Close();
        }

        void OnDocumentWindowClosing(object sender, FormClosingEventArgs e)
        {
            DocumentView doc = (DocumentView)sender;

            doc.FormClosing -= new FormClosingEventHandler(OnDocumentWindowClosing);
            documentViews.Remove(doc);
        }
        /*
        public void ShowLogView()
        {
            if (this.logConsoleView == null || logConsoleView.IsDisposed == true)
            {
                logConsoleView = new LogConsoleView();
                
                //this.logConsoleView.Activate();
                //return;
            }
            logConsoleView.Show(dockPanel, DockState.DockBottomAutoHide);
            this.logConsoleView.Activate();
            if (this.logConsoleView.WindowState == FormWindowState.Minimized)
            {
                this.logConsoleView.WindowState = FormWindowState.Normal;
            }
            //this.logConsoleView.Activate();
        }*/
        /*
        public void HideLogView()
        {
            if (this.logConsoleView == null)
            {
                logConsoleView = new LogConsoleView();
                logConsoleView.Show(dockPanel, DockState.DockBottomAutoHide);
                this.logConsoleView.Activate();
                return;
            }
            if (this.logConsoleView.WindowState != FormWindowState.Normal)
            {
                this.logConsoleView.WindowState = FormWindowState.Minimized;
                logConsoleView.Hide();
            }
            this.logConsoleView.Activate();
            
            if (this.logConsoleView != null && logConsoleView.IsDisposed == false)
            {
                if (this.logConsoleView.WindowState != FormWindowState.Minimized)
                {
                    this.logConsoleView.WindowState = FormWindowState.Minimized;
                }
                this.logConsoleView.Hide();
            }
        }*/

        public void OnOpenProjectEntity(ProjectEntityType type, string name, string extName)
        {
            // Open your schema and other document types here by entity name
            DocumentView view = null;
            switch (type)
            {
                case ProjectEntityType.Schema:
                    foreach (DocumentView doc in documentViews)
                    {
                        if (doc is SchemaView)
                        {
                            if (doc.DocumentName == name)
                            {
                                doc.Activate();
                                currentDocument = doc;
                                currentSchema = doc as SchemaView;
                                return;
                            }
                        }
                    }
                    view = new SchemaView();
                    if ((view == null) || (view as SchemaView).LoadDocument(name) == false)
                    {
                        System.Windows.MessageBox.Show(DialogMessages.CannotLoadSchema,
                                                        DialogMessages.ErrorCaption,
                                                        System.Windows.MessageBoxButton.OK,
                                                        System.Windows.MessageBoxImage.Error);
                        return;
                    }
                    break;
                case ProjectEntityType.VariableListView:
                    foreach (DocumentView doc in documentViews)
                    {
                        if (doc is VariablesView)
                        {
                            doc.Activate();
                            currentDocument = doc;
                            return;
                        }
                    }
                    view = new VariablesView();
                    if ((view == null))
                    {
                        System.Windows.MessageBox.Show(DialogMessages.CannotLoadVariables,
                                                        DialogMessages.ErrorCaption,
                                                        System.Windows.MessageBoxButton.OK,
                                                        System.Windows.MessageBoxImage.Error);
                        return;
                    }
                    break;
            }
            documentViews.Add(view);
            view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
            view.MdiParent = this.MDIParant;
            view.FormBorderStyle = FormBorderStyle.None;
            view.Dock = DockStyle.Fill;
            //view.AutoScroll = false;
            //view.WindowState = FormWindowState.Maximized;
            view.Show();
            currentDocument = view;
            currentSchema = view as SchemaView;
            //SchemaView temp = view as SchemaView;
            //temp.SetScrollBar(System.Windows.Controls.ScrollBarVisibility.Disabled);
            //view.HorizontalScroll.Visible = false;
            //view.VerticalScroll.Visible = false;
            //view.MainMenuStrip.Visible = false;
            
            //view.Show(dockPanel, DockState.Document);
            //currentDocument = (DocumentView)dockPanel.ActiveDocument;
        }
        /*
        void OnActiveDocumentChanged(object sender, EventArgs e)
        {
            if (currentDocument != null)
                currentDocument.OnDeactivated();

            currentDocument = (DocumentView)dockPanel.ActiveDocument;

            if (currentDocument != null)
                currentDocument.OnActivated();
        }*/

        /// <summary>
        /// Load a project. Asks user for a file.
        /// </summary>
        /// <returns>Returns true if project was successfully loaded</returns>
        public bool LoadProject()
        {
            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = StringResources.FileDialogFilter;
            fd.FilterIndex = 0;
            fd.RestoreDirectory = true;

            if (fd.ShowDialog() != DialogResult.OK)
                return false;
            Close();
            Env.Current.Project.Load(fd.FileName);
            mruManager.Add(fd.FileName);

            return true;
        }

        /// <summary>
        /// Load a project, taking filename from command line argument.
        /// </summary>
        /// <returns>Returns true if project was successfully loaded</returns>
        public bool LoadProject(string fileToLoad)
        {
            Close();
            Env.Current.Project.Load(fileToLoad);
            mruManager.Add(fileToLoad);
            return true;
        }
        /*
        public void ShowQueryView()
        {
            foreach (DockContent view in dockPanel.Contents)
            {
                if (view is QueryView)
                {
                    view.Activate();
                    return;
                }
            }

            QueryView queryView = new QueryView();
            queryView.Show(dockPanel, DockState.DockRight);
            dockPanel.DockRightPortion = (double)(queryView.MinimumSize.Width + 4) / dockPanel.Size.Width;

            queryView.FormClosed += new FormClosedEventHandler(OnQueryViewClosed);
            queryView.OpenTableView += new QueryView.ExecuteQueryHandler(OnOpenTableView);
            queryView.OpenGraphView += new QueryView.ExecuteQueryHandler(OnOpenGraphView);
        }*/
        public bool StartRuntime()
        {
            if (Env.Current.CommunicationPlugins.Connect())
            {
                if (ArchiverMain.Current.DatabaseSettings.EnableArchiving)
                {
                    if (ArchiverMain.Current.Start() == false)
                    {
                        Env.Current.CommunicationPlugins.Disconnect();
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public void StopRuntime()
        {
            if (ArchiverMain.Current.DatabaseSettings.EnableArchiving)
                ArchiverMain.Current.Stop();

            Env.Current.CommunicationPlugins.Disconnect();

        }
        /*
        void OnOpenTableView(QueryInfo query)
        {
            ArchiverTableView view = new ArchiverTableView();
            if (view.Open(query) == true)
            {
                view.Show(dockPanel, DockState.Document);
                view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
                documentViews.Add(view);
            }
        }

        void OnOpenGraphView(QueryInfo query)
        {
            ArchiverGraphView view = new ArchiverGraphView();
            if (view.Open(query) == true)
            {
                view.Show(dockPanel, DockState.Document);
                view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
                documentViews.Add(view);
            }
        }

        void OnQueryViewClosed(object sender, FormClosedEventArgs e)
        {
            QueryView queryView = sender as QueryView;

            queryView.FormClosed -= new FormClosedEventHandler(OnQueryViewClosed);
            queryView.OpenTableView -= new QueryView.ExecuteQueryHandler(OnOpenTableView);
        }*/

        void OnMRUItemClicked(object sender, string file)
        {
            LoadProject(file);
        }


        #region IDisposable Members

        public void Dispose()
        {
            Close();

            mruManager.ItemClicked -= new MRUManager.ItemClickedDelegate(OnMRUItemClicked);
            //projectContentView.OpenEntity -= new ProjectContentView.OpenEntityHandler(OnOpenProjectEntity);
            //dockPanel.ActiveDocumentChanged -= new EventHandler(OnActiveDocumentChanged);
			Env.Current.ScriptManager.ScriptApplication.OpenEntity -= new FreeSCADA.Common.Scripting.FScadaApplication.OpenEntityHandler(OnOpenProjectEntity);

            //projectContentView.Dispose();
            //logConsoleView.Dispose();
            //dockPanel.Dispose();
            mruManager.Dispose();
        }

        #endregion
    }
}
