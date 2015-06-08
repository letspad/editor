using System.Windows.Input;

namespace Letspad.ToolWindows.SolutionExplorer
{
    public partial class SolutionExplorer
    {
        public SolutionExplorerVM Model
        {
            get
            {
                return (SolutionExplorerVM) DataContext;
            }
        }

        public SolutionExplorer()
        {
            InitializeComponent();
        }

        private void Clicked(object sender, MouseButtonEventArgs e)
        {
            var solutionItem = TreeView.SelectedItem as SolutionItem;
            Model.OpenSolutionItem(solutionItem);
        }
    }
}
