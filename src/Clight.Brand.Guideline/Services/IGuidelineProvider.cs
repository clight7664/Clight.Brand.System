using Clight.Brand.Guideline.Models;

namespace Clight.Brand.Guideline.Services;

/// <summary>
/// Provider service for brand guidelines, philosophy declarations, and governance documents in Chinese and English.
/// </summary>
public interface IGuidelineProvider
{
    IReadOnlyList<BrandPhilosophyItem> GetPhilosophyItems(string lang = "zh");
    IReadOnlyList<UsageRule> GetUsageRules(string lang = "zh");
    ConstructionSpec GetConstructionSpec();
    string GetLogoMarkdown(string lang = "zh");
    string GetConstructionMarkdown(string lang = "zh");
    string GetApplicationMarkdown(string lang = "zh");
    string GetTypographyMarkdown(string lang = "zh");
    string GetColorsMarkdown(string lang = "zh");
    string GetFullGuidelineMarkdown(string lang = "zh");
}
