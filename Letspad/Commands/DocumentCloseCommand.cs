using Common;

namespace Letspad.Commands
{
    public class DocumentCloseCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public DocumentCloseCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (_mainWindowVM.ActiveDocumentWindow != null)
            {
                _mainWindowVM.Documents.Remove(_mainWindowVM.ActiveDocumentWindow);
            }
        }
    }
}