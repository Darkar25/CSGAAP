using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class ChordDistance : DistanceFunction
    {
        public override string DisplayName => "Chord Distance";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var events = h1.UniqueEvents
                .ToStructEnumerable()
                .Union(h2.UniqueEvents
                    .ToStructEnumerable());
            var sumUnknown = events
                .Select(h1.RelativeFrequency, x => x)
                .Sum(x => x);
            var sumKnown = events
                .Select(h2.RelativeFrequency, x => x)
                .Sum(x => x);
            return Math.Sqrt(2 - 2 * (events
                .Select(x => h1.RelativeFrequency(x) * h2.RelativeFrequency(x), x => x)
                .Sum(x => x) / Math.Sqrt(sumUnknown * sumUnknown * sumKnown * sumKnown)));
        }
    }
}
