namespace Clight.Logo.Core.Models;

/// <summary>
/// Encapsulates the mathematically computed coordinate geometry, arc dimensions,
/// and SVG path commands for the Clight Logo.
/// </summary>
public record LogoGeometry
{
    /// <summary>
    /// The canvas viewbox dimension (e.g. 512).
    /// </summary>
    public double ViewBoxSize { get; init; }

    /// <summary>
    /// Center of the master canvas.
    /// </summary>
    public Point2D CanvasCenter { get; init; }

    /// <summary>
    /// Center point of the outer circle arc.
    /// </summary>
    public Point2D OuterCenter { get; init; }

    /// <summary>
    /// Calculated center point of the inner circle arc.
    /// </summary>
    public Point2D InnerCenter { get; init; }

    /// <summary>
    /// Outer arc radius.
    /// </summary>
    public double OuterRadius { get; init; }

    /// <summary>
    /// Calculated inner arc radius.
    /// </summary>
    public double InnerRadius { get; init; }

    /// <summary>
    /// Thickness at the maximum crest point.
    /// </summary>
    public double StrokeWidth { get; init; }

    /// <summary>
    /// Top sharp crescent tip coordinate.
    /// </summary>
    public Point2D TopTip { get; init; }

    /// <summary>
    /// Bottom sharp crescent tip coordinate.
    /// </summary>
    public Point2D BottomTip { get; init; }

    /// <summary>
    /// Outer crest apex coordinate (leftmost point of outer curve).
    /// </summary>
    public Point2D OuterCrest { get; init; }

    /// <summary>
    /// Inner crest apex coordinate (leftmost point of inner curve).
    /// </summary>
    public Point2D InnerCrest { get; init; }

    /// <summary>
    /// Sweep angle of the outer arc in degrees.
    /// </summary>
    public double OuterArcSweepDegrees { get; init; }

    /// <summary>
    /// Sweep angle of the inner arc in degrees.
    /// </summary>
    public double InnerArcSweepDegrees { get; init; }

    /// <summary>
    /// Large arc flag for outer SVG arc command.
    /// </summary>
    public int LargeArcOuter { get; init; }

    /// <summary>
    /// Large arc flag for inner SVG arc command.
    /// </summary>
    public int LargeArcInner { get; init; }

    /// <summary>
    /// The standard SVG Path 'd' attribute string.
    /// </summary>
    public string SvgPathData { get; init; } = string.Empty;

    /// <summary>
    /// Golden Ratio verification ratio (OuterRadius / InnerRadius).
    /// </summary>
    public double RadiusRatio { get; init; }

    /// <summary>
    /// Horizontal span width of the crescent geometry.
    /// </summary>
    public double GeometryWidth => Math.Abs(TopTip.X - OuterCrest.X);

    /// <summary>
    /// Vertical span height of the crescent geometry.
    /// </summary>
    public double GeometryHeight => Math.Abs(BottomTip.Y - TopTip.Y);
}
