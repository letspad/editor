using System.Linq;
using Common;
using Letspad.ToolWindows;
using Letspad.ToolWindows.SyntaxAnalysis;

namespace Letspad.Commands
{
    public class ToggleSyntaxAnalysisCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public ToggleSyntaxAnalysisCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (_mainWindowVM.ToolWindows.All(t => !(t is SyntaxAnalysisVM)))
            {
                _mainWindowVM.ToolWindows.Add(new SyntaxAnalysisVM(_mainWindowVM));
            }
        }
    }
}