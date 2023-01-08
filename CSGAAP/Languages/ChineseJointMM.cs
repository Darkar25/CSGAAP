using CSGAAP.Generics;
using CSGAAP.WordSegment;
using CSGAAP.WordSegment.Strategies;
using System.Text;

namespace CSGAAP.Languages
{
    public class ChineseJointMM : Language
    {
        public override string Name => "Chinese JointMM (GB2312)";

        public override string Lang => "chinese";

        //public override Encoding Charset => Encoding.GetEncoding("gb2312");
        public override Encoding Charset => Encoding.Unicode;

        public override string ParseDocument(string document) => string.Join(' ', new WordSegmenter(new JointMM()).Segment(document));
    }
}
