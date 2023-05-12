namespace CSGAAP.Generics
{
    public abstract class EventDriver : Parameterizable, IDisplayable, IComparable<EventDriver>
    {
        public abstract string DisplayName { get; }
        public virtual string ToolTipText => DisplayName;
        public virtual string LongDescription => ToolTipText;
        public virtual bool ShowInGUI => true;

        public int CompareTo(EventDriver? other) => DisplayName.CompareTo(other?.DisplayName);

        public abstract EventSet CreateEventSet(ReadOnlyMemory<char> text);
    }
}
