using CSGAAP.Backend;
using CSGAAP.Generics;
using CSGAAP.Properties;
using CSGAAP.Util;
using System.ComponentModel;
using System.Text;

namespace CSGAAP.EventDrivers
{
    public class NamingTimeEventDriver : NaiveWordEventDriver, INotifyPropertyChanged
    {
        public override string DisplayName => "Naming Reaction Times";
        public override string ToolTipText => "Naming Times from English Lexicon Project";
        public override bool ShowInGUI => API.Instance.Language.Lang == "english";

        public NamingTimeEventDriver()
        {
            API.Instance.PropertyChanged += Language_Changed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Language_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Language") PropertyChanged?.Invoke(this, new(nameof(ShowInGUI)));
        }

        private static Dictionary<string, string> NamingTimes { get; } = Encoding.Default.GetString(Resources.ELPnaming)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Where(x => !x.StartsWith('#'))
            .Select(x => x.Split("/", StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(x => x[0], x => x[1]);

        public override EventSet CreateEventSet(string text) => new(InternalCreateEventSet(text));

        private IEnumerable<Event> InternalCreateEventSet(string text)
        {
            foreach (var w in base.CreateEventSet(text))
                if (NamingTimes.TryGetValue(w.ToString(), out var o))
                    yield return new(o, this);
            yield break;
        }
    }
}
