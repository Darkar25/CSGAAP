using CSGAAP.Backend;
using CSGAAP.Generics;
using CSGAAP.Properties;
using CSGAAP.Util;
using System.ComponentModel;
using System.Text;

namespace CSGAAP.EventDrivers
{
    public class ReactionTimeEventDriver : NaiveWordEventDriver, INotifyPropertyChanged
    {
        public override string DisplayName => "Lexical Decision Reaction Times";
        public override string ToolTipText => "Reaction times from English Lexicon Project";
        public override bool ShowInGUI => API.Instance.Language.Lang == "english";

        public ReactionTimeEventDriver()
        {
            API.Instance.PropertyChanged += Language_Changed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Language_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Language") PropertyChanged?.Invoke(this, new(nameof(ShowInGUI)));
        }

        private static Dictionary<string, ReadOnlyMemory<char>> ReactionTimes { get; } = Encoding.Default.GetString(Resources.ELPrt)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Where(x => !x.StartsWith('#'))
            .Select(x => x.Split("/", StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(x => x[0], x => new ReadOnlyMemory<char>(x[1].ToCharArray()));

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(InternalCreateEventSet(text));

        private IEnumerable<Event> InternalCreateEventSet(ReadOnlyMemory<char> text)
        {
            foreach (var w in base.CreateEventSet(text))
                if (ReactionTimes.TryGetValue(w.ToString(), out var o))
                    yield return new(o, this);
        }
    }
}
