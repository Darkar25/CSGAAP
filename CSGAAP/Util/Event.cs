using CSGAAP.Generics;
using IKVM.Runtime.Extensions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

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

    /*[DebuggerDisplay("{Data} {EventDriver}")]
    public readonly record struct Event(ReadOnlySpan<char> Data, EventDriver EventDriver) {
        public override readonly string ToString() => Data;
    }*/
    [DebuggerDisplay("{Data} {EventDriver}")]
    public readonly struct Event
    {
        public readonly ReadOnlyMemory<char> Data;
        public readonly EventDriver EventDriver;

        public override string ToString() => Data.ToString();

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Event other && this == other;

        public override int GetHashCode() => Data.Span.GetHashCodeExtension();

        public static bool operator ==(Event left, Event right) => left.Data.Equals(right.Data) || left.Data.Span.SequenceEqual(right.Data.Span);

        public static bool operator !=(Event left, Event right) => !(left == right);

        public Event(ReadOnlyMemory<char> data, EventDriver eventDriver) {
            this.Data = data;
            this.EventDriver = eventDriver;
        }

        [Obsolete("FOR TESTING PURPOSES ONLY!!!")]
        public Event(string data, EventDriver eventDriver) : this(new ReadOnlyMemory<char>(data.ToCharArray()), eventDriver) { }
    }
}
