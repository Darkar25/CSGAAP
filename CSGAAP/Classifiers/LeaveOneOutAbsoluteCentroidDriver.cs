using CSGAAP.Exceptions;
using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.Classifiers
{
    public class LeaveOneOutAbsoluteCentroidDriver : ValidationDriver
    {
        private Dictionary<string, AbsoluteHistogram>? knownCentroids;
        private ILookup<string, Document>? knownDocuments;
        private Dictionary<Document, AbsoluteHistogram>? knownAbsoluteHistograms;

        public override string Name => "Leave One Out Absolute Centroid";
        public override string ToolTipText => "";

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            try
            {
                return new[] { new KeyValuePair<string, double>(unknownDocument.Author!, Distance.Distance(knownAbsoluteHistograms![unknownDocument], AbsoluteHistogram.Centroid(knownDocuments![unknownDocument.Author!]
                    .Where(x => x != unknownDocument)
                    .Select(x => knownAbsoluteHistograms![x])))) }
                .Concat(knownCentroids!
                    .Where(x => x.Key != unknownDocument.Author)
                    .Select(x => new KeyValuePair<string, double>(x.Key, Distance.Distance(knownAbsoluteHistograms![unknownDocument], x.Value))))
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
            knownAbsoluteHistograms = knownDocuments.ToDictionary(x => x, x => new AbsoluteHistogram(x));
            knownCentroids = knownAbsoluteHistograms.ToLookup(x => x.Key.Author!).ToDictionary(x => x.Key, x => AbsoluteHistogram.Centroid(x.Select(x => x.Value)));
        }
    }
}
