using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventDrivers
{
    public class PunctuationEventDriver : EventDriver
    {
        public override string DisplayName => "Punctuation";
        public override string ToolTipText => "Non alphanumeric nor whitspace";

        public override EventSet CreateEventSet(string text) => new(text.ToStructEnumerable().Where(x => !(char.IsLetterOrDigit(x) || char.IsNumber(x)), x => x).Select(x => new Event(x.ToString(), this), x => x).ToEnumerable());
    }
}
