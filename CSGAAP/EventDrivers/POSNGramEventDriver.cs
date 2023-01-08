using CSGAAP.Backend;
using CSGAAP.Generics;
using System.ComponentModel;

namespace CSGAAP.EventDrivers
{
    public class POSNGramEventDriver : NGramEventDriver, INotifyPropertyChanged
    {
        public override string DisplayName => "POS NGrams";

        public override string ToolTipText => "Groups of N Successive Parts-of-Speach";

        public override bool ShowInGUI => API.Instance.Language.Lang == "english";

        public POSNGramEventDriver()
        {
            API.Instance.PropertyChanged += Language_Changed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Language_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Language") PropertyChanged?.Invoke(this, new(nameof(ShowInGUI)));
        }

        public override EventSet CreateEventSet(string text) => TransformToNGram(new PartOfSpeechEventDriver().CreateEventSet(text));
    }
}
