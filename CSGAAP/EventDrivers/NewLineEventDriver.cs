using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class NewLineEventDriver : EventDriver
    {
        public override string DisplayName => "New Lines";
        public override string ToolTipText => "Events are split on contiguous groups of \\n";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(CreateEventSetInternal(text));
        public IEnumerable<Event> CreateEventSetInternal(ReadOnlyMemory<char> text)
        {
            var last = 0;
            for(int i = 0;i < text.Length;i++)
            {
                if (text.Span[i] != '\n' && text.Span[i] != '\r') continue;
                if (i - last >= 1) yield return new(text[last..i], this);
                last = i + 1;
            }
            if (text.Length - last >= 1) yield return new(text[last..], this);
        }
    }
}
