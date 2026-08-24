using Clight.Logo.Core.Constants;

namespace Clight.Logo.Renderer.Options;

/// <summary>
/// Settings for modular spatial grid overlay.
/// </summary>
public record GridRenderOptions
{
    public int GridStep { get; init; } = 32;
    public int Subdivisions { get; init; } = 4;
    public bool ShowCoordinates { get; init; } = true;
    public string MajorGridColor { get; init; } = BrandColorConstants.MistGrayHex;
    public string MinorGridColor { get; init; } = "#F0F0F0";
    public string AxisColor { get; init; } = BrandColorConstants.InkBlackHex;
}
