using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class AngularSeparationDistance : DistanceFunction
    {
        public override string DisplayName => "Angular Separation Distance";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var events = h1.UniqueEvents
                .ToStructEnumerable()
                .Union(h2.UniqueEvents
                    .ToStructEnumerable());
            return 1 - (events
                .Select(x => h1.RelativeFrequency(x) * h2.RelativeFrequency(x), x => x)
                .Sum(x => x) / Math.Sqrt(Math.Pow(events
                .Select(h1.RelativeFrequency, x => x)
                .Sum(x => x), 2) * Math.Pow(events
                .Select(h2.RelativeFrequency, x => x)
                .Sum(x => x), 2)));
        }
    }
}
