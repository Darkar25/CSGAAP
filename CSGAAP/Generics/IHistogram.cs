using CSGAAP.Util;

namespace CSGAAP.Generics
{
    public interface IHistogram
    {
        public abstract double RelativeFrequency(Event @event);
        public abstract double NormalizedFrequency(Event @event);
        public abstract int AbsoluteFrequency(Event @event);
        public abstract bool Contains(Event @event);
        public abstract IEnumerable<Event> UniqueEvents { get; }
    }
}
