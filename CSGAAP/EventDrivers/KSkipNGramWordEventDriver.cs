using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class KSkipNGramWordEventDriver : KSkipNGramEventDriver
    {
        public override string DisplayName => "K Skip N Word Gram";

        public override string ToolTipText => "Generate word grams with N words with K words skipped between each word";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => TransformToKSkipNGram(new NaiveWordEventDriver().CreateEventSet(text));
    }
}
