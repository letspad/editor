using System.Windows.Input;

namespace Letspad.Commands
{
    public class ClipboardCopyCommand : WrappedCommand
    {
        public ClipboardCopyCommand()
        {
            Command = ApplicationCommands.Copy;
        }
    }
}