using CSGAAP.Generics;
using CSGAAP.Properties;
using CSGAAP.Util;
using System.Text;
using System.Text.RegularExpressions;
using Serilog;

namespace CSGAAP.EventDrivers
{
    public partial class SentenceEventDriver : EventDriver
    {
        public override string DisplayName => "Sentences";
        public override string ToolTipText => "Full sentences including punctuation";

        private static readonly Regex regex;

        static SentenceEventDriver() {
            var abbreviations = Encoding.Default.GetString(Resources.abbreviation).Split('\n', StringSplitOptions.RemoveEmptyEntries).SelectMany(x => new string[] { x, x.ToUpper(), x.ToLower() });
            regex = new($@".*({string.Join("|", abbreviations)})\s?[?!\.]$");
        }

        public override EventSet CreateEventSet(string text)
        {
            Log.Debug(regex.ToString());
            var sentences = SentenceRegex().Split(text);
            StringBuilder edgeCase = new();
            List<Event> events = new();
            foreach(var x  in sentences) {
                edgeCase.Append(x);
                if(!regex.IsMatch(x)) {
                    events.Add(new Event(edgeCase.ToString(), this));
                    edgeCase.Clear();
                } else
                    edgeCase.Append(' ');
            }
            if (edgeCase.Length > 0)
                events.Add(new(edgeCase.ToString(), this));

            return new(events);
        }

        [GeneratedRegex("(?<=[?!\\.])\\s+")]
        private static partial Regex SentenceRegex();
    }
}
