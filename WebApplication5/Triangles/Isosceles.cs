using System.Collections.Generic;

namespace WebApplication5.Triangles
{
    public class Isosceles : ITriangle
    {
        public TriangleOutcome GetSides(TriangleDimensions dimensions)
        {
            if (dimensions == null || dimensions.Height == -1 || dimensions.Base == -1) return null;

            return new TriangleOutcome
            {
                Type = TriangleType.Isosceles.ToString(),
                VertexList = new List<int>
                {
                    0,
                    0,
                    dimensions.Base,
                    0,
                    DimensionRoundUp.Round(dimensions.Base*0.5),
                    dimensions.Height
                }
            };
        }
    }
}