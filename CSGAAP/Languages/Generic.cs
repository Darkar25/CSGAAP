using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class Generic : Language
    {
        public override string Name => "Generic";

        public override string Lang => "generic";

        public override Encoding Charset => Encoding.Default;
    }
}
