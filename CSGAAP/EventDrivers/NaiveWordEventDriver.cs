using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public partial class NaiveWordEventDriver : EventDriver
    {
        public override string DisplayName => "Words";
        public override string ToolTipText => "Words (White Space as Separators)";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(CreateEventSetInternal(text));
        public IEnumerable<Event> CreateEventSetInternal(ReadOnlyMemory<char> text)
        {
            var lastSpace = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (!" \n\t\r".Contains(text.Span[i])) continue;
                if (i - lastSpace >= 1) yield return new(text[lastSpace..i], this);
                lastSpace = i + 1;
            }
            if(text.Length - lastSpace >= 1) yield return new(text[lastSpace..].TrimEnd(), this);
        }
    }
}
