using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class PearsonCorrelationDistance : DistanceFunction
    {
        public override string DisplayName => "Pearson Correlation Distance";
        public override string ToolTipText => "Pearson Correlation Distance Nearest Neighbor Classifier";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var events = h1.UniqueEvents.ToStructEnumerable().Union(h2.UniqueEvents.ToStructEnumerable());

            var n = events.Count(x => x); // number of elements
            var sigX = 0.0; // sum of relative frequencies in h1;
            var sigY = 0.0; // sum of relative frequencies in h2;
            var sigXY = 0.0; // sum of products of relative frequencies
            var sigX2 = 0.0; // sum of squared relative frequencies in h1;
            var sigY2 = 0.0; // sum of squared relative frequencies in h2;
            foreach (var e in events)
            {
                double x = h1.RelativeFrequency(e);
                double y = h2.RelativeFrequency(e);
                sigX += x;
                sigY += y;
                sigX2 += x * x;
                sigY2 += y * y;
                sigXY += x * y;
            };
            // factors of denominator
            var denom1 = sigX2 - (sigX * sigX) / n;
            var denom2 = sigY2 - (sigY * sigY) / n;
            if (Math.Abs(denom1) < 0.000001 &&
                Math.Abs(denom2) < 0.000001) return 0;

            if (Math.Abs(denom1) < 0.000001 ||
                Math.Abs(denom2) < 0.000001) return 1;

            return 1.0 - (sigXY - sigX * sigY / n) / Math.Sqrt(denom1 * denom2);
        }
    }
}
