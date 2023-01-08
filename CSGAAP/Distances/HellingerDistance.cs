using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class HellingerDistance : DistanceFunction
    {
        public override string DisplayName => "Hellinger Distance";
        public override string ToolTipText => "1/sqrt(2) * sqrt( sum( (sqrt(pi)-sqrt(qi))^2 ) )";

        public override double Distance(IHistogram h1, IHistogram h2) => h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => Math.Pow(Math.Sqrt(h1.RelativeFrequency(x)) - Math.Sqrt(h2.RelativeFrequency(x)), 2), x => x)
            .Sum(x => x);
    }
}
