namespace ParticleSwarmOptimisation;

/// <summary>
/// This class encapsulates basic 2D vector operations (addition, subtraction, scalar multiplication).
/// It also acts a helper class for simplifying the velocity and position calculations.
/// </summary>
/// <remarks>Author: Anish Arya</remarks>
public class Vector
{
    public double X { get; private set; } // X-Coordinate

    public double Y { get; private set; } // Y-Coordinate

    private const double _TOLERANCE = 1e-6; // Tolerance to prevent precision errors

    public Vector(
      [DisallowNull] double x,
      [DisallowNull] double y)
    {
        this.X = x;
        this.Y = y;
    }

    // Overloaded addition operator for vector addition.
    public static Vector operator +(
      [DisallowNull] Vector v1,
      [DisallowNull] Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);

    // Overloaded subtraction operator for vector subtraction.
    public static Vector operator -(
      [DisallowNull] Vector v1,
      [DisallowNull] Vector v2) => new Vector(v1.X - v2.X, v1.Y - v2.Y);

    // Overload multiplication by scalar
    public static Vector operator *(
      [DisallowNull] double scalar,
      [DisallowNull] Vector v1) => new Vector(v1.X * scalar, v1.Y * scalar);

    // Override ToString() to return a formatted string representation of the vector.
    public override string ToString()
    {
        double xFormatted = Math.Abs(X) < _TOLERANCE ? 0 : X;
        double yFormatted = Math.Abs(Y) < _TOLERANCE ? 0 : Y;
        return $"({xFormatted:F6}, {yFormatted:F6})";
    }
}