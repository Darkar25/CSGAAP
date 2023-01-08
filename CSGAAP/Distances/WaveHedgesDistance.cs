using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class WaveHedgesDistance : DistanceFunction
    {
        public override string DisplayName => "Wave Hedges Distance";

        public override double Distance(IHistogram h1, IHistogram h2) => h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => {
                double unknown = h1.RelativeFrequency(x);
                double known = h2.RelativeFrequency(x);
                return 1 - Math.Min(unknown, known) / Math.Max(unknown, known);
            }, x => x)
            .Sum(x => x);
    }
}
