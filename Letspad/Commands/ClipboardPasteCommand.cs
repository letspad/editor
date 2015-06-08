using System.Windows.Input;

namespace Letspad.Commands
{
    public class ClipboardPasteCommand : WrappedCommand
    {
        public ClipboardPasteCommand()
        {
            Command = ApplicationCommands.Paste;
        }
    }
}