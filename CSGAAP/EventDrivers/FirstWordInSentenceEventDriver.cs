using CSGAAP.Generics;
using CSGAAP.Util;
using System.Text.RegularExpressions;

namespace CSGAAP.EventDrivers
{
    public partial class FirstWordInSentenceEventDriver : SentenceEventDriver
    {
        public override string DisplayName => "First Word In Sentence";
        public override string ToolTipText => DisplayName;

        public override EventSet CreateEventSet(string text) => new(InternalCreateEventSet(text));

        private IEnumerable<Event> InternalCreateEventSet(string text)
        {
            foreach (var s in base.CreateEventSet(text))
            {
                var words = WordRegex().Split(s.ToString());
                if (words.Length > 0)
                    yield return new(words[0], this);
            }
            yield break;
        }

        [GeneratedRegex("\\s+")]
        private static partial Regex WordRegex();
    }
}
