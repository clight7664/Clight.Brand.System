using Clight.Logo.Core.Constants;
using Clight.Logo.Core.Enums;

namespace Clight.Logo.Core.Models;

/// <summary>
/// Comprehensive parametric definition for constructing and generating the Clight crescent logo.
/// </summary>
public record LogoParameters
{
    /// <summary>
    /// The master SVG coordinate grid canvas size (default: 512x512).
    /// </summary>
    public double ViewBoxSize { get; set; } = 512.0;

    /// <summary>
    /// The outer circle arc radius (default: 220.0).
    /// </summary>
    public double OuterRadius { get; set; } = GoldenRatioConstants.DefaultOuterRadius;

    /// <summary>
    /// Explicit inner radius override. If null, automatically calculated via Golden Ratio.
    /// </summary>
    public double? InnerRadiusOverride { get; set; }

    /// <summary>
    /// The maximum thickness of the crescent at its central crest (default: 26.0).
    /// </summary>
    public double StrokeWidth { get; set; } = GoldenRatioConstants.DefaultStrokeWidth;

    /// <summary>
    /// The acute tip opening angle in degrees relative to the horizontal axis (default: 46.0°).
    /// </summary>
    public double TipAngleDegrees { get; set; } = GoldenRatioConstants.DefaultTipAngleDegrees;

    /// <summary>
    /// Horizontal offset of the crescent center from canvas midpoint.
    /// </summary>
    public double CenterOffsetX { get; set; } = 0.0;

    /// <summary>
    /// Vertical offset of the crescent center from canvas midpoint.
    /// </summary>
    public double CenterOffsetY { get; set; } = 0.0;

    /// <summary>
    /// Whether to strictly enforce Golden Ratio (φ = 1.618034) curvature proportions.
    /// </summary>
    public bool UseGoldenRatio { get; set; } = true;

    /// <summary>
    /// The active stroke weight preset (Thin, Regular, Bold, Custom).
    /// </summary>
    public WeightPreset Weight { get; set; } = WeightPreset.Regular;

    /// <summary>
    /// Mirror the crescent vertically (flip top and bottom).
    /// </summary>
    public bool MirrorVertical { get; set; } = false;

    /// <summary>
    /// Mirror the crescent horizontally (opening leftwards rather than rightwards).
    /// </summary>
    public bool MirrorHorizontal { get; set; } = false;

    /// <summary>
    /// Additional rotation angle in degrees.
    /// </summary>
    public double RotationAngleDegrees { get; set; } = 0.0;

    /// <summary>
    /// The fill color hex (default: #111111).
    /// </summary>
    public string FillColor { get; set; } = BrandColorConstants.InkBlackHex;

    /// <summary>
    /// The background color hex (default: #FAF9F6 for Light, or transparent).
    /// </summary>
    public string BackgroundColor { get; set; } = BrandColorConstants.PaperWhiteHex;

    /// <summary>
    /// Factory method providing the canonical approved Clight Logo parameters.
    /// </summary>
    public static LogoParameters CreateApproved() => new()
    {
        ViewBoxSize = 512.0,
        OuterRadius = 220.0,
        StrokeWidth = 26.0,
        TipAngleDegrees = 46.0,
        CenterOffsetX = 0.0,
        CenterOffsetY = 0.0,
        UseGoldenRatio = true,
        Weight = WeightPreset.Regular,
        FillColor = BrandColorConstants.InkBlackHex,
        BackgroundColor = BrandColorConstants.PaperWhiteHex
    };

    /// <summary>
    /// Creates a cloned instance configured for a specific weight preset.
    /// </summary>
    public LogoParameters WithWeight(WeightPreset preset)
    {
        double width = preset switch
        {
            WeightPreset.Thin => 16.0,
            WeightPreset.Regular => 26.0,
            WeightPreset.Bold => 42.0,
            _ => StrokeWidth
        };

        return this with
        {
            Weight = preset,
            StrokeWidth = width
        };
    }
}
