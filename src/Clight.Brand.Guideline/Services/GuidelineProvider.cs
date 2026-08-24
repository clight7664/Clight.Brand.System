using Clight.Brand.Guideline.Models;

namespace Clight.Brand.Guideline.Services;

/// <summary>
/// Production implementation of brand guideline documents and design tokens.
/// </summary>
public class GuidelineProvider : IGuidelineProvider
{
    public IReadOnlyList<BrandPhilosophyItem> GetPhilosophyItems()
    {
        return
        [
            new BrandPhilosophyItem
            {
                Key = "Reflection",
                Title = "Reflection",
                ChineseTitle = "反思 · 启思",
                Subtitle = "Reflection is the beginning of growth",
                Description = "Like the moon reflecting radiant light, true intelligence begins with deep observation and reflective restraint.",
                ChineseDescription = "灵感源自月亮的盈缺。光芒不争自明，通过极简的曲线映照思维与智慧的本源。"
            },
            new BrandPhilosophyItem
            {
                Key = "Intelligence",
                Title = "Intelligence",
                ChineseTitle = "极简智能",
                Subtitle = "Intelligence drives innovation forward",
                Description = "Adaptive precision engineering where complexity is distilled into fluid, mathematical harmony.",
                ChineseDescription = "AI 原生时代的纯净算力与极简架构。去除一切冗余修饰，以精准的数学曲率驱动进化。"
            },
            new BrandPhilosophyItem
            {
                Key = "Harmony",
                Title = "Harmony",
                ChineseTitle = "东方留白 · 和谐",
                Subtitle = "Harmony brings balance and beauty",
                Description = "The serene silhouette of an orchid petal meeting the universal letter C in perfect equilibrium.",
                ChineseDescription = "兰花的优雅身姿与西方现代主义字母 C 的融合，在虚实与留白之间达成终极平衡。"
            },
            new BrandPhilosophyItem
            {
                Key = "Timeless",
                Title = "Timeless",
                ChineseTitle = "恒久沉静",
                Subtitle = "Timeless design creates lasting value",
                Description = "Resistant to fleeting trends through rigorous golden ratio geometry and enduring monochrome clarity.",
                ChineseDescription = "基于黄金分割比例与黑白纯粹美学，经历岁月洗炼依然历久弥新。"
            },
            new BrandPhilosophyItem
            {
                Key = "Lightweight",
                Title = "Lightweight",
                ChineseTitle = "轻盈敏捷",
                Subtitle = "Lightweight design for powerful ideas",
                Description = "SVG-first architecture engineered for sub-millisecond rendering and infinite scalability.",
                ChineseDescription = "极致的代码体积极致的渲染性能，为现代高并发与端侧应用注入澎湃动力。"
            }
        ];
    }

    public IReadOnlyList<UsageRule> GetUsageRules()
    {
        return
        [
            new UsageRule
            {
                Title = "Preserve Golden Aspect Ratio",
                Description = "Always scale the logo uniformly. Never stretch, squash, or distort the natural crescent curvature.",
                IsAllowed = true
            },
            new UsageRule
            {
                Title = "Don't Stretch or Deform",
                Description = "Do not alter the horizontal or vertical proportions independently.",
                IsAllowed = false
            },
            new UsageRule
            {
                Title = "Maintain Standard Orientation",
                Description = "Do not arbitrarily rotate the logo. The crescent must maintain its default opening orientation.",
                IsAllowed = false
            },
            new UsageRule
            {
                Title = "No Drop Shadows or Filters",
                Description = "Do not apply bevels, heavy 3D drop shadows, gradients, or outer glows to the vector path.",
                IsAllowed = false
            },
            new UsageRule
            {
                Title = "Strict Monochromatic Palette",
                Description = "Only use approved Ink Black on light surfaces or Pure White on dark surfaces. Do not use unapproved neon or bright colors.",
                IsAllowed = false
            },
            new UsageRule
            {
                Title = "Don't Add Outlines / Strokes",
                Description = "The logo is a pure solid silhouette. Do not add arbitrary multi-colored borders or stroked hulls.",
                IsAllowed = false
            }
        ];
    }

    public ConstructionSpec GetConstructionSpec() => new();

    public string GetLogoMarkdown() => """
# Clight Brand System — Logo Philosophy & Symbolism

## 1. Symbol Triad: 月 · 兰 · C (Moon · Orchid · Letter C)
The Clight symbol is a unified synthesis of three archetypes:
1. **The Crescent Moon (月)**: Signifying quiet illumination, celestial cycles, and reflective intelligence.
2. **The Orchid Petal (兰)**: Embodying organic fluid grace, natural resilience, and Eastern aesthetic balance (东方留白).
3. **The Letter C (C)**: Representing **Clight**, **Clarity**, **Computation**, and **Continuous Evolution**.

## 2. Core Meaning & Design Pillars
- **Reflection (反思 · 启思)**: "Reflection is the beginning of growth."
- **Minimal Intelligence (极简智能)**: "Intelligence drives innovation forward."
- **Harmony (和谐共生)**: "Harmony brings balance and beauty."
- **Timeless (恒久价值)**: "Timeless design creates lasting value."
- **Lightweight (轻盈高效)**: "Lightweight design for powerful ideas."

## 3. Aesthetic Principles
- **Minimalist**: Reduction of all decorative excess to essential vector contours.
- **Fluid Curves**: Continuous tangential curvature without abrupt inflection artifacts.
- **Golden Ratio**: Every radius, thickness, and spatial margin is rooted in $\phi \approx 1.618$.
- **Monochrome Purity**: Ink Black (`#111111`) and Paper White (`#FAF9F6`).
- **Eastern Negative Space**: Embracing stillness, breathing room, and intentional silence.
""";

