#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.

using CSGAAP.EventDrivers;
using CSGAAP.Generics;
using CSGAAP.Util;
using System.Numerics;

namespace CSGAAP.Tests
{
    public class EventDrivers
    {
        [Test]
        public void Character()
        {
            string text = "abcdefghijklmnopqrstuvwxyz abcdefghijklmnopqrstuvwxyz.";
            CharacterEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            var tmp = new Event[]
            {
                new("a", eventDriver),
                new("b", eventDriver),
                new("c", eventDriver),
                new("d", eventDriver),
                new("e", eventDriver),
                new("f", eventDriver),
                new("g", eventDriver),
                new("h", eventDriver),
                new("i", eventDriver),
                new("j", eventDriver),
                new("k", eventDriver),
                new("l", eventDriver),
                new("m", eventDriver),
                new("n", eventDriver),
                new("o", eventDriver),
                new("p", eventDriver),
                new("q", eventDriver),
                new("r", eventDriver),
                new("s", eventDriver),
                new("t", eventDriver),
                new("u", eventDriver),
                new("v", eventDriver),
                new("w", eventDriver),
                new("x", eventDriver),
                new("y", eventDriver),
                new("z", eventDriver)
            };
            EventSet expectedEventSet = new(tmp
                .Append(new(" ", eventDriver))
                .Concat(tmp)
                .Append(new(".", eventDriver)));

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void CharacterBiGram()
        {
            string text = "abcdefghijklmnopqrstuvwxyz .";
            CharacterNGramEventDriver eventDriver = new();
            eventDriver["N"] = 2;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            var tmp = new Event[]
            {
                new("ab", eventDriver),
                new("bc", eventDriver),
                new("cd", eventDriver),
                new("de", eventDriver),
                new("ef", eventDriver),
                new("fg", eventDriver),
                new("gh", eventDriver),
                new("hi", eventDriver),
                new("ij", eventDriver),
                new("jk", eventDriver),
                new("kl", eventDriver),
                new("lm", eventDriver),
                new("mn", eventDriver),
                new("no", eventDriver),
                new("op", eventDriver),
                new("pq", eventDriver),
                new("qr", eventDriver),
                new("rs", eventDriver),
                new("st", eventDriver),
                new("tu", eventDriver),
                new("uv", eventDriver),
                new("vw", eventDriver),
                new("wx", eventDriver),
                new("xy", eventDriver),
                new("yz", eventDriver),
                new("z ", eventDriver),
                new(" .", eventDriver)
            };
            EventSet expectedEventSet = new(tmp);

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void CharacterTriGram()
        {
            string text = "abcdefghijklmnopqrstuvwxyz .";
            CharacterNGramEventDriver eventDriver = new();
            eventDriver["N"] = 3;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            var tmp = new Event[]
            {
                new("abc", eventDriver),
                new("bcd", eventDriver),
                new("cde", eventDriver),
                new("def", eventDriver),
                new("efg", eventDriver),
                new("fgh", eventDriver),
                new("ghi", eventDriver),
                new("hij", eventDriver),
                new("ijk", eventDriver),
                new("jkl", eventDriver),
                new("klm", eventDriver),
                new("lmn", eventDriver),
                new("mno", eventDriver),
                new("nop", eventDriver),
                new("opq", eventDriver),
                new("pqr", eventDriver),
                new("qrs", eventDriver),
                new("rst", eventDriver),
                new("stu", eventDriver),
                new("tuv", eventDriver),
                new("uvw", eventDriver),
                new("vwx", eventDriver),
                new("wxy", eventDriver),
                new("xyz", eventDriver),
                new("yz ", eventDriver),
                new("z .", eventDriver)
            };
            EventSet expectedEventSet = new(tmp);

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void CharacterTetraGram()
        {
            string text = "abcdefghijklmnopqrstuvwxyz .";
            CharacterNGramEventDriver eventDriver = new();
            eventDriver["N"] = 4;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            var tmp = new Event[]
            {
                new("abcd", eventDriver),
                new("bcde", eventDriver),
                new("cdef", eventDriver),
                new("defg", eventDriver),
                new("efgh", eventDriver),
                new("fghi", eventDriver),
                new("ghij", eventDriver),
                new("hijk", eventDriver),
                new("ijkl", eventDriver),
                new("jklm", eventDriver),
                new("klmn", eventDriver),
                new("lmno", eventDriver),
                new("mnop", eventDriver),
                new("nopq", eventDriver),
                new("opqr", eventDriver),
                new("pqrs", eventDriver),
                new("qrst", eventDriver),
                new("rstu", eventDriver),
                new("stuv", eventDriver),
                new("tuvw", eventDriver),
                new("uvwx", eventDriver),
                new("vwxy", eventDriver),
                new("wxyz", eventDriver),
                new("xyz ", eventDriver),
                new("yz .", eventDriver)
            };
            EventSet expectedEventSet = new(tmp);

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void NaiveWord()
        {
            string text = "We hold these truths to be self-evident,\n\"My phone # is 867-5309; don't forget it!\" she said.\n\t\t\"I won't,\" \t he grumbled.\n";

            NaiveWordEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("We", eventDriver),
                new("hold", eventDriver),
                new("these", eventDriver),
                new("truths", eventDriver),
                new("to", eventDriver),
                new("be", eventDriver),
                new("self-evident,", eventDriver),
                new("\"My", eventDriver),
                new("phone", eventDriver),
                new("#", eventDriver),
                new("is", eventDriver),
                new("867-5309;", eventDriver),
                new("don't", eventDriver),
                new("forget", eventDriver),
                new("it!\"", eventDriver),
                new("she", eventDriver),
                new("said.", eventDriver),
                new("\"I", eventDriver),
                new("won't,\"", eventDriver),
                new("he", eventDriver),
                new("grumbled.", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void Null()
        {
            string text = "sir I send a rhyme excelling\nin sacred truth and rigid spelling\nnumerical sprites elucidate\nfor me the lexicons full weight\nif nature gain who can complain\ntho dr johnson fulminate";

            NullEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("sir I send a rhyme excelling\nin sacred truth and rigid spelling\nnumerical sprites elucidate\nfor me the lexicons full weight\nif nature gain who can complain\ntho dr johnson fulminate", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void DisLegomena()
        {
            string text = "The Quick Brown Fox Jumped Over The Lazy Dog 3 3 3 4 4 4 4 5 5 5 5 5";

            RareWordsEventDriver eventDriver = new();
            eventDriver["M"] = 2;
            eventDriver["N"] = 2;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("The", eventDriver),
                new("The", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void FirstWordInSentence()
        {
            string text = "Hello, Dr. Jones!  I'm not.feeling.too well today.  What's the matter Mr. Adams?  My stomach hurts, or A.K.A, cramps.";

            FirstWordInSentenceEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("Hello,", eventDriver),
                new("I'm", eventDriver),
                new("What's", eventDriver),
                new("My", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void Freq()
        {
            string text = "a aah Aaron aback abacus abandon abandoned zones zoning zoo zoologist zoology zoom zooming zooms zucchini Zurich";

            FreqEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("16.18", eventDriver),
                new("5.40", eventDriver),
                new("9.29", eventDriver),
                new("5.96", eventDriver),
                new("6.24", eventDriver),
                new("8.23", eventDriver),
                new("8.55", eventDriver),
                new("8.13", eventDriver),
                new("6.71", eventDriver),
                new("8.37", eventDriver),
                new("5.71", eventDriver),
                new("6.56", eventDriver),
                new("8.50", eventDriver),
                new("6.26", eventDriver),
                new("5.95", eventDriver),
                new("5.75", eventDriver),
                new("7.48", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void HDLegomena()
        {
            string text = "Jack be nimble, Jack be quick, Jack jump over the candlestick.";

            RareWordsEventDriver eventDriver = new();
            eventDriver["M"] = 1;
            eventDriver["N"] = 2;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("be", eventDriver),
                new("nimble,", eventDriver),
                new("be", eventDriver),
                new("quick,", eventDriver),
                new("jump", eventDriver),
                new("over", eventDriver),
                new("the", eventDriver),
                new("candlestick.", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void HapaxLegomena()
        {
            string text = "The Quick Brown Fox Jumped Over The Lazy Dog 3 3 3 4 4 4 4 5 5 5 5 5";

            RareWordsEventDriver eventDriver = new();
            eventDriver["M"] = 1;
            eventDriver["N"] = 1;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("Quick", eventDriver),
                new("Brown", eventDriver),
                new("Fox", eventDriver),
                new("Jumped", eventDriver),
                new("Over", eventDriver),
                new("Lazy", eventDriver),
                new("Dog", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void KSkipNGramCharacter()
        {
            string text = "Lorem ipsum dolor sit amet, ";

            KSkipNGramCharacterEventDriver eventDriver = new();
            eventDriver["K"] = 3;
            eventDriver["N"] = 4;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("L m s d", eventDriver),
                new("o   u o", eventDriver),
                new("r i m l", eventDriver),
                new("e p   o", eventDriver),
                new("m s d r", eventDriver),
                new("u o", eventDriver),
                new("i m l s", eventDriver),
                new("p   o i", eventDriver),
                new("s d r t", eventDriver),
                new("u o", eventDriver),
                new("m l s a", eventDriver),
                new("o i m", eventDriver),
                new("d r t e", eventDriver),
                new("o     t", eventDriver),
                new("l s a ,", eventDriver),
                new("o i m", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void KSkipNGramWord()
        {
            string text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.";

            KSkipNGramWordEventDriver eventDriver = new();
            eventDriver["K"] = 3;
            eventDriver["N"] = 4;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("Lorem amet, Maecenas Fusce", eventDriver),
                new("ipsum consectetuer porttitor posuere,", eventDriver),
                new("dolor adipiscing congue magna", eventDriver),
                new("sit elit. massa. sed", eventDriver),
                new("amet, Maecenas Fusce pulvinar", eventDriver),
                new("consectetuer porttitor posuere, ultricies,", eventDriver),
                new("adipiscing congue magna purus", eventDriver),
                new("elit. massa. sed lectus", eventDriver),
                new("Maecenas Fusce pulvinar malesuada", eventDriver),
                new("porttitor posuere, ultricies, libero,", eventDriver),
                new("congue magna purus sit", eventDriver),
                new("massa. sed lectus amet", eventDriver),
                new("Fusce pulvinar malesuada commodo", eventDriver),
                new("posuere, ultricies, libero, magna", eventDriver),
                new("magna purus sit eros", eventDriver),
                new("sed lectus amet quis", eventDriver),
                new("pulvinar malesuada commodo urna.", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void LineLength()
        {
            string text = @"There once was a man from Nantucket
Who kept all his cash in a bucket.
	But his daughter, named Nan,
	Ran away with a man
And as for the bucket, Nantucket.

But he followed the pair to Pawtucket,
The man and the girl with the bucket;
	And he said to the man,
	He was welcome to Nan,
But as for the bucket, Pawtucket.

Then the pair followed Pa to Manhasset,
Where he still held the cash as an asset,
	But Nan and the man
	Stole the money and ran,
And as for the asset, Manhasset.";

            LineLengthEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("7", eventDriver),
                new("8", eventDriver),
                new("5", eventDriver),
                new("5", eventDriver),
                new("6", eventDriver),
                new("7", eventDriver),
                new("8", eventDriver),
                new("6", eventDriver),
                new("5", eventDriver),
                new("6", eventDriver),
                new("7", eventDriver),
                new("9", eventDriver),
                new("5", eventDriver),
                new("5", eventDriver),
                new("6", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void MNLetterWord()
        {
            string text = "a b c d e f g h i j k l m n o p q r s t u v w x y z " +
"aa bb cc dd ee ff gg hh ii jj kk ll mm nn oo pp qq rr ss tt uu vv ww xx yy zz " +
"aaa bbb ccc " +
"aaaa bbbb cccc " +
"aaaaa bbbbb ccccc ddddd eeeee fffff ggggg hhhhh iiiii jjjjj kkkkk lllll mmmmm nnnnn ooooo ppppp qqqqq rrrrr sssss ttttt uuuuu vvvvv wwwww xxxxx yyyyy zzzzz " +
"A B C D E F G H I J K L M N O P Q R S T U V W X Y Z " +
"AA BB CC DD EE FF GG HH II JJ KK LL MM NN OO PP QQ RR SS TT UU VV WW XX YY ZZ " +
"AAA BBB CCC " +
"AAAA BBBB CCCC " +
"AAAAA BBBBB CCCCC DDDDD EEEEE FFFFF GGGGG HHHHH IIIII JJJJJ KKKKK LLLLL MMMMM NNNNN OOOOO PPPPP QQQQQ RRRRR SSSSS TTTTT UUUUU VVVVV WWWWW XXXXX YYYYY ZZZZZ" +
"1 22 333 4444 55555 " +
"! @@ ### $$$$ %%%%% ";

            MNLetterWordEventDriver eventDriver = new();
            eventDriver["M"] = 3;
            eventDriver["N"] = 4;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("aaa", eventDriver),
                new("bbb", eventDriver),
                new("ccc", eventDriver),
                new("aaaa", eventDriver),
                new("bbbb", eventDriver),
                new("cccc", eventDriver),
                new("AAA", eventDriver),
                new("BBB", eventDriver),
                new("CCC", eventDriver),
                new("AAAA", eventDriver),
                new("BBBB", eventDriver),
                new("CCCC", eventDriver),
                new("333", eventDriver),
                new("4444", eventDriver),
                new("###", eventDriver),
                new("$$$$", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void MWFunctionWords()
        {
            string text = @"a all also an and any are as at be been but by can do down
even every for from had has have her his if in into is it its
may more must my no not now of on one only or our shall should
so some such than that the their then there things this to up
upon was were what when which who will with would your
distractor fail eliminate megafail lose gark hoser shimatta";

            MWFunctionWordsEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("a", eventDriver),
                new("all", eventDriver),
                new("also", eventDriver),
                new("an", eventDriver),
                new("and", eventDriver),
                new("any", eventDriver),
                new("are", eventDriver),
                new("as", eventDriver),
                new("at", eventDriver),
                new("be", eventDriver),
                new("been", eventDriver),
                new("but", eventDriver),
                new("by", eventDriver),
                new("can", eventDriver),
                new("do", eventDriver),
                new("down", eventDriver),
                new("even", eventDriver),
                new("every", eventDriver),
                new("for", eventDriver),
                new("from", eventDriver),
                new("had", eventDriver),
                new("has", eventDriver),
                new("have", eventDriver),
                new("her", eventDriver),
                new("his", eventDriver),
                new("if", eventDriver),
                new("in", eventDriver),
                new("into", eventDriver),
                new("is", eventDriver),
                new("it", eventDriver),
                new("its", eventDriver),
                new("may", eventDriver),
                new("more", eventDriver),
                new("must", eventDriver),
                new("my", eventDriver),
                new("no", eventDriver),
                new("not", eventDriver),
                new("now", eventDriver),
                new("of", eventDriver),
                new("on", eventDriver),
                new("one", eventDriver),
                new("only", eventDriver),
                new("or", eventDriver),
                new("our", eventDriver),
                new("shall", eventDriver),
                new("should", eventDriver),
                new("so", eventDriver),
                new("some", eventDriver),
                new("such", eventDriver),
                new("than", eventDriver),
                new("that", eventDriver),
                new("the", eventDriver),
                new("their", eventDriver),
                new("then", eventDriver),
                new("there", eventDriver),
                new("things", eventDriver),
                new("this", eventDriver),
                new("to", eventDriver),
                new("up", eventDriver),
                new("upon", eventDriver),
                new("was", eventDriver),
                new("were", eventDriver),
                new("what", eventDriver),
                new("when", eventDriver),
                new("which", eventDriver),
                new("who", eventDriver),
                new("will", eventDriver),
                new("with", eventDriver),
                new("would", eventDriver),
                new("your", eventDriver),
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void NamingTime()
        {
            string text = "a aah Aaron aback abacus abandon abandoned zones zoning zoo zoologist zoology zoom zooming zooms zucchini Zurich";

            NamingTimeEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("662.09", eventDriver),
                new("646.40", eventDriver),
                new("686.11", eventDriver),
                new("596.54", eventDriver),
                new("792.69", eventDriver),
                new("623.96", eventDriver),
                new("635.16", eventDriver),
                new("590.08", eventDriver),
                new("694.85", eventDriver),
                new("662.57", eventDriver),
                new("732.70", eventDriver),
                new("687.12", eventDriver),
                new("639.86", eventDriver),
                new("672.37", eventDriver),
                new("613.83", eventDriver),
                new("756.00", eventDriver),
                new("822.64", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        // Unable to test, MaxentTagger takes forever to initialize, idk why
        /*[Test]
        public void POSBiGram()
        {
            string text = "There once was a man from Nantucket , " +
                           "who kept all his cash in a bucket , " +
                           "but his daughter, named Nan , " +
                           "ran away with a man, " +
                           "and as for the bucket, Nantucket .";

            POSNGramEventDriver eventDriver = new();
            eventDriver["N"] = 2;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("[EX, RB]", eventDriver),
                new("[RB, VBD]", eventDriver),
                new("[VBD, DT]", eventDriver),
                new("[DT, NN]", eventDriver),
                new("[NN, IN]", eventDriver),
                new("[IN, NNP]", eventDriver),
                new("[NNP, ,]", eventDriver),
                new("[,, WP]", eventDriver),
                new("[WP, VBD]", eventDriver),
                new("[VBD, DT]", eventDriver),
                new("[DT, PRP$]", eventDriver),
                new("[PRP$, NN]", eventDriver),
                new("[NN, IN]", eventDriver),
                new("[IN, DT]", eventDriver),
                new("[DT, NN]", eventDriver),
                new("[NN, ,]", eventDriver),
                new("[,, CC]", eventDriver),
                new("[CC, PRP$]", eventDriver),
                new("[PRP$, NN]", eventDriver),
                new("[NN, ,]", eventDriver),
                new("[,, VBN]", eventDriver),
                new("[VBN, NN]", eventDriver),
                new("[NN, ,]", eventDriver),
                new("[,, VBD]", eventDriver),
                new("[VBD, RB]", eventDriver),
                new("[RB, IN]", eventDriver),
                new("[IN, DT]", eventDriver),
                new("[DT, NN]", eventDriver),
                new("[NN, ,]", eventDriver),
                new("[,, CC]", eventDriver),
                new("[CC, IN]", eventDriver),
                new("[IN, IN]", eventDriver),
                new("[IN, DT]", eventDriver),
                new("[DT, NN]", eventDriver),
                new("[NN, ,]", eventDriver),
                new("[,, NNP]", eventDriver),
                new("[NNP, .]", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }*/

        [Test]
        public void ReactionTime()
        {
            string text = "a aah Aaron aback abacus abandon abandoned zones zoning zoo zoologist zoology zoom zooming zooms zucchini Zurich";

            ReactionTimeEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("798.92", eventDriver),
                new("816.43", eventDriver),
                new("736.06", eventDriver),
                new("796.27", eventDriver),
                new("964.40", eventDriver),
                new("695.72", eventDriver),
                new("860.77", eventDriver),
                new("605.23", eventDriver),
                new("726.43", eventDriver),
                new("572.56", eventDriver),
                new("714.09", eventDriver),
                new("685.28", eventDriver),
                new("549.76", eventDriver),
                new("709.69", eventDriver),
                new("666.93", eventDriver),
                new("848.68", eventDriver),
                new("763.00", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void RareWords()
        {
            string text = "one " +
                            "two two  " +
                            "three three three " +
                            "four four four four " +
                            "five five five five five " +
                            "six six six six six six";

            RareWordsEventDriver eventDriver = new();
            eventDriver["M"] = 4;
            eventDriver["N"] = 5;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("four", eventDriver),
                new("four", eventDriver),
                new("four", eventDriver),
                new("four", eventDriver),
                new("five", eventDriver),
                new("five", eventDriver),
                new("five", eventDriver),
                new("five", eventDriver),
                new("five", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void Sentence()
        {
            string text = "Hello, Dr. Jones!  I'm not.feeling.too well today.  What's the matter Mr. Adams?  My stomach hurts, A.K.A, cramps.";

            SentenceEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("Hello, Dr. Jones!", eventDriver),
                new("I'm not.feeling.too well today.", eventDriver),
                new("What's the matter Mr. Adams?", eventDriver),
                new("My stomach hurts, A.K.A, cramps.", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void SentenceLengthWithWords()
        {
            string text = "Hello, Dr. Jones!  I'm not.feeling.too well today.  What's the matter Mr. Adams? My stomach hurts, or A.K.A, cramps.";

            SentenceLengthWithWordsEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("3", eventDriver),
                new("4", eventDriver),
                new("5", eventDriver),
                new("6", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void VowelInitialWord()
        {
            string text =   "alpha bravo charlie delta echo foxtrot golf hotel india " +
                            "juliet kilo lima mike november oscar papa quebec romeo " +
                            "sierra tango uniform victor whiskey x-ray yankee zebra " +
                            "Alpha Bravo Charlie Delta Echo Foxtrot Golf Hotel India " +
                            "Juliet Kilo Lima Mike November Oscar Papa Quebec Romeo " +
                            "Sierra Tango Uniform Victor Whiskey X-ray Yankee Zebra " +
                            "_none ?of #these *should 1be 4included +in ^output";

            VowelInitialWordEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("alpha", eventDriver),
                new("echo", eventDriver),
                new("india", eventDriver),
                new("oscar", eventDriver),
                new("uniform", eventDriver),
                new("yankee", eventDriver),
                new("Alpha", eventDriver),
                new("Echo", eventDriver),
                new("India", eventDriver),
                new("Oscar", eventDriver),
                new("Uniform", eventDriver),
                new("Yankee", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void WordBiGram()
        {
            string text = @"Mary had a little lamb;
Its fleece was white as snow.
And everywhere that Mary went,
The lamb was sure to go.";

            WordNGramEventDriver eventDriver = new();
            eventDriver["N"] = 2;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("[Mary, had]", eventDriver),
                new("[had, a]", eventDriver),
                new("[a, little]", eventDriver),
                new("[little, lamb;]", eventDriver),
                new("[lamb;, Its]", eventDriver),
                new("[Its, fleece]", eventDriver),
                new("[fleece, was]", eventDriver),
                new("[was, white]", eventDriver),
                new("[white, as]", eventDriver),
                new("[as, snow.]", eventDriver),
                new("[snow., And]", eventDriver),
                new("[And, everywhere]", eventDriver),
                new("[everywhere, that]", eventDriver),
                new("[that, Mary]", eventDriver),
                new("[Mary, went,]", eventDriver),
                new("[went,, The]", eventDriver),
                new("[The, lamb]", eventDriver),
                new("[lamb, was]", eventDriver),
                new("[was, sure]", eventDriver),
                new("[sure, to]", eventDriver),
                new("[to, go.]", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void WordTriGram()
        {
            string text = @"Mary had a little lamb;
Its fleece was white as snow.
And everywhere that Mary went,
The lamb was sure to go.";

            WordNGramEventDriver eventDriver = new();
            eventDriver["N"] = 3;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("[Mary, had, a]", eventDriver),
                new("[had, a, little]", eventDriver),
                new("[a, little, lamb;]", eventDriver),
                new("[little, lamb;, Its]", eventDriver),
                new("[lamb;, Its, fleece]", eventDriver),
                new("[Its, fleece, was]", eventDriver),
                new("[fleece, was, white]", eventDriver),
                new("[was, white, as]", eventDriver),
                new("[white, as, snow.]", eventDriver),
                new("[as, snow., And]", eventDriver),
                new("[snow., And, everywhere]", eventDriver),
                new("[And, everywhere, that]", eventDriver),
                new("[everywhere, that, Mary]", eventDriver),
                new("[that, Mary, went,]", eventDriver),
                new("[Mary, went,, The]", eventDriver),
                new("[went,, The, lamb]", eventDriver),
                new("[The, lamb, was]", eventDriver),
                new("[lamb, was, sure]", eventDriver),
                new("[was, sure, to]", eventDriver),
                new("[sure, to, go.]", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void WordTetraGram()
        {
            string text = @"Mary had a little lamb, little lamb. Its fleece was white as snow.";

            WordNGramEventDriver eventDriver = new();
            eventDriver["N"] = 4;
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("[Mary, had, a, little]", eventDriver),
                new("[had, a, little, lamb,]", eventDriver),
                new("[a, little, lamb,, little]", eventDriver),
                new("[little, lamb,, little, lamb.]", eventDriver),
                new("[lamb,, little, lamb., Its]", eventDriver),
                new("[little, lamb., Its, fleece]", eventDriver),
                new("[lamb., Its, fleece, was]", eventDriver),
                new("[Its, fleece, was, white]", eventDriver),
                new("[fleece, was, white, as]", eventDriver),
                new("[was, white, as, snow.]", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);
        }

        [Test]
        public void WordLength()
        {
            string text = @"sir I send a rhyme excelling
in sacred truth and rigid spelling
numerical sprites elucidate
for me the lexicons full weight
if nature gain who can complain
tho dr johnson fulminate";

            WordLengthEventDriver eventDriver = new();
            EventSet sampleEventSet = eventDriver.CreateEventSet(text);
            EventSet expectedEventSet = new(new Event[]
            {
                new("3", eventDriver),
                new("1", eventDriver),
                new("4", eventDriver),
                new("1", eventDriver),
                new("5", eventDriver),
                new("9", eventDriver),
                new("2", eventDriver),
                new("6", eventDriver),
                new("5", eventDriver),
                new("3", eventDriver),
                new("5", eventDriver),
                new("8", eventDriver),
                new("9", eventDriver),
                new("7", eventDriver),
                new("9", eventDriver),
                new("3", eventDriver),
                new("2", eventDriver),
                new("3", eventDriver),
                new("8", eventDriver),
                new("4", eventDriver),
                new("6", eventDriver),
                new("2", eventDriver),
                new("6", eventDriver),
                new("4", eventDriver),
                new("3", eventDriver),
                new("3", eventDriver),
                new("8", eventDriver),
                new("3", eventDriver),
                new("2", eventDriver),
                new("7", eventDriver),
                new("9", eventDriver),
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);

            text = "`the' quick brown \"fox\" isn't very? dumb!";

            sampleEventSet = eventDriver.CreateEventSet(text);
            expectedEventSet = new(new Event[]
            {
                new("5", eventDriver),
                new("5", eventDriver),
                new("5", eventDriver),
                new("5", eventDriver),
                new("5", eventDriver),
                new("5", eventDriver),
                new("5", eventDriver)
            });

            CollectionAssert.AreEqual(expectedEventSet, sampleEventSet);

            text = "\t         \t\n";

            sampleEventSet = eventDriver.CreateEventSet(text);

            CollectionAssert.AreEqual(Array.Empty<Event>(), sampleEventSet);
        }
    }
}