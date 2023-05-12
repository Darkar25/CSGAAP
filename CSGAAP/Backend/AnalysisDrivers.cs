using CSGAAP.Generics;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CSGAAP.Backend
{
    public class AnalysisDrivers
    {
        public static ReadOnlyCollection<AnalysisDriver> List { get; } = new(Assembly.GetExecutingAssembly().DefinedTypes
            .Where(x => x.IsAssignableTo(typeof(AnalysisDriver)) && !x.IsAbstract && !x.IsInterface)
            .Select(x => Activator.CreateInstance(x.AsType()) as AnalysisDriver)
            .ToList()!);
        public static ReadOnlyDictionary<string, AnalysisDriver> Map => new(List.ToDictionary(x => x.DisplayName.ToLower().Trim(), x => x));

        public static AnalysisDriver GetAnalysisDriver(string action)
        {
            var tmp = action.Split('|', 2);
            var inst = List.SingleOrDefault(x => x.DisplayName.Trim().Equals(tmp[0].Trim(), StringComparison.InvariantCultureIgnoreCase)) ?? throw new Exception($"Action {tmp[0]} not found!");
            var copy = (AnalysisDriver)inst.NewInstanceWithParams();
            foreach (var param in tmp[1].Split("|").Select(x => x.Split(":", 2)))
                copy.ParseValue(param[0].Trim(), param[1].Trim());
            return copy;
        }
    }
}
