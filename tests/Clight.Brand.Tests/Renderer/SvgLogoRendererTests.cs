using Clight.Logo.Core.Constants;
using Clight.Logo.Core.Enums;
using Clight.Logo.Core.Models;
using Clight.Logo.Core.Services;
using Clight.Logo.Renderer.Options;
using Clight.Logo.Renderer.Services;
using Xunit;

namespace Clight.Brand.Tests.Renderer;

public class SvgLogoRendererTests
{
    private readonly ISvgLogoRenderer _renderer;

    public SvgLogoRendererTests()
    {
        var calculator = new LogoCalculator();
        _renderer = new SvgLogoRenderer(calculator);
    }

    [Fact]
    public void RenderSvg_ReturnsValidSvgString()
    {
        var parameters = LogoParameters.CreateApproved();
        string svg = _renderer.RenderSvg(parameters, new SvgRenderOptions { IncludeXmlDeclaration = true });

        Assert.NotNull(svg);
        Assert.StartsWith("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", svg);
        Assert.Contains("<svg xmlns=\"http://www.w3.org/2000/svg\"", svg);
        Assert.Contains("viewBox=\"0 0 512 512\"", svg);
        Assert.Contains("<path d=\"M ", svg);
        Assert.EndsWith("</svg>", svg);
    }

    [Fact]
    public void RenderConstructionSvg_IncludesEngineeringGuides()
    {
        var parameters = LogoParameters.CreateApproved();
        string svg = _renderer.RenderConstructionSvg(parameters);

        Assert.Contains("<circle", svg);
        Assert.Contains("R_out =", svg);
        Assert.Contains("R_in =", svg);
        Assert.Contains("stroke-dasharray", svg);
        Assert.Contains("<line", svg);
    }

    [Fact]
    public void RenderGridSvg_IncludesGridLines()
    {
        var parameters = LogoParameters.CreateApproved();
        string svg = _renderer.RenderGridSvg(parameters, new GridRenderOptions { GridStep = 32 });

        Assert.Contains("<line", svg);
        Assert.Contains("x1=\"32\"", svg);
        Assert.Contains("<path d=", svg);
    }

    [Fact]
    public void RenderSafeAreaSvg_Includes1XClearSpace()
    {
        var parameters = LogoParameters.CreateApproved();
        string svg = _renderer.RenderSafeAreaSvg(parameters);

        Assert.Contains("1X Clear Space", svg);
        Assert.Contains("stroke-dasharray", svg);
    }

    [Theory]
    [InlineData(LogoTheme.Light, BrandColorConstants.InkBlackHex, BrandColorConstants.PaperWhiteHex)]
    [InlineData(LogoTheme.Dark, BrandColorConstants.PureWhiteHex, BrandColorConstants.InkBlackHex)]
    [InlineData(LogoTheme.Paper, BrandColorConstants.InkBlackHex, "#F5F2EB")]
    public void RenderThemeLogo_AppliesThemeColors(LogoTheme theme, string expectedFill, string expectedBg)
    {
        var parameters = LogoParameters.CreateApproved();
        string svg = _renderer.RenderThemeLogo(theme, parameters);

        Assert.Contains($"fill=\"{expectedFill}\"", svg);
        Assert.Contains($"fill=\"{expectedBg}\"", svg);
    }
}
