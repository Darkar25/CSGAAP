using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.Classifiers
{
    public class BurrowsDelta : AnalysisDriver
    {
        private ILookup<string, EventMap>? knownHistograms;
        private IEnumerable<Event>? events;
        private Dictionary<string, EventMap>? knownCentroids;
        private Dictionary<Event, double>? eventStddev;

        public override string Name => "Burrows Delta";
        public override string ToolTipText => "Burrow's Delta with Argamon's Formula";
        public override bool SupportsDistance => false;

        public BurrowsDelta()
        {
            AddParameter(
                Name: "centroid",
                DisplayName: "Centroid Model",
                DefaultValue: false,
                true, false);
        }

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            EventMap unknownEventMap = new(unknownDocument);
            if ((bool)this["centroid"])
                return knownCentroids!
                    .ToDictionary(x => x.Key, x => events!
                        .Sum(y => Math.Abs((unknownEventMap.RelativeFrequency(y) - x.Value.RelativeFrequency(y)) / eventStddev![y])))
                    .OrderBy(x => x.Value);
            return knownHistograms!
                .SelectMany(x => x
                    .Select(y => new KeyValuePair<string, double>(x.Key, events!
                        .Sum(z => Math.Abs((unknownEventMap.RelativeFrequency(z) - y.RelativeFrequency(z)) / eventStddev![z])))))
                .OrderBy(x => x.Value);
        }

        public override void Train(IEnumerable<Document> knownDocuments)
        {
            knownHistograms = knownDocuments.ToLookup(x => x.Author!, x => new EventMap(x));
            events = knownHistograms.SelectMany(x => x.SelectMany(x => x.UniqueEvents));
            if ((bool)this["centroid"])
            {
                knownCentroids = knownHistograms.ToDictionary(x => x.Key, EventMap.Centroid);
                eventStddev = events
                    .ToDictionary(x => x, x => knownCentroids.Values
                        .Select(y => y.RelativeFrequency(x))
                        .StdDev());
            }
            else
                eventStddev = events
                    .ToDictionary(x => x, x => knownHistograms
                        .SelectMany(y => y
                            .Select(z => z.RelativeFrequency(x)))
                        .StdDev());
        }
    }
}
