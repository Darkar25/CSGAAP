namespace CSGAAP.WordSegment
{
    public interface ISegmentStrategy
    {
        public IEnumerable<string> Segment(string sentence, WordDictionary dict);
    }
}
