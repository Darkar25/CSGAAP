namespace CSGAAP.WordSegment.Strategies
{
    public class BMM : ISegmentStrategy
    {
        public IEnumerable<string> Segment(string sentence, WordDictionary dict)
        {
            int targetLength = dict.MaxWordLength;
            while (sentence.Length > 0)
            {
                string tempStr = sentence[^targetLength..].ToString();
                if (dict.ContainsKey(tempStr) || targetLength == 1)
                {
                    yield return tempStr;
                    sentence = sentence[..^targetLength];
                    targetLength = Math.Min(dict.MaxWordLength, sentence.Length);
                    continue;
                }
                targetLength--;
            }
            yield break;
        }
    }
}