namespace CSGAAP.Generics
{
    public interface IDisplayable
    {
        public abstract string DisplayName { get; }
        public virtual string ToolTipText => DisplayName;
        public virtual string LongDescription => LongDescription;
        public virtual bool ShowInGUI => true;
    }
}
