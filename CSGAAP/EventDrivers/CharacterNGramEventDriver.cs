using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.EventDrivers
{
    public class CharacterNGramEventDriver : EventDriver
    {
        public override string DisplayName => "Character NGrams";
        public override string ToolTipText => "Groups of N successive characters";
        public override string LongDescription => "Groups of N successive characters (sliding window); N is given as a parameter.";

        public CharacterNGramEventDriver()
        {
            AddParameter(
                Name: "N",
                DisplayName: "N",
                DefaultValue: 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50);
        }

        public override EventSet CreateEventSet(string text) => new(CreateEventSetInternal(text));

        private IEnumerable<Event> CreateEventSetInternal(string text)
        {
            int n = (int)this["N"];
            for (int i = 0; i <= text.Length - n; i++) yield return new(text[i..(i + n)].ToString(), this);
            yield break;
        }
    }
}
