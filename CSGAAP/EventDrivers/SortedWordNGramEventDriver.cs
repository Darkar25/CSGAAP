using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class SortedWordNGramEventDriver : SortedNGramEventDriver
    {
        public override string DisplayName => "Sorted Word NGram";

        public override string ToolTipText => "The words in each ngram are alphabetically sorted";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => TransformToNGram(new NaiveWordEventDriver().CreateEventSet(text));
    }
}
