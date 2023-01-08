using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class LeaveKOutWordNGramEventDriver : LeaveKOutNGramEventDriver
    {
        public override string DisplayName => "Leave K Out Word NGram";

        public override string ToolTipText => "Leave out all permutations k words from a word gram of size n";

        public override EventSet CreateEventSet(string text) => TransformEventSet(new NaiveWordEventDriver().CreateEventSet(text));
    }
}
