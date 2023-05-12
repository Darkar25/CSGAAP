using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Canonicizers
{
    public class IntervieweeText : Canonicizer
    {
        public override string DisplayName => "Interviewee Text";

        public override string ToolTipText => "Extract Interviewee Text";

        public override string LongDescription => "Extract Interviewee Text from LINDSEI L1 Corpus";

        public override bool ShowInGUI => false;

        public override string Process(string text)
        {
            StringBuilder ret = new(text.Length);
            int end = 0;

            while (true)
            {
                int start = text[(end + 3)..].IndexOf("<B>", StringComparison.Ordinal);

                if (start == -1)
                    return ret.ToString();

                end = text[start..].IndexOf("</B>", StringComparison.Ordinal);

                ret.Append($"{ret} {text[(start + 3)..end]} ");
            }
        }
    }
}
