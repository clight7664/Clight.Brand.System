using Clight.Logo.Core.Constants;

namespace Clight.Logo.Renderer.Options;

/// <summary>
/// Configuration options for SVG generation and presentation.
/// </summary>
public record SvgRenderOptions
{
    public string FillColor { get; init; } = BrandColorConstants.InkBlackHex;
    public string? BackgroundColor { get; init; }
    public bool IncludeXmlDeclaration { get; init; } = false;
    public double? Width { get; init; }
    public double? Height { get; init; }
    public string? CssClass { get; init; }
    public string? ElementId { get; init; }
    public bool Responsive { get; init; } = true;
    public bool EnableGlowEffect { get; init; } = false;
}
