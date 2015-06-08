using Common;
using Microsoft.Win32;
using Roslyn.Compilers.CSharp;

namespace Letspad.Commands
{
    public class SaveAsCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public SaveAsCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var saveDialog = new SaveFileDialog();

            var supportSaveAs = _mainWindowVM.ActiveDocumentWindow as ISupportSave;

            if (supportSaveAs != null && saveDialog.ShowDialog().Value)
            {
                supportSaveAs.SaveAs(saveDialog.FileName);
            }
        }
    }
}