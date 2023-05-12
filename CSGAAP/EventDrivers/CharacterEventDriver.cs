using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class CharacterEventDriver : EventDriver
    {
        public override string DisplayName => "Characters";
        public override string ToolTipText => "UNICODE Characters";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(CreateEventSetInternal(text));

        public IEnumerable<Event> CreateEventSetInternal(ReadOnlyMemory<char> text)
        {
            for (int i = 0; i < text.Length; i++)
                yield return new(text[i..(i + 1)], this);
        }
    }
}
