using CSGAAP.Generics;
using CSGAAP.WordSegment;
using CSGAAP.WordSegment.Strategies;
using System.Text;

namespace CSGAAP.Languages
{
    public class ChineseFMM : Language
    {
        public override string Name => "Chinese FMM (GB2312)";

        public override string Lang => "chinese";

        //public override Encoding Charset => Encoding.GetEncoding("gb2312");
        public override Encoding Charset => Encoding.Unicode;

        public override string ParseDocument(string document) => string.Join(' ', new WordSegmenter(new FMM()).Segment(document));
    }
}
