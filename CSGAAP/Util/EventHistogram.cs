using CSGAAP.Generics;
using System.Collections;

namespace CSGAAP.Util
{
    public class EventHistogram : IHistogram, IEnumerable<Event>
    {
        private readonly Dictionary<Event, int> histogram = new();

        public EventHistogram() { }

        public EventHistogram(IEnumerable<Event> eventSet)
        {
            histogram = eventSet.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            Tokens = histogram.Values.Sum();
        }

        public int Tokens { get; private set; } = 0;
        public int Types => histogram.Count;
        public IEnumerable<KeyValuePair<Event, int>> Sorted => histogram.OrderByDescending(x => x.Value);
        public IEnumerable<Event> UniqueEvents => histogram.Keys;

        public int AbsoluteFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r : 0;

        public bool Contains(Event @event) => histogram.ContainsKey(@event);

        public IEnumerator<Event> GetEnumerator() => UniqueEvents.GetEnumerator();

        public double NormalizedFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r * 100_000 / (double)Tokens : 0;

        public double RelativeFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r / (double)Tokens : 0;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(Event @event)
        {
            histogram.TryGetValue(@event, out var e);
            histogram[@event] = e + 1;
            Tokens++;
        }

        public void Clear()
        {
            histogram.Clear();
            Tokens = 0;
        }
    }
}
