using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class SentenceLengthWithWordsEventDriver : SentenceEventDriver
    {
        public override string DisplayName => "Sentence Length";

        public override string ToolTipText => "Sentence length (With words)";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(CreateEventSetInternal(text));
        public IEnumerable<Event> CreateEventSetInternal(ReadOnlyMemory<char> text)
        {
            foreach (var sentence in base.CreateEventSet(text))
            {
                var words = 0;
                var lastSpace = 0;
                for (int i = 0; i < sentence.Data.Length; i++)
                {
                    if (sentence.Data.Span[i] != ' ') continue;
                    if (i - lastSpace > 1) words++;
                    lastSpace = i;
                }
                words++;
                yield return new(new ReadOnlyMemory<char>(words.ToString().ToCharArray()), this);
            }
        }
    }
}
