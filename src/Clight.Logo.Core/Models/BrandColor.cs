using System.Globalization;

namespace Clight.Logo.Core.Models;

/// <summary>
/// Represents a color in the Clight Brand System with Hex, RGBA, and CMYK color conversions.
/// </summary>
public record BrandColor
{
    public string Name { get; init; } = string.Empty;
    public string Hex { get; init; } = "#000000";
    public byte R { get; init; }
    public byte G { get; init; }
    public byte B { get; init; }
    public double Alpha { get; init; } = 1.0;
    public int Cyan { get; init; }
    public int Magenta { get; init; }
    public int Yellow { get; init; }
    public int KeyBlack { get; init; }
    public string Description { get; init; } = string.Empty;

    public static BrandColor InkBlack => new()
    {
        Name = "Ink Black (水墨黑)",
        Hex = "#111111",
        R = 17,
        G = 17,
        B = 17,
        Cyan = 0,
        Magenta = 0,
        Yellow = 0,
        KeyBlack = 93,
        Description = "Primary dark brand tone reflecting digital minimalism and quiet authority."
    };

    public static BrandColor PaperWhite => new()
    {
        Name = "Paper White (宣纸白)",
        Hex = "#FAF9F6",
        R = 250,
        G = 249,
        B = 246,
        Cyan = 0,
        Magenta = 0,
        Yellow = 2,
        KeyBlack = 2,
        Description = "Primary light canvas tone with gentle natural warmth and Eastern negative space."
    };

    public static BrandColor MistGray => new()
    {
        Name = "Mist Gray (雾灰)",
        Hex = "#E0E0E0",
        R = 224,
        G = 224,
        B = 224,
        Cyan = 0,
        Magenta = 0,
        Yellow = 0,
        KeyBlack = 12,
        Description = "Subtle grid and construction wireframe delimiter tone."
    };

    public static BrandColor DeepGray => new()
    {
        Name = "Deep Gray (深灰)",
        Hex = "#444444",
        R = 68,
        G = 68,
        B = 68,
        Cyan = 0,
        Magenta = 0,
        Yellow = 0,
        KeyBlack = 73,
        Description = "Secondary typography and structural outline color."
    };

    public static BrandColor PureWhite => new()
    {
        Name = "Pure White",
        Hex = "#FFFFFF",
        R = 255,
        G = 255,
        B = 255,
        Cyan = 0,
        Magenta = 0,
        Yellow = 0,
        KeyBlack = 0,
        Description = "High contrast foreground element on dark backgrounds."
    };

    public static BrandColor PureBlack => new()
    {
        Name = "Pure Black",
        Hex = "#000000",
        R = 0,
        G = 0,
        B = 0,
        Cyan = 0,
        Magenta = 0,
        Yellow = 0,
        KeyBlack = 100,
        Description = "Absolute black for extreme contrast applications."
    };

    /// <summary>
    /// Computes the relative luminance according to WCAG 2.1 specs.
    /// </summary>
    public double CalculateRelativeLuminance()
    {
        double rNorm = NormalizeChannel(R);
        double gNorm = NormalizeChannel(G);
        double bNorm = NormalizeChannel(B);
        return 0.2126 * rNorm + 0.7152 * gNorm + 0.0722 * bNorm;
    }

    private static double NormalizeChannel(byte channel)
    {
        double val = channel / 255.0;
        return val <= 0.03928 ? val / 12.92 : Math.Pow((val + 0.055) / 1.055, 2.4);
    }

    /// <summary>
    /// Calculates the WCAG contrast ratio against another BrandColor.
    /// </summary>
    public double CalculateContrastRatio(BrandColor other)
    {
        double l1 = CalculateRelativeLuminance();
        double l2 = other.CalculateRelativeLuminance();
        double lighter = Math.Max(l1, l2);
        double darker = Math.Min(l1, l2);
        return (lighter + 0.05) / (darker + 0.05);
    }

    /// <summary>
    /// Parses a hex color string into a BrandColor model.
    /// </summary>
    public static BrandColor FromHex(string hex, string name = "Custom")
    {
        string cleanHex = hex.Trim().TrimStart('#');
        if (cleanHex.Length == 3)
        {
            cleanHex = $"{cleanHex[0]}{cleanHex[0]}{cleanHex[1]}{cleanHex[1]}{cleanHex[2]}{cleanHex[2]}";
        }

        if (cleanHex.Length >= 6 &&
            byte.TryParse(cleanHex[..2], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte r) &&
            byte.TryParse(cleanHex.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte g) &&
            byte.TryParse(cleanHex.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte b))
        {
            return new BrandColor
            {
                Name = name,
                Hex = $"#{cleanHex[..6].ToUpperInvariant()}",
                R = r,
                G = g,
                B = b,
                Alpha = 1.0
            };
        }

        return InkBlack;
    }
}
