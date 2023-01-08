using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;
using StructLinq;
using System.Linq;

namespace CSGAAP.Classifiers
{
    public class KNearestNeighborDriver : AnalysisDriver
    {
        private Dictionary<Document, EventMap>? knowns;

        public override string Name => "K-Nearest Neighbor Driver";
        public override string ToolTipText => "";

        public KNearestNeighborDriver()
        {
            AddParameter(
                Name: "k",
                DisplayName: "K",
                DefaultValue: 5,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            IHistogram unknownHistogram = new AbsoluteHistogram(unknownDocument);
            Ballot<string> ballot = new(new LastPickedComparator());
            var k = (int)this["k"];
            var res = knowns!
                .ToStructEnumerable()
                .Select(x => {
                    try
                    {
                        var current = Distance.Distance(new EventMap(unknownDocument), x.Value);
                        Log.Debug($"{unknownDocument.URI?.AbsolutePath}(Unknown):{x.Key.URI?.AbsolutePath}({x.Key.Author}) Distance:{current}");
                        return new KeyValuePair<string, double>(x.Key.Author!, current);
                    }
                    catch (DistanceCalculationException e)
                    {
                        Log.Fatal(e, $"Distance {Distance.DisplayName} failed");
                        throw new AnalyzeException($"Distance {Distance.DisplayName} failed");
                    }
                }, x => x)
                .OrderBy(x => x.Value)
                .Take(k, x => x)
                .ToArray();
            for (int i = 0; i < res.Length; i++)
                ballot.Vote(res[i].Key, 1 + Math.Pow(2, -1.0 * (i + 1)));
            return ballot.Results.OrderBy(x => x.Value);
        }

        public override void Train(IEnumerable<Document> knownDocuments) => knowns = knownDocuments.ToDictionary(x => x, x => new EventMap(x));
    }
}
