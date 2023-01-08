using CSGAAP.Generics;
using CSGAAP.Util;
using System.Text.RegularExpressions;

namespace CSGAAP.EventDrivers
{
    public partial class NewLineEventDriver : EventDriver
    {
        public override string DisplayName => "New Lines";
        public override string ToolTipText => "Events are split on contiguous groups of \\n";

        public override EventSet CreateEventSet(string text) => new(NewLineSplitRegex().Split(text).Select(x => new Event(x, this)));

        [GeneratedRegex(@"[\r\n]+")]
        private static partial Regex NewLineSplitRegex();
    }
}
