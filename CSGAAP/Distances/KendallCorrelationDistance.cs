using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.Distances
{
    public class KendallCorrelationDistance : DistanceFunction
    {
        public override string DisplayName => "Kendall Correlation Distance";
        public override string ToolTipText => "Kendall Correlation Distance Nearest Neighbor Classifier";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var h1s = h1.UniqueEvents.ToStructEnumerable();
            var h2s = h2.UniqueEvents.ToStructEnumerable();
            var events = h1s.Union(h2s);
            double correlation = 0.0;
            var hm1 = Utils.RankEvents(h1s, h1);
            var hm2 = Utils.RankEvents(h2s, h2);
            foreach (var e in events)
                foreach (var e2 in events)
                {
                    if (e.Equals(e2)) continue;
                    if (!hm1.TryGetValue(e, out var x1))
                        x1 = hm1.Count + 1;
                    if (!hm2.TryGetValue(e, out var x2))
                        x2 = hm2.Count + 1;
                    if (!hm1.TryGetValue(e2, out var y1))
                        y1 = hm1.Count + 1;
                    if (!hm2.TryGetValue(e2, out var y2))
                        y2 = hm2.Count + 1;
                    correlation += Math.Sign(x1.CompareTo(y1)) * Math.Sign(x2.CompareTo(y2));
                }
            return 1 - correlation / (hm1.Count * (hm2.Count - 1));
        }
    }
}
