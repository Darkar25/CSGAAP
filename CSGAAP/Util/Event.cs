using CSGAAP.Generics;
using System.Diagnostics;

namespace CSGAAP.Util
{
    /*public struct Event : IComparable<Event>
    {
        private readonly string Data;
        public EventDriver EventDriver { get; private set; }
        public string EventString => ToString() + EventDriver;

        public Event(char data, EventDriver driver)
        {
            Data = new(new char[] { data });
            EventDriver = driver;
        }

        public Event(string data, EventDriver driver)
        {
            Data = data;
            EventDriver = driver;
        }

        public override bool Equals(object? obj) => obj is Event e && Data.Equals(e.Data) && ((EventDriver is null && e.EventDriver is null) || (EventDriver is not null && EventDriver.Equals(e.EventDriver)));
        public override int GetHashCode() => Data.GetHashCode() % 0x8000 | (int)((uint)(EventDriver is null ? 0 : EventDriver.GetHashCode()) & 0xFFFF0000);
        public override string ToString() => Data;

        public int CompareTo(Event other) => GetHashCode().CompareTo(other.GetHashCode());

        public static bool operator ==(Event left, Event right) => left.Equals(right);

        public static bool operator !=(Event left, Event right) => !(left == right);
    }*/
    [DebuggerDisplay("{Data} {EventDriver}")]
    public record struct Event(string Data, EventDriver EventDriver) {
        public override string ToString() => Data;
    }
}
