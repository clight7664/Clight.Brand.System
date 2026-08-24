using Microsoft.JSInterop;

namespace Clight.LogoStudio.Wasm.Services;

/// <summary>
/// Production i18n localization service supporting Chinese, English, Japanese, and Korean.
/// </summary>
public class LocalizationService : ILocalizationService
{
    private readonly IJSRuntime _jsRuntime;
    private string _currentLanguage = "zh";
    private bool _isInitialized = false;

    public LocalizationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public string CurrentLanguage => _currentLanguage;

    public IReadOnlyList<(string Code, string NativeName, string EnglishName, string Flag)> SupportedLanguages { get; } =
    [
        ("zh", "简体中文", "Chinese", "🇨🇳"),
        ("en", "English", "English", "🇺🇸"),
        ("ja", "日本語", "Japanese", "🇯🇵"),
        ("ko", "한국어", "Korean", "🇰🇷")
    ];

    public event Action? OnLanguageChanged;

    public string this[string key] => T(key);

    public string T(string key, params object[] args)
    {
        if (string.IsNullOrWhiteSpace(key)) return string.Empty;

        if (Translations.TryGetValue(_currentLanguage, out var langDict) && langDict.TryGetValue(key, out var translation))
        {
            return args.Length > 0 ? string.Format(translation, args) : translation;
        }

        // Fallback to English
        if (Translations.TryGetValue("en", out var enDict) && enDict.TryGetValue(key, out var enTranslation))
        {
            return args.Length > 0 ? string.Format(enTranslation, args) : enTranslation;
        }

        // Fallback to Chinese
        if (Translations.TryGetValue("zh", out var zhDict) && zhDict.TryGetValue(key, out var zhTranslation))
        {
            return args.Length > 0 ? string.Format(zhTranslation, args) : zhTranslation;
        }

        return key;
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;
        try
        {
            var saved = await _jsRuntime.InvokeAsync<string>("logoStudio.getLanguage");
            if (!string.IsNullOrWhiteSpace(saved) && Translations.ContainsKey(saved))
            {
                _currentLanguage = saved;
            }
            _isInitialized = true;
        }
        catch
        {
            _currentLanguage = "zh";
            _isInitialized = true;
        }
    }

