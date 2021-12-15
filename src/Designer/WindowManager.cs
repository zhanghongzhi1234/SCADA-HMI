using FreeSCADA.Common;
using FreeSCADA.Common.Scripting;
using FreeSCADA.Designer.Dialogs;
using FreeSCADA.Designer.Views;
using FreeSCADA.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
namespace FreeSCADA.Designer
{
	class WindowManager: IDisposable
    {
        public static string password;
		WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		MRUManager mruManager;

        List<DocumentView> documentViews = new List<DocumentView>();
        DocumentView currentDocument;

		ProjectContentView projectContentView;
        PropertyBrowserView propertyBrowserView;
		ToolBoxView toolBoxView;
        private System.IO.FileStream ipcFile;

        public WindowManager(WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel, MRUManager mruManager)
		{
            WindowManager.password = "";
			this.dockPanel = dockPanel;
			this.mruManager = mruManager;

			mruManager.ItemClicked += new MRUManager.ItemClickedDelegate(OnMRUItemClicked);

			//Create toolwindows
			projectContentView = new ProjectContentView();
			projectContentView.Show(dockPanel, DockState.DockLeft);
			projectContentView.OpenEntity += new ProjectContentView.OpenEntityHandler(OnOpenProjectEntity);
			projectContentView.SelectNode += new ProjectContentView.SelectNodeHandler(OnSelectProjectNode);
            this.projectContentView.SchemaRename += new System.EventHandler(this.OnSchemaRename);

			toolBoxView = new ToolBoxView();
			toolBoxView.Show(dockPanel, DockState.DockRight);
            
            propertyBrowserView = new PropertyBrowserView();
			propertyBrowserView.Show(toolBoxView.Pane, DockAlignment.Bottom, 0.6);

			//Connect Windows Manager to heleper events
			dockPanel.ActiveDocumentChanged += new EventHandler(OnActiveDocumentChanged);

			Env.Current.ScriptManager.NewScriptCreated += new NewScriptCreatedHandler(OnOpenScript);
		}

		public void ForceWindowsClose()
		{
            while (documentViews.Count > 0)
            {
                DocumentView doc = documentViews[0];
                //doc.HandleModifiedOnClose = false;
                doc.Close(); //this window should be removed from documentViews on closing
                documentViews.Remove(doc);
            }
            documentViews.Clear();

			projectContentView.Close();
			toolBoxView.Close();
			propertyBrowserView.Close();

			dockPanel.ActiveDocumentChanged -= new EventHandler(OnActiveDocumentChanged);
		}

        private void OnSchemaRename(object sender, System.EventArgs e)
        {
            NodeRename nodeRename = (NodeRename)sender;
            bool flag = false;
            foreach (DocumentView current in this.documentViews)
            {
                if (current.DocumentName == nodeRename.oldName)
                {
                    if (current is SchemaView)
                    {
                        flag = true;
                    }
                    this.ReName(current, nodeRename);
                }
            }
            if (nodeRename.ftype == ProjectEntityType.Schema && !flag)
            {
                this.OnOpenProjectEntity(ProjectEntityType.Schema, nodeRename.newName);
                DocumentView rendoc = this.currentDocument;
                this.ReName(rendoc, nodeRename);
            }
            System.Windows.Forms.MessageBox.Show("文件名称修改完毕，请手动修改脚本的类名!");
        }
        
        private void ReName(DocumentView rendoc, NodeRename nr)
        {
            if (rendoc is SchemaView)
            {
                rendoc.DocumentName = nr.newName;
                Canvas mainCanvas = (rendoc as SchemaView).MainCanvas;
                foreach (UIElement uIElement in mainCanvas.Children)
                {
                    System.Windows.DependencyObject dependencyObject = uIElement;
                    bool flag = false;
                    EventScriptCollection eventScriptCollection = null;
                    if (dependencyObject.ReadLocalValue(EventScriptCollection.EventScriptCollectionProperty) != System.Windows.DependencyProperty.UnsetValue)
                    {
                        eventScriptCollection = (EventScriptCollection)dependencyObject.GetValue(EventScriptCollection.EventScriptCollectionProperty);
                    }
                    if (eventScriptCollection != null && eventScriptCollection.Associations.Count > 0)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, ScriptCallInfo> current in eventScriptCollection.Associations)
                        {
                            if (current.Value.ClassName != rendoc.DocumentName)
                            {
                                current.Value.ClassName = rendoc.DocumentName;
                                rendoc.IsModified = true;
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            dependencyObject.ClearValue(EventScriptCollection.EventScriptCollectionProperty);
                            EventScriptCollection.SetEventScriptCollection(dependencyObject, eventScriptCollection);
                            flag = false;
                        }
                    }
                }
                rendoc.SaveDocument();
                return;
            }
            if (rendoc is SharpCodeView && rendoc.DocumentName == nr.oldName)
            {
                rendoc.DocumentName = nr.newName;
            }
        }

