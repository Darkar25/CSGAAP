using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class CanberraDistance : DistanceFunction
    {
        public override string DisplayName => "Canberra Distance";
        public override string ToolTipText => "Canberra Distance Nearest Neighbor Classifier";

        public override double Distance(IHistogram h1, IHistogram h2) => h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => Math.Abs(h1.RelativeFrequency(x) - h2.RelativeFrequency(x)) / (h1.RelativeFrequency(x) + h2.RelativeFrequency(x)), x => x)
            .Sum(x => x);
    }
}
