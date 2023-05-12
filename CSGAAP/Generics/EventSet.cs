using System.Collections;
using CSGAAP.Util;

namespace CSGAAP.Generics
{
    public readonly struct EventSet : IEnumerable<Event>
    {
        private readonly IEnumerable<Event> inner;

        public EventSet(IEnumerable<Event> events) => inner = events;

        public override string ToString() => string.Join(", ", inner);
        public override bool Equals(object? obj) => obj is EventSet e && inner.Equals(e.inner);
        public override int GetHashCode() => inner.GetHashCode();

        public IEnumerator<Event> GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool operator ==(EventSet left, EventSet right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EventSet left, EventSet right)
        {
            return !(left == right);
        }
    }
}
