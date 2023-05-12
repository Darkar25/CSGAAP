using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;

namespace CSGAAP.Classifiers
{
    public class AbsoluteCentroidDriver : AnalysisDriver
    {
        public override string Name => "Absolute Centroid Driver";
        public override string ToolTipText => "Computes one centroid per author.\nCentroids are the frequency of events over all documents provided.\n";

        private Dictionary<string, AbsoluteHistogram> KnownHistograms { get; set; } = new();

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            IHistogram histogram = new AbsoluteHistogram(unknownDocument);
            return KnownHistograms
                .ToDictionary(x => x.Key, x => {
                    try
                    {
                        var current = Distance.Distance(histogram, x.Value);
                        Log.Debug($"{unknownDocument.Title} ({unknownDocument.URI?.AbsolutePath}) -> {x.Key}:{current}");
                        return current;
                    }
                    catch (DistanceCalculationException e)
                    {
                        Log.Fatal(e, $"Distance {Distance.DisplayName} failed");
                        throw new AnalyzeException($"Distance {Distance.DisplayName} failed", e);
                    }
                })
                .OrderBy(x => x.Value);
        }

        public override void Train(IEnumerable<Document> knownDocuments) => KnownHistograms = knownDocuments
            .GroupBy(x => x.Author)
            .ToDictionary(x => x.Key!, x => AbsoluteHistogram.Centroid(x
                .Select(x => new AbsoluteHistogram(x))));
    }
}
