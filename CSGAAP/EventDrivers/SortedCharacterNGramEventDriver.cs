using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class SortedCharacterNGramEventDriver : SortedNGramEventDriver
    {
        public override string DisplayName => "Sorted Character NGram";

        public override string ToolTipText => "The characters in each ngram are alphabetically sorted";

        public override EventSet CreateEventSet(string text) => TransformToNGram(new CharacterEventDriver().CreateEventSet(text));
    }
}
