using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class WordLengthEventDriver : NaiveWordEventDriver
    {
        public override string DisplayName => "Word Lengths";

        public override string ToolTipText => "Lengths of Word-Events";

        public override string LongDescription => "Lengths (in characters) of Word-Events";

        public override EventSet CreateEventSet(string text) => new(base.CreateEventSet(text).Select(x => new Event(x.Data.Length.ToString(), this)));
    }
}