    public string GetConstructionMarkdown() => """
# Clight Brand System — Geometric Construction Blueprint

## 1. Mathematical Architecture
The Clight Logo is constructed from two intersecting circular arcs with strictly proportioned centers and radii:

- **Master ViewBox**: $512 \times 512$ unit Cartesian coordinate system.
- **Outer Circle Arc**:
  - Center: $(C_{xo}, C_{yo}) = (256.0, 256.0)$
  - Radius: $R_{outer} = 220.0$
  - Tip Opening Angle: $\alpha = 46.0^\circ$ from horizontal
  - Top Tip: $(256 + 220 \cos 46^\circ, 256 - 220 \sin 46^\circ) \approx (408.825, 97.745)$
  - Bottom Tip: $(256 + 220 \cos 46^\circ, 256 + 220 \sin 46^\circ) \approx (408.825, 414.255)$
- **Inner Circle Arc**:
  - Center: $(C_{xi}, C_{yi}) \approx (271.518, 256.0)$
  - Radius: $R_{inner} \approx 209.518$
  - Maximum Crest Thickness: $W = 26.0\text{px}$ (Proportional to $R_{outer} / \phi^4 \times 10$)
  
## 2. Golden Ratio Matrix ($\phi = 1.61803398875$)
- **Concentric Energy Bands**: Guide radii at $R_1 = R_o / \phi \approx 135.97$ and $R_2 = R_1 / \phi \approx 84.03$.
- **Clear Space Multiplier**: Standard clearance margin $1X = 1.618 \times W \approx 42\text{px}$.

## 3. Production SVG Path Definition
```xml
<path d="M 408.825 97.745 A 220.000 220.000 0 1 0 408.825 414.255 A 209.518 209.518 0 1 1 408.825 97.745 Z" fill="#111111" />
```
""";

    public string GetApplicationMarkdown() => """
# Clight Brand System — Application Guidelines & Governance

## 1. Clear Space (Exclusion Zone)
- The minimum clear space surrounding the logo is designated as **$1X$**, where $X$ equals the maximum crescent crest width ($W = 26\text{px}$ on standard grid).
- No typography, graphics, photos, or interface borders may encroach inside the exclusion zone.

## 2. Minimum Size Requirements
- **Digital Screens**: $16 \times 16\text{ px}$ (Optimized favicon and micro-indicator).
- **Mobile App Icons**: $180 \times 180\text{ px}$ (iOS) / $192 \times 192\text{ px}$ (Android).
- **Physical Print**: Minimum height of $5.0\text{ mm}$ with $1200\text{ DPI}$ vector output.

## 3. Contrast & Background Compliance
- **Light Context**: Ink Black (`#111111`) on Paper White (`#FAF9F6`), Pure White (`#FFFFFF`), or light gray textures.
- **Dark Context**: Pure White (`#FFFFFF`) on Ink Black (`#111111`), Deep Charcoal (`#1A1A1A`), or dark OLED surfaces.
- **WCAG Contrast Ratio**: Maintains $> 18.5:1$ contrast ratio, vastly exceeding WCAG AAA standard ($7:1$).
""";

    public string GetTypographyMarkdown() => """
# Clight Brand System — Typography & Font Pairing

## 1. Primary Interface Font: Inter / Plus Jakarta Sans
- **Role**: Primary UI, headings, body text, documentation, and digital products.
- **Character**: Clean geometric neo-grotesque, ultra-readable at micro sizes, neutral yet modern.
- **Weights**: Light (300), Regular (400), Medium (500), SemiBold (600).

## 2. Display Accent Font: Cormorant Garamond / Playfair
- **Role**: Editorial headlines, brand storytelling, philosophy cards.
- **Character**: Elegant high-contrast serif reflecting classical literature and refined grace.

## 3. Typographic Scale (1.250 Major Third)
- **Display 1**: 48px / 1.1 Line Height
- **Heading 1**: 32px / 1.2 Line Height
- **Heading 2**: 24px / 1.3 Line Height
- **Body**: 15px / 1.5 Line Height
- **Caption / Meta**: 12px / 1.4 Line Height
""";

    public string GetColorsMarkdown() => """
# Clight Brand System — Color System Tokens

| Color Name | Hex Code | RGB | CMYK | Usage |
| :--- | :--- | :--- | :--- | :--- |
| **Ink Black (水墨黑)** | `#111111` | 17, 17, 17 | 0, 0, 0, 93 | Primary dark brand color, logo, headings |
| **Paper White (宣纸白)** | `#FAF9F6` | 250, 249, 246 | 0, 0, 2, 2 | Primary light canvas, backgrounds |
| **Mist Gray (雾灰)** | `#E0E0E0` | 224, 224, 224 | 0, 0, 0, 12 | Hairline dividers, gridlines, inactive borders |
| **Deep Gray (深灰)** | `#444444` | 68, 68, 68 | 0, 0, 0, 73 | Secondary body text, metadata, captions |
| **Pure White** | `#FFFFFF` | 255, 255, 255 | 0, 0, 0, 0 | Dark-mode foreground glyphs, high contrast |
""";
}
