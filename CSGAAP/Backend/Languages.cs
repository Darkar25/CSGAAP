using CSGAAP.Generics;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CSGAAP.Backend
{
    public class Languages
    {
        public static ReadOnlyCollection<Language> List { get; } = new(Assembly.GetExecutingAssembly().DefinedTypes
            .Where(x => x.IsAssignableTo(typeof(Language)) && !x.IsAbstract && !x.IsInterface)
            .Select(x => Activator.CreateInstance(x.AsType()) as Language)
            .ToList()!);
        public static ReadOnlyDictionary<string, Language> Map => new(List.ToDictionary(x => x.DisplayName.ToLower().Trim(), x => x));

        public static Language GetLanguage(string action)
        {
            var tmp = action.Split('|', 2);
            var inst = List.SingleOrDefault(x => x.DisplayName.Trim().Equals(tmp[0].Trim(), StringComparison.InvariantCultureIgnoreCase)) ?? throw new Exception($"Language {tmp[0]} not found!");
            var copy = Activator.CreateInstance(inst.GetType()) as Language;
            return copy!;
        }
    }
}
