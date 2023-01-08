using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class WordNGramEventDriver : NGramEventDriver
    {
        public override string DisplayName => "Word NGrams";

        public override string ToolTipText => "Groups of N Successive Words";

        public override string LongDescription => "Groups of N successive words (using sliding window)";

        public override EventSet CreateEventSet(string text) => TransformToNGram(new NaiveWordEventDriver().CreateEventSet(text));
    }
}
