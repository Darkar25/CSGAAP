using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using CSGAAP.Generics;

namespace CSGAAP.Util
{
    public class Document : INotifyPropertyChanged
    {
        private string? _author;
        public string? Author {
            get => _author;
            set {
                _author = value;
                PropertyChanged?.Invoke(this, new(nameof(Author)));
            }
        }
        public bool IsKnownAuthor => Author is not null;
        public string Title { get; } = "";
        public Uri? URI { get; init; }
        public string Text { get; private set; } = "";
        public DocumentType Type { get; init; }
        public Dictionary<EventDriver, EventSet> EventSets { get; } = new();
        public Dictionary<AnalysisDriver, IEnumerable<KeyValuePair<string, double>>> Results { get; } = new();

        public Document(string filepath, string author) : this(filepath, author, "") { }
        public Document(string filepath = "", string author = "", string title = "")
        {
            if (!string.IsNullOrWhiteSpace(filepath)) URI = new(filepath);
            if (string.IsNullOrWhiteSpace(author)) Author = null;
            else Author = author;
            if (string.IsNullOrWhiteSpace(title)) Title = URI?.GetFileName() ?? "";
            else Title = title;
            Type = URI?.GetExtension().ToLowerInvariant() switch
            {
                ".pdf" => DocumentType.PDF,
                ".doc" or ".docx" => DocumentType.DOC,
                ".htm" or ".html" => DocumentType.HTML,
                _ => DocumentType.GENERIC
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task Load(Language lang, CancellationToken token)
        {
            var data = URI is null ? Array.Empty<byte>() : await Utils.LoadData(URI, token);
            if (data.Length == 0) throw new Exception($"Document: {URI!.GetFileName()} was empty.");
            Text = lang.ParseDocument(lang.Charset.GetString(data));
        }

        public string Stringify() => new(Text);

        public void Canonicize(IEnumerable<Canonicizer> canonicizers)
        {
            var text = Text;
            foreach (var canon in canonicizers)
                text = canon.Process(text);
            Text = text;
        }

        public override string ToString() => Title + "(" + (Author ?? "unknown") + ")";
    }
}
