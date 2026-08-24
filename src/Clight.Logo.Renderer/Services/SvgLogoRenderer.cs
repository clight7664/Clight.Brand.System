using System.Globalization;
using System.Text;
using Clight.Logo.Core.Constants;
using Clight.Logo.Core.Enums;
using Clight.Logo.Core.Models;
using Clight.Logo.Core.Services;
using Clight.Logo.Renderer.Options;

namespace Clight.Logo.Renderer.Services;

/// <summary>
/// Production SVG rendering engine implementation for the Clight Design System.
/// </summary>
public class SvgLogoRenderer : ISvgLogoRenderer
{
    private readonly ILogoCalculator _calculator;

    public SvgLogoRenderer(ILogoCalculator calculator)
    {
        _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
    }

    public string RenderSvg(LogoParameters parameters, SvgRenderOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        options ??= new SvgRenderOptions();

        LogoGeometry geo = _calculator.CalculateGeometry(parameters);
        var sb = new StringBuilder();

        if (options.IncludeXmlDeclaration)
        {
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        }

        string widthAttr = options.Width.HasValue ? $" width=\"{options.Width.Value.ToString(CultureInfo.InvariantCulture)}\"" : "";
        string heightAttr = options.Height.HasValue ? $" height=\"{options.Height.Value.ToString(CultureInfo.InvariantCulture)}\"" : "";
        string classAttr = !string.IsNullOrWhiteSpace(options.CssClass)
            ? $" class=\"{options.CssClass}\""
            : (options.Responsive ? " class=\"w-full h-full max-w-full max-h-full\"" : "");
        string idAttr = !string.IsNullOrWhiteSpace(options.ElementId) ? $" id=\"{options.ElementId}\"" : "";

        sb.AppendLine(string.Format(
            CultureInfo.InvariantCulture,
            "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 {0} {0}\"{1}{2}{3}{4}>",
            geo.ViewBoxSize, widthAttr, heightAttr, classAttr, idAttr
        ));

        if (!string.IsNullOrWhiteSpace(options.BackgroundColor) && options.BackgroundColor != "transparent")
        {
            sb.AppendLine(string.Format(
                CultureInfo.InvariantCulture,
                "  <rect width=\"{0}\" height=\"{0}\" fill=\"{1}\" />",
                geo.ViewBoxSize, options.BackgroundColor
            ));
        }

        string fillColor = !string.IsNullOrWhiteSpace(options.FillColor) ? options.FillColor : parameters.FillColor;
        sb.AppendLine($"  <path d=\"{geo.SvgPathData}\" fill=\"{fillColor}\" />");
        sb.Append("</svg>");

        return sb.ToString();
    }

