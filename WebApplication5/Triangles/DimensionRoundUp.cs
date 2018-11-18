namespace WebApplication5.Triangles
{
    public static class DimensionRoundUp
    {
        public static int Round(double d)
        {
            if (d < 0)
            {
                return (int)(d - 0.5);
            }
            return (int)(d + 0.5);
        }
    }
}