using System.Windows;
using Common;

namespace Letspad.Commands
{
    public class ExitApplicationCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}