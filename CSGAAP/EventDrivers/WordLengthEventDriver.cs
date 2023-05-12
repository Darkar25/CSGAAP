using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class WordLengthEventDriver : NaiveWordEventDriver
    {
        public override string DisplayName => "Word Lengths";

        public override string ToolTipText => "Lengths of Word-Events";

        public override string LongDescription => "Lengths (in characters) of Word-Events";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(base.CreateEventSet(text).Select(x => new Event(new ReadOnlyMemory<char>(x.Data.Length.ToString().ToCharArray()), this)));
    }
}
