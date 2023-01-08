using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class EnglishUTF16 : Language
    {
        public override string Name => "English (UTF16)";

        public override string Lang => "english";

        public override Encoding Charset => Encoding.Unicode;
    }
}
