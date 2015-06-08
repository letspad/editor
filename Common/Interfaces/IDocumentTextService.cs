namespace Common.Interfaces
{
    public interface IDocumentTextService
    {
        string GetText();
        void SetText(string text);
        bool IsTextDocumentActive();
    }
}