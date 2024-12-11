namespace Helpers;

public readonly record struct Point(int X, int Y)
{
   private Point(Point p) : this(0, 0)
   {
      X = p.X;
      Y = p.Y;
   }

   public static Point operator -(Point p) => new(-p.X, -p.Y);
   public static Point operator +(Point p1, Point p2) => new(p1.X + p2.X, p1.Y + p2.Y);
   public static Point operator -(Point p1, Point p2) => new(p1 + -p2);
   public static Point operator *(Point p1, int f) => new(p1.X * f, p1.Y * f);
}
