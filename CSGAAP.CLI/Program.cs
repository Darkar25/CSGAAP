using CommandLine;
using CSGAAP.Backend;

namespace CSGAAP.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CLIOptions>(args).WithParsed(opts => {
                if(!string.IsNullOrEmpty(opts.ExperimentEngine))
                {
                    ExperimentEngine.RunExperiment(opts.ExperimentEngine, opts.Language);
                    return;
                }
                API instance = API.PrivateInstance;
                using (HttpClient client = new())
                {
                    var docsfile = client.GetStringAsync(opts.LoadFile).Result;
                    foreach (var a in docsfile.Split('\n').Select(x => x.Split(';')))
                        instance.Documents.Add(new(a[1], a[0], a[2]));
                }
                instance.Language = Backend.Languages.GetLanguage(opts.Language);
                instance.EventDrivers.Add(Backend.EventDrivers.GetEventDriver(opts.EventDriver));
                foreach (var ec in opts.EventCullers)
                    instance.EventCullers.Add(Backend.EventCullers.GetEventCuller(ec));
                var analysis = Backend.AnalysisDrivers.GetAnalysisDriver(opts.AnalysisDriver);
                if (analysis.SupportsDistance && string.IsNullOrEmpty(opts.DistanceFunction)) throw new Exception($"Analysis driver {analysis} requires a distance function.");
                if (analysis.SupportsDistance)
                    analysis.Distance = Backend.DistanceFunctions.GetDistanceFunction(opts.DistanceFunction!);
                instance.AnalysisDrivers.Add(analysis);
                if (!instance.Execute().Result) throw new Exception("Analysis error");
                if (string.IsNullOrEmpty(opts.SaveFile)) Console.WriteLine(instance.FormattedResult());
                else File.AppendAllText(opts.SaveFile, instance.FormattedResult() + "\n");
            });
        }
    }
}