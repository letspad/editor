using Common;
using Letspad.Documents;

namespace Letspad.Commands
{
    public class ZoomInCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public ZoomInCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var textDocumentVM = _mainWindowVM.ActiveDocumentWindow as TextDocumentVM;

            if (textDocumentVM != null && textDocumentVM.TextEditor.FontSize<48)
            {
                textDocumentVM.TextEditor.FontSize += 1;
            }
        }
    }
}