using CSGAAP.Util;
using System.Reflection;

namespace CSGAAP.Generics
{
    public abstract class Canonicizer : Parameterizable, IDisplayable, IComparable<Canonicizer>
    {
        public abstract string DisplayName { get; }
        public virtual string ToolTipText => DisplayName;
        public virtual string LongDescription => DisplayName;
        public virtual bool ShowInGUI => true;

        public override string ToString() => DisplayName;
        public override int GetHashCode() => DisplayName.ToLower().GetHashCode();
        public override bool Equals(object? obj) => obj is Canonicizer o && DisplayName.Equals(o.DisplayName, StringComparison.OrdinalIgnoreCase);

        public int CompareTo(Canonicizer? other) => DisplayName.CompareTo(other?.DisplayName);

        public abstract string Process(string text);

        public Canonicizer()
        {
            AddParameter(
                Name: "docType",
                DisplayName: "Document type",
                DefaultValue: DocumentType.ALL,
                DocumentType.ALL, DocumentType.DOC, DocumentType.GENERIC, DocumentType.HTML, DocumentType.PDF);
        }
    }
}