		public void CreateNewSchema()
		{
			SchemaView view = new SchemaView();
			if(view.CreateNewDocument() == false)
			{
				System.Windows.MessageBox.Show( DialogMessages.CannotCreateSchema,
												DialogMessages.ErrorCaption,
												System.Windows.MessageBoxButton.OK,
												System.Windows.MessageBoxImage.Error);
				return;
			}

			//Generate unique name
			for (int i = 1; i < 1000; i++)
			{
				string newName = string.Format("{0}_{1}", StringResources.UntitledSchema, i);
				bool hasTheSameDocument = false;
				foreach (DocumentView doc in documentViews)
				{
					if (doc is SchemaView && doc.DocumentName == newName)
					{
						hasTheSameDocument = true;
						break;
					}
				}
				if (hasTheSameDocument == false && !Env.Current.Project.ContainsEntity(ProjectEntityType.Schema,newName))
				{
					view.DocumentName = newName;
					break;
				}
			}
            
			view.ToolsCollectionChanged += toolBoxView.OnToolsCollectionChanged;
            view.SetCurrentTool += toolBoxView.OnSetCurrentTool;
            view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
			documentViews.Add(view);
			view.Show(dockPanel, DockState.Document);
		}

        void OnDocumentWindowClosing(object sender, FormClosingEventArgs e)
		{
            DocumentView documentView = (DocumentView)sender;
            if (documentView.HandleModifiedOnClose && documentView.IsModified)
			{
				System.Windows.MessageBoxResult res = System.Windows.MessageBox.Show(	DialogMessages.NotSavedDocument,
																						DialogMessages.SaveDocumentCaption,
																						System.Windows.MessageBoxButton.YesNoCancel,
                                                                                        System.Windows.MessageBoxImage.Exclamation);
                if (res == System.Windows.MessageBoxResult.Yes)
                {
                    if (!documentView.SaveDocument())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    if (res == System.Windows.MessageBoxResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
			}
            if (documentView is ArchiverSettingsView || documentView is SharpCodeView)
            {
                this.projectContentView.RefreshContent(Env.Current.Project);
            }
            //doc.ToolsCollectionChanged -= toolBoxView.OnToolsCollectionChanged;
            documentView.FormClosing -= new FormClosingEventHandler(OnDocumentWindowClosing);
            documentViews.Remove(documentView);
            propertyBrowserView.ShowProperties(new Object());
        }

        public void OnOpenProjectEntity(ProjectEntityType type, string name)
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
                                return;
                            }
                        }
                    }
                    view = new SchemaView();
                    if (view == null || view.LoadDocument(name) == false)
                    {
                        System.Windows.MessageBox.Show(DialogMessages.CannotLoadSchema,
                                                        DialogMessages.ErrorCaption,
                                                        System.Windows.MessageBoxButton.OK,
                                                        System.Windows.MessageBoxImage.Error);
                        return;
                    }
                    view.ToolsCollectionChanged += toolBoxView.OnToolsCollectionChanged;
                    view.SetCurrentTool += toolBoxView.OnSetCurrentTool;
                    view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
                    documentViews.Add(view);
                    view.Show(dockPanel, DockState.Document);
                    break;
                case ProjectEntityType.Archiver:
                    ShowArchiverSettings();
                    break;
				case ProjectEntityType.Script:
					OnOpenScript(this, name);
					break;
                // etc....
                default:
                    break;
            }
		}

		void OnSelectProjectNode(FreeSCADA.Designer.Views.ProjectNodes.BaseNode node)
		{
			if (node is FreeSCADA.Designer.Views.ProjectNodes.ChannelNode)
			{
				FreeSCADA.Interfaces.IChannel ch = (node as FreeSCADA.Designer.Views.ProjectNodes.ChannelNode).Channel;
				propertyBrowserView.ShowProperties(ch);
			}
		}

		/// <summary>
		/// Create or active existing "Events" view.
		/// </summary>
		public void ShowEvents()
		{
			foreach (DocumentView doc in documentViews)
			{
				if (doc is EventsView)
				{
					doc.Activate();
					return;
				}
			}

			EventsView view = new EventsView();
			view.Show(dockPanel, DockState.Document);

			view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
			documentViews.Add(view);
		}

        /// <summary>
        /// Create or active existing "Archiver Settings" view.
        /// </summary>
        public void ShowArchiverSettings()
        {
            foreach (DocumentView doc in documentViews)
            {
                if (doc is ArchiverSettingsView)
                {
                    doc.Activate();
                    return;
                }
            }

            ArchiverSettingsView view = new ArchiverSettingsView();
            view.Show(dockPanel, DockState.Document);

            view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
            documentViews.Add(view);
        }

        /// <summary>
        /// Create or active existing "Variables" view.
        /// </summary>
        public void ShowVariablesView()
        {
            foreach (DocumentView doc in documentViews)
            {
                if (doc is VariablesView)
                {
                    doc.Activate();
                    return;
                }
            }

            VariablesView view = new VariablesView();
            view.Show(dockPanel, DockState.Document);

            view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
            view.SelectChannel += new VariablesView.SelectChannelHandler(OnSelectChannel);
            documentViews.Add(view);
        }

        void OnSelectChannel(object channel)
        {
            if (propertyBrowserView != null)
                propertyBrowserView.ShowProperties(channel);
        }

        /*void OnOpenScript(object sender, Script script)
		{
			foreach (DocumentView doc in documentViews)
			{
				if (doc is ScriptView && doc.DocumentName == script.Name)
				{
					doc.Activate();
					return;
				}
			}

			ScriptView view = new ScriptView(script);
			view.Show(dockPanel, DockState.Document);

			view.FormClosing += new FormClosingEventHandler(OnDocumentWindowClosing);
			documentViews.Add(view);
		}*/

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void OnOpenScript(object sender, string scriptName)
        {
            foreach (DocumentView current in this.documentViews)
            {
                if (current is SharpCodeView && current.DocumentName == scriptName)
                {
                    current.Activate();
                    return;
                }
            }
            SharpCodeView sharpCodeView = new SharpCodeView(scriptName);
            sharpCodeView.Show(this.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            sharpCodeView.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnDocumentWindowClosing);
            this.documentViews.Add(sharpCodeView);
        }

        /// <summary>
		/// SaveDocument current document
		/// </summary>
		public void SaveDocument()
		{
			if (currentDocument != null && currentDocument.IsModified)
				currentDocument.SaveDocument();
			projectContentView.RefreshContent(Env.Current.Project);
		}

		/// <summary>
		/// SaveDocument all opened documents
		/// </summary>
		public void SaveAllDocuments()
		{
			foreach(DocumentView documentWindow in documentViews)
				if (documentWindow != null && documentWindow.IsModified)
					documentWindow.SaveDocument();
			projectContentView.RefreshContent(Env.Current.Project);
		}

		/// <summary>
		/// Save current project. Asks user for a file if needed.
		/// </summary>
		/// <returns>Returns true if project was successfully saved</returns>
		public bool SaveProject()
		{
			string projectFileName = Env.Current.Project.FileName;
			if (projectFileName == "")
			{
				SaveFileDialog fd = new SaveFileDialog();

				fd.Filter = StringResources.FileOpenDialogFilter;
				fd.FilterIndex = 0;
				fd.RestoreDirectory = true;

				if (fd.ShowDialog() == DialogResult.OK)
					projectFileName = fd.FileName;
				else
					return false;
			}
            Env.Current.ScriptManager.SaveRefrence();
			SaveAllDocuments();
			Env.Current.Project.Save(projectFileName);
			projectContentView.RefreshContent(Env.Current.Project);
			mruManager.Add(projectFileName);
            System.Windows.Forms.MessageBox.Show(null, "项目已经保存", "消息", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
			return true;
		}

		/// <summary>
		/// Load a project. Asks user for a file.
		/// </summary>
		/// <returns>Returns true if project was successfully loaded</returns>
		public bool LoadProject()
		{
			OpenFileDialog fd = new OpenFileDialog();

			fd.Filter = StringResources.FileOpenDialogFilter;
			fd.FilterIndex = 0;
			fd.RestoreDirectory = true;

			if (fd.ShowDialog() != DialogResult.OK)
				return false;

            if (this.Close() && !this.CheckOpened(fd.FileName))
            {
                Env.Current.Project.Load(fd.FileName);
                mruManager.Add(fd.FileName);
                return true;
            }
            return false;
		}

        public bool LoadProject(string fileName)
        {
            if (this.Close() && !this.CheckOpened(fileName))
            {
                Env.Current.Project.Load(fileName);
                this.mruManager.Add(fileName);
                return true;
            }
            return false;
        }

        private bool CheckOpened(string fileopen)
        {
            string text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            text = System.IO.Path.Combine(text, fileopen.GetHashCode().ToString());
            try
            {
                this.ipcFile = new System.IO.FileStream(text, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);
            }
            catch (System.Exception)
            {
                System.Windows.Forms.MessageBox.Show(null, "当前文件已被打开", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return true;
            }
            return false;
        }

		void OnMRUItemClicked(object sender, string file)
		{
			Close();
			Env.Current.Project.Load(file);
			mruManager.Add(file);
		}

		/// <summary>
		/// Close project. If there are unsaved open documents, an appropriate dialog will be show. User will be able 
		/// to save these documents or cancel cloasure. Return false if closing should be canceled.
		/// </summary>
		/// <returns>Return true if all views successfully close. Otherwise returns false which 
		/// should prevent application closing.</returns>
		public bool Close()
		{
            List<string> unsaved_documents = new List<string>();
            List<DocumentView> other_documents = new List<DocumentView>();

            if (Env.Current.Project.IsModified)
			{
				if(Env.Current.Project.FileName == "")
					unsaved_documents.Add(StringResources.UnsavedProjectName);
				else
					unsaved_documents.Add(Env.Current.Project.FileName);
			}

			foreach (DocumentView documentWindow in documentViews)
			{
                if (documentWindow != null && documentWindow.IsModified)
                    unsaved_documents.Add(documentWindow.DocumentName);
                else
                    other_documents.Add(documentWindow);
			}

            foreach (DocumentView documentWindow in other_documents)
            {
                documentWindow.Close();
                documentViews.Remove(documentWindow);
            }

			if (unsaved_documents.Count > 0)
			{
				SaveDocumentsDialog dlg = new SaveDocumentsDialog(unsaved_documents);
				System.Windows.Forms.DialogResult res = dlg.ShowDialog(Env.Current.MainWindow);
				if (res == System.Windows.Forms.DialogResult.No)
				{
					while (documentViews.Count > 0)
					{
						DocumentView doc = documentViews[0];
						doc.HandleModifiedOnClose = false;
						doc.Close(); //this window should be removed from documentViews on closing
                        documentViews.Remove(doc);
					}
					return true;
				}
				if (res == System.Windows.Forms.DialogResult.Cancel)
					return false;
				if (res == System.Windows.Forms.DialogResult.Yes)
				{
					if (SaveProject() == false)
						return false;

                    while (documentViews.Count > 0)
                    {
                        DocumentView doc = documentViews[0];
                        doc.HandleModifiedOnClose = false;
                        doc.Close();
                        documentViews.Remove(doc);
                    }
                    documentViews.Clear();
				}
			}
		return true;
		}

		void OnActiveDocumentChanged(object sender, EventArgs e)
		{
			DeactivatingDocument();
			currentDocument = (DocumentView)dockPanel.ActiveDocument;
			ActivatingDocument();
		}

		private void ActivatingDocument()
		{
			//Notify and subscribe document to appropriate tool windows
			if (currentDocument != null)
			{
                //toolBoxView.Clean(); 
                currentDocument.OnActivated();
				toolBoxView.ToolActivated += currentDocument.OnToolActivated;
    			currentDocument.ObjectSelected += propertyBrowserView.ShowProperties;
                //(Env.Current.MainWindow as MainForm).undoButton.Tag = currentDocument;
                //(Env.Current.MainWindow as MainForm).redoButton.Tag = currentDocument;
            }
		}

		private void DeactivatingDocument()
		{
			//Notify and unsubscribe document from all tool windows
			if (currentDocument != null)
			{
				currentDocument.OnDeactivated();
				toolBoxView.ToolActivated -= currentDocument.OnToolActivated;
                currentDocument.ObjectSelected -= propertyBrowserView.ShowProperties;
            }
            propertyBrowserView.ShowProperties(new Object());
        }

        public void SetCurrentDocumentFocus()
        {
            if (currentDocument != null) currentDocument.Focus();
        }

      

        public void ExecuteCommand(FreeSCADA.Interfaces.ICommand command, Object param)
        {
			//SchemaEditor.SchemaCommands.SchemaCommand cmd = (SchemaEditor.SchemaCommands.SchemaCommand)command;
			//if(cmd != null)
			//	cmd.ControlledObject = (param == null) ? currentDocument : param;

			//command.Execute();
        }


		#region IDisposable Members

		public void Dispose()
		{
			ForceWindowsClose();

			mruManager.ItemClicked -= new MRUManager.ItemClickedDelegate(OnMRUItemClicked);
			projectContentView.OpenEntity -= new ProjectContentView.OpenEntityHandler(OnOpenProjectEntity);
			projectContentView.SelectNode -= new ProjectContentView.SelectNodeHandler(OnSelectProjectNode);
			dockPanel.ActiveDocumentChanged -= new EventHandler(OnActiveDocumentChanged);
			Env.Current.ScriptManager.NewScriptCreated -= new NewScriptCreatedHandler(OnOpenScript);

			//Create toolwindows
			projectContentView.Dispose();
			toolBoxView.Dispose();
			propertyBrowserView.Dispose();

			mruManager.Dispose();
		}

		#endregion
	}
}
