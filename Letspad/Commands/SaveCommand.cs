using Common;

namespace Letspad.Commands
{
    public class SaveCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public SaveCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var supportSaveAs = _mainWindowVM.ActiveDocumentWindow as ISupportSave;
            if (supportSaveAs != null)
            {
                supportSaveAs.Save();
            }
        }
    }
}