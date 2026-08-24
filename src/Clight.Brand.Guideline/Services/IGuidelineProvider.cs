using Clight.Brand.Guideline.Models;

namespace Clight.Brand.Guideline.Services;

/// <summary>
/// Provider service for brand guidelines, philosophy declarations, and governance documents.
/// </summary>
public interface IGuidelineProvider
{
    IReadOnlyList<BrandPhilosophyItem> GetPhilosophyItems();
    IReadOnlyList<UsageRule> GetUsageRules();
    ConstructionSpec GetConstructionSpec();
    string GetLogoMarkdown();
    string GetConstructionMarkdown();
    string GetApplicationMarkdown();
    string GetTypographyMarkdown();
    string GetColorsMarkdown();
}
