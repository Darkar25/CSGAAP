﻿using CSGAAP.Generics;
using CSGAAP.Util;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class CoefficientOfVariation : FilterEventCuller
    {
        public override string DisplayName => "Coefficient of Variation";
        public override string ToolTipText => $"Analyze N events with the lowest {DisplayName}";
        public override string LongDescription => $"{ToolTipText}\nCoV = (\u03C3/\u03BC)*100";

        public CoefficientOfVariation()
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
            var CoV = new EventHistogram(es.SelectMany(x => x).ToEnumerable()).ToStructEnumerable().Select(x =>
            {
                var freq = histograms.Select(y => (double)y.AbsoluteFrequency(x), x => x).ForceEnumerate(); // Enumerate to avoid reenumeration
                var mean = freq.Average();
                var stddev = 0d;
                if (freq.Count > 1)
                {
                    //Perform the Sum of (value-avg)^2
                    double sum = freq.Select(d => (d - mean) * (d - mean), x => x).Sum(x => x);

                    //Put it all together
                    stddev = Math.Sqrt(sum / (double)(freq.Count - 1));
                }
                return new KeyValuePair<Event, double>(x, (stddev / mean) * 100d);
            }, x => x);

            return new(CoV.OrderBy(x => mostInformative ? -x.Value : x.Value).Select(x => x.Key, x => x).Take(numEvents, x => x).ForceEnumerate().ToEnumerable());
        }
    }
}
