using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;

namespace CSGAAP.Classifiers
{
    public class NullAnalysis : AnalysisDriver
    {
        public override string Name => "Null Analysis";
        public override string ToolTipText => "Prints all event sets received";
        public override bool SupportsDistance => false;

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            Log.Information("--- Unknown Event Set ---");
            foreach (var set in unknownDocument.EventSets.Values)
                foreach (var e in set)
                    Log.Information(e + " *** ");
            return new[] { new KeyValuePair<string, double>("No analysis performed.\n", 0) };
        }

        public override void Train(IEnumerable<Document> knownDocuments)
        {
            var i = 0;
            foreach(var d in knownDocuments)
            {
                Log.Information($"--- Known Event Set #{i++} ---");
                foreach (var set in d.EventSets.Values)
                    foreach (var e in set)
                        Log.Information(e + " *** ");
            }
        }
    }
}
