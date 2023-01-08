using CSGAAP.Generics;
using System.Text.RegularExpressions;

namespace CSGAAP.Canonicizers
{
    public partial class SmashI : Canonicizer
    {
        public override string DisplayName => "Smash I";

        public override string ToolTipText => "Converts all uses of \"I\" to lowercase.";

        public override string LongDescription => "Converts all uses of \"I\" as a word to lowercase.";

        public override string Process(string text) => IRegex().Replace(text, "$1i$2");

        [GeneratedRegex("(^|\\W+)I(\\W+|$)")]
        private static partial Regex IRegex();
    }
}
