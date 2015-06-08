using System.IO;
using System.Windows;
using Common;
using Common.Interfaces;
using Letspad.Services;
using Roslyn.Scripting.CSharp;

namespace Letspad
{
    public partial class MainWindowView
    {
        private readonly MainWindowVM _mainWindowVM = new MainWindowVM();

        public MainWindowView()
        {
            InitializeComponent();
            DataContext = _mainWindowVM;

            ExecuteScript("StartUp.csx");
        }

        private void ExecuteScript(string scriptFileName)
        {
            string startupConfig = File.ReadAllText(scriptFileName);

            var scriptEngine = new ScriptEngine();
            scriptEngine.AddReference("System");
            scriptEngine.AddReference("Common.dll");
            scriptEngine.AddReference(GetType().Assembly.Location);
            
            var scriptHostModel = CreateScriptHostModel();
            var session = scriptEngine.CreateSession(scriptHostModel);
            session.Execute(startupConfig);
        }

        private ScriptHostModel CreateScriptHostModel()
        {
            var scriptHostModel = new ScriptHostModel();
            scriptHostModel.Register<IRibbonService>(new RibbonService(Ribbon));
            scriptHostModel.Register<IDocumentTextService>(new DocumentTextService(_mainWindowVM));
            return scriptHostModel;
        }
    }
}