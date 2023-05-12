#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
#pragma warning disable CS0618 // Тип или член устарел

using CSGAAP.EventCullers;
using CSGAAP.Generics;
using CSGAAP.Util;
using System.Numerics;

namespace CSGAAP.Tests;

public class EventCullers
{
    [Test]
    public void CoefficientOfVariation()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        CoefficientOfVariation culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("B", null),
                new("C", null),
                new("D", null),
                new("H", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void Extreme()
    {
        List<EventSet> eventSets = new();
        EventSet eventSet1 = new(new Event[] {
                new("The", null),
                new("quick", null),
                new("brown", null),
                new("fox", null),
                new("jumps", null),
                new("over", null),
                new("the", null),
                new("lazy", null),
                new("dog", null)
            });
        eventSets.Add(eventSet1);
        EventSet eventSet2 = new(new Event[] {
                new("The", null),
                new("lazy", null),
                new("grey", null),
                new("fox", null),
                new("jumps", null),
                new("over", null),
                new("the", null),
                new("dead", null),
                new("dog", null)
            });
        eventSets.Add(eventSet2);
        EventSet eventSet3 = new(new Event[] {
                new("A", null),
                new("slow", null),
                new("brown", null),
                new("fox", null),
                new("leaps", null),
                new("over", null),
                new("the", null),
                new("tired", null),
                new("dog", null)
            });
        eventSets.Add(eventSet3);

        ExtremeCuller culler = new();
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("fox", null),
                new("over", null),
                new("the", null),
                new("dog", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void IQR()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("C", null),
                new("B", null),
                new("B", null),
                new("E", null),
                new("F", null),
                new("G", null)
            }),
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("C", null),
                new("B", null),
                new("B", null),
                new("E", null),
                new("E", null),
                new("F", null)
            }),
            new(new Event[] {
                new("A", null),
                new("C", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("E", null),
                new("E", null),
                new("E", null),
                new("E", null)
            }),
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("C", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("F", null)
            }),
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("C", null),
                new("F", null),
                new("F", null),
                new("F", null),
                new("F", null),
                new("F", null),
                new("F", null)
            }),
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("C", null),
                new("E", null),
                new("E", null),
                new("E", null),
                new("E", null),
                new("E", null),
                new("E", null)
            }),
            new(new Event[] {
                new("A", null),
                new("C", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("E", null),
                new("F", null),
                new("G", null)
            })
        };

        IQRCuller culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("B", null),
                new("E", null),
                new("F", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void IndexOfDispersion()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        IndexOfDispersion culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("E", null),
                new("F", null),
                new("G", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void InformationGain()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        InformationGain culler = new();

        culler["numEvents"] = 4;
        culler["I"] = "Most";
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("B", null),
                new("E", null),
                new("F", null)
            });
        CollectionAssert.AreEqual(expected, results);

        culler["I"] = "Least";
        results = culler.Train(eventSets);
        expected = new(new Event[] {
                new("C", null),
                new("D", null),
                new("G", null),
                new("H", null)
            });
        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void LeastCommonEvents()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null)
            })
        };

        LeastCommonEvents culler = new();
        culler["numEvents"] = 1;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("C", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void MeanAbsoluteDeviation()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        MeanAbsoluteDeviation culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("B", null),
                new("E", null),
                new("F", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void MostCommonEvents()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null)
            })
        };

        MostCommonEvents culler = new();
        culler["numEvents"] = 1;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void Range()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        RangeCuller culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("B", null),
                new("E", null),
                new("F", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void StandardDeviation()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        StandardDeviationCuller culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("B", null),
                new("E", null),
                new("F", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void Variance()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        VarianceCuller culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("B", null),
                new("E", null),
                new("F", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }

    [Test]
    public void WeightedVariance()
    {
        List<EventSet> eventSets = new() {
            new(new Event[] {
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("C", null)
            }),
            new(new Event[] {
                new("A", null),
                new("B", null),
                new("C", null),
                new("D", null),
                new("E", null),
                new("F", null),
                new("F", null),
                new("G", null),
                new("H", null)
            }),
            new(new Event[] {
                new("E", null),
                new("E", null),
                new("E", null),
                new("F", null),
                new("A", null),
                new("B", null),
                new("D", null),
                new("H", null),
                new("C", null)
            })
        };

        WeightedVariance culler = new();
        culler["numEvents"] = 4;
        EventSet results = culler.Train(eventSets);
        EventSet expected = new(new Event[] {
                new("A", null),
                new("B", null),
                new("E", null),
                new("F", null)
            });

        CollectionAssert.AreEqual(expected, results);
    }
}