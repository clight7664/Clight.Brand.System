using System.Text;
using System.Text.Json;
using Clight.Asset.Generator.Encoders;
using Clight.Asset.Generator.Models;
using Clight.Logo.Core.Constants;
using Clight.Logo.Core.Enums;
using Clight.Logo.Core.Models;
using Clight.Logo.Renderer.Options;
using Clight.Logo.Renderer.Services;

namespace Clight.Asset.Generator.Services;

/// <summary>
/// Production implementation of the Brand Asset Generation Engine.
/// </summary>
public class AssetGenerator : IAssetGenerator
{
    private readonly ISvgLogoRenderer _svgRenderer;

    public AssetGenerator(ISvgLogoRenderer svgRenderer)
    {
        _svgRenderer = svgRenderer ?? throw new ArgumentNullException(nameof(svgRenderer));
    }

    public IReadOnlyList<GeneratedAsset> GenerateSvgAssets(LogoParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        var list = new List<GeneratedAsset>();

        // 1. Primary Canonical SVG (Black on Transparent)
        string canonicalSvg = _svgRenderer.RenderSvg(parameters, new SvgRenderOptions
        {
            FillColor = BrandColorConstants.InkBlackHex,
            BackgroundColor = null,
            IncludeXmlDeclaration = true
        });
        list.Add(new GeneratedAsset
        {
            FileName = "clight-logo.svg",
            RelativePath = "svg/clight-logo.svg",
            Extension = ".svg",
            MimeType = "image/svg+xml",
            TextContent = canonicalSvg,
            Data = Encoding.UTF8.GetBytes(canonicalSvg),
            Category = "SVG Vectors"
        });

        // 2. Black Theme SVG (Black on Paper White)
        string blackSvg = _svgRenderer.RenderSvg(parameters, new SvgRenderOptions
        {
            FillColor = BrandColorConstants.InkBlackHex,
            BackgroundColor = BrandColorConstants.PaperWhiteHex,
            IncludeXmlDeclaration = true
        });
        list.Add(new GeneratedAsset
        {
            FileName = "clight-logo-black.svg",
            RelativePath = "svg/clight-logo-black.svg",
            Extension = ".svg",
            MimeType = "image/svg+xml",
            TextContent = blackSvg,
            Data = Encoding.UTF8.GetBytes(blackSvg),
            Category = "SVG Vectors"
        });

        // 3. White Theme SVG (White on Ink Black)
        string whiteSvg = _svgRenderer.RenderSvg(parameters, new SvgRenderOptions
        {
            FillColor = BrandColorConstants.PureWhiteHex,
            BackgroundColor = BrandColorConstants.InkBlackHex,
            IncludeXmlDeclaration = true
        });
        list.Add(new GeneratedAsset
        {
            FileName = "clight-logo-white.svg",
            RelativePath = "svg/clight-logo-white.svg",
            Extension = ".svg",
            MimeType = "image/svg+xml",
            TextContent = whiteSvg,
            Data = Encoding.UTF8.GetBytes(whiteSvg),
            Category = "SVG Vectors"
        });

        // 4. White on Transparent SVG (for Dark Mode UI / Overlays)
        string whiteTransparentSvg = _svgRenderer.RenderSvg(parameters, new SvgRenderOptions
        {
            FillColor = BrandColorConstants.PureWhiteHex,
            BackgroundColor = null,
            IncludeXmlDeclaration = true
        });
        list.Add(new GeneratedAsset
        {
            FileName = "clight-logo-white-transparent.svg",
            RelativePath = "svg/clight-logo-white-transparent.svg",
            Extension = ".svg",
            MimeType = "image/svg+xml",
            TextContent = whiteTransparentSvg,
            Data = Encoding.UTF8.GetBytes(whiteTransparentSvg),
            Category = "SVG Vectors"
        });

        // 5. Construction Blueprint SVG
        string constructionSvg = _svgRenderer.RenderConstructionSvg(parameters, new ConstructionRenderOptions());
        list.Add(new GeneratedAsset
        {
            FileName = "clight-logo-construction.svg",
            RelativePath = "svg/clight-logo-construction.svg",
            Extension = ".svg",
            MimeType = "image/svg+xml",
            TextContent = constructionSvg,
            Data = Encoding.UTF8.GetBytes(constructionSvg),
            Category = "Engineering Blueprint"
        });

        // 5. Grid Blueprint SVG
        string gridSvg = _svgRenderer.RenderGridSvg(parameters, new GridRenderOptions());
        list.Add(new GeneratedAsset
        {
            FileName = "clight-logo-grid.svg",
            RelativePath = "svg/clight-logo-grid.svg",
            Extension = ".svg",
            MimeType = "image/svg+xml",
            TextContent = gridSvg,
            Data = Encoding.UTF8.GetBytes(gridSvg),
            Category = "Engineering Blueprint"
        });

        // 6. Safe Area Blueprint SVG
        string safeAreaSvg = _svgRenderer.RenderSafeAreaSvg(parameters, new SafeAreaOptions());
        list.Add(new GeneratedAsset
        {
            FileName = "clight-logo-safe-area.svg",
            RelativePath = "svg/clight-logo-safe-area.svg",
            Extension = ".svg",
            MimeType = "image/svg+xml",
            TextContent = safeAreaSvg,
            Data = Encoding.UTF8.GetBytes(safeAreaSvg),
            Category = "Engineering Blueprint"
        });

        return list;
    }

