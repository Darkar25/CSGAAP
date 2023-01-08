using CSGAAP.Util;
using StructLinq;
using StructLinq.IEnumerable;
using System.Collections;

namespace CSGAAP.Generics
{
    public abstract class EventCuller : Parameterizable, IDisplayable, IComparable<EventCuller>
    {
        public abstract string DisplayName { get; }
        public virtual string ToolTipText => DisplayName;
        public virtual string LongDescription => DisplayName;
        public virtual bool ShowInGUI => true;

        public int CompareTo(EventCuller? other) => DisplayName.CompareTo(other?.DisplayName);

        public abstract void Initialize(IEnumerable<EventSet> eventSets);
        public abstract EventSet Cull(EventSet eventSet);
    }
}
