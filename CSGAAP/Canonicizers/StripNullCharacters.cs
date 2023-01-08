using CSGAAP.Generics;
using System.Text.RegularExpressions;

namespace CSGAAP.Canonicizers
{
    public partial class StripNullCharacters : Canonicizer
    {
        public override string DisplayName => "Strip Null Characters";

        public override string ToolTipText => "Strip Null (0x00) characters from text";

        public override string Process(string text) => NullRegex2().Replace(NullRegex().Replace(text, " "), "");

        [GeneratedRegex("\\s\\u0000\\s")]
        private static partial Regex NullRegex();

        [GeneratedRegex("\\u0000")]
        private static partial Regex NullRegex2();
    }
}
