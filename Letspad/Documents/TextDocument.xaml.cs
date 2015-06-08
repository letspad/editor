using System;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Folding;

namespace Letspad.Documents
{
    public partial class TextDocumentView
    {
        private FoldingManager _foldingManager;
        private AbstractFoldingStrategy _foldingStrategy;
        private bool _changeEventRaised;

        protected TextDocumentVM Model
        {
            get { return (TextDocumentVM) DataContext; }
        }

        public TextDocumentView()
        {
            InitializeComponent();
            TextEditor.DocumentChanged += TextEditorDocumentChanged;
        }

        void DocumentContentChanged(object sender, ICSharpCode.AvalonEdit.Document.DocumentChangeEventArgs e)
        {
            _changeEventRaised = false;
        }

        void TextEditorDocumentChanged(object sender, EventArgs e)
        {
            Model.TextEditor = TextEditor;

            if (_foldingStrategy != null)
            {
                return;
            }

            _foldingManager = FoldingManager.Install(TextEditor.TextArea);
            _foldingStrategy = new XmlFoldingStrategy();
            _foldingStrategy.UpdateFoldings(_foldingManager, TextEditor.Document);
            var foldingUpdateTimer = new DispatcherTimer
                                         {
                                             Interval = TimeSpan.FromSeconds(2)
                                         };
            foldingUpdateTimer.Tick += DocumentTimerTick;
            foldingUpdateTimer.Start();
        }

        void DocumentTimerTick(object sender, EventArgs e)
        {
            if (TextEditor.Document != null)
            {
                TextEditor.Document.Changed += DocumentContentChanged;                
            }

            if (!_changeEventRaised)
            {
                Model.RaiseDocumentChangedEvent();
                _changeEventRaised = true;
            }

            if (_foldingStrategy != null)
            {
                _foldingStrategy.UpdateFoldings(_foldingManager, TextEditor.Document);
            }            
        }
    }
}
