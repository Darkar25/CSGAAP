using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.Distances
{
    public class KullbackLeiblerDivergence : DivergenceFunction
    {
        public override string DisplayName => "Kullback Leibler Distance";
        public override string ToolTipText => "Kullback Leibler Distance Nearest Neighbor Classifier";

        protected override double Divergence(IHistogram histogram1, IHistogram histogram2) => Math.Abs(histogram1.UniqueEvents
            .ToStructEnumerable().Intersect(histogram2.UniqueEvents
                .ToStructEnumerable())
            .Select(x => histogram1.RelativeFrequency(x) * Math.Log(histogram1.RelativeFrequency(x) / histogram2.RelativeFrequency(x)), x => x)
            .Sum(x => x));
    }
}
