namespace CSGAAP.Util
{
    public class Bag<T>
    {
        private readonly List<T> elements = new();

        public Bag() {  }
        public Bag(IEnumerable<T> events) => elements = new(events);

        public void Add(T @event) => elements.Add(@event);
        public void AddRange(IEnumerable<T> events) => elements.AddRange(events);
        public void Clear() => elements.Clear();

        public T Next() => elements[Random.Shared.Next(elements.Count)];
        public IEnumerable<T> Next(int amount)
        {
            for (int i = 0; i < amount; i++) yield return Next();
        }
    }
}
