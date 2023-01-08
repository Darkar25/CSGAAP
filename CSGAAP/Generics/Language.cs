using System.Text;

namespace CSGAAP.Generics
{
    public abstract class Language : IDisplayable, IComparable<Language>
    {
        public abstract string Name { get; }
        public abstract string Lang { get; }
        public abstract Encoding Charset { get; }
        public string DisplayName => Name;
        public virtual string ToolTipText => DisplayName;
        public virtual string LongDescription => DisplayName;
        public virtual bool ShowInGUI => true;

        public int CompareTo(Language? other) => Name.CompareTo(other?.Name);
        public override int GetHashCode() => Name.ToLower().GetHashCode();
        public override bool Equals(object? obj) => obj is Language o && Name.Equals(o.Name, StringComparison.OrdinalIgnoreCase);

        // this counts as "unparsable" language...to make the language parsable override this method...
        public virtual string ParseDocument(string document) => document;
    }
}
