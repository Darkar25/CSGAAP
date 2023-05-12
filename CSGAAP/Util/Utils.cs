using CSGAAP.Generics;
using StructLinq;
using StructLinq.Array;
using StructLinq.IEnumerable;

namespace CSGAAP.Util
{
    public static class Utils
    {
        public static double StdDev(this IEnumerable<double> values) => values.ToStructEnumerable().StdDev();

        public static double StdDev<TEnumerator>(this IStructEnumerable<double, TEnumerator> values) where TEnumerator : struct, IStructEnumerator<double>
        {
            int count = values.Count(x => x);
            if (count <= 1) return 0;
            //Compute the Average
            double avg = values.Sum() / count;

            //Perform the Sum of (value-avg)^2
            double sum = values.Select(d => (d - avg) * (d - avg)).Sum(x => x);

            //Put it all together
            return Math.Sqrt(sum / (count - 1));
        }

        public static double Average<TEnumerator>(this IStructEnumerable<double, TEnumerator> values) where TEnumerator : struct, IStructEnumerator<double> => values.Sum(x => x) / values.Count(x => x);
        
        public static double Average(this ArrayEnumerable<double> values) => values.Sum(x => x) / values.Count;

        //public static double Sum<TEnumerator>(this IStructEnumerable<double, TEnumerator> values, Func<double, double> f) where TEnumerator : struct, IStructEnumerator<double> => values.Select(f, x => x).Sum(x => x);

        public static IEnumerable<TResult> SelectMany<TResult>(this IEnumerable<IEnumerable<TResult>> source) => source.SelectMany(x => x);

        public static IEnumerable<TResult> ForceEnumerate<TResult>(this IEnumerable<TResult> source) => source.ToArray();

        public static ArrayEnumerable<TResult> ForceEnumerate<TResult, TEnumerator>(this IStructEnumerable<TResult, TEnumerator> source) where TEnumerator : struct, IStructEnumerator<TResult> => source.ToArray().ToStructEnumerable();

        public static string GetFileName(this Uri uri) => Path.GetFileNameWithoutExtension(uri.AbsolutePath);

        public static string GetExtension(this Uri uri) => Path.GetExtension(uri.AbsolutePath);

        public static Dictionary<Event, int> RankEvents(StructEnumerableFromIEnumerable<Event> events, IHistogram hist)
        {
            int rank = 0;
            int count = 0;
            var oldfreq = double.PositiveInfinity;
            return events
                .Select(x => Tuple.Create(x, hist.RelativeFrequency(x)), x => x)
                .OrderByDescending(x => x.Item2)
                .ToEnumerable()
                .ToDictionary(x => x.Item1, x =>
                {
                    count++;
                    if (oldfreq != x.Item2)
                    {
                        rank = count;
                        oldfreq = x.Item2;
                    }
                    return rank;
                });
        }

        public static (Dictionary<Event, int> events, List<int> ties) RankEventsWithTies(IEnumerable<KeyValuePair<Event, double>> events)
        {
            List<int> tiesList = new();
            int rank = 0;
            int count = 0;
            int ties = 0;
            double oldfreq = 0;
            return (events
                .ToDictionary(x => x.Key, x =>
                {
                    count++;
                    if (oldfreq != x.Value)
                    {
                        if (ties != 0)
                        {
                            tiesList.Add(ties);
                            ties = 0;
                        }
                        rank = count;
                        oldfreq = x.Value;
                    }
                    else ties++;
                    return rank;
                }), tiesList);
        }

        public static Task<byte[]> LoadData(Uri uri, CancellationToken token = default)
        {
            return uri.Scheme switch
            {
                "http" or "https" => new HttpClient().GetByteArrayAsync(uri, token),
                _ => File.ReadAllBytesAsync(System.Web.HttpUtility.UrlDecode(uri.AbsolutePath), token)
            };
        }
    }
}
