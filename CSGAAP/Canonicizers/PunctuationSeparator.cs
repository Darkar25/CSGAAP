using CSGAAP.Generics;
using System.Text.RegularExpressions;

namespace CSGAAP.Canonicizers
{
    public partial class PunctuationSeparator : Canonicizer
    {
        public override string DisplayName => "Punctuation Separator";

        public override string ToolTipText => "Whitespace pad all punctuation";

        public override string LongDescription => "Put a single space before and after each punctuation mark, to keep them separate from adjacent words.";

        // Very hacky implementation, but at least it works and passes the tests
        public override string Process(string text) => string.Join(' ', PunctuationRegex().Replace(text, (m) => " " + m + " ").Split(' ', StringSplitOptions.RemoveEmptyEntries));

        [GeneratedRegex("[\\p{P}]")]
        private static partial Regex PunctuationRegex();
    }
}
