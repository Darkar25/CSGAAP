using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class ManhattanDistance : DistanceFunction
    {
        public override string DisplayName => "Manhattan Distance";
        public override string ToolTipText => "Manhattan Distance (aka taxicab or Minkowski L1 distance)";

        public override double Distance(IHistogram h1, IHistogram h2) => h1.UniqueEvents
            .ToStructEnumerable()
            .Union(h2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => Math.Abs(h1.RelativeFrequency(x) - h2.RelativeFrequency(x)), x => x)
            .Sum(x => x);
    }
}
