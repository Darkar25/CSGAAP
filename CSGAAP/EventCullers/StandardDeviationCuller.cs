using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class StandardDeviationCuller : FilterEventCuller
    {
        public override string DisplayName => "Standard Deviation Culler";
        public override string ToolTipText => "Analyze N events with highest standard deviation";

        public StandardDeviationCuller()
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
            var es = eventSets.ToStructEnumerable();

            var numEvents = (int)this["numEvents"];
            var mostInformative = ((string)this["I"]).Equals("Most");

            var histograms = es.Select(x => new EventHistogram(x), x => x).ForceEnumerate(); // Enumerate to avoid reenumeration
            var stddev = new EventHistogram(es.SelectMany(x => x).ToEnumerable()).ToStructEnumerable().Select(x =>
            {
                var freq = histograms.Select(y => (double)y.AbsoluteFrequency(x), x => x);
                return new KeyValuePair<Event, double>(x, freq.StdDev());
            }, x => x);

            return new(stddev.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ToEnumerable());
        }
    }
}
