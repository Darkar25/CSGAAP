using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;
using System.Text.RegularExpressions;

namespace CSGAAP.EventDrivers
{
    public partial class NaiveWordEventDriver : EventDriver
    {
        public override string DisplayName => "Words";
        public override string ToolTipText => "Words (White Space as Separators)";

        public override EventSet CreateEventSet(string text) => new(WhiteSpaceSplitRegex().Split(text.ToString()).ToStructEnumerable().Where(x => !string.IsNullOrWhiteSpace(x), x => x).Select(x => new Event(x, this), x => x).ToEnumerable());

        [GeneratedRegex("\\s+")]
        private static partial Regex WhiteSpaceSplitRegex();
    }
}
