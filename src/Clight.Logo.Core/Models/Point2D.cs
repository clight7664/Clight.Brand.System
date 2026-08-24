namespace Clight.Logo.Core.Models;

/// <summary>
/// Represents a two-dimensional geometric point in Cartesian coordinate space.
/// </summary>
/// <param name="X">The horizontal X-coordinate.</param>
/// <param name="Y">The vertical Y-coordinate.</param>
public readonly record struct Point2D(double X, double Y)
{
    /// <summary>
    /// Gets the origin point (0, 0).
    /// </summary>
    public static Point2D Zero => new(0, 0);

    /// <summary>
    /// Computes the Euclidean distance to another point.
    /// </summary>
    public double DistanceTo(Point2D other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    /// <summary>
    /// Translates the point by given delta offsets.
    /// </summary>
    public Point2D Translate(double dx, double dy) => new(X + dx, Y + dy);

    /// <summary>
    /// Scales the point relative to an origin point.
    /// </summary>
    public Point2D Scale(double scaleFactor, Point2D origin)
    {
        return new Point2D(
            origin.X + (X - origin.X) * scaleFactor,
            origin.Y + (Y - origin.Y) * scaleFactor
        );
    }

    /// <summary>
    /// Rotates the point around an origin point by an angle in radians.
    /// </summary>
    public Point2D Rotate(double angleRadians, Point2D origin)
    {
        double cos = Math.Cos(angleRadians);
        double sin = Math.Sin(angleRadians);
        double dx = X - origin.X;
        double dy = Y - origin.Y;

        return new Point2D(
            origin.X + (dx * cos - dy * sin),
            origin.Y + (dx * sin + dy * cos)
        );
    }

    /// <summary>
    /// Formats the coordinate for SVG path command output.
    /// </summary>
    public string ToSvgCoordinate(int decimalPlaces = 3)
    {
        string fmt = "F" + decimalPlaces;
        return $"{X.ToString(fmt, System.Globalization.CultureInfo.InvariantCulture)} {Y.ToString(fmt, System.Globalization.CultureInfo.InvariantCulture)}";
    }

    /// <inheritdoc/>
    public override string ToString() => $"({X:F2}, {Y:F2})";
}