    public async Task SetLanguageAsync(string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode) || !Translations.ContainsKey(languageCode))
            return;

        if (_currentLanguage != languageCode)
        {
            _currentLanguage = languageCode;
            try
            {
                await _jsRuntime.InvokeVoidAsync("logoStudio.setLanguage", languageCode);
            }
            catch { }

            OnLanguageChanged?.Invoke();
        }
    }

    private static readonly Dictionary<string, Dictionary<string, string>> Translations = new()
    {
        ["zh"] = new()
        {
            // Navigation
            ["Nav.Dashboard"] = "01 概览",
            ["Nav.Preview"] = "02 预览",
            ["Nav.Generator"] = "04 生成器",
            ["Nav.Export"] = "05 导出",
            ["Nav.Evolution"] = "06 演化过程",
            ["Nav.Guideline"] = "07 品牌规范",
            ["Nav.Applications"] = "08 应用场景",
            ["Nav.Typography"] = "09 色彩与字型",

            // Common
            ["Common.BrandSystemVersion"] = "品牌系统 v1.0",
            ["Common.Motto"] = "月 · 兰 · C — 反思启思 · 东方留白",
            ["Common.Reset"] = "重置",
            ["Common.Copy"] = "复制",
            ["Common.Copied"] = "已复制",
            ["Common.Download"] = "下载",
            ["Common.Export"] = "导出",
            ["Common.Open"] = "打开",
            ["Common.Loading"] = "加载中...",
            ["Common.All"] = "全部",
            ["Common.ThemeToggle"] = "切换主题模式",
            ["Common.Language"] = "语言",

            // Dashboard
            ["Dashboard.Badge"] = "AI 原生品牌系统 • 月 · 兰 · C",
            ["Dashboard.HeroTitle1"] = "探索与设计",
            ["Dashboard.HeroTitle2"] = "Clight 品牌系统",
            ["Dashboard.HeroDesc"] = "设计 · 预览 · 生成 · 导出。基于黄金分割曲率、流线美学与东方留白构筑的精密参数化品牌标识系统。",
            ["Dashboard.CardPreviewTitle"] = "多模预览",
            ["Dashboard.CardPreviewDesc"] = "实时预览 Logo 效果，多背景多尺寸实时缩放矩阵",
            ["Dashboard.CardPreviewAction"] = "进入预览",
            ["Dashboard.CardGenTitle"] = "参数生成器",
            ["Dashboard.CardGenDesc"] = "自定义生成 Logo，调整曲线、外径、内径与黄金比例",
            ["Dashboard.CardGenAction"] = "进入生成器",
            ["Dashboard.CardExportTitle"] = "资源导出中心",
            ["Dashboard.CardExportDesc"] = "导出全规格矢量 SVG、多分辨率 PNG 及 Windows ICO 资源包",
            ["Dashboard.CardExportAction"] = "进入导出",
            ["Dashboard.CardGuideTitle"] = "品牌设计规范",
            ["Dashboard.CardGuideDesc"] = "几何构造线、安全留白与标准字型系统使用指南",
            ["Dashboard.CardGuideAction"] = "查看规范",
            ["Dashboard.PhilosophyTitle"] = "品牌哲学",
            ["Dashboard.PhilosophyDesc"] = "月 · 兰 · C — 灵感源自月亮的盈缺、兰花的优雅以及字母 C 的本源",
            ["Dashboard.PhilosophyQuote"] = "反思 · 极简智能 · 和谐共生",
            ["Dashboard.QuickExportTitle"] = "快捷导出",
            ["Dashboard.QuickExportDesc"] = "单键下载常用位图与纯矢量文件",
            ["Dashboard.QuickExportZip"] = "一键打包导出全部 (ZIP)",
            ["Dashboard.PackagingZip"] = "正在打包...",

            // Preview
            ["Preview.Title"] = "Clight 标识多维预览",
            ["Preview.Desc"] = "在不同背景、尺寸和工程上下文环境中全方位预览标识效果。",
            ["Preview.ThemeLight"] = "浅色",
            ["Preview.ThemeDark"] = "深色",
            ["Preview.ThemeTransparent"] = "透明棋盘",
            ["Preview.ThemePaper"] = "纸感",
            ["Preview.LayerLabel"] = "工程图层：",
            ["Preview.LayerStandard"] = "标准标识",
            ["Preview.LayerConstruction"] = "几何构造蓝图",
            ["Preview.LayerGrid"] = "32px 模块化网格",
            ["Preview.LayerSafeArea"] = "1X 安全呼吸空间",
            ["Preview.ControlsTitle"] = "控制选项",
            ["Preview.ExportFormat"] = "导出格式",
            ["Preview.LogoResolution"] = "目标分辨率",
            ["Preview.ExportCurrent"] = "导出当前配置",
            ["Preview.ExportNotice"] = "ⓘ 所有导出均采用官方审核的 Clight 标识几何参数。标识为单条平滑流线黑线。",
            ["Preview.FaviconPreviewTitle"] = "Favicon 微缩预览",
            ["Preview.AppIconPreviewTitle"] = "App 图标微缩预览",
            ["Preview.SizeMatrixTitle"] = "尺寸矩阵 (Size Matrix)",
            ["Preview.SizeMatrixRange"] = "16px — 1024px 全规格",

            // Generator
            ["Generator.Title"] = "Logo 参数生成器 (Generator)",
            ["Generator.Desc"] = "实时参数调优：曲线曲率、外圆半径、峰值厚度与黄金比例和谐度。",
            ["Generator.ResetApproved"] = "重置为官方标准",
            ["Generator.CopySvgPath"] = "复制 SVG 路径代码",
            ["Generator.Copied"] = "✓ 路径已复制",
            ["Generator.CurveGeometry"] = "曲线几何参数",
            ["Generator.PhiLock"] = "φ 黄金比例锁 (1.618)",
            ["Generator.OuterRadius"] = "外圆半径 (R_out)",
            ["Generator.StrokeWidth"] = "峰值厚度 (W)",
            ["Generator.TipAngle"] = "开口尖角 (α)",
            ["Generator.WeightPresets"] = "粗细预设 (Weight Presets)",
            ["Generator.WeightThin"] = "细线 Thin (16px)",
            ["Generator.WeightRegular"] = "标准 Regular (26px)",
            ["Generator.WeightBold"] = "粗体 Bold (42px)",
            ["Generator.PositionSymmetry"] = "对称与位移 (Position & Symmetry)",
            ["Generator.XOffset"] = "X 轴偏移",
            ["Generator.YOffset"] = "Y 轴偏移",
            ["Generator.MirrorVertical"] = "垂直翻转 (Mirror V)",
            ["Generator.MirrorHorizontal"] = "水平翻转 (Mirror H)",
            ["Generator.GeneratedPathOutput"] = "实时生成的 SVG Path 路径 (d 属性)",

            // Export
            ["Export.Title"] = "品牌资产导出 (Brand Asset Export)",
            ["Export.Desc"] = "导出官方认证的矢量 SVG、多分辨率 PNG 位图与 Windows ICO 格式。",
            ["Export.ExportSelected"] = "导出已选规格 ({0})",
            ["Export.ExportAllZip"] = "一键导出全套资产 (ZIP)",
            ["Export.PngSizes"] = "PNG 像素规格",
            ["Export.SelectAll"] = "全选",
            ["Export.DeselectAll"] = "取消全选",
            ["Export.DirectDownloads"] = "单文件直下",
            ["Export.GalleryTitle"] = "导出资源缩略画廊",
            ["Export.GalleryDesc"] = "各目标分辨率下的标识清晰度预览",

            // Evolution
            ["Evolution.Title"] = "设计演化历程 (Design Evolution)",
            ["Evolution.Desc"] = "记录 Clight Logo 从多重几何探索、兰花曲线到极简月牙的演进之路。",
            ["Evolution.Stage1Title"] = "原始构造 (Stage 01)",
            ["Evolution.Stage1Desc"] = "多重黄金分割圆与极坐标辅助线的复杂几何交织。",
            ["Evolution.Stage2Title"] = "兰花曲线 (Stage 02)",
            ["Evolution.Stage2Desc"] = "受兰花花瓣轮廓启发的有机自然生长流线。",
            ["Evolution.Stage3Title"] = "月之月魄 (Stage 03)",
            ["Evolution.Stage3Desc"] = "通过数学精确校准月牙尖角与峰值厚度。",
            ["Evolution.Stage4Title"] = "官方定稿 (Stage 04)",
            ["Evolution.Stage4Desc"] = "月亮、兰花与字母 C 的终极纯粹融合。",
            ["Evolution.Stage4Approved"] = "官方标准认证",
            ["Evolution.PhilosophyTitle"] = "演化设计哲学",
            ["Evolution.PhilosophyQuote"] = "“由繁入简，由形入神。Clight Logo 在持续提纯中实现东方意境与现代智能的平衡。”",
            ["Evolution.PhilosophyText"] = "从纷繁的几何构造到纯粹的留白意境，通过反复推敲外径、内径与黄金分割的数学配比，去除一切多余笔触，最终凝聚成这一道兼具东方静谧与现代智能力量的极简弧线。",

            // Guideline
            ["Guideline.Title"] = "品牌设计规范 (Brand Guideline)",
            ["Guideline.Desc"] = "数学构造规范、安全留白空间、最小尺寸阈值与设计治理准则。",
            ["Guideline.DownloadMdZh"] = "下载中文规范 (.MD)",
            ["Guideline.DownloadMdEn"] = "下载英文规范 (.MD)",
            ["Guideline.ConstructionTitle"] = "几何构造",
            ["Guideline.ConstructionSub"] = "黄金比例圆弧、切线端点与基准中心线",
            ["Guideline.ConstructionDesc"] = "外圆半径 R_out = 220px，内圆半径 R_in = 209.5px，尖角开口 46°。两圆交汇自然形成渐变月牙形弧度。",
            ["Guideline.ClearSpaceTitle"] = "安全留白空间",
            ["Guideline.ClearSpaceSub"] = "基于最大峰值厚度的 1X 隔离保护区",
            ["Guideline.ClearSpaceDesc"] = "保持标识周围至少 1X (42px) 的呼吸空间，严禁任何文字、图案或界线侵入安全留白区域。",
            ["Guideline.MinSizeTitle"] = "最小尺寸阈值",
            ["Guideline.MinSizeSub"] = "数字屏幕与实体印刷最小极限",
            ["Guideline.MinSizeDesc"] = "数字屏幕最小显示尺寸为 16×16 px，印刷介质最小高度为 5.0 mm，确保极细尖角的清晰还原。",
            ["Guideline.ColorUsageTitle"] = "色彩使用规范",
            ["Guideline.ColorUsageSub"] = "纯粹黑白高对比度经典组合",
            ["Guideline.ColorUsageDesc"] = "仅使用经审核的水墨黑 (#111111) 与纯白 (#FFFFFF)，严格杜绝非官方彩色、渐变或低对比度搭配。",

            // Applications
            ["Applications.Title"] = "应用场景展示 (Application Board)",
            ["Applications.Desc"] = "网页端、操作系统、开发文档及办公文具的真实场景模拟。",
            ["Applications.TabAll"] = "全部场景",
            ["Applications.TabWeb"] = "网页与图标",
            ["Applications.TabApp"] = "App 与移动端",
            ["Applications.TabStationery"] = "商务与印刷",
            ["Applications.FaviconTitle"] = "浏览器标签微标",
            ["Applications.FaviconSub"] = "官网活跃标签页微缩图标",
            ["Applications.HeaderTitle"] = "网站导航栏",
            ["Applications.HeaderSub"] = "桌面端顶部导航栏与品牌标识组合",
            ["Applications.AppIconTitle"] = "应用程序图标",
            ["Applications.AppIconSub"] = "iOS / macOS 圆角矩形图标 (浅色与深色)",
            ["Applications.DocsTitle"] = "开发者文档门户",
            ["Applications.DocsSub"] = "开发文档侧边栏导航与搜索",
            ["Applications.SplashTitle"] = "移动端开屏体验",
            ["Applications.SplashSub"] = "极简竖屏启动动效模拟",
            ["Applications.AvatarTitle"] = "社交网络头像",
            ["Applications.AvatarSub"] = "1:1 圆形头像与 20% 安全边距",
            ["Applications.CardTitle"] = "商务名片 / 标牌",
            ["Applications.CardSub"] = "棉柔触感纸质搭配烫黑工艺",

            // Typography & Color
            ["Typography.Title"] = "色彩体系与标准字型 (Color & Typography)",
            ["Typography.Desc"] = "官方色彩调色板规范、字体层级搭配与标识使用合规治理。",
            ["Typography.ColorPaletteTitle"] = "品牌核心调色板",
            ["Typography.ColorTokensCount"] = "5 个核心色彩令牌",
            ["Typography.TypeSystemTitle"] = "标准字型系统",
            ["Typography.GovernanceTitle"] = "Logo 使用合规治理 (正确与错误用法)"
        },

        ["en"] = new()
        {
            // Navigation
            ["Nav.Dashboard"] = "01 Dashboard",
            ["Nav.Preview"] = "02 Preview",
            ["Nav.Generator"] = "04 Generator",
            ["Nav.Export"] = "05 Export",
            ["Nav.Evolution"] = "06 Evolution",
            ["Nav.Guideline"] = "07 Guideline",
            ["Nav.Applications"] = "08 Applications",
            ["Nav.Typography"] = "09 Color & Type",

            // Common
            ["Common.BrandSystemVersion"] = "Brand System v1.0",
            ["Common.Motto"] = "Moon · Orchid · C — Reflection & Harmony",
            ["Common.Reset"] = "Reset",
            ["Common.Copy"] = "Copy",
            ["Common.Copied"] = "Copied",
            ["Common.Download"] = "Download",
            ["Common.Export"] = "Export",
            ["Common.Open"] = "Open",
            ["Common.Loading"] = "Loading...",
            ["Common.All"] = "All",
            ["Common.ThemeToggle"] = "Toggle Theme Mode",
            ["Common.Language"] = "Language",

            // Dashboard
            ["Dashboard.Badge"] = "AI-Native Brand System • Moon · Orchid · C",
            ["Dashboard.HeroTitle1"] = "Welcome to",
            ["Dashboard.HeroTitle2"] = "Clight Logo Studio",
            ["Dashboard.HeroDesc"] = "Design. Preview. Generate. Export. A precision parametric brand identity system engineered with golden ratio curvature, fluid aesthetics, and Eastern negative space.",
            ["Dashboard.CardPreviewTitle"] = "Preview Studio",
            ["Dashboard.CardPreviewDesc"] = "Real-time preview across diverse backgrounds, layers, and scale matrices",
            ["Dashboard.CardPreviewAction"] = "Open Preview",
            ["Dashboard.CardGenTitle"] = "Logo Generator",
            ["Dashboard.CardGenDesc"] = "Custom parametric calibration for outer radius, stroke width, and golden ratios",
            ["Dashboard.CardGenAction"] = "Open Generator",
            ["Dashboard.CardExportTitle"] = "Asset Export",
            ["Dashboard.CardExportDesc"] = "Multi-format downloads for SVGs, high-res PNGs, and Windows ICO icon bundles",
            ["Dashboard.CardExportAction"] = "Open Export",
            ["Dashboard.CardGuideTitle"] = "Brand Guideline",
            ["Dashboard.CardGuideDesc"] = "Geometric construction blueprints, 1X clear space, and typography standards",
            ["Dashboard.CardGuideAction"] = "Open Guideline",
            ["Dashboard.PhilosophyTitle"] = "Brand Philosophy",
            ["Dashboard.PhilosophyDesc"] = "Moon · Orchid · C — Inspired by celestial phases, botanical grace, and the primal letter C",
            ["Dashboard.PhilosophyQuote"] = "Reflection. Intelligence. Harmony.",
            ["Dashboard.QuickExportTitle"] = "Quick Export",
            ["Dashboard.QuickExportDesc"] = "Download individual raster or vector formats immediately",
            ["Dashboard.QuickExportZip"] = "Export All Assets (ZIP)",
            ["Dashboard.PackagingZip"] = "Packaging ZIP...",

            // Preview
            ["Preview.Title"] = "Clight Logo Preview",
            ["Preview.Desc"] = "Preview your logo in different backgrounds, scales, and engineering contexts.",
            ["Preview.ThemeLight"] = "Light",
            ["Preview.ThemeDark"] = "Dark",
            ["Preview.ThemeTransparent"] = "Transparent",
            ["Preview.ThemePaper"] = "Paper",
            ["Preview.LayerLabel"] = "Layer:",
            ["Preview.LayerStandard"] = "Standard",
            ["Preview.LayerConstruction"] = "Construction",
            ["Preview.LayerGrid"] = "Grid",
            ["Preview.LayerSafeArea"] = "1X Safe Space",
            ["Preview.ControlsTitle"] = "Controls",
            ["Preview.ExportFormat"] = "Export Format",
            ["Preview.LogoResolution"] = "Logo Resolution",
            ["Preview.ExportCurrent"] = "Export Current Configuration",
            ["Preview.ExportNotice"] = "ⓘ All exports use the approved Clight logo geometry. The logo is a single fluid outer black curved line.",
            ["Preview.FaviconPreviewTitle"] = "Favicon Preview",
            ["Preview.AppIconPreviewTitle"] = "App Icon Preview",
            ["Preview.SizeMatrixTitle"] = "Size Matrix",
            ["Preview.SizeMatrixRange"] = "16px — 1024px",

            // Generator
            ["Generator.Title"] = "Logo Generator",
            ["Generator.Desc"] = "Customize your logo parameters: curve curvature, outer radius, stroke width, and golden ratio harmony.",
            ["Generator.ResetApproved"] = "Reset Approved",
            ["Generator.CopySvgPath"] = "Copy SVG Path",
            ["Generator.Copied"] = "✓ Path Copied",
            ["Generator.CurveGeometry"] = "Curve Geometry",
            ["Generator.PhiLock"] = "φ Lock (1.618)",
            ["Generator.OuterRadius"] = "Outer Radius (R_out)",
            ["Generator.StrokeWidth"] = "Stroke Width (W)",
            ["Generator.TipAngle"] = "Tip Angle (α)",
            ["Generator.WeightPresets"] = "Weight Presets",
            ["Generator.WeightThin"] = "Thin (16px)",
            ["Generator.WeightRegular"] = "Regular (26px)",
            ["Generator.WeightBold"] = "Bold (42px)",
            ["Generator.PositionSymmetry"] = "Position & Symmetry",
            ["Generator.XOffset"] = "X Offset",
            ["Generator.YOffset"] = "Y Offset",
            ["Generator.MirrorVertical"] = "Mirror Vertical",
            ["Generator.MirrorHorizontal"] = "Mirror Horizontal",
            ["Generator.GeneratedPathOutput"] = "Generated SVG Path Output (d attribute)",

            // Export
            ["Export.Title"] = "Brand Asset Export",
            ["Export.Desc"] = "Export approved brand assets across vector SVGs, multi-resolution PNGs, and Windows ICO formats.",
            ["Export.ExportSelected"] = "Export Selected ({0})",
            ["Export.ExportAllZip"] = "Export All (ZIP)",
            ["Export.PngSizes"] = "PNG Sizes",
            ["Export.SelectAll"] = "Select All",
            ["Export.DeselectAll"] = "Deselect All",
            ["Export.DirectDownloads"] = "Direct Vector & Icon Downloads",
            ["Export.GalleryTitle"] = "Export Canvas Gallery",
            ["Export.GalleryDesc"] = "Visual preview of approved logo geometry scaled across target resolutions.",

            // Evolution
            ["Evolution.Title"] = "Design Evolution",
            ["Evolution.Desc"] = "The iterative transformation of the Clight logo from initial geometric exploration to refined minimalist essence.",
            ["Evolution.Stage1Title"] = "Original (Stage 01)",
            ["Evolution.Stage1Desc"] = "Complex geometric intersection of overlapping golden ratio circles and radial coordinates.",
            ["Evolution.Stage2Title"] = "Concept (Stage 02)",
            ["Evolution.Stage2Desc"] = "Fluid organic curve inspired by the petal contour of an orchid and natural growth spirals.",
            ["Evolution.Stage3Title"] = "Refined (Stage 03)",
            ["Evolution.Stage3Desc"] = "Mathematical crescent calibration harmonizing tip knife-edges with crest thickness.",
            ["Evolution.Stage4Title"] = "Final (Stage 04)",
            ["Evolution.Stage4Desc"] = "The definitive, pure synthesis of Moon, Orchid, and Letter C with golden ratio curvature.",
            ["Evolution.Stage4Approved"] = "Stage 04 • Approved",
            ["Evolution.PhilosophyTitle"] = "Design Evolution Philosophy",
            ["Evolution.PhilosophyQuote"] = "\"From complexity to simplicity, from shape to spirit. The Clight logo evolves through continuous refinement to achieve the perfect balance of Eastern aesthetics and modern innovation.\"",
            ["Evolution.PhilosophyText"] = "From intricate geometric construction to pure negative space, the evolution of the Clight Logo embodies design distillation. Through rigorous mathematical calibration of outer radius, inner arc, and golden ratios, all decorative clutter was eliminated.",

            // Guideline
            ["Guideline.Title"] = "Brand Guideline",
            ["Guideline.Desc"] = "Mathematical construction, clear space boundaries, minimum size thresholds, and governance standards.",
            ["Guideline.DownloadMdZh"] = "Download Guideline (Chinese .MD)",
            ["Guideline.DownloadMdEn"] = "Download Guideline (English .MD)",
            ["Guideline.ConstructionTitle"] = "Construction",
            ["Guideline.ConstructionSub"] = "Golden ratio circles, tangent coordinates, and centerlines",
            ["Guideline.ConstructionDesc"] = "Outer radius R_out = 220px, inner radius R_in = 209.5px, opening angle 46°. Two arcs naturally intersect to create the fluid crescent.",
            ["Guideline.ClearSpaceTitle"] = "Clear Space",
            ["Guideline.ClearSpaceSub"] = "1X exclusion zone based on maximum crest thickness",
            ["Guideline.ClearSpaceDesc"] = "Maintain at least 1X (42px) clear breathing room around the logo. No typography, graphics, or borders may intrude.",
            ["Guideline.MinSizeTitle"] = "Minimum Size",
            ["Guideline.MinSizeSub"] = "Digital screen threshold and physical print limits",
            ["Guideline.MinSizeDesc"] = "Minimum digital display size is 16×16 px. Minimum physical print height is 5.0 mm to guarantee knife-edge clarity.",
            ["Guideline.ColorUsageTitle"] = "Color Usage",
            ["Guideline.ColorUsageSub"] = "Pure monochrome high contrast combinations",
            ["Guideline.ColorUsageDesc"] = "Only use approved Ink Black (#111111) and Pure White (#FFFFFF). Non-approved neon, colorful, or low-contrast palettes are strictly prohibited.",

            // Applications
            ["Applications.Title"] = "Application Board",
            ["Applications.Desc"] = "Real-world design mockups across web, operating systems, documentation, and brand stationery.",
            ["Applications.TabAll"] = "All Mockups",
            ["Applications.TabWeb"] = "Web & Favicon",
            ["Applications.TabApp"] = "App & Mobile",
            ["Applications.TabStationery"] = "Stationery & Print",
            ["Applications.FaviconTitle"] = "Browser Favicon",
            ["Applications.FaviconSub"] = "Active tab favicon on clight.com",
            ["Applications.HeaderTitle"] = "Website Header",
            ["Applications.HeaderSub"] = "Desktop navigation bar with Clight wordmark",
            ["Applications.AppIconTitle"] = "Application Icons",
            ["Applications.AppIconSub"] = "iOS / macOS squircle icons (Light & Dark)",
            ["Applications.DocsTitle"] = "Documentation Portal",
            ["Applications.DocsSub"] = "Developer portal sidebar & search",
            ["Applications.SplashTitle"] = "Mobile Splash Screen",
            ["Applications.SplashSub"] = "Minimalist portrait launching experience",
            ["Applications.AvatarTitle"] = "Social Avatar",
            ["Applications.AvatarSub"] = "1:1 circular profile avatar with 20% safe margin",
            ["Applications.CardTitle"] = "Signage / Business Card",
            ["Applications.CardSub"] = "Cotton tactile card with debossed black foil",

            // Typography & Color
            ["Typography.Title"] = "Brand Color & Typography",
            ["Typography.Desc"] = "Official color palette specifications, font hierarchy pairings, and logo integrity governance.",
            ["Typography.ColorPaletteTitle"] = "Brand Color Palette",
            ["Typography.ColorTokensCount"] = "5 Primary Tokens",
            ["Typography.TypeSystemTitle"] = "Typography System",
            ["Typography.GovernanceTitle"] = "Logo Usage Governance (Do's & Don'ts)"
        },

        ["ja"] = new()
        {
            // Navigation
            ["Nav.Dashboard"] = "01 ダッシュボード",
            ["Nav.Preview"] = "02 プレビュー",
            ["Nav.Generator"] = "04 ジェネレーター",
            ["Nav.Export"] = "05 エクスポート",
            ["Nav.Evolution"] = "06 進化プロセス",
            ["Nav.Guideline"] = "07 ガイドライン",
            ["Nav.Applications"] = "08 応用シナリオ",
            ["Nav.Typography"] = "09 配色とタイポ",

            // Common
            ["Common.BrandSystemVersion"] = "ブランドシステム v1.0",
            ["Common.Motto"] = "月 · 蘭 · C — 内省と東洋の余白美",
            ["Common.Reset"] = "リセット",
            ["Common.Copy"] = "コピー",
            ["Common.Copied"] = "コピー完了",
            ["Common.Download"] = "ダウンロード",
            ["Common.Export"] = "エクスポート",
            ["Common.Open"] = "開く",
            ["Common.Loading"] = "読み込み中...",
            ["Common.All"] = "すべて",
            ["Common.ThemeToggle"] = "テーマ切替",
            ["Common.Language"] = "言語",

            // Dashboard
            ["Dashboard.Badge"] = "AI ネイティブブランドシステム • 月 · 蘭 · C",
            ["Dashboard.HeroTitle1"] = "ようこそ",
            ["Dashboard.HeroTitle2"] = "Clight Logo Studio",
            ["Dashboard.HeroDesc"] = "デザイン・プレビュー・生成・エクスポート。黄金比の曲線美と東洋の余白の美学を融合したパラメトリックロゴシステム。",
            ["Dashboard.CardPreviewTitle"] = "マルチプレビュー",
            ["Dashboard.CardPreviewDesc"] = "複数背景・スケールでのロゴリアルタイム確認",
            ["Dashboard.CardPreviewAction"] = "プレビューを開く",
            ["Dashboard.CardGenTitle"] = "パラメトリック生成器",
            ["Dashboard.CardGenDesc"] = "外径・線の太さ・先端角度・黄金比の自由調整",
            ["Dashboard.CardGenAction"] = "生成器を開く",
            ["Dashboard.CardExportTitle"] = "アセット出力",
            ["Dashboard.CardExportDesc"] = "SVG・高解像度PNG・Windows ICO の一括ダウンロード",
            ["Dashboard.CardExportAction"] = "エクスポートを開く",
            ["Dashboard.CardGuideTitle"] = "ブランド設計規範",
            ["Dashboard.CardGuideDesc"] = "幾何学構造図・アイソレーション・標準タイポグラフィ",
            ["Dashboard.CardGuideAction"] = "ガイドラインを開く",
            ["Dashboard.PhilosophyTitle"] = "ブランド哲学",
            ["Dashboard.PhilosophyDesc"] = "月 · 蘭 · C — 月の満ち欠け、蘭の優雅さ、文字Cの調和",
            ["Dashboard.PhilosophyQuote"] = "内省 · ミニマル知性 · 調和",
            ["Dashboard.QuickExportTitle"] = "クイックエクスポート",
            ["Dashboard.QuickExportDesc"] = "主要ベクター・ラスターアセットを瞬時に取得",
            ["Dashboard.QuickExportZip"] = "すべて一括書き出し (ZIP)",
            ["Dashboard.PackagingZip"] = "圧縮中...",

            // Preview
            ["Preview.Title"] = "Clight ロゴプレビュー",
            ["Preview.Desc"] = "多様な背景・スケール・エンジニアリング図層でロゴを確認できます。",
            ["Preview.ThemeLight"] = "ライト",
            ["Preview.ThemeDark"] = "ダーク",
            ["Preview.ThemeTransparent"] = "透過市松",
            ["Preview.ThemePaper"] = "和紙質感",
            ["Preview.LayerLabel"] = "レイヤー：",
            ["Preview.LayerStandard"] = "標準ロゴ",
            ["Preview.LayerConstruction"] = "幾何構造青写真",
            ["Preview.LayerGrid"] = "32px グリッド",
            ["Preview.LayerSafeArea"] = "1X アイソレーション",
            ["Preview.ControlsTitle"] = "操作パネル",
            ["Preview.ExportFormat"] = "出力フォーマット",
            ["Preview.LogoResolution"] = "解像度",
            ["Preview.ExportCurrent"] = "現在の設定で出力",
            ["Preview.ExportNotice"] = "ⓘ すべての出力は公式承認された Clight 幾何構造を使用します。",
            ["Preview.FaviconPreviewTitle"] = "ファビコン縮小表示",
            ["Preview.AppIconPreviewTitle"] = "アプリアイコン縮小表示",
            ["Preview.SizeMatrixTitle"] = "サイズマトリックス",
            ["Preview.SizeMatrixRange"] = "16px — 1024px",

            // Generator
            ["Generator.Title"] = "ロゴ生成器 (Generator)",
            ["Generator.Desc"] = "曲率、外径、峰の太さ、先端角度を自在に調整。",
            ["Generator.ResetApproved"] = "公式標準にリセット",
            ["Generator.CopySvgPath"] = "SVG パスコードをコピー",
            ["Generator.Copied"] = "✓ コピー完了",
            ["Generator.CurveGeometry"] = "曲線幾何パラメータ",
            ["Generator.PhiLock"] = "φ 黄金比固定 (1.618)",
            ["Generator.OuterRadius"] = "外径 (R_out)",
            ["Generator.StrokeWidth"] = "峰の太さ (W)",
            ["Generator.TipAngle"] = "先端角度 (α)",
            ["Generator.WeightPresets"] = "太さプリセット",
            ["Generator.WeightThin"] = "細線 (16px)",
            ["Generator.WeightRegular"] = "標準 (26px)",
            ["Generator.WeightBold"] = "太線 (42px)",
            ["Generator.PositionSymmetry"] = "対称とオフセット",
            ["Generator.XOffset"] = "X 軸オフセット",
            ["Generator.YOffset"] = "Y 轴オフセット",
            ["Generator.MirrorVertical"] = "垂直反転",
            ["Generator.MirrorHorizontal"] = "水平反転",
            ["Generator.GeneratedPathOutput"] = "生成された SVG パス (d 属性)",

            // Export
            ["Export.Title"] = "ブランドアセット書き出し",
            ["Export.Desc"] = "SVG ベクター、各種解像度 PNG、Windows ICO ファイルを生成・ダウンロード。",
            ["Export.ExportSelected"] = "選択した規格を出力 ({0})",
            ["Export.ExportAllZip"] = "全アセット一括出力 (ZIP)",
            ["Export.PngSizes"] = "PNG サイズ規格",
            ["Export.SelectAll"] = "すべて選択",
            ["Export.DeselectAll"] = "選択解除",
            ["Export.DirectDownloads"] = "ダイレクトダウンロード",
            ["Export.GalleryTitle"] = "書き出しアセットギャラリー",
            ["Export.GalleryDesc"] = "解像度別レンダリングプレビュー",

            // Evolution
            ["Evolution.Title"] = "デザイン進化の軌跡",
            ["Evolution.Desc"] = "幾何学探求から極限のミニマリズムへ至る洗練のプロセス。",
            ["Evolution.Stage1Title"] = "初期構造 (Stage 01)",
            ["Evolution.Stage1Desc"] = "重なり合う黄金比の円と極座標補助線の幾何学的探求。",
            ["Evolution.Stage2Title"] = "蘭の曲線 (Stage 02)",
            ["Evolution.Stage2Desc"] = "蘭の花びらの有機的な美しさに着想を得た流線型デザイン。",
            ["Evolution.Stage3Title"] = "月魄の洗練 (Stage 03)",
            ["Evolution.Stage3Desc"] = "数学的精密計算による先端と峰の調和。",
            ["Evolution.Stage4Title"] = "公式決定稿 (Stage 04)",
            ["Evolution.Stage4Desc"] = "月、蘭、文字Cの完璧な融合。",
            ["Evolution.Stage4Approved"] = "公式承認済み",
            ["Evolution.PhilosophyTitle"] = "進化のデザイン哲学",
            ["Evolution.PhilosophyQuote"] = "「複雑から極限の簡潔へ。Clight ロゴは東洋の静寂と先進の知性を体現します。」",
            ["Evolution.PhilosophyText"] = "幾何学的構築から純粋な余白の美学へ。一切の装飾を削ぎ落とし、東洋の静寂と現代のインテリジェンスを兼ね備えた一本の美しい弧線に昇華させました。",

            // Guideline
            ["Guideline.Title"] = "ブランドガイドライン",
            ["Guideline.Desc"] = "幾何学構築、アイソレーション、最小表示サイズ、カラー規定。",
            ["Guideline.DownloadMdZh"] = "中国語版規範ダウンロード (.MD)",
            ["Guideline.DownloadMdEn"] = "英語版規範ダウンロード (.MD)",
            ["Guideline.ConstructionTitle"] = "幾何構造",
            ["Guideline.ConstructionSub"] = "黄金比の円弧、接線座標、中心軸",
            ["Guideline.ConstructionDesc"] = "外径 R_out = 220px、内径 R_in = 209.5px、先端角度 46°。二つの円弧が自然に交差し三日月を形成。",
            ["Guideline.ClearSpaceTitle"] = "アイソレーション",
            ["Guideline.ClearSpaceSub"] = "峰の太さを基準とする 1X 保護エリア",
            ["Guideline.ClearSpaceDesc"] = "ロゴ周囲には最低 1X (42px) の余白を確保し、他の要素の侵入を防いでください。",
            ["Guideline.MinSizeTitle"] = "最小表示サイズ",
            ["Guideline.MinSizeSub"] = "画面表示と印刷時の最小限界",
            ["Guideline.MinSizeDesc"] = "デジタル画面での最小サイズは 16×16 px、印刷物では 5.0 mm 以上を維持してください。",
            ["Guideline.ColorUsageTitle"] = "カラー規定",
            ["Guideline.ColorUsageSub"] = "高コントラストな公式モノクローム",
            ["Guideline.ColorUsageDesc"] = "水墨ブラック (#111111) とピュアホワイト (#FFFFFF) のみを使用してください。",

            // Applications
            ["Applications.Title"] = "応用シナリオ展示",
            ["Applications.Desc"] = "Web、OS、ドキュメント、ステーショナリーでの実用モックアップ。",
            ["Applications.TabAll"] = "すべて",
            ["Applications.TabWeb"] = "Web / アイコン",
            ["Applications.TabApp"] = "アプリ / モバイル",
            ["Applications.TabStationery"] = "文具 / 印刷",
            ["Applications.FaviconTitle"] = "ブラウザファビコン",
            ["Applications.FaviconSub"] = "タブ上での高精細表示",
            ["Applications.HeaderTitle"] = "Web サイトヘッダー",
            ["Applications.HeaderSub"] = "デスクトップナビゲーションとブランドロゴ",
            ["Applications.AppIconTitle"] = "アプリアイコン",
            ["Applications.AppIconSub"] = "iOS / macOS スクワークルアイコン (Light & Dark)",
            ["Applications.DocsTitle"] = "開発者向けドキュメント",
            ["Applications.DocsSub"] = "ポータルサイドバーと検索画面",
            ["Applications.SplashTitle"] = "モバイル起動画面",
            ["Applications.SplashSub"] = "ミニマルなスプラッシュスクリーン",
            ["Applications.AvatarTitle"] = "ソーシャルアバター",
            ["Applications.AvatarSub"] = "1:1 正円プロフィール画像",
            ["Applications.CardTitle"] = "ビジネスカード / 銘板",
            ["Applications.CardSub"] = "上質紙への黒箔押し加工",

            // Typography & Color
            ["Typography.Title"] = "配色とタイポグラフィ",
            ["Typography.Desc"] = "公式カラーパレット、フォントペアリング、ロゴ使用のガイドライン。",
            ["Typography.ColorPaletteTitle"] = "公式カラーパレット",
            ["Typography.ColorTokensCount"] = "5 つのコアカラートークン",
            ["Typography.TypeSystemTitle"] = "タイポグラフィシステム",
            ["Typography.GovernanceTitle"] = "ロゴ使用の禁止事項とルール"
        },

        ["ko"] = new()
        {
            // Navigation
            ["Nav.Dashboard"] = "01 대시보드",
            ["Nav.Preview"] = "02 미리보기",
            ["Nav.Generator"] = "04 생성기",
            ["Nav.Export"] = "05 내보내기",
            ["Nav.Evolution"] = "06 진화 과정",
            ["Nav.Guideline"] = "07 가이드라인",
            ["Nav.Applications"] = "08 응용 분야",
            ["Nav.Typography"] = "09 색상 및 서체",

            // Common
            ["Common.BrandSystemVersion"] = "브랜드 시스템 v1.0",
            ["Common.Motto"] = "달 · 난초 · C — 성찰과 동양적 여백의 미",
            ["Common.Reset"] = "초기화",
            ["Common.Copy"] = "복사",
            ["Common.Copied"] = "복사됨",
            ["Common.Download"] = "다운로드",
            ["Common.Export"] = "내보내기",
            ["Common.Open"] = "열기",
            ["Common.Loading"] = "로딩 중...",
            ["Common.All"] = "전체",
            ["Common.ThemeToggle"] = "테마 전환",
            ["Common.Language"] = "언어",

            // Dashboard
            ["Dashboard.Badge"] = "AI 네이티브 브랜드 시스템 • 달 · 난초 · C",
            ["Dashboard.HeroTitle1"] = "환영합니다",
            ["Dashboard.HeroTitle2"] = "Clight Logo Studio",
            ["Dashboard.HeroDesc"] = "디자인 · 미리보기 · 생성 · 내보내기. 황금비 곡률과 유려한 미학, 동양적 여백이 조화를 이루는 정밀 파라메트릭 브랜드 아이덴티티 시스템.",
            ["Dashboard.CardPreviewTitle"] = "다각도 미리보기",
            ["Dashboard.CardPreviewDesc"] = "다양한 배경과 스케일에서 실시간 로고 렌더링 확인",
            ["Dashboard.CardPreviewAction"] = "미리보기 열기",
            ["Dashboard.CardGenTitle"] = "파라메트릭 생성기",
            ["Dashboard.CardGenDesc"] = "외경, 두께, 팁 각도 및 황금비 자유 조절",
            ["Dashboard.CardGenAction"] = "생성기 열기",
            ["Dashboard.CardExportTitle"] = "자산 내보내기",
            ["Dashboard.CardExportDesc"] = "벡터 SVG, 고해상도 PNG 및 Windows ICO 일괄 다운로드",
            ["Dashboard.CardExportAction"] = "내보내기 열기",
            ["Dashboard.CardGuideTitle"] = "브랜드 가이드라인",
            ["Dashboard.CardGuideDesc"] = "기하학적 설계도, 안전 여백 규정 및 표준 타이포그래피",
            ["Dashboard.CardGuideAction"] = "가이드라인 보기",
            ["Dashboard.PhilosophyTitle"] = "브랜드 철학",
            ["Dashboard.PhilosophyDesc"] = "달 · 난초 · C — 달의 위상 변화, 난초의 우아함, 문자 C의 본질",
            ["Dashboard.PhilosophyQuote"] = "성찰 · 미니멀 인텔리전스 · 조화",
            ["Dashboard.QuickExportTitle"] = "빠른 내보내기",
            ["Dashboard.QuickExportDesc"] = "주요 벡터 및 래스터 포맷을 즉시 다운로드",
            ["Dashboard.QuickExportZip"] = "모든 자산 ZIP 압축 다운로드",
            ["Dashboard.PackagingZip"] = "압축 생성 중...",

            // Preview
            ["Preview.Title"] = "Clight 로고 미리보기",
            ["Preview.Desc"] = "다양한 배경, 크기 및 엔지니어링 컨텍스트에서 로고를 확인하세요.",
            ["Preview.ThemeLight"] = "라이트",
            ["Preview.ThemeDark"] = "다크",
            ["Preview.ThemeTransparent"] = "투명 체커보드",
            ["Preview.ThemePaper"] = "한지 질감",
            ["Preview.LayerLabel"] = "도면 레이어:",
            ["Preview.LayerStandard"] = "기본 로고",
            ["Preview.LayerConstruction"] = "기하학 설계도",
            ["Preview.LayerGrid"] = "32px 그리드",
            ["Preview.LayerSafeArea"] = "1X 안전 여백",
            ["Preview.ControlsTitle"] = "제어판",
            ["Preview.ExportFormat"] = "내보내기 형식",
            ["Preview.LogoResolution"] = "해상도",
            ["Preview.ExportCurrent"] = "현재 설정으로 내보내기",
            ["Preview.ExportNotice"] = "ⓘ 모든 내보내기는 공식 승인된 Clight 기하학 구조를 사용합니다.",
            ["Preview.FaviconPreviewTitle"] = "파비콘 미리보기",
            ["Preview.AppIconPreviewTitle"] = "앱 아이콘 미리보기",
            ["Preview.SizeMatrixTitle"] = "사이즈 매트릭스",
            ["Preview.SizeMatrixRange"] = "16px — 1024px",

            // Generator
            ["Generator.Title"] = "로고 파라메트릭 생성기",
            ["Generator.Desc"] = "곡률, 외경, 최대 두께, 끝단 각도를 직관적으로 조절하세요.",
            ["Generator.ResetApproved"] = "공식 표준으로 리셋",
            ["Generator.CopySvgPath"] = "SVG 경로 코드 복사",
            ["Generator.Copied"] = "✓ 복사 완료",
            ["Generator.CurveGeometry"] = "곡선 기하학 매개변수",
            ["Generator.PhiLock"] = "φ 황금비 고정 (1.618)",
            ["Generator.OuterRadius"] = "외경 반지름 (R_out)",
            ["Generator.StrokeWidth"] = "최대 두께 (W)",
            ["Generator.TipAngle"] = "끝단 각도 (α)",
            ["Generator.WeightPresets"] = "두께 프리셋",
            ["Generator.WeightThin"] = "얇게 Thin (16px)",
            ["Generator.WeightRegular"] = "표준 Regular (26px)",
            ["Generator.WeightBold"] = "굵게 Bold (42px)",
            ["Generator.PositionSymmetry"] = "대칭 및 오프셋",
            ["Generator.XOffset"] = "X축 이동",
            ["Generator.YOffset"] = "Y축 이동",
            ["Generator.MirrorVertical"] = "수직 반전",
            ["Generator.MirrorHorizontal"] = "수평 반전",
            ["Generator.GeneratedPathOutput"] = "실시간 생성된 SVG Path (d 속성)",

            // Export
            ["Export.Title"] = "브랜드 자산 내보내기",
            ["Export.Desc"] = "SVG 벡터, 해상도별 PNG 이미지 및 Windows ICO 아이콘을 다운로드합니다.",
            ["Export.ExportSelected"] = "선택한 규격 내보내기 ({0})",
            ["Export.ExportAllZip"] = "전체 자산 일괄 내보내기 (ZIP)",
            ["Export.PngSizes"] = "PNG 픽셀 규격",
            ["Export.SelectAll"] = "전체 선택",
            ["Export.DeselectAll"] = "선택 해제",
            ["Export.DirectDownloads"] = "개별 파일 바로 받기",
            ["Export.GalleryTitle"] = "내보내기 갤러리",
            ["Export.GalleryDesc"] = "목표 해상도별 로고 렌더링 미리보기",

            // Evolution
            ["Evolution.Title"] = "디자인 진화 과정",
            ["Evolution.Desc"] = "복잡한 기하학적 탐구에서 궁극의 미니멀리즘으로 나아간 여정.",
            ["Evolution.Stage1Title"] = "원시 구조 (Stage 01)",
            ["Evolution.Stage1Desc"] = "황금비 원과 극좌표 보조선이 교차하는 복잡한 기하 구조.",
            ["Evolution.Stage2Title"] = "난초 곡선 (Stage 02)",
            ["Evolution.Stage2Desc"] = "난초 꽃잎의 유기적인 자연미에서 영감을 얻은 유선형 곡선.",
            ["Evolution.Stage3Title"] = "초승달의 정제 (Stage 03)",
            ["Evolution.Stage3Desc"] = "수학적 정밀 계산을 통한 끝단 각도와 최대 두께의 조화.",
            ["Evolution.Stage4Title"] = "최종 승인 (Stage 04)",
            ["Evolution.Stage4Desc"] = "달, 난초, 문자 C의 완벽한 융합.",
            ["Evolution.Stage4Approved"] = "공식 표준 인증",
            ["Evolution.PhilosophyTitle"] = "진화의 디자인 철학",
            ["Evolution.PhilosophyQuote"] = "\"복잡함에서 단순함으로, 형태에서 정신으로. Clight 로고는 동양적 고요함과 현대적 지능을 담아냅니다.\"",
            ["Evolution.PhilosophyText"] = "정교한 기하학적 구조에서 순수한 여백의 미학으로. 모든 불필요한 장식을 배제하고 동양의 고요함과 현대의 지성을 겸비한 단 하나의 유려한 곡선으로 완성되었습니다.",

            // Guideline
            ["Guideline.Title"] = "브랜드 가이드라인",
            ["Guideline.Desc"] = "수학적 기하 구조, 안전 여백, 최소 크기 기준 및 사용 규정.",
            ["Guideline.DownloadMdZh"] = "중문 가이드라인 다운로드 (.MD)",
            ["Guideline.DownloadMdEn"] = "영문 가이드라인 다운로드 (.MD)",
            ["Guideline.ConstructionTitle"] = "기하 구조 설계도",
            ["Guideline.ConstructionSub"] = "황금비 원호, 접선 좌표 및 중심축",
            ["Guideline.ConstructionDesc"] = "외경 반지름 220px, 내경 반지름 209.5px, 끝단 각도 46°. 두 원호가 교차하여 자연스러운 초승달 형상을 이룹니다.",
            ["Guideline.ClearSpaceTitle"] = "안전 여백 (Clear Space)",
            ["Guideline.ClearSpaceSub"] = "최대 두께를 기준으로 하는 1X 보호 영역",
            ["Guideline.ClearSpaceDesc"] = "로고 주변에 최소 1X(42px)의 여백을 확보하여 어떤 텍스트나 그래픽도 침범하지 않도록 합니다.",
            ["Guideline.MinSizeTitle"] = "최소 표시 크기",
            ["Guideline.MinSizeSub"] = "디지털 화면 및 인쇄물 최소 한계",
            ["Guideline.MinSizeDesc"] = "디지털 화면 최소 크기는 16×16 px, 인쇄물 최소 높이는 5.0 mm입니다.",
            ["Guideline.ColorUsageTitle"] = "색상 사용 규정",
            ["Guideline.ColorUsageSub"] = "고대비 공식 흑백 조합",
            ["Guideline.ColorUsageDesc"] = "공식 승인된 먹색(#111111)과 순백색(#FFFFFF)만 사용하세요.",

            // Applications
            ["Applications.Title"] = "응용 분야 전시",
            ["Applications.Desc"] = "웹, 운영체제, 개발 문서 및 사무용품에서의 실사용 목업.",
            ["Applications.TabAll"] = "전체",
            ["Applications.TabWeb"] = "웹 & 아이콘",
            ["Applications.TabApp"] = "앱 & 모바일",
            ["Applications.TabStationery"] = "인쇄 & 문구",
            ["Applications.FaviconTitle"] = "브라우저 파비콘",
            ["Applications.FaviconSub"] = "웹사이트 활성 탭 아이콘",
            ["Applications.HeaderTitle"] = "웹사이트 헤더",
            ["Applications.HeaderSub"] = "데스크톱 내비게이션 바와 브랜드 워드마크",
            ["Applications.AppIconTitle"] = "애플리케이션 아이콘",
            ["Applications.AppIconSub"] = "iOS / macOS 스쿼클 아이콘 (라이트 & 다크)",
            ["Applications.DocsTitle"] = "개발자 문서 포털",
            ["Applications.DocsSub"] = "문서 사이드바 내비게이션 및 검색",
            ["Applications.SplashTitle"] = "모바일 스플래시 화면",
            ["Applications.SplashSub"] = "미니멀 세로형 앱 시작 화면",
            ["Applications.AvatarTitle"] = "소셜 프로필 아바타",
            ["Applications.AvatarSub"] = "1:1 원형 프로필 아바타",
            ["Applications.CardTitle"] = "명함 및 사이니지",
            ["Applications.CardSub"] = "코튼 촉감 명함과 먹박 가공",

            // Typography & Color
            ["Typography.Title"] = "색상 체계 및 서체",
            ["Typography.Desc"] = "공식 색상 팔레트 규격, 폰트 계층 구조 및 로고 사용 거버넌스.",
            ["Typography.ColorPaletteTitle"] = "브랜드 핵심 팔레트",
            ["Typography.ColorTokensCount"] = "5개 핵심 색상 토큰",
            ["Typography.TypeSystemTitle"] = "표준 서체 시스템",
            ["Typography.GovernanceTitle"] = "로고 사용 가이드 (Do's & Don'ts)"
        }
    };
}
