using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class PunctuationNGramEventDriver : NGramEventDriver
    {
        public override string DisplayName => "Punctuation NGrams";

        public override string ToolTipText => "Sliding windows of punctuation.";

        public override EventSet CreateEventSet(string text) => TransformToNGram(new PunctuationEventDriver().CreateEventSet(text));
    }
}
