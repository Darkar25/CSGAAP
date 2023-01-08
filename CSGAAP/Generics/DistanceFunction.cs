namespace CSGAAP.Generics
{
    public abstract class DistanceFunction : Parameterizable, IDisplayable
    {
        public abstract string DisplayName { get; }
        public virtual string ToolTipText => DisplayName;
        public virtual string LongDescription => ToolTipText;
        public virtual bool ShowInGUI => true;

        public abstract double Distance(IHistogram h1, IHistogram h2);
    }
}
