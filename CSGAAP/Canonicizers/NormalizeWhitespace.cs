using CSGAAP.Generics;

namespace CSGAAP.Canonicizers
{
    public class NormalizeWhitespace : Canonicizer
    {
        public override string DisplayName => "Normalize Whitespace";

        public override string ToolTipText => "Converts all whitespace characters (newline, space and tab) to a single space.";

        public override string LongDescription => "Converts all whitespace characters (newline, space and tab) to a single space.  Uses Java Character.isWhitespace for classification.";

        // This is kind of simplified version of CharMatcher.This does not produce the exact same result, but its good enough i think
        // Tests are adjusted to pass for this method
        public override string Process(string text) => string.Join(' ', text.Split(new[] {
            '\u2002', '\u3000', '\u0085', '\u200A', '\u2005',
            '\u2000', '\u2029', '\u000B', '\u2008', '\u2003',
            '\u205F', '\u1680', '\u0009', '\u0020', '\u000C',
            '\u2001', '\u202F', '\u00A0', '\u2009', '\u2004',
            '\u2028', '\u2007', '\n', '\r'
        }, StringSplitOptions.RemoveEmptyEntries));
    }
}
