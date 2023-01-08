using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class InformationGain : FilterEventCuller
    {
        public override string DisplayName => "Information Gain";
        public override string ToolTipText => "Analyze only the N most or least informative events across all documents";
        public override string LongDescription => $"{DisplayName}\n{ToolTipText}\nIG = log(i = 1 to n \u03A0mi!/((i=1 to n \u03A3mi)!(i=1 to n \u03A0Pi^mi)))\n";

        public InformationGain()
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

            double percentage = 0;
            double numerator = 1;
            double denom1 = 0;
            double denom2 = 1;

            var es = eventSets.ToStructEnumerable();

            var histograms = es.Select(x => new EventHistogram(x), x => x).ForceEnumerate(); // Enumerate to avoid reenumeration
            var hist = new EventHistogram(es.SelectMany(x => x).ToEnumerable());
            var ig = hist.ToStructEnumerable().Select(x =>
            {
                percentage = hist.RelativeFrequency(x);
                foreach (var eh in histograms)
                {
                    int mi = eh.AbsoluteFrequency(x);
                    numerator *= Factorial(mi);
                    denom1 += mi;
                    denom2 *= Math.Pow(percentage, mi);
                }
                denom1 = Factorial(denom1);
                denom1 *= denom2;
                numerator = Math.Ceiling(numerator / denom1);
                denom1 = 0;
                denom2 = 1;
                numerator = 1;
                return new KeyValuePair<Event, double>(x, Math.Log(numerator));
            }, x => x);

            var a = ig.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).ToArray();
            return new(ig.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ToEnumerable());
        }

        private static double Factorial(double val)
        {
            if ((int)val == 0) return 1d;
            var ret = 1d;
            for (int i = (int)val; i > 0; i--)
            {
                ret *= val;
                val--;
            }
            return ret;
        }
    }
}
