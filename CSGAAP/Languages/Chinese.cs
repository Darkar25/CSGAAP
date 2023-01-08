using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class Chinese : Language
    {
        public override string Name => "Chinese (GB2312)";

        public override string Lang => "chinese";

        //public override Encoding Charset => Encoding.GetEncoding("gb2312");
        public override Encoding Charset => Encoding.Unicode;
    }
}
