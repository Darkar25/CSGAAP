using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventCullers
{
    public class PercentageRangeCuller : EventCuller
    {
        public override string DisplayName => "Percentage Range Event Culler";
        public override string ToolTipText => "Analyze only the words whose relative frequency is between X% and Y%";
        public override string LongDescription => "Analyze only events whose relative frequency is in a percentage range across all documents (e.g., the words whose relative frequncy is between 0.10% and 0.15% in the corpus). The parameter minPercent is the lower bound of the events to be included, in Double form, (e.g. 0.0010 in the example above), while maxPercent is the upper bound of the events to be included, in Double form, (e.g. 0.0015).\nAny floating point value can be entered which can be stored in the Java Double data type.\nIf either parameter falls outside the 0.0 to 1.0 range, they are reduced or raised to be either 0.0 or 1.0 depending on where they fall out of the range. If minPercent is greater than maxPercent, the two values are switched.";
        public override bool ShowInGUI => false;

        private double minPercent;
        private double maxPercent;
        private EventHistogram? hist;

        public PercentageRangeCuller()
        {
            AddParameter(
                Name: "minPercent",
                DisplayName: "min",
                DefaultValue: .0025,
                0, .0025, .005, .0075, .01, .05, .1);
            AddParameter(
                Name: "maxPercent",
                DisplayName: "max",
                DefaultValue: .0075,
                .0025, .005, .0075, .01, .05, .1, 1);
        }

        public override EventSet Cull(EventSet eventSet) => new(eventSet.Where(x => hist!.RelativeFrequency(x) >= minPercent && hist!.RelativeFrequency(x) <= maxPercent));

        public override void Initialize(IEnumerable<EventSet> eventSets)
        {
            minPercent = Math.Min(Math.Max((double)this["minPercent"], 0), 1);
            maxPercent = Math.Max(Math.Min((double)this["maxPercent"], 1), 0);
            if (minPercent > maxPercent) (minPercent, maxPercent) = (maxPercent, minPercent);
            this["minPercent"] = minPercent;
            this["maxPercent"] = maxPercent;
            hist = new(eventSets.SelectMany(x => x));
        }
    }
}
