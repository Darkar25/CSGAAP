using CSGAAP.Generics;

namespace CSGAAP.Util
{
    public class AbsoluteHistogram : IHistogram
    {
        private readonly Dictionary<Event, int> histogram;
        private readonly Dictionary<EventDriver, int> totals;

        public IEnumerable<Event> UniqueEvents => histogram.Keys;

        public int AbsoluteFrequency(Event @event) => histogram.TryGetValue(@event, out var e) ? e : 0;

        public bool Contains(Event @event) => histogram.ContainsKey(@event);

        public double NormalizedFrequency(Event @event) => RelativeFrequency(@event) * 100_000;

        public double RelativeFrequency(Event @event)
        {
            if (histogram.TryGetValue(@event, out var e))
                return e / (double)totals[@event.EventDriver];
            return 0.0;
        }

        private AbsoluteHistogram(Dictionary<Event, int> histogram, Dictionary<EventDriver, int> totals)
        {
            this.histogram = histogram;
            this.totals = totals;
        }

        public AbsoluteHistogram(Document document)
        {
            totals = document.EventSets
                .ToDictionary(x => x.Key, x => x.Value.Count());
            histogram = document.EventSets
                .SelectMany(x => x.Value)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x
                    .Count());
        }

        public static AbsoluteHistogram Centroid(IEnumerable<AbsoluteHistogram> histograms)
        {
            var absoluteHistograms = histograms as AbsoluteHistogram[] ?? histograms.ToArray();
            var tmp1 = absoluteHistograms
                .SelectMany(x => x.histogram)
                .ToLookup(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, x => x.Sum());
            var tmp2 = absoluteHistograms
                .SelectMany(x => x.totals)
                .ToLookup(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, x => x.Sum());
            return new(tmp1, tmp2);
        }
    }
}
