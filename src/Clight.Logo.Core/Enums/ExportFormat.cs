namespace Clight.Logo.Core.Enums;

/// <summary>
/// Target asset export formats supported by the Clight Asset Generation Engine.
/// </summary>
public enum ExportFormat
{
    /// <summary>
    /// Scalable Vector Graphics format.
    /// </summary>
    Svg,

    /// <summary>
    /// Portable Network Graphics raster format.
    /// </summary>
    Png,

    /// <summary>
    /// Multi-resolution Windows Icon format.
    /// </summary>
    Ico,

    /// <summary>
    /// Standard Progressive Web App asset package (manifest, icons, apple touch icon).
    /// </summary>
    WebPackage,

    /// <summary>
    /// Complete ZIP bundle containing all vectors, rasters, and guideline documents.
    /// </summary>
    ZipBundle
}
