using CSGAAP.Generics;
using Serilog;
using System;

namespace CSGAAP.Canonicizers
{
    public class NullCanonicizer : Canonicizer
    {
        public override string DisplayName => "Null Canonicizer";

        public override string ToolTipText => "This preprocessor makes no changes to the text, but prints it to the console.";

        public override string LongDescription => "This preprocessor makes no changes to the text, but prints it to the console.  Generally only useful for software testing.";

        public override string Process(string text)
        {
            Log.Information($" --- Begin Document ---\n{text}\n --- End Document ---\n");
            return text;
        }
    }
}
