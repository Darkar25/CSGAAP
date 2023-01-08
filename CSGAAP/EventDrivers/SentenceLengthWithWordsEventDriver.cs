using CSGAAP.Generics;
using CSGAAP.Util;
using System.Text.RegularExpressions;

namespace CSGAAP.EventDrivers
{
    public partial class SentenceLengthWithWordsEventDriver : SentenceEventDriver
    {
        public override string DisplayName => "Sentence Length";

        public override string ToolTipText => "Sentence length (With words)";

        public override EventSet CreateEventSet(string text) => new(base.CreateEventSet(text).Select(x => new Event((SplitRegex().Count(x.ToString()) + 1).ToString(), this)));
        
        [GeneratedRegex("\\s+")]
        private static partial Regex SplitRegex();
    }
}
