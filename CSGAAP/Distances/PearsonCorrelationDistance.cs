using CSGAAP.Generics;
using CSGAAP.Util;
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
            int n; // number of elements

            double sigX; // sum of relative frequencies in h1;
            double sigY; // sum of relative frequencies in h2;
            double sigXY; // sum of products of relative frequencies
            double sigX2; // sum of squared relative frequencies in h1;
            double sigY2; // sum of squared relative frequencies in h2;

            double denom1, denom2; // factors of denominator

            n = events.Count(x => x);
            sigX = 0.0;
            sigY = 0.0;
            sigXY = 0.0;
            sigX2 = 0.0;
            sigY2 = 0.0;
            foreach (Event e in events)
            {
                double x = h1.RelativeFrequency(e);
                double y = h2.RelativeFrequency(e);
                sigX += x;
                sigY += y;
                sigX2 += x * x;
                sigY2 += y * y;
                sigXY += x * y;
            };

            denom1 = sigX2 - (sigX * sigX) / n;
            denom2 = sigY2 - (sigY * sigY) / n;
            if (Math.Abs(denom1) < 0.000001 &&
                Math.Abs(denom2) < 0.000001) return 0;

            if (Math.Abs(denom1) < 0.000001 ||
                Math.Abs(denom2) < 0.000001) return 1;

            return 1.0 - (sigXY - sigX * sigY / n) / Math.Sqrt(denom1 * denom2);
        }
    }
}
