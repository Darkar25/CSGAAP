namespace CSGAAP.Generics
{
    public abstract class DivergenceFunction : DistanceFunction
    {
        public DivergenceFunction()
        {
            AddParameter(
                Name: "divergenceType",
                DisplayName: "Divergence Type",
                DefaultValue: DivergenceType.STANDARD,
                DivergenceType.AVERAGE, DivergenceType.CROSS, DivergenceType.MAX, DivergenceType.MIN, DivergenceType.REVERSE, DivergenceType.STANDARD);
        }

        public override double Distance(IHistogram histogram1, IHistogram histogram2) => (DivergenceType)this["divergenceType"] switch
        {
            DivergenceType.AVERAGE => (Divergence(histogram1, histogram2) + Divergence(histogram2, histogram1)) / 2.0,
            DivergenceType.MAX => Math.Max(Divergence(histogram1, histogram2), Divergence(histogram1, histogram2)),
            DivergenceType.MIN => Math.Min(Divergence(histogram1, histogram2), Divergence(histogram1, histogram2)),
            DivergenceType.REVERSE => Divergence(histogram2, histogram1),
            DivergenceType.CROSS => Divergence(histogram1, histogram2) * Divergence(histogram1, histogram2),
            _ => Divergence(histogram1, histogram2),
        };

        protected abstract double Divergence(IHistogram histogram1, IHistogram histogram2);

        public enum DivergenceType
        {
            STANDARD, AVERAGE, MAX, MIN, REVERSE, CROSS
        }
    }
}
