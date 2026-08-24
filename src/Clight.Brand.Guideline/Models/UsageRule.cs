namespace Clight.Brand.Guideline.Models;

/// <summary>
/// Brand governance rules for logo integrity (Do's and Don'ts).
/// </summary>
public record UsageRule
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsAllowed { get; init; }
    public string BadgeText => IsAllowed ? "DO" : "DON'T";
    public string VisualStyleClass => IsAllowed ? "border-emerald-500/40 bg-emerald-500/5 text-emerald-700 dark:text-emerald-300" : "border-rose-500/40 bg-rose-500/5 text-rose-700 dark:text-rose-300";
    public string ExampleTransformSvg { get; init; } = string.Empty;
}
