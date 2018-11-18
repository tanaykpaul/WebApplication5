using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication5.Triangles
{
    public class Scalene : ITriangle
    {
        public TriangleOutcome GetSides(TriangleDimensions dimensions)
        {
            if (dimensions.Sides == null || dimensions.Sides.Count != 3) return null;

            var maxIndex = dimensions.Sides.Select((value, index) => new {Value = value, Index = index})
                .Aggregate((a, b) => (a.Value > b.Value) ? a : b)
                .Index;
            int minIndex1 = -1, minIndex2 = -1;
            for (var i = 0; i < 3; i++)
            {
                if (maxIndex == i)
                {
                    continue;
                }

                if (minIndex1 == -1)
                {
                    minIndex1 = i;
                }
                else if (minIndex2 == -1)
                {
                    minIndex2 = i;
                    break;
                }
            }

            if (dimensions.Sides[maxIndex] > dimensions.Sides[minIndex1] + dimensions.Sides[minIndex2])
            {
                return null;
            }

            var heightBasePoint = DimensionRoundUp.Round(
                ((Math.Pow(dimensions.Sides[minIndex1], 2)) -
                 (Math.Pow(dimensions.Sides[minIndex2], 2)) +
                 (Math.Pow(dimensions.Sides[maxIndex], 2))
                    )*0.50*Math.Pow(dimensions.Sides[maxIndex], -1)
                );
            var height = DimensionRoundUp.Round(
                Math.Sqrt((Math.Pow(dimensions.Sides[minIndex1], 2)) -
                          (Math.Pow(heightBasePoint, 2))
                    )
                );

            return new TriangleOutcome
            {
                Type = TriangleType.Scalene.ToString(),
                VertexList = new List<int>
                {
                    0,
                    0,
                    dimensions.Sides[maxIndex],
                    0,
                    heightBasePoint,
                    height
                }
            };
        }
    }
}