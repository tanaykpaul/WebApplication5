namespace WebApplication5.Triangles
{
    public interface ITriangle
    {
        TriangleOutcome GetSides(TriangleDimensions dimensions);
    }
}