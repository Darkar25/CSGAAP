namespace CSGAAP.WordSegment.Strategies
{
    public class JointMM : ISegmentStrategy
    {
        public IEnumerable<string> Segment(string sentence, WordDictionary dict)
        {
            BMM bmm = new();
            FMM fmm = new();
            foreach(var s in sentence.Split(new string[] { "£¡", "¡°", "¡±", "¡®", "¡¯", "£¬", "¡£", "£º", "£»", "£¿", "¡¢", "¡­¡­" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var bres = bmm.Segment(s, dict);
                var fres = fmm.Segment(s, dict);
                if (bres.Count() > fres.Count())
                    foreach (var f in fres)
                        yield return f;
                else
                    foreach (var b in bres)
                        yield return b;
            }
            yield break;
        }
    }
}
