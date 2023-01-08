using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.Classifiers
{
    public class LeaveOneOutCentroidDriver : ValidationDriver
    {
        private Dictionary<string, EventMap>? knownCentroids;
        private ILookup<string, Document>? knownDocuments;
        private Dictionary<Document, EventMap>? knownEventMaps;

        public override string Name => "Leave One Out Centroid";
        public override string ToolTipText => "";

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            try
            {
                return new[] { new KeyValuePair<string, double>(unknownDocument.Author!, Distance.Distance(knownEventMaps![unknownDocument], EventMap.Centroid(knownDocuments![unknownDocument.Author!]
                    .Where(x => x != unknownDocument)
                    .Select(x => knownEventMaps![x])))) }
                .Concat(knownCentroids!
                    .Where(x => x.Key != unknownDocument.Author)
                    .Select(x => new KeyValuePair<string, double>(x.Key, Distance.Distance(knownEventMaps![unknownDocument], x.Value))))
                .OrderBy(x => x.Value);
            }
            catch (DistanceCalculationException e)
            {
                throw new AnalyzeException($"Distance Method {Distance.DisplayName} has failed");
            }
        }

        public override void Train(IEnumerable<Document> knownDocuments)
        {
            this.knownDocuments = knownDocuments.ToLookup(x => x.Author!);
            knownEventMaps = knownDocuments.ToDictionary(x => x, x => new EventMap(x));
            knownCentroids = knownEventMaps.ToLookup(x => x.Key.Author!).ToDictionary(x => x.Key, x => EventMap.Centroid(x.Select(x => x.Value)));
        }
    }
}
