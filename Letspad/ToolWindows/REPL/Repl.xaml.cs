using System.Windows.Input;

namespace Letspad.ToolWindows.REPL
{
    public partial class Repl
    {
        protected ReplVM Model
        {
            get
            {
                return (ReplVM) DataContext;
            }
        }

        public Repl()
        {
            InitializeComponent();            
        }

        private void OnCommandTextBoxKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Model.ExecuteCommand.Execute(null);
            }
        }
    }
}
