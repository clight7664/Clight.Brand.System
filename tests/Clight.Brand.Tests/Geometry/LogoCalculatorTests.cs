using Clight.Logo.Core.Constants;
using Clight.Logo.Core.Enums;
using Clight.Logo.Core.Models;
using Clight.Logo.Core.Services;
using Xunit;

namespace Clight.Brand.Tests.Geometry;

public class LogoCalculatorTests
{
    private readonly ILogoCalculator _calculator = new LogoCalculator();

    [Fact]
    public void CalculateGeometry_ApprovedDefault_ReturnsAccurateCoordinates()
    {
        var parameters = LogoParameters.CreateApproved();
        var geometry = _calculator.CalculateGeometry(parameters);

        Assert.Equal(512.0, geometry.ViewBoxSize);
        Assert.Equal(220.0, geometry.OuterRadius);
        Assert.Equal(26.0, geometry.StrokeWidth);
        Assert.True(geometry.InnerRadius > 200.0 && geometry.InnerRadius < 220.0);
        
        // Assert top and bottom tips have symmetric Y about centerY (256) and identical X
        Assert.Equal(geometry.TopTip.X, geometry.BottomTip.X, 3);
        Assert.Equal(256.0 - geometry.TopTip.Y, geometry.BottomTip.Y - 256.0, 3);
        
        // Leftmost outer crest should be at cx - r_out = 256 - 220 = 36
        Assert.Equal(36.0, geometry.OuterCrest.X, 2);
        Assert.Equal(256.0, geometry.OuterCrest.Y, 2);

        // Leftmost inner crest should be at cx - r_out + stroke_width = 36 + 26 = 62
        Assert.Equal(62.0, geometry.InnerCrest.X, 2);
        Assert.Equal(256.0, geometry.InnerCrest.Y, 2);

        // SVG Path structure
        Assert.StartsWith("M ", geometry.SvgPathData);
        Assert.EndsWith(" Z", geometry.SvgPathData);
        Assert.Contains(" A ", geometry.SvgPathData);
    }

    [Theory]
    [InlineData(WeightPreset.Thin, 16.0)]
    [InlineData(WeightPreset.Regular, 26.0)]
    [InlineData(WeightPreset.Bold, 42.0)]
    public void WithWeight_ConfiguresExpectedThickness(WeightPreset preset, double expectedWidth)
    {
        var parameters = LogoParameters.CreateApproved().WithWeight(preset);
        Assert.Equal(expectedWidth, parameters.StrokeWidth);

        var geometry = _calculator.CalculateGeometry(parameters);
        Assert.Equal(expectedWidth, geometry.StrokeWidth);
        Assert.Equal(36.0 + expectedWidth, geometry.InnerCrest.X, 2);
    }

    [Fact]
    public void CalculateGeometry_HorizontalMirror_InvertsOpeningDirection()
    {
        var normalParams = LogoParameters.CreateApproved();
        var mirroredParams = LogoParameters.CreateApproved() with { MirrorHorizontal = true };

        var normalGeo = _calculator.CalculateGeometry(normalParams);
        var mirroredGeo = _calculator.CalculateGeometry(mirroredParams);

        // Normal opens to the right, tips are to the right of center (X > 256)
        Assert.True(normalGeo.TopTip.X > 256.0);
        
        // Mirrored opens to the left, tips are to the left of center (X < 256)
        Assert.True(mirroredGeo.TopTip.X < 256.0);
        Assert.Equal(512.0 - normalGeo.TopTip.X, mirroredGeo.TopTip.X, 2);
    }

    [Fact]
    public void ValidateParameters_CatchesInvalidValues()
    {
        var invalidParams = new LogoParameters
        {
            OuterRadius = 350.0, // Too large
            StrokeWidth = 400.0, // Exceeds radius
            TipAngleDegrees = 95.0 // Invalid angle
        };

        bool isValid = _calculator.ValidateParameters(invalidParams, out var errors);
        Assert.False(isValid);
        Assert.NotEmpty(errors);
        Assert.True(errors.Count >= 2);
    }
}