    public GeneratedAsset GenerateWebManifest()
    {
        var manifest = new WebManifest();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(manifest, options);

        return new GeneratedAsset
        {
            FileName = "manifest.json",
            RelativePath = "web/manifest.json",
            Extension = ".json",
            MimeType = "application/manifest+json",
            TextContent = json,
            Data = Encoding.UTF8.GetBytes(json),
            Category = "Web Assets"
        };
    }

    public GeneratedAsset GenerateIco(IReadOnlyDictionary<int, byte[]> pngFrames, string fileName = "favicon.ico")
    {
        byte[] icoData = IcoEncoder.EncodeIco(pngFrames);
        return new GeneratedAsset
        {
            FileName = fileName,
            RelativePath = $"ico/{fileName}",
            Extension = ".ico",
            MimeType = "image/x-icon",
            Data = icoData,
            Category = "Icon Assets"
        };
    }

    public AssetExportResult CompileBrandCatalog(LogoParameters parameters, IReadOnlyDictionary<string, byte[]>? renderedPngs = null)
    {
        var assets = new List<GeneratedAsset>();

        // 1. SVGs
        assets.AddRange(GenerateSvgAssets(parameters));

        // 2. Web Manifest
        assets.Add(GenerateWebManifest());

        // 3. PNGs if supplied
        if (renderedPngs != null && renderedPngs.Count > 0)
        {
            var icoPngDict = new Dictionary<int, byte[]>();

            foreach (var (name, bytes) in renderedPngs)
            {
                string category = "PNG Raster";
                if (name.Contains("favicon", StringComparison.OrdinalIgnoreCase)) category = "Web Assets";
                else if (name.Contains("apple", StringComparison.OrdinalIgnoreCase)) category = "Web Assets";
                else if (name.Contains("android", StringComparison.OrdinalIgnoreCase)) category = "Web Assets";

                assets.Add(new GeneratedAsset
                {
                    FileName = name,
                    RelativePath = $"png/{name}",
                    Extension = Path.GetExtension(name),
                    MimeType = "image/png",
                    Data = bytes,
                    Category = category
                });

                // Extract size for ICO candidate
                foreach (int size in SizeDimension.IcoSizes)
                {
                    if (name.Equals($"clight-logo-{size}.png", StringComparison.OrdinalIgnoreCase) ||
                        name.Equals($"favicon-{size}.png", StringComparison.OrdinalIgnoreCase))
                    {
                        icoPngDict[size] = bytes;
                    }
                }
            }

            // 4. Generate multi-resolution ICOs if candidate sizes exist
            if (icoPngDict.Count > 0)
            {
                assets.Add(GenerateIco(icoPngDict, "favicon.ico"));
                assets.Add(GenerateIco(icoPngDict, "app.ico"));
            }
        }

        return new AssetExportResult
        {
            Assets = assets,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}
