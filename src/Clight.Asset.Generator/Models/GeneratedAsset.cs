namespace Clight.Asset.Generator.Models;

/// <summary>
/// Represents a generated brand asset file artifact.
/// </summary>
public record GeneratedAsset
{
    public string FileName { get; init; } = string.Empty;
    public string RelativePath { get; init; } = string.Empty;
    public string Extension { get; init; } = string.Empty;
    public string MimeType { get; init; } = "application/octet-stream";
    public byte[] Data { get; init; } = [];
    public string? TextContent { get; init; }
    public string Category { get; init; } = "General";
    public int? Width { get; init; }
    public int? Height { get; init; }
    public long FileSizeBytes => Data.Length > 0 ? Data.Length : (TextContent != null ? System.Text.Encoding.UTF8.GetByteCount(TextContent) : 0);
}
