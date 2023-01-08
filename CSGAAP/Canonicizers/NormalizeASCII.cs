using CSGAAP.Generics;
using System.Text;
using System.Linq;

namespace CSGAAP.Canonicizers
{
    public class NormalizeASCII : Canonicizer
    {
        public override string DisplayName => "Normalize ASCII";

        public override string ToolTipText => "Strip all non-ASCII, non-printing characters from the text.  Whitespace is preserved.";

        public override string LongDescription => "Strip all non-ASCII, non-printing characters from the text.  Whitespace is preserved.  (Geek content: Whitespace is defined as characters 0x09-0x0D, inclusive, and printable ASCII is characters 0x20-0x7E, inclusive.)";

        public override string Process(string text) => new(text.Where(c => (c > 0x08 && c <= 0x0D) || (c > 0x1F && c <= 0x7E)).ToArray());
    }
}
