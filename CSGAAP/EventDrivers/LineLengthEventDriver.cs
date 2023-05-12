using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class LineLengthEventDriver : NewLineEventDriver
    {
        public override string DisplayName => "Line Length";

        public override string ToolTipText => "Gets the number of words per line of text.";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(CreateEventSetInternal(text));
        public IEnumerable<Event> CreateEventSetInternal(ReadOnlyMemory<char> text)
        {
            foreach (var line in base.CreateEventSet(text))
            {
                var words = 0;
                var lastSpace = 0;
                for (int i = 0; i < line.Data.Length; i++)
                {
                    if (line.Data.Span[i] != ' ') continue;
                    if (i - lastSpace > 1) words++;
                    lastSpace = i;
                }
                if (line.Data.Length - lastSpace > 1) words++;
                yield return new(new ReadOnlyMemory<char>(words.ToString().ToCharArray()), this);
            }
        }
        //=> new(base.CreateEventSet(text).Select(x => new Event(SplitRegex().Split(x.ToString().Trim()).Length.ToString(), this)));
    }
}
