namespace CSGAAP.Util
{
    public class ROMComparer<T> : IEqualityComparer<ReadOnlyMemory<T>>
    {
        public bool Equals(ReadOnlyMemory<T> x, ReadOnlyMemory<T> y)
        {
            return x.Span.SequenceEqual(y.Span);
        }

        public int GetHashCode(ReadOnlyMemory<T> obj)
        {
            unchecked
            {
                int hash = 19;
                foreach (var word in obj.Span)
                    hash = hash * 31 + (word == null ? 0 : word.GetHashCode());
                return hash;
            }
        }
    }
}
