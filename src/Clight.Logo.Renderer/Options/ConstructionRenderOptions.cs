using Clight.Logo.Core.Constants;

namespace Clight.Logo.Renderer.Options;

/// <summary>
/// Visual options for rendering engineering construction lines and Golden Ratio geometry guides.
/// </summary>
public record ConstructionRenderOptions
{
    public bool ShowOuterCircle { get; init; } = true;
    public bool ShowInnerCircle { get; init; } = true;
    public bool ShowGoldenCircles { get; init; } = true;
    public bool ShowCenterCrosshair { get; init; } = true;
    public bool ShowTipAngles { get; init; } = true;
    public bool ShowTangentLines { get; init; } = true;
    public bool ShowDimensionLabels { get; init; } = true;
    public bool ShowBoundingBox { get; init; } = true;
    public string PrimaryGuideColor { get; init; } = BrandColorConstants.AccentCyanHex;
    public string GoldenGuideColor { get; init; } = BrandColorConstants.AccentGoldHex;
    public string DimensionTextColor { get; init; } = BrandColorConstants.DeepGrayHex;
}
