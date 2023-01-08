using System.Text.Json;

namespace CSGAAP.WordSegment
{
    public class WordSegmenter
    {
        public WordDictionary Dictionary { get; set; } = JsonSerializer.Deserialize<WordDictionary>(Properties.Resources.chinese_dictionary)!;
        
        public ISegmentStrategy Strategy { get; }

        public WordSegmenter(ISegmentStrategy strategy)
        {
            Strategy = strategy;
        }

        public IEnumerable<string> Segment(string sentence) => Strategy.Segment(sentence, Dictionary);
    }
}
