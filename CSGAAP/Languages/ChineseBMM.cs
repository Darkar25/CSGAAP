using CSGAAP.Generics;
using CSGAAP.WordSegment;
using CSGAAP.WordSegment.Strategies;
using System.Text;

namespace CSGAAP.Languages
{
    public class ChineseBMM : Language
    {
        public override string Name => "Chinese BMM (GB2312)";

        public override string Lang => "chinese";

        //public override Encoding Charset => Encoding.GetEncoding("gb2312");
        public override Encoding Charset => Encoding.Unicode;

        public override string ParseDocument(string document) => string.Join(' ', new WordSegmenter(new BMM()).Segment(document));
    }
}
