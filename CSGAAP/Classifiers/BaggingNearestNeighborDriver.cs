using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;
using StructLinq;

namespace CSGAAP.Classifiers
{
    public class BaggingNearestNeighborDriver : AnalysisDriver
    {
        public override string Name => "Bagging Nearest Neighbor";
        public override string ToolTipText => "Nearest Neighbor using bagging to generated the traing data.";
        public override bool ShowInGUI => false;

        private ILookup<string, EventMap>? AuthorHistograms { get; set; }

        public BaggingNearestNeighborDriver()
        {
            AddParameter(
                Name: "Samples",
                DisplayName: "Samples",
                DefaultValue: 5,
                1, 5, 10, 15, 20, 25, 30, 40, 50);
            AddParameter(
                Name: "SampleSize",
                DisplayName: "Sample Size",
                DefaultValue: 500,
                500, 750, 1_000, 1_500, 2_000, 3_000, 4_000, 5_000, 10_000);
            AddParameter(
                Name: "Score",
                DisplayName: "Score",
                DefaultValue: false,
                true, false);
        }

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            List<KeyValuePair<string, double>> res = new();
            var unknown = unknownDocument.EventSets.Values.SelectMany(x => x);
            EventMap histogram = new(new EventSet(unknown));
            foreach(var entry in AuthorHistograms!)
                foreach(var knownHistogram in entry)
                    try
                    {
                        res.Add(new(entry.Key, Distance.Distance(histogram, knownHistogram)));
                    } catch(DistanceCalculationException e)
                    {
                        Log.Fatal(e, $"Distance {Distance.DisplayName} failed");
                        throw new AnalyzeException($"Distance {Distance.DisplayName} failed", e);
                    }
            if ((bool)this["score"])
            {
                int samples = (int)this["samples"];
                return res
                    .OrderBy(x => x.Value)
                    .Take(samples)
                    .GroupBy(x => x.Key)
                    .Select(x => new KeyValuePair<string, double>(x.Key, x.Count() / (double)samples))
                    .OrderByDescending(x => x.Value);
            }
            return res.OrderBy(x => x.Value);
        }

        public override void Train(IEnumerable<Document> knownDocuments)
        {
            int samples = (int)this["samples"];
            int sampleSize = (int)this["sampleSize"];
            AuthorHistograms = knownDocuments
                .GroupBy(x => x.Author!)
                .ToStructEnumerable()
                .Select(x => new KeyValuePair<string, EventBag>(x.Key, new EventBag(x
                    .SelectMany(x => x.EventSets
                        .SelectMany(y => y.Value)))), x => x)
                .Select(x => Tuple.Create(x.Key, new EventMap(new EventSet(x.Value.Next(sampleSize)))), x => x)
                .Take(samples, x => x)
                .ToEnumerable()
                .ToLookup(x => x.Item1, x => x.Item2);
        }
    }
}
