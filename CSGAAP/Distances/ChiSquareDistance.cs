using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class ChiSquareDistance : DistanceFunction
    {
        public override string DisplayName => "Chi Square Distance";

        public override double Distance(IHistogram h1, IHistogram h2) => h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(e =>
            {
                double x = h1.RelativeFrequency(e);
                double y = h2.RelativeFrequency(e);
                return (x - y) * (x - y) / (x + y);
            }, x => x)
            .Sum(x => x);
    }
}
