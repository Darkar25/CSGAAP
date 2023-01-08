using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class SoergleDistance : DistanceFunction
    {
        public override string DisplayName => "Soergle Distance";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var events = h1.UniqueEvents.ToStructEnumerable().Union(h2.UniqueEvents.ToStructEnumerable());
            return events
                .Select(x => Math.Abs(h1.RelativeFrequency(x) - h2.RelativeFrequency(x)), x => x)
                .Sum(x => x) / events
                .Select(x => Math.Max(h1.RelativeFrequency(x), h2.RelativeFrequency(x)), x => x)
                .Sum(x => x);
        }
    }
}
