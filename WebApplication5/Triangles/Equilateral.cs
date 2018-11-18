using System;
using System.Collections.Generic;

namespace WebApplication5.Triangles
{
    public class Equilateral : ITriangle
    {
        public TriangleOutcome GetSides(TriangleDimensions dimensions)
        {
            if (dimensions.Sides == null || dimensions.Sides.Count != 1) return null;

            return new TriangleOutcome
            {
                Type = TriangleType.Equilateral.ToString(),
                VertexList = new List<int>
                {
                    0,
                    0,
                    dimensions.Sides[0],
                    0,
                    DimensionRoundUp.Round(dimensions.Sides[0]*0.5),
                    DimensionRoundUp.Round(Math.Sqrt(3)*0.50*dimensions.Sides[0])
                }
            };
        }
    }
}