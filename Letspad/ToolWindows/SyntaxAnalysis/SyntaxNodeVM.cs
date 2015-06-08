using System.Collections.Generic;
using Letspad.Documents;
using Roslyn.Compilers.CSharp;

namespace Letspad.ToolWindows.SyntaxAnalysis
{
    public class SyntaxNodeVM
    {
        private readonly TextDocumentVM _textDocumentVM;
        private readonly Location _location;
        public string Title { get; set; }

        public IList<SyntaxNodeVM> ChildNodes { get; set; }

        public object OriginalNode { get; set; }

        public SyntaxNodeVM(SyntaxNode syntaxNode, TextDocumentVM textDocumentVM)
        {
            OriginalNode = syntaxNode;
            _location = syntaxNode.GetLocation();
            _textDocumentVM = textDocumentVM;
            Title = string.Format("{0} {1}", syntaxNode.Kind.ToString(), _location);

            ChildNodes = new List<SyntaxNodeVM>();

            foreach (var childNode in syntaxNode.ChildNodes())
            {
                ChildNodes.Add(new SyntaxNodeVM(childNode, textDocumentVM));
            }
        }

        public void Highlight()
        {
            var sourceSpan = _location.SourceSpan;
            _textDocumentVM.TextEditor.Select(sourceSpan.Start, sourceSpan.Length);
        }
    }
}