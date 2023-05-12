using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class Arabic : Language
    {
        public override string Name => "Arabic (ISO-8859-6)";

        public override string Lang => "arabic";

        //public override Encoding Charset => Encoding.GetEncoding("iso-8859-6");
        public override Encoding Charset => Encoding.UTF8;
    }
}
