namespace Common
{
    public interface IDocumentChangeAware
    {
        void OnDocumentChanged(TabbedElementVM document);
    }

    public interface IActiveDocumentChangeAware
    {
        void OnActiveDocumentChanged(TabbedElementVM oldDocument, TabbedElementVM newDocument);
    }
}
