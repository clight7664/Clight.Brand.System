namespace Clight.Logo.Core.Constants;

/// <summary>
/// Provides mathematical constants related to the Golden Ratio (φ) utilized across the Clight Brand System.
/// </summary>
public static class GoldenRatioConstants
{
    /// <summary>
    /// The Golden Ratio (φ = (1 + √5) / 2 ≈ 1.61803398875).
    /// </summary>
    public const double Phi = 1.618033988749895;

    /// <summary>
    /// The Golden Ratio conjugate (1/φ = φ - 1 ≈ 0.61803398875).
    /// </summary>
    public const double PhiConjugate = 0.618033988749895;

    /// <summary>
    /// Golden Angle in degrees (≈ 137.507764°).
    /// </summary>
    public const double GoldenAngleDegrees = 137.50776405003785;

    /// <summary>
    /// Standard Golden Ratio curve exponent factor for fluid organic crescent tapering.
    /// </summary>
    public const double FluidCurveFactor = 0.8541019662496847; // (1 + 1/φ) / 2

    /// <summary>
    /// Default outer circle radius for the 512x512 master coordinate grid.
    /// </summary>
    public const double DefaultOuterRadius = 220.0;

    /// <summary>
    /// Default stroke thickness at the primary crescent crest (calculated with φ proportionality).
    /// </summary>
    public const double DefaultStrokeWidth = 26.0;

    /// <summary>
    /// Default tip acute angle in degrees from horizontal axis.
    /// </summary>
    public const double DefaultTipAngleDegrees = 46.0;
}
