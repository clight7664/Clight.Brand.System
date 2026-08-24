using System.Text.Json.Serialization;

namespace Clight.Asset.Generator.Models;

/// <summary>
/// Progressive Web App (PWA) manifest specification.
/// </summary>
public record WebManifest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = "Clight Brand System & Logo Studio";

    [JsonPropertyName("short_name")]
    public string ShortName { get; init; } = "Clight";

    [JsonPropertyName("description")]
    public string Description { get; init; } = "Minimalist parametric AI-native brand asset system and studio.";

    [JsonPropertyName("start_url")]
    public string StartUrl { get; init; } = "/";

    [JsonPropertyName("display")]
    public string Display { get; init; } = "standalone";

    [JsonPropertyName("background_color")]
    public string BackgroundColor { get; init; } = "#FAF9F6";

    [JsonPropertyName("theme_color")]
    public string ThemeColor { get; init; } = "#111111";

    [JsonPropertyName("icons")]
    public List<ManifestIcon> Icons { get; init; } =
    [
        new() { Src = "favicon.png", Sizes = "64x64 32x32 24x24 16x16", Type = "image/png" },
        new() { Src = "apple-touch-icon.png", Sizes = "180x180", Type = "image/png", Purpose = "apple touch icon" },
        new() { Src = "android-chrome-192.png", Sizes = "192x192", Type = "image/png", Purpose = "any maskable" },
        new() { Src = "android-chrome-512.png", Sizes = "512x512", Type = "image/png", Purpose = "any maskable" }
    ];
}

public record ManifestIcon
{
    [JsonPropertyName("src")]
    public string Src { get; init; } = string.Empty;

    [JsonPropertyName("sizes")]
    public string Sizes { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = "image/png";

    [JsonPropertyName("purpose")]
    public string? Purpose { get; init; }
}
