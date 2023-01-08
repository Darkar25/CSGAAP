using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class RangeCuller : FilterEventCuller
    {
        public override string DisplayName => "Range Culler";
        public override string ToolTipText => "Analyze N events with the highest ranges";
        public override string LongDescription => "Analyze N events with the highest frequency range";

        public RangeCuller()
        {
            AddParameter(
                Name: "numEvents",
                DisplayName: "N",
                DefaultValue: 50,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 40, 45, 50, 75, 100, 150, 200);
            AddParameter(
                Name: "I",
                DisplayName: "Informative",
                DefaultValue: "Most",
                "Most", "Least");
        }

        public override EventSet Train(IEnumerable<EventSet> eventSets)
        {
            var numEvents = (int)this["numEvents"];
            var mostInformative = ((string)this["I"]).Equals("Most");

            var es = eventSets.ToStructEnumerable();
            var histograms = es.Select(x => new EventHistogram(x), x => x).ForceEnumerate(); // Enumerate to avoid reenumeration
            var range = new EventHistogram(es.SelectMany(x => x).ToEnumerable()).ToStructEnumerable().Select(x =>
            {
                var freq = histograms.Select(y => y.AbsoluteFrequency(x), x => x);
                return new KeyValuePair<Event, double>(x, freq.Last(x => x) - freq.First(x => x));
            });

            return new(range.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ToEnumerable());
        }
    }
}
