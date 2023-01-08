using CSGAAP.Generics;
using CSGAAP.Properties;
using CSGAAP.Util;
using System.Text;

namespace CSGAAP.EventDrivers
{
    public class MWFunctionWordsEventDriver : NaiveWordEventDriver
    {
        public override string DisplayName => "MW Function Words";

        public override string ToolTipText => "Function Words from Mosteller-Wallace";

        public override string LongDescription => "Function Words from Mosteller-Wallace's study of the Federalist Papers";

        private static readonly List<string> functionWords = new(Encoding.Default.GetString(Resources.MWfunctionwords).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));

        public override EventSet CreateEventSet(string text) => new(base.CreateEventSet(text).Where(x => functionWords.Contains(x.Data)));
    }
}
