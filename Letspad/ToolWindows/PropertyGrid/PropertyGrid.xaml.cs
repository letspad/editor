using System.Windows.Input;
using Letspad.ToolWindows.PropertyGrid;

namespace Letspad.ToolWindows.PropertyGrid
{
    public partial class PropertyGrid
    {
        protected PropertyGridVM Model
        {
            get
            {
                return (PropertyGridVM) DataContext;
            }
        }

        public PropertyGrid()
        {
            InitializeComponent();            
        }
    }
}
