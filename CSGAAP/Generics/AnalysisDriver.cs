using CSGAAP.Util;

namespace CSGAAP.Generics
{
    public abstract class AnalysisDriver : Parameterizable, IDisplayable, IComparable<AnalysisDriver>
    {
        public abstract string Name { get; }
        public virtual bool SupportsDistance => true;
        private DistanceFunction? _distance;
        public DistanceFunction Distance
        {
            get => _distance;
            set
            {
                if (!SupportsDistance) return;
                _distance = value;
            }
        }
        public string DisplayName => Name + (SupportsDistance ? " with metric " + Distance.DisplayName : "");
        public virtual string ToolTipText => Name;
        public virtual string LongDescription => ToolTipText;
        public virtual bool ShowInGUI => true;

        public int CompareTo(AnalysisDriver? other) => DisplayName.CompareTo(other?.DisplayName);

        public abstract void Train(IEnumerable<Document> knownDocuments);
        public abstract IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument);
    }
}
