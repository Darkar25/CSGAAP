using CSGAAP.Generics;
using System.Text.RegularExpressions;

namespace CSGAAP.Canonicizers
{
    public partial class StripNumbers : Canonicizer
    {
        public override string DisplayName => "Strip Numbers";

        public override string ToolTipText => "Converts numbers to a single 0";

        public override string LongDescription => "Converts numbers (digit strings) to a single 0";

        public override string Process(string text) => NumberRegex().Replace(text, "0");

        [GeneratedRegex(@"-?(([1-9]\d*)|0)(\.0*[1-9](0*[1-9])*)?")]
        private static partial Regex NumberRegex();
    }
}
