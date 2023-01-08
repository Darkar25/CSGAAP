using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class VarianceCuller : FilterEventCuller
    {
        public override string DisplayName => "Variance Culler";
        public override string LongDescription => "Ananlyze N events with the highest variance\n1/n \u03A3 for i = 1 to n (xi - \u03BC)\u00B2";

        public VarianceCuller()
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
            var ret = new EventHistogram(es.SelectMany(x => x).ToEnumerable()).ToStructEnumerable().Select(x =>
            {
                var freq = histograms.Select(y => (double)y.AbsoluteFrequency(x), x => x).ForceEnumerate(); // Enumerate to avoid reenumeration
                double avg = freq.Sum(x => x) / (double)freq.Count;
                return new KeyValuePair<Event, double>(x, 1d / (double)freq.Count * freq.Select(y => Math.Pow(y-avg, 2), x => x).Sum(x => x));
            }, x => x);

            return new(ret.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ToEnumerable());
        }
    }
}
