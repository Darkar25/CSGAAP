using CSGAAP.Generics;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CSGAAP.Backend
{
    public class DistanceFunctions
    {
        public static ReadOnlyCollection<DistanceFunction> List { get; } = new(Assembly.GetExecutingAssembly().DefinedTypes
            .Where(x => x.IsAssignableTo(typeof(DistanceFunction)) && !x.IsAbstract && !x.IsInterface)
            .Select(x => Activator.CreateInstance(x.AsType()) as DistanceFunction)
            .ToList()!);
        public static ReadOnlyDictionary<string, DistanceFunction> Map => new(List.ToDictionary(x => x.DisplayName.ToLower().Trim(), x => x));

        public static DistanceFunction GetDistanceFunction(string action)
        {
            var tmp = action.Split('|', 2);
            var driver = List.SingleOrDefault(x => x.DisplayName.Trim().Equals(tmp[0].Trim(), StringComparison.InvariantCultureIgnoreCase));
            if (driver is null) throw new Exception($"Distance function {tmp[0]} not found!");
            var copy = (DistanceFunction)driver.NewInstanceWithParams();
            foreach (var param in tmp[1].Split("|").Select(x => x.Split(":", 2)))
                copy.ParseValue(param[0].Trim(), param[1].Trim());
            return copy;
        }
    }
}
