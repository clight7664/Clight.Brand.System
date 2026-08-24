namespace Clight.Brand.Guideline.Models;

/// <summary>
/// Engineering and geometric design specifications for the Clight Logo.
/// </summary>
public record ConstructionSpec
{
    public double GoldenRatioPhi { get; init; } = 1.61803398875;
    public double DefaultOuterRadius { get; init; } = 220.0;
    public double DefaultInnerRadius { get; init; } = 209.52;
    public double DefaultStrokeWidth { get; init; } = 26.0;
    public double DefaultTipAngleDegrees { get; init; } = 46.0;
    public double ClearSpaceMultiplier { get; init; } = 1.618;
    public int MinimumDigitalSizePixels { get; init; } = 16;
    public double MinimumPrintSizeMillimeters { get; init; } = 5.0;
}
