using Clight.Logo.Core.Enums;
using Clight.Logo.Core.Models;

namespace Clight.Asset.Generator.Models;

/// <summary>
/// Parameterized request for batch exporting brand asset bundles.
/// </summary>
public record AssetExportRequest
{
    public LogoParameters Parameters { get; init; } = LogoParameters.CreateApproved();
    public List<ExportFormat> Formats { get; init; } = [ExportFormat.Svg, ExportFormat.Png, ExportFormat.Ico, ExportFormat.WebPackage];
    public List<int> SelectedPngSizes { get; init; } = [16, 32, 48, 64, 128, 180, 192, 256, 512, 1024];
    public bool IncludeGuidelines { get; init; } = true;
    public bool IncludeConstruction { get; init; } = true;
    public bool IncludeDarkVariants { get; init; } = true;
}
