using CSGAAP.Generics;
using CSGAAP.Util;
using Serilog;

namespace CSGAAP.EventDrivers
{
    public class RareWordsEventDriver : NaiveWordEventDriver
    {
        public override string DisplayName => "Rare Words";
        public override string ToolTipText => "Rare words such as Words appearing only once or twice per document";

        public RareWordsEventDriver()
        {
            AddParameter(
                Name: "M",
                DisplayName: "M",
                DefaultValue: 1,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50);
            AddParameter(
                Name: "N",
                DisplayName: "N",
                DefaultValue: 2,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50);
        }

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text)
        {
            var ret = base.CreateEventSet(text);
            int m = (int)this["M"];
            int n = (int)this["N"];
            EventHistogram hist = new(ret);
            Log.Debug($"M = {m}; N = {n}");
            return new(ret.Where(x => hist.AbsoluteFrequency(x) >= m && hist.AbsoluteFrequency(x) <= n));
        }
    }
}
