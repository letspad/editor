using Common;
using ICSharpCode.AvalonEdit.CodeCompletion;
using Letspad.Documents;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace Letspad.Commands
{
    public class SemanticModelDemoCommand : CommandBase
    {
        private readonly MainWindowVM _mainWindowVM;

        public SemanticModelDemoCommand(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        //DEMO 4
        public override void Execute(object parameter)
        {
            var textDocumentVM = _mainWindowVM.ActiveDocumentWindow as TextDocumentVM;

            if (textDocumentVM != null)
            {
                var content = textDocumentVM.TextDocument.Text;
                var position = textDocumentVM.TextEditor.SelectionStart;

                var syntaxTree = SyntaxTree.ParseText(content);
                var assemblyReference = MetadataReference.CreateAssemblyReference("System");
                var assemblyReference2 = MetadataReference.CreateAssemblyReference("System.Core");
                var compilation = Compilation.Create("SemanticModelDemo").AddReferences(assemblyReference, assemblyReference2).AddSyntaxTrees(syntaxTree);
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var lookupSymbols = semanticModel.LookupSymbols(position);

                var completionWindow = new CompletionWindow(textDocumentVM.TextEditor.TextArea);

                foreach (var lookupSymbol in lookupSymbols)
                {
                    completionWindow.CompletionList.CompletionData.Add(new ComplitionItem(lookupSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat), lookupSymbol.Name));
                }

                completionWindow.Show();
            }
        }
    }
}