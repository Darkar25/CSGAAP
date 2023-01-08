using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class ComputerLanguage : Language
    {
        public override string Name => "Computer Language";

        public override string Lang => "computer";

        public override Encoding Charset => Encoding.Default;

        public override bool ShowInGUI => false;
    }
}
