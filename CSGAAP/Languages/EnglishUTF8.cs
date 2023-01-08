using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class EnglishUTF8 : Language
    {
        public override string Name => "English (UTF8)";

        public override string Lang => "english";

        public override Encoding Charset => Encoding.UTF8;
    }
}
