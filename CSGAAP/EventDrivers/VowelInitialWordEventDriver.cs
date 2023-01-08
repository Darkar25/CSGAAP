using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class VowelInitialWordEventDriver : NaiveWordEventDriver
    {
        public override string DisplayName => "Vowel-initial Words";

        public override string ToolTipText => "Words beginning with A, E, I, O, U (or lowercase equivalent)";

        private const string vowels = "aeiouyAEIOUY";

        public override EventSet CreateEventSet(string text) => new(base.CreateEventSet(text).Where(x => vowels.Contains(x.Data[0])));
    }
}
