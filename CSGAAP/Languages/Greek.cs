using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class Greek : Language
    {
        public override string Name => "Greek (ISO-8859-7)";

        public override string Lang => "greek";

        //public override Encoding Charset => Encoding.GetEncoding("iso-8859-7");
        public override Encoding Charset => Encoding.UTF8;
    }
}
