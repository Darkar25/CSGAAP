#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.

using CSGAAP.Util;

namespace CSGAAP.Tests
{
    public class Generics
    {
        [Test]
        public void EventHistogram()
        {
            EventHistogram hist = new()
            {
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("B", null),
                new("Z", null),
                new("Z", null),
                new("Z", null),
                new("Z", null),
                new("A", null),
                new("A", null),
                new("A", null),
                new("N", null),
                new("N", null),
                new("I", null)
            };

            var list = hist.Sorted.ToArray();

            Assert.Multiple(() =>
            {
                Assert.That(new KeyValuePair<Event, int>(new("I", null), 1), Is.EqualTo(list[4]));
                Assert.That(new KeyValuePair<Event, int>(new("N", null), 2), Is.EqualTo(list[3]));
                Assert.That(new KeyValuePair<Event, int>(new("A", null), 3), Is.EqualTo(list[2]));
                Assert.That(new KeyValuePair<Event, int>(new("Z", null), 4), Is.EqualTo(list[1]));
                Assert.That(new KeyValuePair<Event, int>(new("B", null), 5), Is.EqualTo(list[0]));
            });
        }
    }
}