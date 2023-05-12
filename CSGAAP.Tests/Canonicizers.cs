#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
#pragma warning disable CS0618 // Тип или член устарел

using CSGAAP.Canonicizers;
using System.Text;

namespace CSGAAP.Tests;

public class Canonicizers
{
    [Test]
    public void AddErrors()
    {
        int goodTest = 0;

        for (int i = 0; i <= 99; i++)
        {
            StringBuilder test1 = new();
            test1.Append('b', 999);

            AddErrors thing = new();
            thing["percenterror"] = 25;
            string test2 = thing.Process(test1.ToString());
            double notChanged = test2.Count(x => x == 'b');

            double probUnchanged = notChanged / 1000.0;
            double p = (double)((notChanged * (1000 - notChanged) / 1000.0) / 1000.0);
            double testStat = 1.96 * Math.Sqrt(p);

            if (.75 >= probUnchanged - testStat && .75 <= probUnchanged + testStat)
                goodTest++;
        }

        Assert.That(goodTest / 100.0, Is.GreaterThanOrEqualTo(.95));

        int[][] LA = new int[1000][];
        for (int i = 0; i < LA.GetLength(0); i++)
        {
            LA[i] = new int[26];

            StringBuilder test1 = new();
            test1.Append('b', 999);

            AddErrors thing = new();
            thing["percenterror"] = 100;
            string test2 = thing.Process(test1.ToString());
            foreach (var  j in test2)
                if (j != 'b')
                    LA[i][(int)j - 65] = LA[i][(int)j - 65] + 1;
        }

        // ANOVA

        double ssy = 0.0;
        double sst = 0.0;
        double sse;
        double mst;
        double mse;
        double yBar = 0.0;
        double F;
        double[] yBars = new double[26];

        for (int j = 0; j <= 25; j++)
        {
            yBars[j] = 0.0;
            for (int i = 0; i <= 999; i++)
                yBars[j] = yBars[j] + LA[i][j];
            yBars[j] = yBars[j] / 1000;
        }

        for (int i = 0; i <= 25; i++)
            yBar += yBars[i];

        yBar /= 26;

        for (int i = 0; i <= 999; i++)
            for (int j = 0; j <= 25; j++)
                ssy += ((LA[i][j] - yBar) * (LA[i][j] - yBar));

        for (int i = 0; i <= 25; i++)
            sst += 100 * ((yBars[i] - yBar) * (yBars[i] - yBar));

        sse = ssy - sst;

        mst = sst / 25;
        mse = sse / 25974;

        F = mst / mse;

        if (Math.Abs(F) < 1.506524)
            Assert.Pass();

        Assert.Fail();
    }

    [Test]
    public void NormalizeASCII()
    {
        char[] sample = new char[256];
        for (char c = (char)0; c < 255; c++)
            sample[c] = c;

        char[] expected = {
            '\t', '\n', (char)0x0B, '\f', '\r',
            ' ',
            '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-','.','/',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            ':', ';', '<', '=', '>', '?', '@',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '[', '\\', ']', '^', '_', '`',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '{',
            '|',
            '}',
            '~'
        };

        CollectionAssert.AreEqual(expected, new NormalizeASCII().Process(new(sample)));
    }

    [Test]
    public void NormalizeWhitespace()
    {
        Assert.That(new NormalizeWhitespace().Process("Hello  \t\nWorld!\n"), Is.EqualTo("Hello World!"));
    }

    [Test]
    public void PunctuationSeparator()
    {
        Assert.That(new PunctuationSeparator().Process("This, is ,a test.of what\", happens[']with punctuation."),
            Is.EqualTo("This , is , a test . of what \" , happens [ ' ] with punctuation ."));
    }

    [Test]
    public void SmashI()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new SmashI().Process("I don't care if IPad is supposed to be spelled with a lowercase I"),
                Is.EqualTo("i don't care if IPad is supposed to be spelled with a lowercase i"));
            Assert.That(new SmashI().Process("Sometimes I cannot think of creative things to write for unit tests."),
                Is.EqualTo("Sometimes i cannot think of creative things to write for unit tests."));
            Assert.That(new SmashI().Process("Sometimes\t\tI\tcannot think of creative things to write for unit tests."),
                Is.EqualTo("Sometimes\t\ti\tcannot think of creative things to write for unit tests."));
            Assert.That(new SmashI().Process("iIiIiiIIiiiiiiIIiiIiIiIIiiIIiiIiiIiiIIiiiIiiiIiI"),
                Is.EqualTo("iIiIiiIIiiiiiiIIiiIiIiIIiiIIiiIiiIiiIIiiiIiiiIiI"));
            Assert.That(new SmashI().Process("\"I am here\", she said."), Is.EqualTo("\"i am here\", she said."));
            Assert.That(new SmashI().Process("I'm here."), Is.EqualTo("i'm here."));
            Assert.That(new SmashI().Process("It is I."), Is.EqualTo("It is i."));
        });
    }

    [Test]
    public void StripNonPunc()
    {
        Assert.That(new StripNonPunc().Process("Hello, W;:$orld!,.?!\"'`;:-()&$"), Is.EqualTo(", ;:$!,.?!\"'`;:-()&$"));
    }

    [Test]
    public void StripNumbers()
    {
        Assert.That(new StripNumbers().Process("Pi is 3.1415;2^6 is 64"), Is.EqualTo("Pi is 0;0^0 is 0"));
    }

    [Test]
    public void StripPunctuation()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new StripPunctuation().Process("Hello, W;:$orld!.?()"), Is.EqualTo("Hello World"));
            Assert.That(new StripPunctuation().Process("Hello , W;:$orld!.?()"), Is.EqualTo("Hello World"));
        });
    }

    [Test]
    public void UnifyCase()
    {
        Assert.That(new UnifyCase().Process("Hello World!"), Is.EqualTo("hello world!"));
    }
}