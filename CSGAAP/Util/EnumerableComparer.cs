using System.Diagnostics.CodeAnalysis;

namespace CSGAAP.Util
{
    public class EnumerableComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.SequenceEqual(y);
        }

        public int GetHashCode([DisallowNull] IEnumerable<T> obj)
        {
            unchecked
            {
                int hash = 19;
                foreach (var word in obj)
                    hash = hash * 31 + (word == null ? 0 : word.GetHashCode());
                return hash;
            }
        }
    }
}
