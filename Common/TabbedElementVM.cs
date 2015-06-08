using System.Windows.Input;
using System.Windows.Media;
using Common.Helpers;

namespace Common
{
    public class TabbedElementVM : BindableBase
    {
        private bool _disableChangesTracking;

        public ImageSource IconSource { get; set; }

        public string ContentId { get; set; }

        private bool _isVisible = true;

        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        private bool _isActive = true;

        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        private bool _isSelected = true;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private string _title;

        public string Title
        {
            get { return IsModified ? _title + "*" : _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isModified;

        public bool IsModified
        {
            get { return _isModified; }
            set
            {
                SetProperty(ref _isModified, value);
                OnPropertyChanged(ReflectionHelper.GetPropertyName(() => Title));
            }
        }

        protected void DisableDockAsDocument()
        {
            DockAsDocumentCommand = new RelayCommand(o => { }, o => false);
        }

        protected void DisableCloseCommand()
        {
            CloseCommand = new RelayCommand(o => { }, o => false);
        }

        protected override void OnObjectModified(string propertyName)
        {
            if (!_disableChangesTracking &&
                propertyName != ReflectionHelper.GetPropertyName(() => IsModified) &&
                propertyName != ReflectionHelper.GetPropertyName(() => Title) &&
                propertyName != ReflectionHelper.GetPropertyName(() => IsVisible) &&
                propertyName != ReflectionHelper.GetPropertyName(() => IsActive) &&
                propertyName != ReflectionHelper.GetPropertyName(() => IsSelected))
            {
                IsModified = true;
            }

            base.OnObjectModified(propertyName);
        }

        protected void DisableChangesTracking()
        {
            _disableChangesTracking = true;
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand DockAsDocumentCommand { get; set; }
    }
}