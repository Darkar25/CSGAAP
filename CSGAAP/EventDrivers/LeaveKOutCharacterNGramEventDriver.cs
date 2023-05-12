using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class LeaveKOutCharacterNGramEventDriver : LeaveKOutNGramEventDriver
    {
        public override string DisplayName => "Leave K Out Character NGram";

        public override string ToolTipText => "Leave out all permutations k characters from a word gram of size n";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => TransformEventSet(new CharacterEventDriver().CreateEventSet(text));
    }
}
