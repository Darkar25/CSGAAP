using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class WeightedVariance : FilterEventCuller
    {
        public override string DisplayName => "Standard Deviation Culler";
        public override string ToolTipText => "Analyze N events with highest standard deviation";

        public WeightedVariance()
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
            var hist = new EventHistogram(es.SelectMany(x => x).ToEnumerable());
            var histograms = es.Select(x => new EventHistogram(x)).ForceEnumerate(); // Enumerate to avoid reenumeration
            var WV = hist.ToStructEnumerable().Select(x =>
            {
                var percentage = hist.RelativeFrequency(x);
                var freq = histograms.Select(y => (double)y.AbsoluteFrequency(x), x => x);
                var mean = histograms.Select(y => percentage * y.AbsoluteFrequency(x), x => x).Sum(x => x);
                return new KeyValuePair<Event, double>(x, freq.Select(y => percentage * Math.Pow(y - mean, 2), x => x).Sum(x => x));
            }, x => x);

            return new(WV.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ToEnumerable());
        }
    }
}
