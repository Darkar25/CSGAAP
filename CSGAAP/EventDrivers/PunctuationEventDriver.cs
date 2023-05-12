using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class PunctuationEventDriver : EventDriver
    {
        public override string DisplayName => "Punctuation";
        public override string ToolTipText => "Non alphanumeric nor whitspace";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(CreateEventSetInternal(text));
        public IEnumerable<Event> CreateEventSetInternal(ReadOnlyMemory<char> text)
        {
            for(int i = 0;i < text.Length;i++)
            {
                var c = text.Span[i];
                if (!(char.IsLetterOrDigit(c) || char.IsNumber(c)))
                    yield return new(text[i..(i+1)], this);
            }
        }
    }
}
