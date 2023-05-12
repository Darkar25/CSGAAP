using CSGAAP.Util;

namespace CSGAAP.Generics
{
    public abstract class LeaveKOutNGramEventDriver : EventDriver
    {
        public LeaveKOutNGramEventDriver()
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
                DefaultValue: 3,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50);
        }

        protected EventSet TransformEventSet(EventSet set)
        {
            int k = (int)this["K"];
            int n = (int)this["N"];

            List<Event> events = new();
            var len = set.Count();

            for (var i = 0; i < len - n; i++)
                events.AddRange(
                    GetSubList(set
                        .Skip(i)
                        .Take(n)
                        .Select(x => x.ToString())
                        .ToArray(), k)
                    .Select(x => new Event(new ReadOnlyMemory<char>(("[" + string.Join(", ", x) + "]").ToCharArray()), this)));
            
            return new(events);
        }

        private IEnumerable<string[]> GetSubList(string[] list, int k)
        {
            List<string[]> res = new();
            if (k == 1)
                res.AddRange(ReduceList(list));
            else
                foreach (string[] current in GetSubList(list, k - 1))
                    res.AddRange(ReduceList(current));
            return res.Distinct(new ArrayComparer<string>());
        }

        private IEnumerable<string[]> ReduceList(IReadOnlyList<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                List<string> current = new(list.Count - 1);
                for (int j = 0; j < i; j++)
                    current.Add(list[j]);
                for (int j = i + 1; j < list.Count; j++)
                    current.Add(list[j]);
                yield return current.ToArray();
            }
        }
    }
}
