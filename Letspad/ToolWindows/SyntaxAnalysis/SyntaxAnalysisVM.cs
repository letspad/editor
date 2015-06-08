using System.Collections.Generic;
using Common;
using Letspad.Documents;
using Roslyn.Compilers.CSharp;

namespace Letspad.ToolWindows.SyntaxAnalysis
{
    [AssignedView("Letspad.ToolWindows.SyntaxAnalysis.SyntaxAnalysis")]
    public class SyntaxAnalysisVM : TabbedElementVM, IDocumentChangeAware, IActiveDocumentChangeAware
    {
        private readonly MainWindowVM _mainWindowVM;
        private IList<SyntaxNodeVM> _syntaxNodes;

        public IList<SyntaxNodeVM> SyntaxNodes
        {
            get { return _syntaxNodes; }
            set { SetProperty(ref _syntaxNodes, value); }
        }

        public SyntaxAnalysisVM(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
            Title = "Syntax Analysis";
            DisableChangesTracking();
            
            var textDocumentVM = mainWindowVM.ActiveDocumentWindow as TextDocumentVM;
            
            if (textDocumentVM !=null)
            {
                RebuildSyntaxModel(textDocumentVM);
            }
        }

        public void OnDocumentChanged(TabbedElementVM document)
        {
            RebuildSyntaxModel(document);
        }

        public void OnActiveDocumentChanged(TabbedElementVM oldDocument, TabbedElementVM newDocument)
        {
            RebuildSyntaxModel(newDocument);
        }

        private void RebuildSyntaxModel(TabbedElementVM document)
        {
            var textDocumentVM = document as TextDocumentVM;

            if (textDocumentVM != null)
            {
                var content = textDocumentVM.Content;
                
                SyntaxTree syntaxTree = SyntaxTree.ParseText(content);
                var root = syntaxTree.GetRoot();
                SyntaxNodes = new List<SyntaxNodeVM>
                                  {
                                      new SyntaxNodeVM(root, textDocumentVM)
                                  };
            }
        }

        public void ShowProperties(object o)
        {
            if (_mainWindowVM.PropertyGrid != null)
            {
                _mainWindowVM.PropertyGrid.SelectedObject = o;
            }
        }
    }
}
