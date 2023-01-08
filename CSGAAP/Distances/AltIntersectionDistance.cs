using CSGAAP.Generics;
using StructLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGAAP.Distances
{
    public class AltIntersectionDistance : DistanceFunction
    {
        public override string DisplayName => "Alt Intersection Distance";
        public override string ToolTipText => "One over Event type set intersection plus one";

        public override double Distance(IHistogram h1, IHistogram h2) => 1 / (h1.UniqueEvents
            .ToStructEnumerable()
            .Intersect(h2.UniqueEvents
                .ToStructEnumerable())
            .Count(x => x) + 1);
    }
}
