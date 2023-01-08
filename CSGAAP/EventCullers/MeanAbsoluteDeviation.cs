using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class MeanAbsoluteDeviation : FilterEventCuller
    {
        public override string DisplayName => "Mean Absolute Deviation";
        public override string ToolTipText => $"Analyze N events with the highest {DisplayName}";
        public override string LongDescription => $"{ToolTipText}\nMAD = 1/n \u03A3 for i = 1 to n |xi - \u03BC|";

        public MeanAbsoluteDeviation()
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
            var hist = new EventHistogram(es.SelectMany(x => x).ToEnumerable());
            var MAD = hist.ToStructEnumerable().Select(x =>
            {
                var freq = histograms.Select(y => y.AbsoluteFrequency(x), x => x).ForceEnumerate(); // Enumerate to avoid reenumeration
                double avg = freq.Sum(x => x) / (double)freq.Count;
                return new KeyValuePair<Event, double>(x, 1d / (double)freq.Count * freq.Select(y => Math.Abs(y-avg), x => x).Sum(x => x));
            }, x => x);

            return new(MAD.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ToEnumerable());
        }
    }
}
