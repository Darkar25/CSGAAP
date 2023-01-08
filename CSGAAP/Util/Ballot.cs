namespace CSGAAP.Util
{
    public class Ballot<T> where T : notnull
    {
        private readonly Dictionary<T, double> votes = new();

        public IComparer<KeyValuePair<T, double>>? Comparer { get; set; }

        public IEnumerable<KeyValuePair<T, double>> Results => Comparer is null ? votes : votes.OrderDescending(Comparer);

        public Ballot() { }

        public Ballot(IComparer<KeyValuePair<T, double>> comparer) => Comparer = comparer;

        public void Vote(T candidate, double votesNum)
        {
            votes.TryGetValue(candidate, out var c);
            votes[candidate] = c + votesNum;
        }

        public void Vote(T candidate) => Vote(candidate, 1);
    }
}
