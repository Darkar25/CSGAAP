using CSGAAP.Util;

namespace CSGAAP.Generics
{
    public abstract class KSkipNGramEventDriver : EventDriver
    {
        public KSkipNGramEventDriver()
        {
            AddParameter(
                Name: "K",
                DisplayName: "K",
                DefaultValue: 1,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50);
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

        protected EventSet TransformToKSkipNGram(EventSet set)
        {
            int k = (int)this["K"];
            int n = (int)this["N"];

            List<Event> events = new();
            Event[] arrset = set.ToArray();

            for (var x = 0; x + (k + 1) * (n - 1) < arrset.Length; x++)
            {
                var gram = "";
                var gramTracker = x;

                for (var y = 0; y < n; y++)
                {
                    gram += arrset[gramTracker] + " ";
                    gramTracker += k + 1;
                }

                events.Add(new(gram.Trim(), this));
            }

            return new(events);
        }
    }
}
