using Common;
using Letspad.Documents;
using Letspad.ToolWindows.SolutionExplorer;
using Microsoft.Win32;

namespace Letspad.Commands
{
    public class OpenDocumentCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public OpenDocumentCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var filePath = parameter as string;

            if (filePath != null)
            {
                OpenSingleFile(filePath);
            }
            else
            {
                var openDialog = new OpenFileDialog {Multiselect = true};

                if (openDialog.ShowDialog().Value)
                {
                    foreach (var fileName in openDialog.FileNames)
                    {
                        OpenSingleFile(fileName);
                    }
                }
            }
        }

        private void OpenSingleFile(string fileName)
        {
            //DEMO 5
            if (fileName.EndsWith(".sln"))
            {
                _mainWindowVM.ToolWindows.Add(new SolutionExplorerVM(fileName, _mainWindowVM));
            }
            else
            {
                var textDocumentVM = new TextDocumentVM(_mainWindowVM, fileName);
                _mainWindowVM.Documents.Add(textDocumentVM);
            }
        }
    }
}
