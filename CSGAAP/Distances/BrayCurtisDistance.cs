using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class BrayCurtisDistance : DistanceFunction
    {
        public override string DisplayName => "Bray Curtis Distance";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var events = h1.UniqueEvents
                .ToStructEnumerable()
                .Union(h2.UniqueEvents
                    .ToStructEnumerable());
            return events
                .Select(x => Math.Abs(h1.RelativeFrequency(x) - h2.RelativeFrequency(x)), x => x)
                .Sum(x => x) / events
                .Select(x => h1.RelativeFrequency(x) + h2.RelativeFrequency(x), x => x)
                .Sum(x => x);
        }
    }
}
