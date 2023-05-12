using CSGAAP.Generics;

namespace CSGAAP.Util
{
    public class EventMap : IHistogram
    {
        private readonly Dictionary<Event, double> histogram;

        public IEnumerable<Event> UniqueEvents => histogram.Keys;

        public int AbsoluteFrequency(Event @event) => throw new NotSupportedException();

        public bool Contains(Event @event) => histogram.ContainsKey(@event);

        public double NormalizedFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r * 100_000 : 0;

        public double RelativeFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r : 0;

        public EventMap(Document doc) : this(doc.EventSets.Values) { }

        /*public EventMap(IEnumerable<EventSet> events) => histogram = new(events
            .SelectMany(x => x
                .Select(y => Tuple.Create(y, x)))
            .GroupBy(x => x.Item1)
            .ToDictionary(x => x.Key, x => x
                .Count() / (double)x
                .First().Item2
                .Count()));*/

        public EventMap(IEnumerable<EventSet> events)
        {
            histogram = new();
            var eventLists = events as EventSet[] ?? events.ToArray();
            foreach (var eventList in eventLists)
            {
                var c = eventList.Count();
                var count = eventList.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
                foreach (var @event in eventList)
                    if (!histogram.ContainsKey(@event)) {
                        var sum = 0;
                        foreach (var e in eventLists)
                            sum += count[@event];
                        histogram[@event] = sum / (double)c;
                    }
            }
        }

        public EventMap(EventSet set) : this(new EventSet[] { set }) { }

        public EventMap(IDictionary<Event, double> histogram) => this.histogram = new(histogram);

        public static EventMap Centroid(IEnumerable<EventMap> eventMaps)
        {
            var maps = eventMaps as EventMap[] ?? eventMaps.ToArray();
            return new(maps
            .SelectMany(x => x.histogram)
            .ToLookup(x => x.Key, x => x.Value)
            .ToDictionary(x => x.Key, x => x
                .Sum(y => y / maps.Length)));
        }
    }
}
