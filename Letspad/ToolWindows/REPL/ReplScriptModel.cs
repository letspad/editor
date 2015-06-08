using System;
using System.Linq;
using Letspad.Commands;
using Letspad.Documents;

namespace Letspad.ToolWindows.REPL
{
    public class ReplScriptModel
    {
        private readonly MainWindowVM _mainWindowVM;

        public ReplScriptModel(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public void AppendToCurrentDocument(string s)
        {
            var textDocumentVM = _mainWindowVM.ActiveDocumentWindow as TextDocumentVM;
            if (textDocumentVM != null)
            {
                textDocumentVM.TextDocument.Text += s;
            }
        }

        public void OpenDocument(string fileName)
        {
            new OpenDocumentCommand(_mainWindowVM).Execute(fileName);
        }

        public void FilterCurrentDocument(Func<string, string> filter)
        {
            var textDocumentVM = _mainWindowVM.ActiveDocumentWindow as TextDocumentVM;
            if (textDocumentVM != null)
            {
                string documentText = textDocumentVM.TextDocument.Text;
                string[] documentLines = documentText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                var filteredLines = documentLines.Select(filter);
                textDocumentVM.TextDocument.Text = string.Join(Environment.NewLine, filteredLines);
            }
        }
    }
}