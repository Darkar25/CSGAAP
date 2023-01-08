using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class Russian : Language
    {
        public override string Name => "Russian (ISO-8859-5)";

        public override string Lang => "russian";

        //public override Encoding Charset => Encoding.GetEncoding("iso-8859-5");
        public override Encoding Charset => Encoding.UTF8;
    }
}
