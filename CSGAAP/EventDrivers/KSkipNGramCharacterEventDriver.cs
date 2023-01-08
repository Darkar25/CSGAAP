using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class KSkipNGramCharacterEventDriver : KSkipNGramEventDriver
    {
        public override string DisplayName => "K Skip N Character Gram";

        public override string ToolTipText => "Generate character grams with N characters with K characters skipped between each character";

        public override EventSet CreateEventSet(string text) => TransformToKSkipNGram(new CharacterEventDriver().CreateEventSet(text));
    }
}
