using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class WEDDivergence : DistanceFunction
    {
        public override string DisplayName => "WED Divergence";
        public override string ToolTipText => "Weighted Euclidean Distance (WED) Divergence";

        public override double Distance(IHistogram h1, IHistogram h2) => Math.Sqrt(h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => {
                double unknown = h1.RelativeFrequency(x);
                double known = h2.RelativeFrequency(x);
                return (unknown == 0 ? 1 : unknown) * (unknown - known) * (unknown - known);
            }, x => x)
            .Sum(x => x));
    }
}
