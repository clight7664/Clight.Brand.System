namespace Clight.Logo.Renderer.Options;

/// <summary>
/// Visual options for safe exclusion space (Clear Space) visualizer.
/// </summary>
public record SafeAreaOptions
{
    /// <summary>
    /// Safe margin unit multiplier based on stroke width (1X = 1 standard crest thickness).
    /// </summary>
    public double SafeMarginMultiplier { get; init; } = 1.618;
    public bool ShowClearanceBox { get; init; } = true;
    public bool ShowUnitLabels { get; init; } = true;
    public string SafeBoundaryColor { get; init; } = "#22C55E"; // Green indicator
    public string SafePatternFill { get; init; } = "rgba(34, 197, 94, 0.08)";
}
