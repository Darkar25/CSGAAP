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

        private static readonly List<ReadOnlyMemory<char>> functionWords = new(Encoding.Default.GetString(Resources.MWfunctionwords).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(x => new ReadOnlyMemory<char>(x.ToCharArray())));

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(base.CreateEventSet(text).Where(x => functionWords.Contains(x.Data, new ROMComparer<char>())));
    }
}
