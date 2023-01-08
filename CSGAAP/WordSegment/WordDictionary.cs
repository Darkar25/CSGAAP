using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace CSGAAP.WordSegment
{
    public class WordDictionary : Dictionary<string, int>
    {
        public int MaxWordLength => this.Max(x => x.Key.Length);
        public int TotalWords => this.Sum(x => x.Value);

        public void Add(string word)
        {
            TryGetValue(word, out var v);
            this[word] = v + 1;
        }

        public override string ToString() => string.Join("\n", this.Select(x => x.Key + " " + x.Value));
    }
}
