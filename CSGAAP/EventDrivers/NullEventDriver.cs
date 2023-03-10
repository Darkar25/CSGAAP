using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class NullEventDriver : EventDriver
    {
        public override string DisplayName => "Null Event Set";

        public override string ToolTipText => "Null events for debugging previous sets";

        public override bool ShowInGUI => false;

        public override EventSet CreateEventSet(string text) => new(new Event[] { new Event(text, this) });
    }
}
