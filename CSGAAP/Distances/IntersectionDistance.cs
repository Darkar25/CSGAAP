using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class IntersectionDistance : DistanceFunction
    {
        public override string DisplayName => "Intersection Distance";
        public override string ToolTipText => "Event type set intersection divided by event type set union";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var h1s = h1.UniqueEvents.ToStructEnumerable();
            var h2s = h2.UniqueEvents.ToStructEnumerable();
            return 1d - h1s
            .Intersect(h2s)
            .Count(x => x) / (double)h1s
                .Union(h2s)
                .Count(x => x);
        }
    }
}
