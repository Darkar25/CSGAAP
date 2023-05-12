using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public partial class FirstWordInSentenceEventDriver : SentenceEventDriver
    {
        public override string DisplayName => "First Word In Sentence";
        public override string ToolTipText => DisplayName;

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(InternalCreateEventSet(text));

        private IEnumerable<Event> InternalCreateEventSet(ReadOnlyMemory<char> text)
        {
            foreach (var s in base.CreateEventSet(text))
            {
                var index = s.Data.Span.IndexOf(' ');
                if (index > 0)
                    yield return new(s.Data[..index], this);
            }
        }
    }
}
