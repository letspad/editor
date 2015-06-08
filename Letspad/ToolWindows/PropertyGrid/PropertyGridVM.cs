using Common;

namespace Letspad.ToolWindows.PropertyGrid
{
    [AssignedView("Letspad.ToolWindows.PropertyGrid.PropertyGrid")]
    public class PropertyGridVM : TabbedElementVM
    {
        private object _selectedObject;

        public object SelectedObject
        {
            get { return _selectedObject; }
            set { SetProperty(ref _selectedObject, value);}
        }

        public PropertyGridVM()
        {
            DisableChangesTracking();
            Title = "Property Grid";
        }
    }
}
