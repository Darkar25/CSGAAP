using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class LeastCommonEvents : FilterEventCuller
    {
        public override string DisplayName => "Least Common Events";
        public override string ToolTipText => "Analyze only the N least common events across all documents";
        public override string LongDescription => "Analyze only the N rarest events across all documents; the value of N is passed as a parameter (numEvents).";

        public LeastCommonEvents()
        {
            AddParameter(
                Name: "numEvents",
                DisplayName: "N",
                DefaultValue: 50,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 40, 45, 50, 75, 100, 150, 200);
        }

        public override EventSet Train(IEnumerable<EventSet> eventSets) => new(new EventHistogram(eventSets.SelectMany(x => x)).Sorted.ToStructEnumerable().Reverse(x => x).Select(x => x.Key, x => x).Take((int)this["numEvents"], x => x).ToEnumerable());
    }
}
