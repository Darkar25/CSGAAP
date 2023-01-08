using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class BhattacharyyaDistance : DistanceFunction
    {
        public override string DisplayName => "Alt Intersection Distance";

        public override double Distance(IHistogram h1, IHistogram h2) => -Math.Log(h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => Math.Sqrt(h1.RelativeFrequency(x) * h2.RelativeFrequency(x)), x => x)
            .Sum(x => x));
    }
}
