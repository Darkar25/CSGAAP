using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class NominalKSDistance : DistanceFunction
    {
        public override string DisplayName => "KS Distance";
        public override string ToolTipText => "Nominal Kolmogorov-Smirnov distance (also known as the Minkowski L-infinity metric)";

        public override double Distance(IHistogram h1, IHistogram h2) => .5 * h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => Math.Abs(h1.RelativeFrequency(x) - h2.RelativeFrequency(x)), x => x)
            .Sum(x => x);
    }
}
