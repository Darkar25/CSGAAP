using CSGAAP.Generics;
using StructLinq;
using System.Collections;

namespace CSGAAP.Util
{
    public class EventHistogram : IHistogram, IEnumerable<Event>
    {
        private readonly Dictionary<Event, int> histogram = new();

        public EventHistogram() { }

        public EventHistogram(IEnumerable<Event> eventSet)
        {
            foreach (var e in eventSet)
                Add(e);
        }

        public int Tokens { get; private set; } = 0;
        public int Types => histogram.Count;
        public IEnumerable<KeyValuePair<Event, int>> Sorted => histogram.OrderByDescending(x => x.Value);

        public IEnumerable<Event> UniqueEvents => histogram.Keys;

        public int AbsoluteFrequency(Event @event)
        {
            histogram.TryGetValue(@event, out var r);
            return r;
        }

        public bool Contains(Event @event) => histogram.ContainsKey(@event);

        public IEnumerator<Event> GetEnumerator() => UniqueEvents.GetEnumerator();

        public double NormalzedFrequency(Event @event) => histogram.TryGetValue(@event, out var r) ? r * 100_000 / (double)Tokens : 0;

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
