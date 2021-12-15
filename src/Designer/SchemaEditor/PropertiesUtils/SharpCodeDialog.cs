using FreeSCADA.Common;
using FreeSCADA.Designer.Dialogs;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Indentation.CSharp;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace FreeSCADA.Designer.SchemaEditor.PropertiesUtils
{
    public partial class SharpCodeDialog : Form
    {
        public string expression;
        private ICSharpCode.AvalonEdit.TextEditor textedior;
        private SearchPanel searchpanel;
        private AppCompletionDatas appcomdata;
        private CompletionWindow completionWindow;

        public SharpCodeDialog(string expression)
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.expression = expression;
            this.textedior = new TextEditor();
            this.textedior.ShowLineNumbers = true;
            this.textedior.WordWrap = true;
            this.textedior.FontFamily = new System.Windows.Media.FontFamily("Arial");
            this.textedior.FontSize = 12.0;
            this.textedior.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
            this.textedior.TextArea.TextEntering += new TextCompositionEventHandler(this.textEditor_TextArea_TextEntering);
            this.textedior.TextArea.TextEntered += new TextCompositionEventHandler(this.textEditor_TextArea_TextEntered);
            this.textedior.KeyDown += new System.Windows.Input.KeyEventHandler(this.textedior_KeyDown);
            this.elementHost1.Child = this.textedior;
            this.textedior.TextArea.IndentationStrategy = new CSharpIndentationStrategy(this.textedior.Options);
            this.searchpanel = new SearchPanel();
            this.searchpanel.Attach(this.textedior.TextArea);
            this.textedior.TextChanged += new System.EventHandler(this.textedior_TextChanged);
            this.textedior.Text = expression;
        }

        private void tsSaveClose_Click(object sender, EventArgs e)
        {
            this.expression = textedior.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void tsCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textedior_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F && (Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) != System.Windows.Input.ModifierKeys.None && this.searchpanel.IsClosed)
            {
                this.searchpanel.Open();
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textedior_TextChanged(object sender, System.EventArgs e)
        {
            //this.IsModified = true;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && this.completionWindow != null && !char.IsLetterOrDigit(e.Text[0]))
            {
                this.completionWindow.CompletionList.RequestInsertion(e);
            }
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {/*
            if (e.Text == eYgJk0MPml23SOq7Fh.eyj01t0av(2114))
            {
                int offset = this.textedior.TextArea.Caret.Offset;
                DocumentLine lineByOffset = this.textedior.Document.GetLineByOffset(offset);
                //Trace.WriteLine(eYgJk0MPml23SOq7Fh.eyj01t0av(2120) + this.textedior.Document.GetText(lineByOffset.Offset, offset - lineByOffset.Offset));
                this.completionWindow = new CompletionWindow(this.textedior.TextArea);
                System.Collections.Generic.IList<ICompletionData> completionData = this.completionWindow.CompletionList.CompletionData;
                MyCompletionData[] list = this.appcomdata.List;
                for (int i = 0; i < list.Length; i++)
                {
                    MyCompletionData item = list[i];
                    completionData.Add(item);
                }
                this.completionWindow.Show();
                this.completionWindow.Closed += delegate
                {
                    this.completionWindow = null;
                };
            }*/
        }

        private void tsCompile_Click(object sender, EventArgs e)
        {
            Env.Current.ScriptManager.ScriptHost.SourceText = this.textedior.Text;
            //if (!Env.Current.ScriptManager.ScriptHost.IsCompiled)
            {
                this.textBox1.Text = "";
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                if (Env.Current.ScriptManager.ScriptHost.Compile(true))
                {
                    string text = stopwatch.ElapsedMilliseconds.ToString();
                    this.textBox1.Text = string.Concat(new string[]
					{
						"[",
						System.DateTime.Now.ToString(),
						"] 编译成功，耗时",
						text,
						"ms"
					});
                }
                else
                {
                    this.textBox1.Text = "[" + System.DateTime.Now.ToString() + "] 编译失败.\r\n";
                }
            }
            System.Windows.Forms.TextBox expr_12C = this.textBox1;
            expr_12C.Text += Env.Current.ScriptManager.ScriptHost.CompilerInfo;
        }

        private void tsImport_Click(object sender, EventArgs e)
        {
            ReferanceForm referanceForm = new ReferanceForm();
            string[] references = Env.Current.ScriptManager.ScriptHost.References;
            referanceForm.liblistBox.Items.AddRange(references);
            if (referanceForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string[] array = new string[referanceForm.liblistBox.Items.Count];
                referanceForm.liblistBox.Items.CopyTo(array, 0);
                Env.Current.ScriptManager.ScriptHost.References = array;
                //this.IsModified = true;
            }
        }
    }
}
