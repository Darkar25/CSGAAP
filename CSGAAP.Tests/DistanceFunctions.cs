#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
#pragma warning disable CS0618 // Тип или член устарел

using CSGAAP.Distances;
using CSGAAP.Generics;
using CSGAAP.Util;

namespace CSGAAP.Tests;

public class DistanceFunctions
{
    public const double Epsilon = 1E-10d;

    [Test]
    public void AngularSeparation()
    {

        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new AngularSeparationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.EqualTo(.9d).Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new AngularSeparationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d).Within(Epsilon));
    }

    [Test]
    public void Bhattacharyya()
    {
        var events = new Event[] {
                new("Lorem", null),
                new("Lorem", null),
                new("ipsum", null),
                new("ipsum", null),
                new("ipsum", null),
                new("ipsum", null),
                new("ipsum", null)
            };
        Assert.That(new BhattacharyyaDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(double.IsInfinity(new BhattacharyyaDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2)))), Is.True);
    }

    [Test]
    public void BrayCurtis()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new BrayCurtisDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new BrayCurtisDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d).Within(Epsilon));
    }

    [Test]
    public void Canberra()
    {
        var events = new Event[] {
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
            };
        Assert.That(new CanberraDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        var events2 = new Event[] {
                new("3", null),
                new("..", null),
                new("1", null),
                new("4", null),
                new("11", null),
                new("5", null),
                new("2", null),
                new("6", null),
                new("55", null),
                new("33", null)
            };
        Assert.That(new CanberraDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(20d).Within(Epsilon));
    }

    [Test]
    public void ChiSquare()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new ChiSquareDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new ChiSquareDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(2d).Within(Epsilon));
    }

    [Test]
    public void Chord()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new ChordDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.EqualTo(Math.Sqrt(1.8d)).Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new ChordDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(Math.Sqrt(2d)).Within(Epsilon));
    }

    [Test]
    public void Cosine()
    {
        var events = new Event[] {
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
            };
        Assert.That(new CosineDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("3", null),
                new("..", null),
                new("1", null),
                new("4", null),
                new("11", null),
                new("5", null),
                new("2", null),
                new("6", null),
                new("55", null),
                new("33", null)
            };
        Assert.That(new CosineDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d).Within(Epsilon));

        events = new Event[]
        {
                new("alpha", null)
        };
        events2 = new Event[]
        {
                new("alpha", null),
                new("alpha", null)
        };
        Assert.That(new CosineDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.Zero.Within(Epsilon));

        events2 = new Event[]
        {
                new("alpha", null),
                new("beta", null)
        };
        Assert.That(new CosineDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(0.29289321881345d).Within(Epsilon));
    }

    [Test]
    public void CrossEntropyDivergence()
    {
        var events = new Event[] {
                new("mary", null),
                new("had", null),
                new("a", null),
                new("little", null),
                new("lamb", null),
                new("whose", null),
                new("fleece", null),
                new("was", null),
                new("white", null),
                new("as", null),
                new("snow", null)
            };
        Assert.That(new CrossEntropyDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.EqualTo(2.3978952d).Within(0.000001));

        events = new Event[]
        {
                new("alpha", null),
                new("beta", null)
        };
        var events2 = new Event[]
        {
                new("alpha", null),
                new("alpha", null),
                new("alpha", null),
                new("beta", null)
        };
        Assert.Multiple(() =>
        {
            Assert.That(new CrossEntropyDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(0.836988d).Within(0.000001));
            Assert.That(new CrossEntropyDivergence().Distance(new EventMap(new EventSet(events2)), new EventMap(new EventSet(events))), Is.EqualTo(0.693147d).Within(0.000001));
        });

        events2 = new Event[]
        {
                new("alpha", null),
                new("alpha", null),
                new("beta", null),
                new("gamma", null)
        };
        Assert.Multiple(() =>
        {
            Assert.That(new CrossEntropyDivergence().Distance(new EventMap(new EventSet(events2)), new EventMap(new EventSet(events))), Is.EqualTo(0.5198603854199589d).Within(Epsilon));
            Assert.That(new CrossEntropyDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1.0397207d).Within(0.000001));
        });
    }

    [Test]
    public void Histogram()
    {
        var events = new Event[] {
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
            };
        Assert.That(new HistogramDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        var events2 = new Event[] {
                new("3", null),
                new("..", null),
                new("1", null),
                new("4", null),
                new("11", null),
                new("5", null),
                new("2", null),
                new("6", null),
                new("55", null),
                new("33", null)
            };
        Assert.That(new HistogramDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(.2d).Within(Epsilon));
    }

    [Test]
    public void HistogramIntersection()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new HistogramIntersectionDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new HistogramIntersectionDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d).Within(Epsilon));
    }

    [Test]
    public void Intersection()
    {
        var events = new Event[]
        {
                new("alpha", null),
                new("alpha", null),
                new("beta", null),
                new("gamma", null)
        };
        Assert.That(new IntersectionDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        var events2 = new Event[]
        {
                new("alpha", null),
                new("beta", null),
                new("beta", null),
                new("beta", null),
                new("beta", null),
                new("beta", null),
                new("beta", null),
                new("gamma", null)
        };
        Assert.That(new IntersectionDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.Zero);

        events2 = new Event[]
        {
                new("omega", null)
        };
        Assert.That(new IntersectionDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d));

        events2 = new Event[]
        {
                new("gamma", null),
                new("delta", null),
                new("epsilon", null)
        };
        Assert.That(new IntersectionDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(.8d).Within(Epsilon));

        events = new Event[]
        {
                new("alpha", null),
                new("beta", null),
                new("gamma", null),
                new("delta", null)
        };
        events2 = new Event[]
        {
                new("delta", null)
        };
        Assert.Multiple(() =>
        {
            Assert.That(new IntersectionDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(.75d).Within(Epsilon));
            Assert.That(new IntersectionDistance().Distance(new EventMap(new EventSet(events2)), new EventMap(new EventSet(events))), Is.EqualTo(.75d).Within(Epsilon));
        });
    }

    [Test]
    public void KendallCorrelation()
    {
        var events = new Event[]
        {
                new("alpha", null),
                new("alpha", null),
                new("alpha", null),
                new("alpha", null),
                new("beta", null),
                new("beta", null),
                new("beta", null),
                new("gamma", null),
                new("gamma", null),
                new("delta", null)
        };
        Assert.That(new KendallCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        events = new Event[]
        {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("D", null),
                new("D", null),
                new("E", null)
        };
        var events2 = new Event[]
        {
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("D", null),
                new("D", null),
                new("D", null),
                new("D", null),
                new("D", null),
                new("E", null),
                new("E", null)
        };
        Assert.That(new KendallCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1.2d).Within(Epsilon));

        events = new Event[]
        {
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("C", null)
        };
        events2 = new Event[]
        {
                new("C", null),
                new("C", null),
                new("C", null),
                new("B", null),
                new("B", null),
                new("A", null)
        };
        Assert.That(new KendallCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(2d).Within(Epsilon));
    }

    [Test]
    public void KendallCorrelationTauB()
    {
        var events = new Event[]
        {
                new("alpha", null),
                new("alpha", null),
                new("alpha", null),
                new("alpha", null),
                new("beta", null),
                new("beta", null),
                new("beta", null),
                new("gamma", null),
                new("gamma", null),
                new("delta", null)
        };
        Assert.That(new KendallCorrelationTauBDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        events = new Event[]
        {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("D", null),
                new("D", null),
                new("E", null)
        };
        var events2 = new Event[]
        {
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("D", null),
                new("D", null),
                new("D", null),
                new("D", null),
                new("D", null),
                new("E", null),
                new("E", null)
        };
        Assert.That(new KendallCorrelationTauBDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1.2d).Within(Epsilon));

        events = new Event[]
        {
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("C", null)
        };
        events2 = new Event[]
        {
                new("C", null),
                new("C", null),
                new("C", null),
                new("B", null),
                new("B", null),
                new("A", null)
        };
        Assert.That(new KendallCorrelationTauBDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(2d).Within(Epsilon));
    }

    [Test]
    public void KeseljWeighted()
    {
        var events = new Event[] {
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
            };
        Assert.That(new KeseljWeightedDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        var events2 = new Event[] {
                new("3", null),
                new("..", null),
                new("1", null),
                new("4", null),
                new("11", null),
                new("5", null),
                new("2", null),
                new("6", null),
                new("55", null),
                new("33", null)
            };
        Assert.That(new KeseljWeightedDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(20d).Within(Epsilon));

        events2 = new Event[] {
                new("The", null),
                new("quick", null),
                new("brown", null),
                new("fox", null),
                new("jumps", null)
            };
        Assert.That(new KeseljWeightedDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(5.5555555555d).Within(Epsilon));
    }

    [Test]
    public void KullbackLeiblerDivergence()
    {
        var events = new Event[] {
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
            };
        Assert.Multiple(() =>
        {
            Assert.That(new KullbackLeiblerDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);
            Assert.That(new KullbackLeiblerDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events.Concat(events)))), Is.Zero);
        });

        events = new Event[]
        {
                new("alpha", null),
                new("beta", null)
        };
        var events2 = new Event[]
        {
                new("alpha", null),
                new("alpha", null),
                new("alpha", null),
                new("beta", null)
        };
        Assert.Multiple(() =>
        {
            Assert.That(new KullbackLeiblerDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(0.1438410d).Within(0.00001d));
            Assert.That(new KullbackLeiblerDivergence().Distance(new EventMap(new EventSet(events2)), new EventMap(new EventSet(events))), Is.EqualTo(0.13081203594d).Within(Epsilon));
        });

        events2 = new Event[]
        {
                new("alpha", null),
                new("alpha", null),
                new("beta", null),
                new("gamma", null)
        };
        Assert.That(new KullbackLeiblerDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(0.346574d).Within(0.00001d));
    }

    [Test]
    public void Manhattan()
    {
        var events = new Event[] {
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
            };
        Assert.That(new ManhattanDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        var events2 = new Event[] {
                new("3", null),
                new("..", null),
                new("1", null),
                new("4", null),
                new("11", null),
                new("5", null),
                new("2", null),
                new("6", null),
                new("55", null),
                new("33", null)
            };
        Assert.That(new ManhattanDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(2d).Within(Epsilon));
    }

    [Test]
    public void Matusita()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new MatusitaDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new MatusitaDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(Math.Sqrt(2d)).Within(Epsilon));
    }

    [Test]
    public void NominalKS()
    {
        var events = new Event[] {
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
            };
        Assert.That(new NominalKSDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        var events2 = new Event[] {
                new("33", null),
                new("5", null),
                new("6", null),
                new("8", null),
                new("44", null),
                new("7", null),
                new("33", null),
                new("10", null),
                new("2", null),
                new("..", null)
            };
        Assert.That(new NominalKSDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d).Within(Epsilon));
    }

    [Test]
    public void PearsonCorrelation()
    {
        var events = new Event[]
        {
                new("alpha", null),
                new("beta", null),
                new("beta", null),
                new("gamma", null),
                new("gamma", null),
                new("gamma", null)
        };
        Assert.That(new PearsonCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero);

        events = new Event[]
        {
                new("A", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null)
        };
        var events2 = new Event[]
        {
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("D", null),
                new("D", null),
                new("E", null),
                new("E", null)
        };
        Assert.That(new PearsonCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.Zero.Within(Epsilon));

        events = new Event[]
        {
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("C", null)
        };
        events2 = new Event[]
        {
                new("C", null),
                new("C", null),
                new("C", null),
                new("B", null),
                new("B", null),
                new("A", null)
        };
        Assert.That(new PearsonCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(2d).Within(Epsilon));

        events = new Event[]
        {
                new("A", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("C", null)
        };
        events2 = new Event[]
        {
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("C", null),
                new("C", null)
        };
        Assert.That(new PearsonCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1 - .9608d).Within(0.001d));

        events = new Event[]
        {
                new("A", null),
                new("B", null),
                new("C", null)
        };
        events2 = new Event[]
        {
                new("A", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("C", null),
                new("C", null)
        };
        Assert.That(new PearsonCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d).Within(0.001d));

        events = new Event[]
        {
                new("A", null),
                new("B", null),
                new("C", null)
        };
        events2 = new Event[]
        {
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("C", null),
                new("C", null)
        };
        Assert.That(new PearsonCorrelationDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.Zero.Within(0.001d));
    }

    [Test]
    public void Soergle()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new SoergleDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new SoergleDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(1d).Within(Epsilon));
    }

    [Test]
    public void WEDDivergence()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new WEDDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new WEDDivergence().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(Math.Sqrt(.11d)).Within(Epsilon));
    }

    [Test]
    public void WaveHedges()
    {
        var events = new Event[] {
                new("one", null),
                new("two", null),
                new("three", null),
                new("four", null),
                new("five", null),
                new("six", null),
                new("seven", null),
                new("eight", null),
                new("nine", null),
                new("ten", null)
            };
        Assert.That(new WaveHedgesDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events))), Is.Zero.Within(Epsilon));

        var events2 = new Event[] {
                new("1", null),
                new("2", null),
                new("3", null),
                new("4", null),
                new("5", null),
                new("6", null),
                new("7", null),
                new("8", null),
                new("9", null),
                new("10", null)
            };
        Assert.That(new WaveHedgesDistance().Distance(new EventMap(new EventSet(events)), new EventMap(new EventSet(events2))), Is.EqualTo(20d).Within(Epsilon));
    }
}