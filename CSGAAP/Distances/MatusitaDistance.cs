using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class MatusitaDistance : DistanceFunction
    {
        public override string DisplayName => "Matusita Distance";

        public override double Distance(IHistogram h1, IHistogram h2) => Math.Sqrt(h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => Math.Pow(Math.Sqrt(h1.RelativeFrequency(x)) - Math.Sqrt(h2.RelativeFrequency(x)), 2), x => x)
            .Sum(x => x));
    }
}
