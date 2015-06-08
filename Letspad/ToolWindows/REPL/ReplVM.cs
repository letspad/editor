using System;
using System.Windows.Input;
using Common;
using ICSharpCode.AvalonEdit.Document;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;

namespace Letspad.ToolWindows.REPL
{
    [AssignedView("Letspad.ToolWindows.REPL.Repl")]
    public class ReplVM : TabbedElementVM
    {
        private readonly MainWindowVM _mainWindowVM;
        private string _output;
        private string _commandText;
        private ScriptEngine _scriptEngine;
        private Session _scriptSession;

        public string Output
        {
            get { return _output; }
            set { SetProperty(ref _output, value); }
        }

        public string CommandText
        {
            get { return _commandText; }
            set { SetProperty(ref _commandText, value); }
        }

        public ICommand ExecuteCommand { get; private set; }

        public ICommand ClearCommand { get; private set; }

        public TextDocument CommandHistory { get; private set; }

        public ReplVM(MainWindowVM mainWindowVM)
        {
            _mainWindowVM = mainWindowVM;
            DisableChangesTracking();

            CommandHistory = new TextDocument();
            Title = "Commands";
            CommandText = string.Empty;
            CommandHistory.Text = string.Empty;

            //DEMO 6

            ExecuteCommand = new RelayCommand(OnExecuteCommand);
            ClearCommand = new RelayCommand(OnClearCommand);

            InitializeScripEngine();
        }

        private void OnClearCommand(object obj)
        {
            CommandHistory.Text = string.Empty;
        }

        private void InitializeScripEngine()
        {
            _scriptEngine = new ScriptEngine();
            _scriptEngine.AddReference("System");
            _scriptEngine.AddReference("System.Core");
            _scriptEngine.AddReference("Common.dll");
            _scriptEngine.AddReference(GetType().Assembly.Location);
            var replScriptModel = new ReplScriptModel(_mainWindowVM);
            _scriptSession = _scriptEngine.CreateSession(replScriptModel);            
        }

        private void OnExecuteCommand(object o)
        {
            try
            {
                var result = _scriptSession.Execute(CommandText);
                CommandHistory.Text += CommandText + Environment.NewLine;
                CommandText = string.Empty;

                if (result != null)
                {
                    CommandHistory.Text += "//Result: " + result + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                CommandHistory.Text += "//Exception: " + Environment.NewLine + ex + Environment.NewLine;
            }
        }
    }
}
