using CSGAAP.Generics;
using System.Text.RegularExpressions;

namespace CSGAAP.Canonicizers
{
    public partial class StripNonPunc : Canonicizer
    {
        public override string DisplayName => "Strip AlphaNumeric";

        public override string ToolTipText => "Strip all non-punctuation/non-whitespace characters from the text.";

        public override string LongDescription => "Strip all non-whitespace characters that are not punctuation marks (one of \",.?!\"\'`;:-()&$\") from the text.";

        public override string Process(string text) => PunctuationRegex2().Replace(PunctuationRegex().Replace(text, " "), "");

        [GeneratedRegex(@"\s[^\p{P}\p{S} ]+\s")]
        private static partial Regex PunctuationRegex();

        [GeneratedRegex(@"[^\p{P}\p{S} ]")]
        private static partial Regex PunctuationRegex2();
    }
}
