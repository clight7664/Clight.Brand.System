using System.Globalization;
using Clight.Logo.Core.Constants;
using Clight.Logo.Core.Models;

namespace Clight.Logo.Core.Services;

/// <summary>
/// Production implementation of the Clight Logo mathematical geometry engine.
/// Computes fluid circular arcs, Golden Ratio proportions, and precision SVG path definitions.
/// </summary>
public class LogoCalculator : ILogoCalculator
{
    public LogoGeometry CalculateGeometry(LogoParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        double size = parameters.ViewBoxSize > 0 ? parameters.ViewBoxSize : 512.0;
        double cx = (size / 2.0) + parameters.CenterOffsetX;
        double cy = (size / 2.0) + parameters.CenterOffsetY;
        Point2D outerCenter = new(cx, cy);

        double rOuter = parameters.OuterRadius > 0 ? parameters.OuterRadius : GoldenRatioConstants.DefaultOuterRadius;
        double strokeWidth = parameters.StrokeWidth > 0 ? parameters.StrokeWidth : GoldenRatioConstants.DefaultStrokeWidth;
        double tipAngleDeg = parameters.TipAngleDegrees is >= 10.0 and <= 85.0 ? parameters.TipAngleDegrees : 46.0;

        double alphaRad = tipAngleDeg * Math.PI / 180.0;

        // Calculate outer circle tip coordinates
        double xTop = cx + rOuter * Math.Cos(alphaRad);
        double yTop = cy - rOuter * Math.Sin(alphaRad);

        double xBot = cx + rOuter * Math.Cos(alphaRad);
        double yBot = cy + rOuter * Math.Sin(alphaRad);

        // Leftmost outer crest point
        double xCrestOut = cx - rOuter;
        double yCrestOut = cy;

        // Leftmost inner crest point
        double xCrestIn = xCrestOut + strokeWidth;
        double yCrestIn = cy;

        // Compute inner circle center and radius
        double dx = xTop - xCrestIn;
        double dy = yTop - cy;

        double rInner;
        double cxInner;

        if (parameters.InnerRadiusOverride.HasValue && parameters.InnerRadiusOverride.Value > 0)
        {
            rInner = parameters.InnerRadiusOverride.Value;
            cxInner = xCrestIn + rInner;
        }
        else
        {
            if (dx <= 0.001)
            {
                rInner = rOuter;
                cxInner = cx;
            }
            else
            {
                rInner = (dx * dx + dy * dy) / (2.0 * dx);
                cxInner = xCrestIn + rInner;
            }
        }

        Point2D innerCenter = new(cxInner, cy);
        Point2D topTip = new(xTop, yTop);
        Point2D bottomTip = new(xBot, yBot);
        Point2D outerCrest = new(xCrestOut, yCrestOut);
        Point2D innerCrest = new(xCrestIn, yCrestIn);

        // Handle Transformations (Mirroring / Offsets)
        if (parameters.MirrorVertical)
        {
            topTip = new Point2D(topTip.X, 2 * cy - topTip.Y);
            bottomTip = new Point2D(bottomTip.X, 2 * cy - bottomTip.Y);
        }

        if (parameters.MirrorHorizontal)
        {
            topTip = new Point2D(2 * cx - topTip.X, topTip.Y);
            bottomTip = new Point2D(2 * cx - bottomTip.X, bottomTip.Y);
            outerCrest = new Point2D(2 * cx - outerCrest.X, outerCrest.Y);
            innerCrest = new Point2D(2 * cx - innerCrest.X, innerCrest.Y);
            outerCenter = new Point2D(2 * cx - outerCenter.X, outerCenter.Y);
            innerCenter = new Point2D(2 * cx - innerCenter.X, innerCenter.Y);
        }

        // Calculate arc sweeps and flags
        double outerSweepDeg = 360.0 - (2.0 * tipAngleDeg);
        int largeArcOuter = outerSweepDeg > 180.0 ? 1 : 0;

        // Inner arc sweep calculation
        double angleInBot = Math.Atan2(yBot - cy, xBot - cxInner);
        double angleInTop = Math.Atan2(yTop - cy, xTop - cxInner);
        double innerSweepDeg = (2.0 * Math.PI - (angleInBot - angleInTop)) * 180.0 / Math.PI;
        int largeArcInner = innerSweepDeg > 180.0 ? 1 : 0;

        // Generate SVG Path
        int outerSweepFlag = parameters.MirrorHorizontal ? 1 : 0;
        int innerSweepFlag = parameters.MirrorHorizontal ? 0 : 1;

        string pathData;
        if (!parameters.MirrorHorizontal)
        {
            pathData = string.Format(
                CultureInfo.InvariantCulture,
                "M {0:F3} {1:F3} A {2:F3} {3:F3} 0 {4} 0 {5:F3} {6:F3} A {7:F3} {8:F3} 0 {9} 1 {10:F3} {11:F3} Z",
                topTip.X, topTip.Y,
                rOuter, rOuter,
                largeArcOuter,
                bottomTip.X, bottomTip.Y,
                rInner, rInner,
                largeArcInner,
                topTip.X, topTip.Y
            );
        }
        else
        {
            pathData = string.Format(
                CultureInfo.InvariantCulture,
                "M {0:F3} {1:F3} A {2:F3} {3:F3} 0 {4} 1 {5:F3} {6:F3} A {7:F3} {8:F3} 0 {9} 0 {10:F3} {11:F3} Z",
                topTip.X, topTip.Y,
                rOuter, rOuter,
                largeArcOuter,
                bottomTip.X, bottomTip.Y,
                rInner, rInner,
                largeArcInner,
                topTip.X, topTip.Y
            );
        }

        return new LogoGeometry
        {
            ViewBoxSize = size,
            CanvasCenter = new Point2D(size / 2.0, size / 2.0),
            OuterCenter = outerCenter,
            InnerCenter = innerCenter,
            OuterRadius = rOuter,
            InnerRadius = rInner,
            StrokeWidth = strokeWidth,
            TopTip = topTip,
            BottomTip = bottomTip,
            OuterCrest = outerCrest,
            InnerCrest = innerCrest,
            OuterArcSweepDegrees = outerSweepDeg,
            InnerArcSweepDegrees = innerSweepDeg,
            LargeArcOuter = largeArcOuter,
            LargeArcInner = largeArcInner,
            SvgPathData = pathData,
            RadiusRatio = rOuter > 0 && rInner > 0 ? rOuter / rInner : 1.0
        };
    }

