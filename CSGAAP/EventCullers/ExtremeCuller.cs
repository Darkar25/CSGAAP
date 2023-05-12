using CSGAAP.Generics;
using StructLinq;

namespace CSGAAP.EventCullers
{
    public class ExtremeCuller : FilterEventCuller
    {
        public override string DisplayName => "X-treme Culler";
        public override string ToolTipText => "All Events that appear in all samples.";
        public override string LongDescription => "Analyzes only those Events appear in all samples [as suggested by (Jockers, 2008)].";

        public override EventSet Train(IEnumerable<EventSet> eventSets)
        {
            var es = eventSets.ToStructEnumerable();
            return new(es
            .SelectMany(x => x)
            .Where(x => es
                .All(y => y
                    .Contains(x), x => x), x => x)
            .Distinct(x => x)
            .ToEnumerable());
        }
    }
}
