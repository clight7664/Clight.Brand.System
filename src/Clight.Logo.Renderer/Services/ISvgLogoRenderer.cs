using Clight.Logo.Core.Enums;
using Clight.Logo.Core.Models;
using Clight.Logo.Renderer.Options;

namespace Clight.Logo.Renderer.Services;

/// <summary>
/// High performance SVG rendering engine for Clight brand symbols, construction blueprints,
/// modular grids, and multi-theme variations.
/// </summary>
public interface ISvgLogoRenderer
{
    /// <summary>
    /// Renders a pure vector SVG of the Clight Logo.
    /// </summary>
    string RenderSvg(LogoParameters parameters, SvgRenderOptions? options = null);

    /// <summary>
    /// Renders the architectural construction diagram with guide circles, centerlines, and Golden Ratio overlays.
    /// </summary>
    string RenderConstructionSvg(LogoParameters parameters, ConstructionRenderOptions? options = null);

    /// <summary>
    /// Renders the logo superimposed on a precision modular coordinate grid.
    /// </summary>
    string RenderGridSvg(LogoParameters parameters, GridRenderOptions? options = null);

    /// <summary>
    /// Renders the official brand Clear Space (Safe Zone) diagram.
    /// </summary>
    string RenderSafeAreaSvg(LogoParameters parameters, SafeAreaOptions? options = null);

    /// <summary>
    /// Renders a themed variant (Light, Dark, Paper, Transparent).
    /// </summary>
    string RenderThemeLogo(LogoTheme theme, LogoParameters parameters, SvgRenderOptions? options = null);
}
