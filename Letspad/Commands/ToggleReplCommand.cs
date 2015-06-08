using System.Linq;
using Common;
using Letspad.ToolWindows.REPL;

namespace Letspad.Commands
{
    public class ToggleReplCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public ToggleReplCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (_mainWindowVM.ToolWindows.All(t => !(t is ReplVM)))
            {
                _mainWindowVM.ToolWindows.Add(new ReplVM(_mainWindowVM));
            }
        }
    }
}