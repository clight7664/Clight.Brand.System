using Clight.Brand.Guideline.Services;
using Xunit;

namespace Clight.Brand.Tests.Guideline;

public class GuidelineProviderTests
{
    private readonly IGuidelineProvider _provider = new GuidelineProvider();

    [Fact]
    public void GetPhilosophyItems_ReturnsCore5Items()
    {
        var items = _provider.GetPhilosophyItems();

        Assert.NotNull(items);
        Assert.Equal(5, items.Count);
        Assert.Contains(items, i => i.Key == "Reflection");
        Assert.Contains(items, i => i.Key == "Intelligence");
        Assert.Contains(items, i => i.Key == "Harmony");
        Assert.Contains(items, i => i.Key == "Timeless");
        Assert.Contains(items, i => i.Key == "Lightweight");
    }

    [Fact]
    public void GetUsageRules_ReturnsValidDosAndDonts()
    {
        var rules = _provider.GetUsageRules();

        Assert.NotNull(rules);
        Assert.True(rules.Count >= 5);
        Assert.Contains(rules, r => r.IsAllowed);
        Assert.Contains(rules, r => !r.IsAllowed);
    }

    [Fact]
    public void MarkdownDocuments_AreNonEmptyAndStructured()
    {
        Assert.NotEmpty(_provider.GetLogoMarkdown());
        Assert.NotEmpty(_provider.GetConstructionMarkdown());
        Assert.NotEmpty(_provider.GetApplicationMarkdown());
        Assert.NotEmpty(_provider.GetTypographyMarkdown());
        Assert.NotEmpty(_provider.GetColorsMarkdown());
    }
}
