using Common.Interfaces;
using Letspad.Documents;

namespace Letspad.Services
{
    public class DocumentTextService : IDocumentTextService
    {
        private readonly MainWindowVM _mainWindowVM;

        public DocumentTextService(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public bool IsTextDocumentActive()
        {
            var textDocumentVM = _mainWindowVM.ActiveDocumentWindow as TextDocumentVM;
            return textDocumentVM != null;
        }

        public string GetText()
        {
            var textDocumentVM = (TextDocumentVM)_mainWindowVM.ActiveDocumentWindow;
            return textDocumentVM.TextEditor.Text;
        }

        public void SetText(string text)
        {
            var textDocumentVM = (TextDocumentVM)_mainWindowVM.ActiveDocumentWindow;
            textDocumentVM.TextEditor.Text = text;            
        }
    }
}