using CSGAAP.Generics;
using StructLinq;
using System.Linq;

namespace CSGAAP.Distances
{
    public class KeseljWeightedDistance : DistanceFunction
    {
        public override string DisplayName => "Keselj-weighted Distance";
        public override string ToolTipText => "Histogram Distance (L2 Norm) with Keselj-weighting based on overall frequency";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var h1s = h1.UniqueEvents.ToStructEnumerable();
            return h1s.Sum(x =>
            {
                var fa = h1.RelativeFrequency(x);
                var fx = h2.RelativeFrequency(x);
                return ((fa - fx) * (fa - fx)) / ((fa + fx) * (fa + fx));
            }) + h2.UniqueEvents.ToStructEnumerable().Except(h1s).Count(x => x);
        }
    }
}
