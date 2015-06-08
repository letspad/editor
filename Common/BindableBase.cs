using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        protected void SetProperty<T>(ref T variable, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(variable, value))
            {
                variable = value;
                OnPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

            OnObjectModified(propertyName);
        }

        protected virtual void OnObjectModified(string propertyName)
        {
        }
    }
}