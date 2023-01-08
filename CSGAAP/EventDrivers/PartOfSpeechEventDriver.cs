using CSGAAP.Backend;
using CSGAAP.Canonicizers;
using CSGAAP.Generics;
using CSGAAP.Util;
using edu.stanford.nlp.tagger.maxent;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace CSGAAP.EventDrivers
{
    public partial class PartOfSpeechEventDriver : EventDriver, INotifyPropertyChanged
    {
        private static readonly PunctuationSeparator separator = new();

        public override string DisplayName => "POS";
        public override string ToolTipText => "Parts of Speech";

        public override bool ShowInGUI => API.Instance.Language.Lang == "english";

        public PartOfSpeechEventDriver()
        {
            API.Instance.PropertyChanged += Language_Changed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Language_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Language") PropertyChanged?.Invoke(this, new(nameof(ShowInGUI)));
        }

        private static readonly MaxentTagger tagger = new(Encoding.Unicode.GetString(Properties.Resources.english_left3words_distsim));

        public override EventSet CreateEventSet(string text) {
            
            var strtext = separator.Process(text);
            return new(OuterRegex().Split(strtext).SelectMany(x => InnerRegex().Split(x)).Select(x => {
                var tagged = tagger.tagString(x);
                if (!string.IsNullOrEmpty(tagged))
                    tagged = tagged[(tagged.LastIndexOf('_') + 1)..];

                return new Event(tagged, this);
            }));
        }

        [GeneratedRegex("(?<=[?!\\.])\\s+")]
        private static partial Regex OuterRegex();
        [GeneratedRegex("\\s")]
        private static partial Regex InnerRegex();
    }
}
