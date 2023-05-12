using Serilog;
using System.Text.RegularExpressions;

namespace CSGAAP.Backend
{
    public static partial class ExperimentEngine
    {
        public const int workers = 1;

        public static string GetFileName(IEnumerable<string> canon, IEnumerable<string> events, string analysis, string name, string number)
        {
            var canonname = string.Join(" ", canon.Select(x => x.Trim()));
            canonname = string.IsNullOrEmpty(canonname) ? "none" : canonname;
            var eventname = string.Join(" ", events.Select(x => x.Trim()));
            var path = Path.Combine(
                Constants.TempDirectory, 
                canonname.Replace("/", "\\/"), 
                eventname.Replace("/", "\\/").Replace("|", "_").Replace(":", "-"),
                analysis.Trim().Replace("/", "\\/"));
            _ = Directory.CreateDirectory(path);
            return Path.Combine(path, name, number, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        }

        public static void RunExperiment(string path, string lang)
        {
            try
            {
                RunExperiment(File.ReadAllLines(path).Select(x => x.Split(';')).ToArray(), lang);
            } catch(Exception e)
            {
                Log.Fatal(e, $"Problem processing experiment file {path}");
            }
        }

        public static void RunExperiment(string[][] experimentTable, string lang = "english")
        {
            var name = experimentTable[0][0];
            Parallel.ForEach(experimentTable.Skip(1), new() { MaxDegreeOfParallelism = workers }, row => {
                if (row.Length < 6)
                {
                    Log.Error($"Experiment [{string.Join(", ", row)}] missing {6 - row.Length} column(s)");
                    return;
                }

                var number = row[0];
                var canonicizers = SplitRegex().Split(row[1].Trim()).Where(x => !string.IsNullOrEmpty(x));
                var events = SplitRegex().Split(row[2].Trim());
                var analysis = row[3].Trim();
                var distance = row[4].Trim();
                var docs = row[5].Trim();
                var filename = GetFileName(canonicizers, events, analysis + (string.IsNullOrEmpty(distance) ? "" : ("-" + distance)), name, number);

                API experiment = API.PrivateInstance;

                try
                {
                    experiment.Language = Languages.GetLanguage(lang);
                    foreach (var entry in File.ReadAllLines(docs).Select(x => x.Split(',')).Where(x => x.Length >= 3))
                        experiment.Documents.Add(new(entry[1], entry[0], entry[2]));
                    foreach (var canon in canonicizers)
                        experiment.Canonicizers.Add(Canonicizers.GetCaninicizer(canon));
                    foreach (var e in events)
                    {
                        var tmp = e.Split('@', 2);
                        if(tmp.Length > 1)
                            experiment.EventDrivers.Add(EventDrivers.GetEventDriver(tmp[0].Trim()));
                        else
                        {
                            var tmp2 = e.Split('#', 2);
                            experiment.EventDrivers.Add(tmp2.Length > 1
                                ? EventDrivers.GetEventDriver(tmp2[0].Trim())
                                : EventDrivers.GetEventDriver(e.Trim()));
                        }
                    }
                    var analysisdriver = AnalysisDrivers.GetAnalysisDriver(analysis);
                    switch (analysisdriver.SupportsDistance)
                    {
                        case true when string.IsNullOrWhiteSpace(distance):
                            Log.Error($"AnalysisDriver {analysisdriver.DisplayName} in the experiment {filename} requires a distance function, but none was supplied");
                            return;
                        case true:
                            analysisdriver.Distance = DistanceFunctions.GetDistanceFunction(distance);
                            break;
                    }

                    if(experiment.Execute().Result)
                        File.AppendAllText(filename, experiment.FormattedResult());
                } catch(Exception e)
                {
                    Log.Error(e, $"Could not run experiment {filename}");
                }
            });
        }

        [GeneratedRegex("\\s*&\\s*")]
        private static partial Regex SplitRegex();
    }
}
