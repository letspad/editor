#r "System.Core"
#r "System.Linq"
#r "Roslyn.Compilers.dll"
#r "Roslyn.Compilers.CSharp.dll"

using Roslyn.Compilers.CSharp;
using Common.Interfaces;
using System.Linq;

//DEMO 3
void sortUsingExecute() 
{
	var documentTextService = GetService<IDocumentTextService>();
	var content = documentTextService.GetText();

    var syntaxTree = SyntaxTree.ParseText(content);
	var root = syntaxTree.GetRoot();

    var usingDirectiveSyntaxes = new SyntaxList<UsingDirectiveSyntax>();
    foreach (var source in root.Usings.OrderBy(u => u.ToFullString()))
    {
		usingDirectiveSyntaxes = usingDirectiveSyntaxes.Add(source);
    }

    var newRoot = root.Update(root.Externs, usingDirectiveSyntaxes, root.AttributeLists, root.Members,root.EndOfFileToken);
	var output = newRoot.ToFullString();
	documentTextService.SetText(output);
}

bool sortUsingIsActive() 
{
	var documentTextService = GetService<IDocumentTextService>();
	return documentTextService.IsTextDocumentActive();
}

var ribbonService = GetService<IRibbonService>();
ribbonService.RegisterRibbonCommand("C#", "Refactoring", "Sort Using", sortUsingExecute, sortUsingIsActive);