using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;
using StructLinq;

namespace CSGAAP.Classifiers
{
    public class NearestNeighborDriver : AnalysisDriver
    {
        private Dictionary<Document, EventMap>? knowns;

        public override string Name => "Nearest Neighbor Driver";
        public override string ToolTipText => "Assigns authorship labels by using a nearest-neighbor approach on a given distance/divergence function.";

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            IHistogram unknownHistogram = new AbsoluteHistogram(unknownDocument);
            return knowns!
                .ToStructEnumerable()
                .Select(x => {
                    try
                    {
                        var current = Distance.Distance(new EventMap(unknownDocument), x.Value);
                        Log.Debug($"{unknownDocument.URI?.AbsolutePath}(Unknown):{x.Key.URI?.AbsolutePath}({x.Key.Author}) Distance:{current}");
                        return new KeyValuePair<string, double>($"{x.Key.Author!} -{x.Key.URI?.AbsolutePath}", current);
                    }
                    catch (DistanceCalculationException e)
                    {
                        Log.Fatal(e, $"Distance {Distance.DisplayName} failed");
                        throw new AnalyzeException($"Distance {Distance.DisplayName} failed");
                    }
                }, x => x)
                .OrderBy(x => x.Value)
                .ToEnumerable();
        }

        public override void Train(IEnumerable<Document> knownDocuments) => knowns = knownDocuments.ToDictionary(x => x, x => new EventMap(x));
    }
}
