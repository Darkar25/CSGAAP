using CSGAAP.Generics;

namespace CSGAAP.Canonicizers
{
    public class UnifyCase : Canonicizer
    {
        public override string DisplayName => "Unify Case";

        public override string ToolTipText => "Converts all text to lower case.";

        public override string Process(string text) => text.ToLowerInvariant();
    }
}
