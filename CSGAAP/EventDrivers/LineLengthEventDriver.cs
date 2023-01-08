using CSGAAP.Generics;
using CSGAAP.Util;
using System.Text.RegularExpressions;

namespace CSGAAP.EventDrivers
{
    public partial class LineLengthEventDriver : NewLineEventDriver
    {
        public override string DisplayName => "Line Length";

        public override string ToolTipText => "Gets the number of words per line of text.";

        public override EventSet CreateEventSet(string text) => new(base.CreateEventSet(text).Select(x => new Event(SplitRegex().Split(x.ToString().Trim()).Length.ToString(), this)));

        [GeneratedRegex(@"\s+")]
        private static partial Regex SplitRegex();
    }
}
