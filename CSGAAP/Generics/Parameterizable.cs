using CSGAAP.Util;
using System.Collections.ObjectModel;

namespace CSGAAP.Generics
{
    public abstract class Parameterizable
    {
        private Dictionary<string, Parameter> Parameters { get; set; } = new();

        public object this[string param]
        {
            get => Parameters[param].Value;
            set => Parameters[param].Value = value;
        }

        public ReadOnlyDictionary<string, Parameter> DisplayParameters => new(Parameters);

        public string FormattedParameters => string.Join(", ", Parameters.Select(x => x.Key + " : " + x.Value.Value));

        public void Clear() => Parameters.Clear();
        public bool AddParameter<T>(string Name, string DisplayName, T DefaultValue, params T[] PossibleValues)
        {
            //var values = PossibleValues.Length == 1 && PossibleValues[0].GetType().IsArray ? PossibleValues[0] as [] : PossibleValues;
            return Parameters.TryAdd(Name, new Parameter(DisplayName, DefaultValue!, PossibleValues));
        }
        public bool RemoveParameter(string Name) => Parameters.Remove(Name);

        public void ParseValue(string Name, string Value) => Parameters[Name].SetStringValue(Value);

        public Parameterizable() { }
        //public Parameterizable(Parameterizable copyfrom) => Parameters = new Dictionary<string, Parameter>(copyfrom.Parameters);

        public Parameterizable NewInstanceWithParams()
        {
            var ret = (Activator.CreateInstance(GetType()) as Parameterizable)!;
            ret.Parameters = this.Parameters.ToDictionary(x => x.Key, x => (Parameter)x.Value.Clone());
            return ret;
        }
    }
}
