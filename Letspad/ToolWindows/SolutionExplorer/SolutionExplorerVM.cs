using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Letspad.Documents;
using Roslyn.Services;

namespace Letspad.ToolWindows.SolutionExplorer
{
    [AssignedView("Letspad.ToolWindows.SolutionExplorer.SolutionExplorer")]
    public class SolutionExplorerVM : TabbedElementVM
    {
        private readonly MainWindowVM _mainWindowVM;
        private IList<SolutionItem> _solutionItems;

        public IList<SolutionItem> SolutionItems
        {
            get { return _solutionItems; }
            set { SetProperty(ref _solutionItems, value); }
        }

        public SolutionExplorerVM(string solutionFile, MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
            Title = "Solution Explorer";
            DisableChangesTracking();
            //init model

            SolutionItems = new List<SolutionItem>();

            var workspace = Workspace.LoadSolution(solutionFile);

            foreach (var project in workspace.CurrentSolution.Projects)
            {
                SolutionItems.Add(new SolutionItem(project));
            }

        }

        public void OpenSolutionItem(SolutionItem solutionItem)
        {
            var filePath = solutionItem.FilePath;

            if (filePath!=null && File.Exists(filePath))
            {
                _mainWindowVM.Documents.Add(new TextDocumentVM(_mainWindowVM, filePath));
            }
        }
    }
}
