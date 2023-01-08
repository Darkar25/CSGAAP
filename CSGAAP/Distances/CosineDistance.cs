using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class CosineDistance : DistanceFunction
    {
        public override string DisplayName => "Cosine Distance";
        public override string ToolTipText => "Normalized Dot-Product Nearest Neighbor Classifier";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            var events = h1.UniqueEvents
                .ToStructEnumerable()
                .Union(h2.UniqueEvents
                    .ToStructEnumerable());

            var distance = 0d;
            var h1Magnitude = 0d;
            var h2Magnitude = 0d;

            foreach(var e in events)
            {
                var uNormFreq = h1.NormalzedFrequency(e);
                var kNormFreq = h2.NormalzedFrequency(e);

                distance += uNormFreq * kNormFreq;
                h1Magnitude += uNormFreq * uNormFreq;
                h2Magnitude += kNormFreq * kNormFreq;
            }

            return Math.Abs((distance / Math.Sqrt(h1Magnitude * h2Magnitude)) - 1);
        }
    }
}
