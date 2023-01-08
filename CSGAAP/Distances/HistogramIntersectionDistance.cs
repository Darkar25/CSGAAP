using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class HistogramIntersectionDistance : DistanceFunction
    {
        public override string DisplayName => "Histogram Intersection Distance";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var events = h1.UniqueEvents
                .ToStructEnumerable()
                .Union(h2.UniqueEvents
                    .ToStructEnumerable());
            return 1 - (events
                .Select(x => Math.Min(h1.RelativeFrequency(x), h2.RelativeFrequency(x)), x => x)
                .Sum(x => x) / Math.Min(events
                .Select(h1.RelativeFrequency, x => x)
                .Sum(x => x), events
                .Select(h2.RelativeFrequency, x => x)
                .Sum(x => x)));
        }
    }
}
