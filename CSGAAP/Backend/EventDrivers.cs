using CSGAAP.Generics;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CSGAAP.Backend
{
    public class EventDrivers
    {
        public static ReadOnlyCollection<EventDriver> List { get; } = new(Assembly.GetExecutingAssembly().DefinedTypes
            .Where(x => x.IsAssignableTo(typeof(EventDriver)) && !x.IsAbstract && !x.IsInterface)
            .Select(x => Activator.CreateInstance(x.AsType()) as EventDriver)
            .ToList()!);
        public static ReadOnlyDictionary<string, EventDriver> Map => new(List.ToDictionary(x => x.DisplayName.ToLower().Trim(), x => x));

        public static EventDriver GetEventDriver(string action)
        {
            var tmp = action.Split('|', 2);
            var inst = List.SingleOrDefault(x => x.DisplayName.Trim().Equals(tmp[0].Trim(), StringComparison.InvariantCultureIgnoreCase));
            if (inst is null) throw new Exception($"Event driver {tmp[0]} not found!");
            var copy = (EventDriver)inst.NewInstanceWithParams();
            foreach (var param in tmp[1].Split("|").Select(x => x.Split(":", 2)))
                copy.ParseValue(param[0].Trim(), param[1].Trim());
            return copy;
        }
    }
}
