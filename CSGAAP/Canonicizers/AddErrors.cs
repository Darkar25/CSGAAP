using CSGAAP.Generics;
using Serilog;

namespace CSGAAP.Canonicizers
{
    public class AddErrors : Canonicizer
    {
        public override string DisplayName => "Add Errors";

        public override string ToolTipText => "Add errors to the document.";

        public override string LongDescription => "Add random character replacement errors to the document; useful for evaluating method effectiveness in the presence of OCR-type errors.";

        public override bool ShowInGUI => false;

        public AddErrors()
        {
            AddParameter(
                Name: "percenterror",
                DisplayName: "Percent Error",
                DefaultValue: 0,
                0, 1, 2, 3, 4, 5, 10, 15, 20, 50);
        }

        public override string Process(string text)
        {
            Random random = new();
            int percentErrors = (int)this["percenterror"];
            int numChanges = (int)((percentErrors / 100.0) * text.Length);

            Log.Debug($"Introducing errors to {percentErrors}% of document, or {numChanges} of {text.Length} characters.");

            Span<char> ret = new(text.ToCharArray());

            for (int i = 0; i < numChanges; i++)
            {
                int changePos = random.Next(ret.Length);
                if ((ret[changePos] == ' ') || (ret[changePos] == '\t') || (ret[changePos] == '\n'))
                    ret[changePos] = new char[] { ' ', '\t', '\n' }[random.Next(3)];
                else
                    ret[changePos] = (char)(random.Next(26) + 'A');
            }
            return ret.ToString();
        }
    }
}
