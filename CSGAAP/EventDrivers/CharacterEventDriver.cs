using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class CharacterEventDriver : EventDriver
    {
        public override string DisplayName => "Characters";
        public override string ToolTipText => "UNICODE Characters";

        public override EventSet CreateEventSet(string text) => new(text.Select(x => new Event(x.ToString(), this)));
    }
}
