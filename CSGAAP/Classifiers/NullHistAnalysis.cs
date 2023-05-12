using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;

namespace CSGAAP.Classifiers
{
    public class NullHistAnalysis : AnalysisDriver
    {
        public override string Name => "Null Histogram Analysis";
        public override string ToolTipText => "Prints a Histogram of Event Sets";
        public override bool SupportsDistance => false;

        public override IEnumerable<KeyValuePair<string, double>> Analyze(Document unknownDocument)
        {
            var em = new EventMap(unknownDocument);
            Log.Information("--- Unknown Event Set ---");
            foreach (var e in em.UniqueEvents)
                Log.Information($"'{e.ToString().Replace("'", "\\'")}','{em.RelativeFrequency(e)}',");
            return new[] { new KeyValuePair<string, double>("No analysis performed.\n", 0) };
        }

        public override void Train(IEnumerable<Document> knownDocuments)
        {
            var i = 0;
            foreach (var d in knownDocuments)
            {
                var em = new EventMap(d);
                Log.Information($"--- Known Event Set #{i++} ---");
                foreach (var e in em.UniqueEvents)
                    Log.Information($"'{e.ToString().Replace("'", "\\'")}','{em.RelativeFrequency(e)}',");
            }
        }
    }
}
