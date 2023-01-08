using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class IQRCuller : FilterEventCuller
    {
        public override string DisplayName => "Interquartile Range";
        public override string ToolTipText => $"Ananlze N events with the highest {DisplayName}";
        public override string LongDescription => $"Analyze N events with the highest interquartile range where the interquartile range is the third quartile - the first quartile.\nIQR = Q3 - Q1";

        public IQRCuller()
        {
            AddParameter(
                Name: "numEvents",
                DisplayName: "N",
                DefaultValue: 50,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 40, 45, 50, 75, 100, 150, 200);
            AddParameter(
                Name: "I",
                DisplayName: "Informative",
                DefaultValue: "Least",
                "Most", "Least");
        }

        public override EventSet Train(IEnumerable<EventSet> eventSets)
        {
            var es = eventSets.ToStructEnumerable();

            var numEvents = (int)this["numEvents"];
            var mostInformative = ((string)this["I"]).Equals("Most");

            var histograms = es.Select(x => new EventHistogram(x), x => x).ForceEnumerate(); // Enumerate to avoid reenumeration
            var IQR = new EventHistogram(es.SelectMany(x => x).ToEnumerable()).ToStructEnumerable().Select(x =>
            {
                var freq = histograms.Select(y => (double)y.AbsoluteFrequency(x), x => x).Order().ToArray(); // Enumerate to avoid reenumeration
                var med = freq.Length / 2d;
                double Q1Index = med / 2d;
                double Q1 = 0;
                double Q3 = 0;
                if(med % 1 == 0 && Q1Index % 1 != 0)
                {
                    Q1Index = Math.Round(Q1Index) - 1;
                    var Q3Index = med + Q1Index;
                    Q1 = freq[(int)Q1Index];
                    Q3 = freq[(int)Q3Index];
                } else if(med % 1 == 0 && Q1Index % 1 == 0)
                {
                    var Q3Index = med + Q1Index;
                    Q1 = (freq[(int)Q1Index] + freq[(int)Q1Index - 1]) / 2;
                    Q3 = (freq[(int)Q3Index] + freq[(int)Q3Index - 1]) / 2;
                }
                return new KeyValuePair<Event, double>(x, Q1 - Q3);
            }, x => x);

            return new(IQR.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ForceEnumerate().ToEnumerable());
        }
    }
}
