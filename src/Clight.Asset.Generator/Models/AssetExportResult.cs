namespace Clight.Asset.Generator.Models;

/// <summary>
/// Output bundle result containing all compiled brand assets.
/// </summary>
public record AssetExportResult
{
    public List<GeneratedAsset> Assets { get; init; } = [];
    public int TotalAssetsCount => Assets.Count;
    public long TotalBundleBytes => Assets.Sum(a => a.FileSizeBytes);
    public byte[]? ZipArchiveBytes { get; init; }
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
}
