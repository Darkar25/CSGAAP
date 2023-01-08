using CSGAAP.Generics;

namespace CSGAAP.EventCullers
{
    public class SetCuller : EventCuller
    {
        public override string DisplayName => "Set Culler";
        public override string ToolTipText => "Remove duplicate events from each event set.";
        public override bool ShowInGUI => false;

        public override EventSet Cull(EventSet eventSet) => new(eventSet.Distinct());

        public override void Initialize(IEnumerable<EventSet> eventSets) { }
    }
}
