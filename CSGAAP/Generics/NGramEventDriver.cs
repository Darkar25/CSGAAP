using CSGAAP.Util;

namespace CSGAAP.Generics
{
    public abstract class NGramEventDriver : EventDriver
    {
        public NGramEventDriver()
        {
            AddParameter(
                Name: "N",
                DisplayName: "N",
                DefaultValue: 2,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50);
        }

        protected EventSet TransformToNGram(EventSet set)
        {
            int n = (int)this["N"];

            List<Event> events = new();
            Event[] arrset = set.ToArray();

            for (var i = 0; i + n <= arrset.Length; i++)
                events.Add(new(new ReadOnlyMemory<char>(("[" + string.Join(", ", arrset[i..(i + n)]) + "]").ToCharArray()), this));

            return new(events);
        }
    }
}
