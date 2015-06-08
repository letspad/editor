using System.Windows.Input;

namespace Letspad.Commands
{
    public class ClipboardCutCommand : WrappedCommand
    {
        public ClipboardCutCommand()
        {
            Command = ApplicationCommands.Cut;
        }
    }
}