using Clight.Logo.Core.Constants;
using Clight.Logo.Core.Services;
using Xunit;

namespace Clight.Brand.Tests.Geometry;

public class GoldenRatioTests
{
    private readonly ILogoCalculator _calculator = new LogoCalculator();

    [Fact]
    public void GoldenRatioConstants_ValuesAreMathematicallyAccurate()
    {
        double expectedPhi = (1.0 + Math.Sqrt(5.0)) / 2.0;
        Assert.Equal(GoldenRatioConstants.Phi, expectedPhi, 10);
        Assert.Equal(GoldenRatioConstants.PhiConjugate, GoldenRatioConstants.Phi - 1.0, 10);
    }

    [Fact]
    public void CalculateGoldenRatioStrokeWidth_FollowsProportionality()
    {
        double strokeWidth = _calculator.CalculateGoldenRatioStrokeWidth(220.0);
        Assert.True(strokeWidth is >= 20.0 and <= 32.0);
    }

    [Fact]
    public void CalculateGoldenRatioInnerRadius_CalculatesHarmonicInnerArc()
    {
        double innerRadius = _calculator.CalculateGoldenRatioInnerRadius(220.0);
        Assert.True(innerRadius < 220.0 && innerRadius > 180.0);
    }
}
