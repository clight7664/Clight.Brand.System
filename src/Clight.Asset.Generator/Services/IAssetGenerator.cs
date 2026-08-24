using Clight.Asset.Generator.Models;
using Clight.Logo.Core.Models;

namespace Clight.Asset.Generator.Services;

/// <summary>
/// Production asset generation service capable of creating standard SVGs, Web Manifests,
/// multi-size raster packages, and ICO binaries.
/// </summary>
public interface IAssetGenerator
{
    /// <summary>
    /// Generates standard SVG brand files (canonical, black, white, construction, grid).
    /// </summary>
    IReadOnlyList<GeneratedAsset> GenerateSvgAssets(LogoParameters parameters);

    /// <summary>
    /// Generates standard web configuration files (manifest.json).
    /// </summary>
    GeneratedAsset GenerateWebManifest();

    /// <summary>
    /// Generates multi-frame ICO files from PNG binary buffers.
    /// </summary>
    GeneratedAsset GenerateIco(IReadOnlyDictionary<int, byte[]> pngFrames, string fileName = "favicon.ico");

    /// <summary>
    /// Compiles a complete brand asset catalog into an export result.
    /// </summary>
    AssetExportResult CompileBrandCatalog(LogoParameters parameters, IReadOnlyDictionary<string, byte[]>? renderedPngs = null);
}
