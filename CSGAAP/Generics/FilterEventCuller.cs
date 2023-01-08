using CSGAAP.Languages;
using CSGAAP.Util;
using StructLinq;
using StructLinq.IEnumerable;

namespace CSGAAP.Generics
{
    public abstract class FilterEventCuller : EventCuller
    {
        private EventSet? events;

        public abstract EventSet Train(IEnumerable<EventSet> eventSets);

        public override void Initialize(IEnumerable<EventSet> eventSets) => events = Train(eventSets);

        public override EventSet Cull(EventSet eventSet) => new(eventSet.Union(events!));
    }
}
