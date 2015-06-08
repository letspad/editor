using System;
using System.IO;
using System.Reflection;
using System.Text;
using Common;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Letspad.Documents
{
    [AssignedView("Letspad.Documents.TextDocumentView")]
    public class TextDocumentVM : TabbedElementVM, ISupportSave
    {
        private readonly MainWindowVM _mainWindowVM;
        public string FullFileName { get; set; }
        private TextDocument _textDocument;
        private Encoding _encoding;
        private IHighlightingDefinition _syntaxtHighlighting;

        public TextDocument TextDocument
        {
            get { return _textDocument; }
            set { SetProperty(ref _textDocument, value); }
        }

        public Encoding Encoding
        {
            get { return _encoding; }
            set { SetProperty(ref _encoding, value); }
        }

        public IHighlightingDefinition SyntaxHighlighting
        {
            get { return _syntaxtHighlighting; }
            set { SetProperty(ref _syntaxtHighlighting, value); }
        }

        public string Content 
        { 
            get { return TextDocument.Text; } 
        }

        public TextEditor TextEditor { get; set; }

        public TextDocumentVM(MainWindowVM mainWindowVM, string fullfileName, string content)
        {
            _mainWindowVM = mainWindowVM;
            TextDocument = new TextDocument(content);
            FullFileName = fullfileName;
            Title = Path.GetFileName(fullfileName);

            SetDefaultSyntaxHighlighting(FullFileName);
            IsModified = false;
            Encoding = Encoding.UTF8;
        }

        public TextDocumentVM(MainWindowVM mainWindowVM, string fullfileName)
        {
            _mainWindowVM = mainWindowVM;
            FullFileName = fullfileName;
            Title = Path.GetFileName(fullfileName);

            using (var r = new StreamReader(fullfileName, detectEncodingFromByteOrderMarks: true))
            {
                Encoding = r.CurrentEncoding;
                var fileContent = r.ReadToEnd();
                TextDocument = new TextDocument(fileContent);
            }

            SetDefaultSyntaxHighlighting(FullFileName);
            IsModified = false;
        }

        private void SetDefaultSyntaxHighlighting(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (!string.IsNullOrEmpty(extension))
            {
                extension = extension.ToLower();
            }

            string script = GetScriptForExtension(extension);

            if (script != null)
            {
                
            }

            switch (extension)
            {
                case ".cs":
                case ".csx":
                    SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
                    break;
                case ".xml":
                    SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("XML");
                    break;
            }
        }

        private string GetScriptForExtension(string extension)
        {
            var entryAssemblyLocation = Assembly.GetEntryAssembly().Location;
            var path = Path.GetDirectoryName(entryAssemblyLocation);

            if (path == null)
            {
                throw new InvalidOperationException();
            }

            var scriptFileName = extension.TrimStart('.') + ".csx";

            var scriptPath = Path.Combine(path, "Configuration", scriptFileName);

            if (File.Exists(scriptPath))
            {
                return scriptPath;
            }

            return null;
        }

        public void SaveAs(string fileName)
        {
            File.WriteAllText(fileName, TextDocument.Text, Encoding);
        }

        public void Save()
        {
            File.WriteAllText(FullFileName, TextDocument.Text, Encoding);
        }

        public void RaiseDocumentChangedEvent()
        {
            _mainWindowVM.RaiseEvent<IDocumentChangeAware>(i=>i.OnDocumentChanged(this));
        }
    }
}

