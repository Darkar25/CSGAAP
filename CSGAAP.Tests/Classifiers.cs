#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
#pragma warning disable CS0618 // Тип или член устарел

using CSGAAP.Classifiers;
using CSGAAP.Distances;
using CSGAAP.EventDrivers;
using CSGAAP.Generics;
using CSGAAP.Util;
using System.Net.Security;

namespace CSGAAP.Tests;

public class Classifiers
{
    [Test]
    public void NearestNeighborDriver()
    {
        NearestNeighborDriver nearest = new()
        {
            Distance = new CosineDistance()
        };

        List<Document> knowns = new();
        Document knownDocument1 = new(author: "Mary");
        knownDocument1.EventSets[new NullEventDriver()] = new(new Event[] {
                new("Mary", null),
                new("had", null),
                new("a", null),
                new("little", null),
                new("lamb", null),
                new("whose", null),
                new("fleece", null),
                new("was", null),
                new("white", null),
                new("as", null),
                new("snow.", null)
            });
        knowns.Add(knownDocument1);

        Document knownDocument2 = new(author: "Peter");
        knownDocument2.EventSets[new NullEventDriver()] = new(new Event[] {
                new("Peter", null),
                new("piper", null),
                new("picked", null),
                new("a", null),
                new("pack", null),
                new("of", null),
                new("pickled", null),
                new("peppers.", null)
            });
        knowns.Add(knownDocument2);

        Document unknownDocument = new();
        unknownDocument.EventSets[new NullEventDriver()] = new(new Event[] {
                new("Mary", null),
                new("had", null),
                new("a", null),
                new("little", null),
                new("lambda", null),
                new("whose", null),
                new("syntax", null),
                new("was", null),
                new("white", null),
                new("as", null),
                new("snow.", null)
            });

        nearest.Train(knowns);
        IEnumerable<KeyValuePair<string, double>> t = nearest.Analyze(unknownDocument);

        Assert.That(t.First().Key, Does.StartWith("Mary"));
    }

    [Test]
    public void NullAnalysis()
    {
        EventSet es = new(new Event[] {
                new("The", null),
                new("quick", null),
                new("brown", null),
                new("fox", null),
                new("jumps", null),
                new("over", null),
                new("the", null),
                new("lazy", null),
                new("dog", null),
                new(".", null)
            });

        List<Document> knowns = new();
        Document knownDocument1 = new();
        knownDocument1.EventSets[new NullEventDriver()] = es;
        knowns.Add(knownDocument1);

        Document unknownDocument = new();
        unknownDocument.EventSets[new NullEventDriver()] = es;

        var classifier = new NullAnalysis();
        classifier.Train(knowns);
        IEnumerable<KeyValuePair<string, double>> t = classifier.Analyze(unknownDocument);

        Assert.That(t.First().Key, Is.EqualTo("No analysis performed.\n"));
    }

    [Test]
    public void NullHistAnalysis()
    {
        EventSet es = new(new Event[] {
                new("The", null),
                new("quick", null),
                new("brown", null),
                new("fox", null),
                new("jumps", null),
                new("over", null),
                new("the", null),
                new("lazy", null),
                new("dog", null),
                new(".", null)
            });

        List<Document> knowns = new();
        Document knownDocument1 = new();
        knownDocument1.EventSets[new NullEventDriver()] = es;
        knowns.Add(knownDocument1);

        Document unknownDocument = new();
        unknownDocument.EventSets[new NullEventDriver()] = es;

        var classifier = new NullHistAnalysis();
        classifier.Train(knowns);
        IEnumerable<KeyValuePair<string, double>> t = classifier.Analyze(unknownDocument);

        Assert.That(t.First().Key, Is.EqualTo("No analysis performed.\n"));
    }
}