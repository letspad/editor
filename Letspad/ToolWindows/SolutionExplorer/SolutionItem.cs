using System.Collections.Generic;
using Roslyn.Services;

namespace Letspad.ToolWindows.SolutionExplorer
{
    public class SolutionItem
    {
        public string Title { get; set; }

        public IList<SolutionItem> ChildNodes { get; set; }

        public string FilePath { get; set; }

        public SolutionItem(IProject project)
        {
            Title = project.Name;
            ChildNodes = new List<SolutionItem>();

            foreach (var document in project.Documents)
            {
                ChildNodes.Add(new SolutionItem(document));
            }
        }

        private SolutionItem(IDocument document)
        {
            Title = document.FilePath + "\\" + document.Name;
            FilePath = document.FilePath;
        }
    }
}