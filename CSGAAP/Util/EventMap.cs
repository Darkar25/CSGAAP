using CSGAAP.Generics;
using System.Collections;
using System.Collections.ObjectModel;

namespace CSGAAP.Util
{
    public class EventMap : IHistogram
    {
        private readonly ReadOnlyDictionary<Event, double> histogram;

        public IEnumerable<Event> UniqueEvents => histogram.Keys;

        public int AbsoluteFrequency(Event @event) => throw new NotSupportedException();

        public bool Contains(Event @event) => histogram.ContainsKey(@event);

        public double NormalzedFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r * 100_000 : 0;

        public double RelativeFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r : 0;

        public EventMap(Document doc) : this(doc.EventSets.Values) { }

        public EventMap(IEnumerable<EventSet> events) => histogram = new(events
            .SelectMany(x => x
                .Select(y => Tuple.Create(y, x)))
            .GroupBy(x => x.Item1)
            .ToDictionary(x => x.Key, x => x
                .Count() / (double)x
                .First().Item2
                .Count()));

        public EventMap(EventSet set) : this(new EventSet[] { set }) { }

        public EventMap(IDictionary<Event, double> histogram) => this.histogram = new(histogram);

        public static EventMap Centroid(IEnumerable<EventMap> eventMaps)
        {
            var cnt = eventMaps.Count();
            return new(eventMaps
            .SelectMany(x => x.histogram)
            .ToLookup(x => x.Key, x => x.Value)
            .ToDictionary(x => x.Key, x => x
                .Sum(y => y / cnt)));
        }
    }
}
