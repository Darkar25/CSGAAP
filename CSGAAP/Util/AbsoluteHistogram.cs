using CSGAAP.Generics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGAAP.Util
{
    public class AbsoluteHistogram : IHistogram
    {
        private readonly ReadOnlyDictionary<Event, int> histogram;
        private readonly ReadOnlyDictionary<EventDriver, int> totals;

        public IEnumerable<Event> UniqueEvents => histogram.Keys;

        public int AbsoluteFrequency(Event @event) => histogram.TryGetValue(@event, out var e) ? e : 0;

        public bool Contains(Event @event) => histogram.ContainsKey(@event);

        public double NormalzedFrequency(Event @event) => RelativeFrequency(@event) * 100_000;

        public double RelativeFrequency(Event @event)
        {
            if (histogram.TryGetValue(@event, out var e))
                return e / (double)totals[@event.EventDriver];
            return 0.0;
        }

        private AbsoluteHistogram(IDictionary<Event, int> histogram, IDictionary<EventDriver, int> totals)
        {
            this.histogram = new(histogram);
            this.totals = new(totals);
        }

        public AbsoluteHistogram(Document document)
        {
            totals = new(document.EventSets
                .ToDictionary(x => x.Key, x => x.Value.Count()));
            histogram = new(document.EventSets
                .SelectMany(x => x.Value)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x
                    .Count()));
        }

        public static AbsoluteHistogram Centroid(IEnumerable<AbsoluteHistogram> histograms)
        {
            var tmp1 = histograms
                .SelectMany(x => x.histogram)
                .ToLookup(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, x => x.Sum());
            var tmp2 = histograms
                .SelectMany(x => x.totals)
                .ToLookup(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, x => x.Sum());
            return new(tmp1, tmp2);
        }
    }
}
