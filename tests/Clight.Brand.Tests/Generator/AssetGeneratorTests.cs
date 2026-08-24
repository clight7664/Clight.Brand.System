using Clight.Asset.Generator.Services;
using Clight.Logo.Core.Models;
using Clight.Logo.Core.Services;
using Clight.Logo.Renderer.Services;
using Xunit;

namespace Clight.Brand.Tests.Generator;

public class AssetGeneratorTests
{
    private readonly IAssetGenerator _generator;

    public AssetGeneratorTests()
    {
        var calculator = new LogoCalculator();
        var renderer = new SvgLogoRenderer(calculator);
        _generator = new AssetGenerator(renderer);
    }

    [Fact]
    public void GenerateSvgAssets_CreatesAllStandardVectorVariants()
    {
        var parameters = LogoParameters.CreateApproved();
        var assets = _generator.GenerateSvgAssets(parameters);

        Assert.NotNull(assets);
        Assert.Equal(7, assets.Count);

        Assert.Contains(assets, a => a.FileName == "clight-logo.svg");
        Assert.Contains(assets, a => a.FileName == "clight-logo-black.svg");
        Assert.Contains(assets, a => a.FileName == "clight-logo-white.svg");
        Assert.Contains(assets, a => a.FileName == "clight-logo-white-transparent.svg");
        Assert.Contains(assets, a => a.FileName == "clight-logo-construction.svg");
        Assert.Contains(assets, a => a.FileName == "clight-logo-grid.svg");
        Assert.Contains(assets, a => a.FileName == "clight-logo-safe-area.svg");
    }

    [Fact]
    public void GenerateWebManifest_ReturnsValidJsonManifest()
    {
        var manifestAsset = _generator.GenerateWebManifest();

        Assert.Equal("manifest.json", manifestAsset.FileName);
        Assert.Equal("application/manifest+json", manifestAsset.MimeType);
        Assert.Contains("Clight Brand System", manifestAsset.TextContent);
        Assert.Contains("favicon.png", manifestAsset.TextContent);
    }
}
