﻿using CSGAAP.Generics;

namespace CSGAAP.EventDrivers
{
    public class PorterStemmerWithIrregularEventDriver : PorterStemmerEventDriver
    {
        private static readonly Dictionary<string, string> verbs = new()
        {
            {"awoke", "awake" },
            {"awoken", "awake"},
            {"was", "be"},
            {"were", "be"},
            {"been", "be"},
            {"bore", "bear" },
            {"born", "bear"},
            {"beat", "beat"},
            {"became", "become"},
            {"become", "become"},
            {"began", "begin"},
            {"begun", "begin"},
            {"bent", "bend"},
            {"beset", "beset"},
            {"bet", "bet"},
            {"bid", "bid"},
            {"bade", "bid"},
            {"bidden", "bid"},
            {"bound", "bind"},
            {"bit", "bite"},
            {"bitten", "bite"},
            {"bled", "bleed"},
            {"blew", "blow"},
            {"blown", "blow"},
            {"broke", "break"},
            {"broken", "break"},
            {"bred", "breed"},
            {"brought", "bring"},
            {"broadcast", "broadcast"},
            {"built", "build"},
            {"burned", "burn"},
            {"burnt", "burn"},
            {"burst", "burst"},
            {"bought", "buy"},
            {"cast", "cast"},
            {"caught", "catch"},
            {"chose", "choose"},
            {"chosen", "choose"},
            {"clung", "cling"},
            {"came", "come"},
            {"come", "come"},
            {"cost", "cost"},
            {"crept", "creep"},
            {"cut", "cut"},
            {"dealt", "deal"},
            {"dug", "dig"},
            {"dived", "dive"},
            {"dove", "dive"},
            {"did", "do"},
            {"done", "do"},
            {"drew", "draw"},
            {"drawn", "draw"},
            {"dreamed", "dream"},
            {"dreamt", "dream"},
            {"drove", "drive"},
            {"driven", "drive"},
            {"drank", "drink"},
            {"drunk", "drink"},
            {"ate", "eat"},
            {"eaten", "eat"},
            {"fell", "fall"},
            {"fallen", "fall"},
            {"fed", "feed"},
            {"felt", "feel"},
            {"fought", "fight"},
            {"found", "find"},
            {"fit", "fit"},
            {"fled", "flee"},
            {"flung", "fling"},
            {"flew", "fly"},
            {"flown", "fly"},
            {"forbade", "forbid"},
            {"forbidden", "forbid"},
            {"forgot", "forget"},
            {"forgotten", "forget"},
            {"forewent", "forego"},
            {"foregone", "forego"},
            {"forgave", "forgive"},
            {"forgiven", "forgive"},
            {"forsook", "forsake"},
            {"forsaken", "forsake"},
            {"froze", "freeze"},
            {"frozen", "freeze"},
            {"got", "get"},
            {"gotten", "get"},
            {"gave", "give"},
            {"given", "give"},
            {"went", "go"},
            {"gone", "go"},
            {"ground", "grind"},
            {"grew", "grow"},
            {"grown", "grow"},
            {"hung", "hang"},
            {"heard", "hear"},
            {"hid", "hide"},
            {"hidden", "hide"},
            {"hit", "hit"},
            {"held", "hold"},
            {"hurt", "hurt"},
            {"kept", "keep"},
            {"knelt", "kneel"},
            {"knit", "knit"},
            {"knew", "know"},
            {"know", "know"},
            {"laid", "lay"},
            {"led", "lead"},
            {"leaped", "leap"},
            {"leapt", "leap"},
            {"learned", "learn"},
            {"learnt", "learn"},
            {"left", "leave"},
            {"lent", "lend"},
            {"let", "let"},
            {"lay", "lie"},
            {"lain", "lie"},
            {"lighted", "light"},
            {"lit", "light"},
            {"lost", "lose"},
            {"made", "make"},
            {"meant", "mean"},
            {"met", "meet"},
            {"misspelled", "misspell"},
            {"misspelt", "misspell"},
            {"mistook", "mistake"},
            {"mistaken", "mistake"},
            {"mowed", "mow"},
            {"mown", "mow"},
            {"overcame", "overcome"},
            {"overcome", "overcome"},
            {"overdid", "overdo"},
            {"overdone", "overdo"},
            {"overtook", "overtake"},
            {"overtaken", "overtake"},
            {"overthrew", "overthrow"},
            {"overthrown", "overthrow"},
            {"paid", "pay"},
            {"pled", "plead"},
            {"proved", "prove"},
            {"proven", "prove"},
            {"put", "put"},
            {"quit", "quit"},
            {"read", "read"},
            { "rid", "rid"},
            {"rode", "ride"},
            {"ridden", "ride"},
            {"rang", "ring"},
            {"rung", "ring"},
            {"rose", "rise"},
            {"risen", "rise"},
            {"ran", "run"},
            {"run", "run"},
            {"sawed", "saw"},
            {"sawn", "saw"},
            {"said", "say"},
            {"saw", "see"},
            {"seen", "see"},
            {"sought", "seek"},
            {"sold", "sell"},
            {"sent", "send"},
            {"set", "set"},
            {"sewed", "sew"},
            {"sewn", "sew"},
            {"shook", "shake"},
            {"shaken", "shake"},
            {"shaved", "shave"},
            {"shaven", "shave"},
            {"shore", "shear"},
            {"shorn", "shear"},
            {"shed", "shed"},
            {"shone", "shine"},
            {"shoed", "shoe"},
            {"shod", "shoe"},
            {"shot", "shoot"},
            {"showed", "show"},
            {"shown", "show"},
            {"shrank", "shrink"},
            {"shrunk", "shrink"},
            {"shut", "shut"},
            {"sang", "sing"},
            {"sung", "sing"},
            {"sank", "sink"},
            {"sunk", "sink"},
            {"sat", "sit"},
            {"slept", "sleep"},
            {"slew", "slay"},
            {"slain", "slay"},
            {"slid", "slide"},
            {"slung", "sling"},
            {"slit", "slit"},
            {"smote", "smite"},
            {"smitten", "smite"},
            {"sowed", "sow"},
            {"sown", "sow"},
            {"spoke", "speak"},
            {"spoken", "speak"},
            {"sped", "speed"},
            {"spent", "spend"},
            {"spilled", "spill"},
            {"spilt", "spill"},
            {"spun", "spin"},
            {"spit", "spit"},
            {"spat", "spit"},
            {"split", "split"},
            {"spread", "spread"},
            {"sprang", "spring"},
            {"sprung", "spring"},
            {"stood", "stand"},
            {"stole", "steal"},
            {"stolen", "steal"},
            {"stuck", "stick"},
            {"stung", "sting"},
            {"stank", "stink"},
            {"stunk", "stink"},
            {"strod", "stride"},
            {"stridden", "stride"},
            {"struck", "strike"},
            {"strung", "string"},
            {"strove", "strive"},
            {"striven", "strive"},
            {"swore", "swear"},
            {"sworn", "swear"},
            {"swept", "sweep"},
            {"swelled", "swell"},
            {"swollen", "swell"},
            {"swam", "swim"},
            {"swum", "swim"},
            {"swung", "swing"},
            {"took", "take"},
            {"taken", "take"},
            {"taught", "teach"},
            {"tore", "tear"},
            {"torn", "tear"},
            {"told", "tell"},
            {"thought", "think"},
            {"thrived", "thrive"},
            {"throve", "thrive"},
            {"threw", "throw"},
            {"thrown", "throw"},
            {"thrust", "thrust"},
            {"trod", "tread"},
            {"trodden", "tread"},
            {"understood", "understand"},
            {"upheld", "uphold"},
            {"upset", "upset"},
            {"woke", "wake"},
            {"woken", "wake"},
            {"wore", "wear"},
            {"worn", "wear"},
            {"weaved", "weave"},
            {"wove", "weave"},
            {"woven", "weave"},
            {"wed", "wed"},
            {"wept", "weep"},
            {"wound", "wind"},
            {"won", "win"},
            {"withheld", "withhold"},
            {"withstood", "withstand"},
            {"wrung", "wring"},
            {"wrote", "write"},
            {"written", "write"}
        };
        private static readonly Dictionary<string, string> nouns = new()
        {
            {"alumni", "alumnus"},
            {"analyses", "analysis"},
            {"antennae", "antenna"},
            {"antennas", "antenna"},
            {"appendices", "appendix"},
            {"axes", "axis"},
            {"bacteria", "bacterium"},
            {"bases", "basis"},
            {"beaux", "beau"},
            {"bureaux", "bureau"},
            {"bureaus", "bureau"},
            {"children", "child"},
            {"corpora", "corpus"},
            {"corpuses", "corpus"},
            {"crises", "crisis"},
            {"criteria", "criterion"},
            {"curricula", "curriculum"},
            {"data", "datum"},
            {"deer", "deer"},
            {"diagnoses", "diagnosis"},
            {"ellipses", "ellipsis"},
            {"fish", "fish"},
            {"foci", "focus"},
            {"focuses", "focus"},
            {"feet", "foot"},
            {"formulae", "formula"},
            {"formulas", "formula"},
            {"fungi", "fungus"},
            {"funguses", "fungus"},
            {"genera", "genus"},
            {"geese", "goose"},
            {"hypotheses", "hypothesis"},
            {"indices/indexes", "index"},
            {"lice", "louse"},
            {"men", "man"},
            {"matrices", "matrix"},
            {"means", "means"},
            {"media", "medium"},
            {"mice", "mouse"},
            {"nebulae", "nebula"},
            {"nuclei", "nucleus"},
            {"oases", "oasis"},
            {"oxen", "ox"},
            {"paralyses", "paralysis"},
            {"parentheses", "parenthesis"},
            {"phenomena", "phenomenon"},
            {"radii", "radius"},
            {"series", "series"},
            {"sheep", "sheep"},
            {"species", "species"},
            {"stimuli", "stimulus"},
            {"strata", "stratum"},
            {"syntheses", "synthesis"},
            {"synopses", "synopsis"},
            {"tableaux", "tableau"},
            {"theses", "thesis"},
            {"teeth", "tooth"},
            {"vertebrae", "vertebra"},
            {"vitae", "vita"},
            {"women", "woman"}
        };

        public override string DisplayName => "Word stems w/ Irregular";
        public override string ToolTipText => "Word stems from the Porter Stemmer with ability to handle irregular nouns and verbs";

        public override EventSet CreateEventSet(ReadOnlyMemory<char> text) => new(base.CreateEventSet(text).Select(x => x switch
            {
                _ when verbs.TryGetValue(x.ToString(), out var verb) => new(new ReadOnlyMemory<char>(verb.ToCharArray()), this),
                _ when nouns.TryGetValue(x.ToString(), out var noun) => new(new ReadOnlyMemory<char>(noun.ToCharArray()), this),
                _ => x
            }));
    }
}
