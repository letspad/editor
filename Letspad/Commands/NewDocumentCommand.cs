using Common;
using Letspad.Documents;

namespace Letspad.Commands
{
    public class NewDocumentCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public NewDocumentCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var textDocumentVM = new TextDocumentVM(_mainWindowVM, "New Document.xml", "<xml>Welcome to Letspad</xml>");
            _mainWindowVM.Documents.Add(textDocumentVM);
        }
    }
}