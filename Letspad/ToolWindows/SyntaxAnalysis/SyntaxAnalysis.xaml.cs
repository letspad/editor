namespace Letspad.ToolWindows.SyntaxAnalysis
{
    public partial class SyntaxAnalysis
    {
        protected SyntaxAnalysisVM Model
        {
            get { return (SyntaxAnalysisVM) DataContext; }
        }

        public SyntaxAnalysis()
        {
            InitializeComponent();
        }

        private void SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            var syntaxNodeVM = TreeView.SelectedItem as SyntaxNodeVM;

            if (syntaxNodeVM != null && TreeView.IsKeyboardFocusWithin)
            {
                syntaxNodeVM.Highlight();
                Model.ShowProperties(syntaxNodeVM.OriginalNode);
            }
        }
    }
}
