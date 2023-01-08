using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class CrossEntropyDivergence : DivergenceFunction
    {
        public override string DisplayName => "RN Cross Entropy";
        public override string ToolTipText => "Ryan-Noecker Cross Entropy (Faster)";

        protected override double Divergence(IHistogram h1, IHistogram h2) => h2.UniqueEvents
            .ToStructEnumerable()
            .Select(x => -1 * (h1.RelativeFrequency(x) * Math.Log(h2.RelativeFrequency(x))), x => x)
            .Sum(x => x);
    }
}
