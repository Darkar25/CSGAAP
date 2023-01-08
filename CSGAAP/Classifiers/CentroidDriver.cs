using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;
using System.Security.AccessControl;

namespace CSGAAP.Classifiers
{
    public class CentroidDriver : AnalysisDriver
    {
        private IDictionary<string, IHistogram>? knownCentroids;

        public override string Name => "Centroid Driver";
        public override string ToolTipText => "Computes one centroid per author.\nCentroids are the average relative frequency of events over all documents provided.\ni=1 to n \u03A3frequencyIn_i(event)/n";

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            IHistogram unknownHistogram = new EventMap(unknownDocument);
            return knownCentroids!
                .Select(x => {
                    try
                    {
                        var current = Distance.Distance(unknownHistogram, x.Value);
                        Log.Debug($"{unknownDocument.Title} ({unknownDocument.URI?.AbsolutePath}) -> {x.Key}:{current}");
                        return new KeyValuePair<string, double>(x.Key, current);
                    }
                    catch (DistanceCalculationException e)
                    {
                        Log.Fatal(e, $"Distance {Distance.DisplayName} failed");
                        throw new AnalyzeException($"Distance {Distance.DisplayName} failed");
                    }
                })
                .OrderBy(x => x.Value);
        }

        public override void Train(IEnumerable<Document> knownDocuments) => knownCentroids = (IDictionary<string, IHistogram>)knownDocuments
            .ToLookup(x => x.Author!, x => new EventMap(x))
            .ToDictionary(x => x.Key, EventMap.Centroid);
    }
}
