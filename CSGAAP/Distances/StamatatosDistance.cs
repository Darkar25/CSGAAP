using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class StamatatosDistance : DistanceFunction
    {
        public override string DisplayName => "Stamatatos Distance";
        public override string ToolTipText => "Stamatatos Distance (2*(f(x)-g(x))/(f(x)+g(x)))^2";

        public override double Distance(IHistogram h1, IHistogram h2) => h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => {
                double unknownValue = h1.RelativeFrequency(x);
                double knownValue = h2.RelativeFrequency(x);
                return Math.Pow(2 * (unknownValue - knownValue) / (unknownValue + knownValue), 2);
            }, x => x)
            .Sum(x => x);
    }
}
