using System.Windows.Input;
using Common;

namespace Letspad.Commands
{
    public class WrappedCommand : CommandBase
    {
        protected ICommand Command;

        public WrappedCommand()
        {
            Command = ApplicationCommands.Paste;
            Command.CanExecuteChanged += WrappedCommandCanExecuteChanged;
        }

        private void WrappedCommandCanExecuteChanged(object sender, System.EventArgs e)
        {
            OnCanExecuteChanged(e);
        }

        public override bool CanExecute(object parameter)
        {
            return Command.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Command.Execute(parameter);
        }
    }
}