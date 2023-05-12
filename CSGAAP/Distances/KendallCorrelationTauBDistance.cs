using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.Distances
{
    public class KendallCorrelationTauBDistance : DistanceFunction
    {
        public override string DisplayName => "Kendall Correlation TauB";
        public override string ToolTipText => "Method for calculating tau as suggested by William Knight in A Computer Method for Calculating Kendall's Tau with Ungrouped Data (1966)";

        public override double Distance(IHistogram h1, IHistogram h2)
        {
            return TauDistance(
                h1.UniqueEvents.Select(x => new KeyValuePair<Event, double>(x, h1.RelativeFrequency(x))),
                h2.UniqueEvents.Select(x => new KeyValuePair<Event, double>(x, h2.RelativeFrequency(x))));
        }

        private static double TauDistance(IEnumerable<KeyValuePair<Event, double>> unknown, IEnumerable<KeyValuePair<Event, double>> known)
        {
            var unknowns = unknown as KeyValuePair<Event, double>[] ?? unknown.ToArray();
            var (unknownRanks, unknownTies) = Utils.RankEventsWithTies(unknowns.OrderByDescending(x => x.Value));
            var knowns = known as KeyValuePair<Event, double>[] ?? known.ToArray();
            var (knownRanks, knownTies) = Utils.RankEventsWithTies(knowns.OrderByDescending(x => x.Value));
            var allEvents = unknowns.Concat(knowns).DistinctBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            var ranks = allEvents.Select(x => new KeyValuePair<int, int>(
                unknownRanks.TryGetValue(x.Key, out var urv) ? urv : unknownRanks.Count + 1,
                knownRanks.TryGetValue(x.Key, out var krv) ? krv : knownRanks.Count + 1));
            var ranks2 = ranks as KeyValuePair<int, int>[] ?? ranks.ToArray();
            var y = ranks2.Select(x => x.Value).ToArray();
            ranks = ranks2.Order(new RankComparer());
            var ties = 0;
            List<int> pairTies = new();
            using(var enumerator = ranks.GetEnumerator())
            {
                var prev = enumerator.Current;
                while(enumerator.MoveNext())
                    if(enumerator.Current.Value == prev.Value)
                        ties++;
                    else if(ties != 0)
                    {
                        pairTies.Add(ties);
                        ties = 0;
                    }
            }
            var n0 = allEvents.Count * (allEvents.Count - 1d) / 2d;
            var n1 = unknownTies.Sum(x => x * (x - 1d) / 2d);
            var n2 = knownTies.Sum(x => x * (x - 1d) / 2d);
            var n3 = pairTies.Sum(x => x * (x - 1d) / 2d);
            return 1 - (n0 - n1 - n2 + n3 - 2 * Swaps(y)) / Math.Sqrt((n0 - n1) * (n0 - n2));
        }

        private static int Swaps(int[] list)
        {
            if (list.Length <= 1) return 0;

            int middle = list.Length / 2;
            int[] left = list[..middle];
            int[] right = list[middle..];
            int tmp = Swaps(left) + Swaps(right);
            Array.Sort(left);
            Array.Sort(right);
            int merge = Merge(left, right);

            return tmp + merge;
        }

        private static int Merge(int[] left, int[] right)
        {
            int totalLength = left.Length + right.Length;
            int i = 0, j = 0, swaps = 0;

            while (i + j < totalLength && j < right.Length)
                if (i >= left.Length || right[j] < left[i])
                {
                    swaps += left.Length - i;
                    j++;
                } else
                    i++;

            return swaps;
        }

        private class RankComparer : IComparer<KeyValuePair<int, int>>
        {
            public int Compare(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
            {
                //if (x.Key is null or x.Value is null) return int.MaxValue;
                var result = x.Key.CompareTo(y.Key);
                if (result == 0) result = x.Value.CompareTo(y.Value);
                return result;
            }
        }
    }
}
