using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class CoarsePOSTagger : PartOfSpeechEventDriver
    {
        private static readonly Dictionary<string, string> translationTable = new() {
            { "CC", "C" },
            { "CD", "CD" },
            { "DT", "DT" },
            { "EX", "EX" },
            { "FW", "FW" },
            { "IC", "C" },
            { "JJ", "J" },
            { "JJR", "J" },
            { "JJS", "J" },
            { "LS", "LS" },
            { "MD", "MD" },
            { "NN", "N" },
            { "NNS", "N" },
            { "NNPS", "N" },
            { "PDT", "PDT" },
            { "POS", "PDT" },
            { "PPS", "P" },
            { "PRP$", "P" },
            { "RB", "R" },
            { "RBR", "R" },
            { "RBS", "R" },
            { "RP", "R" },
            { "SYM", "Punc" },
            { "TO", "TO" },
            { "UH", "UH" },
            { "VB", "V" },
            { "VBD", "V" },
            { "VBG", "V" },
            { "VBN", "V" },
            { "VBP", "V" },
            { "VBZ", "V" },
            { "WDT", "W" },
            { "WP", "W" },
            { "WP$", "W" },
            { "WRB", "W" },
            { "#", "Punc" },
            { "$", "Punc" },
            { ".", "Punc" },
            { ",", "Punc" },
            { ":", "Punc" },
            { "(", "Punc" },
            { ")", "Punc" },
            { "`", "Punc" },
            { "'", "Punc" },
            { "\"", "Punc" },
        };

        public override string DisplayName => "Coarse POS Tagger";
        public override string ToolTipText => "A simplification of the normal part of speech tagger";
        public override string LongDescription => "A simplification of the normal part of speech tagger, neutralizing minor variations such as plural inflection; for example, all noun types (proper/common, singular/plural) are grouped.";

        public override EventSet CreateEventSet(string text)
        {
            return new(base.CreateEventSet(text).Select(x => translationTable.TryGetValue(x.ToString(), out var tt) ? new(tt, this) : x));
        }
    }
}
