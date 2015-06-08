using System;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace Letspad.Commands
{
    public class ComplitionItem : ICompletionData
    {
        public ImageSource Image { get; private set; }

        public string Text { get; private set; }

        public object Content { get; private set; }

        public object Description { get; private set; }

        public double Priority { get; private set; }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {            
        }

        public ComplitionItem(string text, string content)
        {
            Text = text;
            Content = content;
        }
    }
}