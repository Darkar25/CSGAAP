namespace CSGAAP.Util
{
    public class LastPickedComparator : IComparer<KeyValuePair<string, double>>
    {
        const double epsilon = 0.0000001;

        public int Compare(KeyValuePair<string, double> x, KeyValuePair<string, double> y)
        {
            if ((int)x.Value != (int)y.Value) return (int)(x.Value - y.Value);
            var first = x.Value;
            var second = y.Value;
            while ((int)first - second > epsilon)
            {
                first *= 2;
                second *= 2;
            }
            if ((int)second - second > epsilon) return 1;
            return -1;
        }
    }
}
