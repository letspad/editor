using System.Linq;
using Common;
using Letspad.ToolWindows.PropertyGrid;

namespace Letspad.Commands
{
    public class TogglePropertyGridCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public TogglePropertyGridCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (_mainWindowVM.ToolWindows.All(t => !(t is PropertyGridVM)))
            {
                _mainWindowVM.PropertyGrid = new PropertyGridVM();
                _mainWindowVM.ToolWindows.Add(_mainWindowVM.PropertyGrid);
            }
        }
    }
}