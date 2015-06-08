using System;
using System.Windows.Input;

namespace Common
{
    public abstract class CommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter);

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;

        protected void OnCanExecuteChanged(EventArgs e)
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}