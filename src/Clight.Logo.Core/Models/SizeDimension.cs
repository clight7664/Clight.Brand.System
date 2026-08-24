namespace Clight.Logo.Core.Models;

/// <summary>
/// Represents standard icon and export dimensions across digital and print contexts.
/// </summary>
/// <param name="Width">Width in pixels.</param>
/// <param name="Height">Height in pixels.</param>
/// <param name="Label">Human readable size label (e.g. "512×512").</param>
/// <param name="TargetName">Standard output file name (e.g. "clight-logo-512.png").</param>
/// <param name="Category">Usage classification (Favicon, AppIcon, RasterExport, Vector).</param>
public record SizeDimension(int Width, int Height, string Label, string TargetName, string Category)
{
    public static readonly SizeDimension[] AllStandardSizes =
    [
        new(16, 16, "16×16", "clight-logo-16.png", "Favicon / Browser Tab"),
        new(20, 20, "20×20", "clight-logo-20.png", "Small UI Indicator"),
        new(24, 24, "24×24", "clight-logo-24.png", "Toolbar / Navigation"),
        new(32, 32, "32×32", "clight-logo-32.png", "Standard Favicon (Retina)"),
        new(48, 48, "48×48", "clight-logo-48.png", "Windows Taskbar / Notification"),
        new(64, 64, "64×64", "clight-logo-64.png", "Desktop Icon / Dock Small"),
        new(128, 128, "128×128", "clight-logo-128.png", "Standard App Icon"),
        new(180, 180, "180×180", "apple-touch-icon.png", "Apple iOS Touch Icon"),
        new(192, 192, "192×192", "android-chrome-192.png", "Android Chrome Small"),
        new(256, 256, "256×256", "clight-logo-256.png", "macOS Dock / Store Medium"),
        new(512, 512, "512×512", "android-chrome-512.png", "PWA Splash / Store Large"),
        new(1024, 1024, "1024×1024", "clight-logo-1024.png", "Master Retina Hero Art")
    ];

    public static readonly int[] IcoSizes = [16, 32, 48, 64, 128, 256];
}
