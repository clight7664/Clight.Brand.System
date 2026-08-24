namespace Clight.Brand.Guideline.Models;

/// <summary>
/// Model representing a core philosophical value or narrative pillar in the Clight Brand System.
/// </summary>
public record BrandPhilosophyItem
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string ChineseTitle { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ChineseDescription { get; init; } = string.Empty;
    public string IconSvg { get; init; } = string.Empty;
}