    public double CalculateGoldenRatioStrokeWidth(double outerRadius)
    {
        // Crest stroke width proportional to Golden Ratio factor: W = R / (2 * φ^3) ≈ 26.0px for R=220
        return Math.Round(outerRadius / (GoldenRatioConstants.Phi * GoldenRatioConstants.Phi * GoldenRatioConstants.Phi * 2.0), 1);
    }

    public double CalculateGoldenRatioInnerRadius(double outerRadius)
    {
        return Math.Round(outerRadius * GoldenRatioConstants.FluidCurveFactor, 2);
    }

    public bool ValidateParameters(LogoParameters parameters, out List<string> validationErrors)
    {
        validationErrors = [];

        if (parameters.OuterRadius is < 50.0 or > 300.0)
        {
            validationErrors.Add("Outer radius must be between 50 and 300 units on a 512 canvas.");
        }

        if (parameters.StrokeWidth is < 2.0 or > 120.0)
        {
            validationErrors.Add("Stroke width must be between 2 and 120 units.");
        }

        if (parameters.TipAngleDegrees is < 15.0 or > 80.0)
        {
            validationErrors.Add("Tip angle must be between 15° and 80°.");
        }

        if (parameters.StrokeWidth >= parameters.OuterRadius)
        {
            validationErrors.Add("Stroke width cannot exceed or equal the outer radius.");
        }

        return validationErrors.Count == 0;
    }
}
