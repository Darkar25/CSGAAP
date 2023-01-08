using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class HistogramDistance : DistanceFunction
    {
        public override string DisplayName => "Histogram Distance";
        public override string ToolTipText => "Histogram Distance (also known as Euclidean or L2 Norm)";

        public override double Distance(IHistogram h1, IHistogram h2) => h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => Math.Pow(h1.RelativeFrequency(x) - h2.RelativeFrequency(x), 2), x => x)
            .Sum(x => x);
    }
}