    public string RenderConstructionSvg(LogoParameters parameters, ConstructionRenderOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        options ??= new ConstructionRenderOptions();

        LogoGeometry geo = _calculator.CalculateGeometry(parameters);
        double size = geo.ViewBoxSize;
        double cx = geo.CanvasCenter.X;
        double cy = geo.CanvasCenter.Y;

        var sb = new StringBuilder();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 {0} {0}\" class=\"w-full h-full\">", size));

        // Background
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  <rect width=\"{0}\" height=\"{0}\" fill=\"#FAF9F6\" />", size));

        // Background subtle grid
        sb.AppendLine("  <g stroke=\"#EFECE6\" stroke-width=\"1\">");
        for (int i = 64; i < size; i += 64)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <line x1=\"{0}\" y1=\"0\" x2=\"{0}\" y2=\"{1}\" />", i, size));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <line x1=\"0\" y1=\"{0}\" x2=\"{1}\" y2=\"{0}\" />", i, size));
        }
        sb.AppendLine("  </g>");

        // Center crosshair
        if (options.ShowCenterCrosshair)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  <g stroke=\"#D1D5DB\" stroke-width=\"1.5\" stroke-dasharray=\"4 4\">" +
                "\n    <line x1=\"0\" y1=\"{0}\" x2=\"{1}\" y2=\"{0}\" />" +
                "\n    <line x1=\"{0}\" y1=\"0\" x2=\"{0}\" y2=\"{1}\" />" +
                "\n  </g>", cy, size));
        }

        // Outer construction circle
        if (options.ShowOuterCircle)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  <circle cx=\"{0:F2}\" cy=\"{1:F2}\" r=\"{2:F2}\" fill=\"none\" stroke=\"{3}\" stroke-width=\"1.5\" stroke-dasharray=\"6 4\" />",
                geo.OuterCenter.X, geo.OuterCenter.Y, geo.OuterRadius, options.PrimaryGuideColor));
        }

        // Inner construction circle
        if (options.ShowInnerCircle)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  <circle cx=\"{0:F2}\" cy=\"{1:F2}\" r=\"{2:F2}\" fill=\"none\" stroke=\"{3}\" stroke-width=\"1.5\" stroke-dasharray=\"4 3\" />",
                geo.InnerCenter.X, geo.InnerCenter.Y, geo.InnerRadius, options.GoldenGuideColor));
        }

        // Golden Ratio concentric circles (φ harmonic ratios)
        if (options.ShowGoldenCircles)
        {
            double rPhi1 = geo.OuterRadius / GoldenRatioConstants.Phi;
            double rPhi2 = rPhi1 / GoldenRatioConstants.Phi;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  <circle cx=\"{0:F2}\" cy=\"{1:F2}\" r=\"{2:F2}\" fill=\"none\" stroke=\"#D4AF37\" stroke-width=\"1\" opacity=\"0.4\" stroke-dasharray=\"2 2\" />",
                cx, cy, rPhi1));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  <circle cx=\"{0:F2}\" cy=\"{1:F2}\" r=\"{2:F2}\" fill=\"none\" stroke=\"#D4AF37\" stroke-width=\"1\" opacity=\"0.3\" stroke-dasharray=\"2 2\" />",
                cx, cy, rPhi2));
        }

        // Tangent & tip angle lines
        if (options.ShowTipAngles)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  <g stroke=\"#EF4444\" stroke-width=\"1.5\">" +
                "\n    <line x1=\"{0:F2}\" y1=\"{1:F2}\" x2=\"{2:F2}\" y2=\"{3:F2}\" stroke-dasharray=\"3 3\" />" +
                "\n    <line x1=\"{0:F2}\" y1=\"{1:F2}\" x2=\"{4:F2}\" y2=\"{5:F2}\" stroke-dasharray=\"3 3\" />" +
                "\n    <circle cx=\"{2:F2}\" cy=\"{3:F2}\" r=\"4\" fill=\"#EF4444\" />" +
                "\n    <circle cx=\"{4:F2}\" cy=\"{5:F2}\" r=\"4\" fill=\"#EF4444\" />" +
                "\n  </g>",
                geo.OuterCenter.X, geo.OuterCenter.Y,
                geo.TopTip.X, geo.TopTip.Y,
                geo.BottomTip.X, geo.BottomTip.Y));
        }

        // The actual Clight Logo (Semi-transparent with crisp outline)
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  <path d=\"{0}\" fill=\"#111111\" opacity=\"0.9\" />",
            geo.SvgPathData));

        // Dimension labels
        if (options.ShowDimensionLabels)
        {
            sb.AppendLine("  <g font-family=\"'Inter', system-ui, sans-serif\" font-size=\"11\" fill=\"#444444\" font-weight=\"500\">");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <text x=\"{0:F2}\" y=\"{1:F2}\">R_out = {2:F1}</text>", geo.OuterCenter.X - geo.OuterRadius + 10, geo.OuterCenter.Y - 10, geo.OuterRadius));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <text x=\"{0:F2}\" y=\"{1:F2}\" fill=\"#B45309\">R_in = {2:F1}</text>", geo.InnerCenter.X - 10, geo.InnerCenter.Y + 24, geo.InnerRadius));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <text x=\"{0:F2}\" y=\"{1:F2}\" fill=\"#DC2626\">Tip: {2:F1}°</text>", geo.TopTip.X + 8, geo.TopTip.Y - 4, parameters.TipAngleDegrees));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <text x=\"{0:F2}\" y=\"{1:F2}\" fill=\"#2563EB\">W = {2:F1}px (φ-ratio)</text>", geo.OuterCrest.X + 2, geo.OuterCrest.Y + 36, geo.StrokeWidth));
            sb.AppendLine("  </g>");
        }

        sb.Append("</svg>");
        return sb.ToString();
    }

    public string RenderGridSvg(LogoParameters parameters, GridRenderOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        options ??= new GridRenderOptions();

        LogoGeometry geo = _calculator.CalculateGeometry(parameters);
        double size = geo.ViewBoxSize;
        int step = options.GridStep > 0 ? options.GridStep : 32;

        var sb = new StringBuilder();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 {0} {0}\" class=\"w-full h-full\">", size));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  <rect width=\"{0}\" height=\"{0}\" fill=\"#FAF9F6\" />", size));

        // Grid lines
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  <g stroke=\"{0}\" stroke-width=\"1\">", options.MajorGridColor));
        for (int i = 0; i <= size; i += step)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <line x1=\"{0}\" y1=\"0\" x2=\"{0}\" y2=\"{1}\" />", i, size));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "    <line x1=\"0\" y1=\"{0}\" x2=\"{1}\" y2=\"{0}\" />", i, size));
        }
        sb.AppendLine("  </g>");

        // Axes
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  <g stroke=\"{0}\" stroke-width=\"2\">" +
            "\n    <line x1=\"0\" y1=\"{1}\" x2=\"{2}\" y2=\"{1}\" />" +
            "\n    <line x1=\"{1}\" y1=\"0\" x2=\"{1}\" y2=\"{2}\" />" +
            "\n  </g>", options.AxisColor, size / 2.0, size));

        // Logo
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  <path d=\"{0}\" fill=\"#111111\" />", geo.SvgPathData));
        sb.Append("</svg>");

        return sb.ToString();
    }

    public string RenderSafeAreaSvg(LogoParameters parameters, SafeAreaOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        options ??= new SafeAreaOptions();

        LogoGeometry geo = _calculator.CalculateGeometry(parameters);
        double size = geo.ViewBoxSize;
        double clearance = geo.StrokeWidth * options.SafeMarginMultiplier;

        double left = Math.Max(0, geo.OuterCrest.X - clearance);
        double top = Math.Max(0, geo.TopTip.Y - clearance);
        double right = Math.Min(size, geo.TopTip.X + clearance);
        double bottom = Math.Min(size, geo.BottomTip.Y + clearance);
        double width = right - left;
        double height = bottom - top;

        var sb = new StringBuilder();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 {0} {0}\" class=\"w-full h-full\">", size));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  <rect width=\"{0}\" height=\"{0}\" fill=\"#FAF9F6\" />", size));

        // Safe area box
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  <rect x=\"{0:F2}\" y=\"{1:F2}\" width=\"{2:F2}\" height=\"{3:F2}\" fill=\"{4}\" stroke=\"{5}\" stroke-width=\"1.5\" stroke-dasharray=\"4 4\" />",
            left, top, width, height, options.SafePatternFill, options.SafeBoundaryColor));

        // Safe margin markers (1X indicator)
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  <g font-family=\"'Inter', system-ui, sans-serif\" font-size=\"12\" fill=\"{0}\" font-weight=\"600\">" +
            "\n    <text x=\"{1:F2}\" y=\"{2:F2}\" text-anchor=\"middle\">1X Clear Space ({3:F0}px)</text>" +
            "\n  </g>",
            options.SafeBoundaryColor, size / 2.0, top - 12, clearance));

        // Logo
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  <path d=\"{0}\" fill=\"#111111\" />", geo.SvgPathData));
        sb.Append("</svg>");

        return sb.ToString();
    }

    public string RenderThemeLogo(LogoTheme theme, LogoParameters parameters, SvgRenderOptions? options = null)
    {
        string fill;
        string? bg;

        switch (theme)
        {
            case LogoTheme.Dark:
                fill = BrandColorConstants.PureWhiteHex;
                bg = BrandColorConstants.InkBlackHex;
                break;
            case LogoTheme.Paper:
                fill = BrandColorConstants.InkBlackHex;
                bg = "#F5F2EB";
                break;
            case LogoTheme.Transparent:
                fill = BrandColorConstants.InkBlackHex;
                bg = null;
                break;
            case LogoTheme.Light:
            default:
                fill = BrandColorConstants.InkBlackHex;
                bg = BrandColorConstants.PaperWhiteHex;
                break;
        }

        options = (options ?? new SvgRenderOptions()) with
        {
            FillColor = fill,
            BackgroundColor = bg
        };

        return RenderSvg(parameters, options);
    }
}
