using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class MNLetterWordEventDriver : NaiveWordEventDriver
    {
        public override string DisplayName => "M--N letter Words";

        public override string ToolTipText => "Words with between M and N letters";
        public override string LongDescription => "Words with between M and N letters (M and N are given as parameters)";

        public MNLetterWordEventDriver()
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

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) {
            int m = (int)this["M"];
            int n = (int)this["N"];
            if (n < 0) n = int.MaxValue;

            return new(base.CreateEventSet(text).Where(x => x.Data.Length >= m && x.Data.Length <= n));
        }
    }
}
