using FreeSCADA.Interfaces;
using System;
using System.Windows.Controls;
using System.Windows.Input;
namespace FreeSCADA.Common.Schema
{
    public class myHelpScrollViewer : ScrollViewer
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down) && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.None && Env.Current.Mode == EnvironmentMode.Designer)
            {
                e.Handled = false;
                return;
            }
            base.OnKeyDown(e);
        }
    }
}