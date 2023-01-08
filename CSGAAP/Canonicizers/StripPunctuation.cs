using CSGAAP.Generics;
using System.Text.RegularExpressions;

namespace CSGAAP.Canonicizers
{
    public partial class StripPunctuation : Canonicizer
    {
        public override string DisplayName => "Strip Punctuation";

        public override string ToolTipText => "Strip all punctuation characters from the text.";

        public override string LongDescription => "Strip all punctuation characters (\",.?!\"'`;:-()&$\") from the text.";

        public override string Process(string text) => PunctuationRegex2().Replace(PunctuationRegex().Replace(text, " "), "");

        [GeneratedRegex(@"\s[\p{P}\p{S}]+\s")]
        private static partial Regex PunctuationRegex();

        [GeneratedRegex(@"[\p{P}\p{S}]")]
        private static partial Regex PunctuationRegex2();
    }
}
