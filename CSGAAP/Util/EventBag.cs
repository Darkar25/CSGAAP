namespace CSGAAP.Util
{
    public class EventBag : Bag<Event>
    {
        public EventBag() : base() { }
        public EventBag(IEnumerable<Event> events) : base(events) { }
    }
}
