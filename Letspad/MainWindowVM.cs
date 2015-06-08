using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Common;
using Letspad.Commands;
using Letspad.ToolWindows.PropertyGrid;

namespace Letspad
{
    public class MainWindowVM : BindableBase
    {
        private string _title;
        
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private TabbedElementVM _activeWindow;
        private TabbedElementVM _activeToolWindow;
        private TabbedElementVM _activeDocumentWindow;

        public TabbedElementVM ActiveWindow
        {
            get { return _activeWindow; }
            set
            {
                SetProperty(ref _activeWindow, value);

                if (Documents.Contains(value))
                {
                    ActiveDocumentWindow = value;
                }

                if (ToolWindows.Contains(value))
                {
                    ActiveToolWindow = value;
                }
            }
        }

        public TabbedElementVM ActiveToolWindow
        {
            get { return _activeToolWindow; }
            set
            {
                SetProperty(ref _activeToolWindow, value);
            }
        }

        public TabbedElementVM ActiveDocumentWindow
        {
            get { return _activeDocumentWindow; }
            set
            {
                var oldDocument = _activeDocumentWindow;
                
                SetProperty(ref _activeDocumentWindow, value);
                
                if (oldDocument != value)
                {
                    RaiseEvent<IActiveDocumentChangeAware>(i => i.OnActiveDocumentChanged(oldDocument, value));
                }
            }
        }

        public ObservableCollection<TabbedElementVM> ToolWindows { get; private set; }

        public ObservableCollection<TabbedElementVM> Documents { get; private set; }

        public ICommand OpenDocumentCommand { get; private set; }

        public ICommand SaveDocumentCommand { get; private set; }

        public ICommand SaveAsCommand { get; private set; }

        public ICommand NewDocumentCommand { get; private set; }

        public ICommand ClipboardPasteCommand { get; private set; }

        public ICommand ClipboardCutCommand { get; private set; }

        public ICommand ClipboardCopyCommand { get; private set; }

        public ICommand DocumentCloseCommand { get; private set; }

        public ICommand ExitApplicationCommand { get; private set; }

        public ICommand ToggleSyntaxAnalysisCommand { get; private set; }

        public ICommand ZoomInCommand { get; private set; }

        public ICommand ZoomOutCommand { get; private set; }

        public ICommand ToggleReplCommand { get; private set; }

        public ICommand SemanticAnalysisDemoCommand { get; private set; }

        public ICommand TogglePropertyGrid { get; private set; }

        public PropertyGridVM PropertyGrid { get; set; }

        public MainWindowVM()
        {
            ToolWindows = new ObservableCollection<TabbedElementVM>();
            Documents = new ObservableCollection<TabbedElementVM>();

            NewDocumentCommand = new NewDocumentCommand(this);
            OpenDocumentCommand = new OpenDocumentCommand(this);
            SaveAsCommand = new SaveAsCommand(this);
            SaveDocumentCommand = new SaveCommand(this);
            DocumentCloseCommand = new DocumentCloseCommand(this);

            ExitApplicationCommand = new ExitApplicationCommand();

            ClipboardPasteCommand = new ClipboardPasteCommand();
            ClipboardCutCommand = new ClipboardCutCommand();
            ClipboardCopyCommand = new ClipboardCopyCommand();

            ToggleSyntaxAnalysisCommand = new ToggleSyntaxAnalysisCommand(this);
            TogglePropertyGrid = new TogglePropertyGridCommand(this);
            ToggleReplCommand = new ToggleReplCommand(this);
            SemanticAnalysisDemoCommand = new SemanticModelDemoCommand(this);

            ZoomInCommand = new ZoomInCommand(this);
            ZoomOutCommand = new ZoomOutCommand(this);
        }

        public void RaiseEvent<T> (Action<T> caller)
        {
            foreach (var document in Documents.OfType<T>())
            {
                caller(document);
            }
            
            foreach (var toolWindow in ToolWindows.OfType<T>())
            {
                caller(toolWindow);
            }
        }
    }
}