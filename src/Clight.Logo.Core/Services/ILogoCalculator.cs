using Clight.Logo.Core.Models;

namespace Clight.Logo.Core.Services;

/// <summary>
/// Service interface responsible for calculating mathematical geometry, 
/// golden ratio proportions, and SVG paths for the Clight Logo.
/// </summary>
public interface ILogoCalculator
{
    /// <summary>
    /// Computes full geometric coordinates, arc parameters, and SVG path data from logo parameters.
    /// </summary>
    /// <param name="parameters">The input parametric settings.</param>
    /// <returns>Computed geometry model.</returns>
    LogoGeometry CalculateGeometry(LogoParameters parameters);

    /// <summary>
    /// Calculates the Golden Ratio compliant stroke thickness given an outer radius.
    /// </summary>
    double CalculateGoldenRatioStrokeWidth(double outerRadius);

    /// <summary>
    /// Calculates the Golden Ratio compliant inner radius given an outer radius.
    /// </summary>
    double CalculateGoldenRatioInnerRadius(double outerRadius);

    /// <summary>
    /// Validates whether the parameters meet brand design criteria and safety boundaries.
    /// </summary>
    bool ValidateParameters(LogoParameters parameters, out List<string> validationErrors);
}
