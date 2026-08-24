using Clight.Brand.Guideline.Models;

namespace Clight.Brand.Guideline.Services;

/// <summary>
/// Production implementation of brand guideline documents and design tokens in Chinese and English.
/// </summary>
public class GuidelineProvider : IGuidelineProvider
{
    public IReadOnlyList<BrandPhilosophyItem> GetPhilosophyItems(string lang = "zh")
    {
        return
        [
            new BrandPhilosophyItem
            {
                Key = "Reflection",
                Title = lang switch { "en" => "Reflection", "ja" => "リフレクション", "ko" => "성찰과 반추", _ => "反思 · 启思" },
                ChineseTitle = "反思 · 启思",
                Subtitle = lang switch { "en" => "Reflection is the beginning of growth", "ja" => "内省は成長の始まり", "ko" => "성찰은 성장의 시작입니다", _ => "反思是成长的序章" },
                Description = "Like the moon reflecting radiant light, true intelligence begins with deep observation and reflective restraint.",
                ChineseDescription = "灵感源自月亮的盈缺。光芒不争自明，通过极简的曲线映照思维与智慧的本源。"
            },
            new BrandPhilosophyItem
            {
                Key = "Intelligence",
                Title = lang switch { "en" => "Intelligence", "ja" => "インテリジェンス", "ko" => "미니멀 인텔리전스", _ => "极简智能" },
                ChineseTitle = "极简智能",
                Subtitle = lang switch { "en" => "Intelligence drives innovation forward", "ja" => "知性が革新を推進する", "ko" => "지능이 혁신을 이끕니다", _ => "智能驱动无限可能" },
                Description = "Adaptive precision engineering where complexity is distilled into fluid, mathematical harmony.",
                ChineseDescription = "AI 原生时代的纯净算力与极简架构。去除一切冗余修饰，以精准的数学曲率驱动进化。"
            },
            new BrandPhilosophyItem
            {
                Key = "Harmony",
                Title = lang switch { "en" => "Harmony", "ja" => "調和と余白", "ko" => "조화와 여백", _ => "东方留白 · 和谐" },
                ChineseTitle = "东方留白 · 和谐",
                Subtitle = lang switch { "en" => "Harmony brings balance and beauty", "ja" => "調和が均衡と美をもたらす", "ko" => "조화가 균형과 아름다움을 만듭니다", _ => "和谐孕育秩序与平衡" },
                Description = "The serene silhouette of an orchid petal meeting the universal letter C in perfect equilibrium.",
                ChineseDescription = "兰花的优雅身姿与西方现代主义字母 C 的融合，在虚实与留白之间达成终极平衡。"
            },
            new BrandPhilosophyItem
            {
                Key = "Timeless",
                Title = lang switch { "en" => "Timeless", "ja" => "タイムレス", "ko" => "영원한 가치", _ => "恒久沉静" },
                ChineseTitle = "恒久沉静",
                Subtitle = lang switch { "en" => "Timeless design creates lasting value", "ja" => "時代を超えたデザインが生む価値", "ko" => "영원한 디자인이 가치를 만듭니다", _ => "恒久设计抵御时光冲刷" },
                Description = "Resistant to fleeting trends through rigorous golden ratio geometry and enduring monochrome clarity.",
                ChineseDescription = "基于黄金分割比例与黑白纯粹美学，经历岁月洗炼依然历久弥新。"
            },
            new BrandPhilosophyItem
            {
                Key = "Lightweight",
                Title = lang switch { "en" => "Lightweight", "ja" => "ライトウェイト", "ko" => "경량성과 민첩함", _ => "轻盈敏捷" },
                ChineseTitle = "轻盈敏捷",
                Subtitle = lang switch { "en" => "Lightweight design for powerful ideas", "ja" => "強力な発想のための軽量設計", "ko" => "강력한 아이디어를 위한 경량 설계", _ => "轻盈架构承载厚重思想" },
                Description = "SVG-first architecture engineered for sub-millisecond rendering and infinite scalability.",
                ChineseDescription = "极致的代码体积极致的渲染性能，为现代高并发与端侧应用注入澎湃动力。"
            }
        ];
    }

    public IReadOnlyList<UsageRule> GetUsageRules(string lang = "zh")
    {
        bool isZh = lang == "zh" || lang == "zh-CN";
        bool isJa = lang == "ja";
        bool isKo = lang == "ko";

        return
        [
            new UsageRule
            {
                Title = isZh ? "保持黄金长宽比例" : (isJa ? "黄金比アスペクト比を維持" : (isKo ? "황금 종횡비 유지" : "Preserve Golden Aspect Ratio")),
                Description = isZh ? "始终等比例缩放 Logo，严禁任意拉伸、挤压或破坏自然的月牙弧线。" : (isJa ? "常に等倍で拡大縮小してください。比率を歪めないでください。" : (isKo ? "항상 비율을 유지하며 크기를 조정하세요. 곡선을 왜곡하지 마세요." : "Always scale the logo uniformly. Never stretch, squash, or distort the natural crescent curvature.")),
                IsAllowed = true
            },
            new UsageRule
            {
                Title = isZh ? "禁止单向拉伸变形" : (isJa ? "歪み・変形の禁止" : (isKo ? "일방향 왜곡 금지" : "Don't Stretch or Deform")),
                Description = isZh ? "禁止单独修改水平或垂直方向的缩放比例。" : (isJa ? "水平または垂直方向のみの変形を禁止します。" : (isKo ? "가로 또는 세로 비율만 독립적으로 변경하지 마세요." : "Do not alter the horizontal or vertical proportions independently.")),
                IsAllowed = false
            },
            new UsageRule
            {
                Title = isZh ? "保持标准默认方向" : (isJa ? "規定方向の維持" : (isKo ? "기본 방향 유지" : "Maintain Standard Orientation")),
                Description = isZh ? "禁止随意旋转 Logo，月牙开口方向须始终保持标准开口度。" : (isJa ? "ロゴを無断で回転させないでください。" : (isKo ? "로고를 임의로 회전하지 마세요." : "Do not arbitrarily rotate the logo. The crescent must maintain its default opening orientation.")),
                IsAllowed = false
            },
            new UsageRule
            {
                Title = isZh ? "禁止添加阴影与滤镜" : (isJa ? "ドロップシャドウ等の禁止" : (isKo ? "그림자 및 필터 금지" : "No Drop Shadows or Filters")),
                Description = isZh ? "禁止在矢量路径上附加 3D 浮雕、重度投影、杂色渐变或外发光特效。" : (isJa ? "シャドウやグラデーション効果を追加しないでください。" : (isKo ? "3D 음영, 그림자, 그라데이션 등을 추가하지 마세요." : "Do not apply bevels, heavy 3D drop shadows, gradients, or outer glows to the vector path.")),
                IsAllowed = false
            },
            new UsageRule
            {
                Title = isZh ? "严格黑白单色规范" : (isJa ? "公式モノクロの遵守" : (isKo ? "흑백 단색 규정 준수" : "Strict Monochromatic Palette")),
                Description = isZh ? "仅使用经审核的水墨黑 (#111111) 或纯白 (#FFFFFF)，严禁使用未经审核的霓虹亮色。" : (isJa ? "承認されたブラックとホワイトのみを使用してください。" : (isKo ? "승인된 먹색(#111111) 또는 순백색(#FFFFFF)만 사용하세요." : "Only use approved Ink Black on light surfaces or Pure White on dark surfaces. Do not use unapproved neon or bright colors.")),
                IsAllowed = false
            },
            new UsageRule
            {
                Title = isZh ? "禁止添加额外边框" : (isJa ? "アウトライン追加の禁止" : (isKo ? "외곽선 추가 금지" : "Don't Add Outlines / Strokes")),
                Description = isZh ? "Logo 为纯粹实体轮廓剪影，禁止在其外部增加多重描边或装饰性轮廓线。" : (isJa ? "ロゴは純粋なシルエットです。枠線を追加しないでください。" : (isKo ? "로고는 단일 실루엣입니다. 임의의 테두리선을 추가하지 마세요." : "The logo is a pure solid silhouette. Do not add arbitrary multi-colored borders or stroked hulls.")),
                IsAllowed = false
            }
        ];
    }

    public ConstructionSpec GetConstructionSpec() => new();

    public string GetLogoMarkdown(string lang = "zh")
    {
        if (lang == "en")
        {
            return """
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
        }

        return """
# Clight 品牌系统 — 标识哲学与核心意象

## 1. 核心意象三元组：月 · 兰 · C
Clight 品牌超级符号融合了三大经典意象：
1. **月魄 (月)**：代表静谧的启迪、晨昏的循环与反思省察的智能。
2. **幽兰 (兰)**：象征流线自然的有机美感、顽强生命力与东方美学的虚实留白。
3. **字母 C (C)**：代表 **Clight**、**计算 (Computation)**、**清晰 (Clarity)** 与 **持续演化 (Continuous Evolution)**。

## 2. 五维品牌哲学核心
- **反思启思 (Reflection)**：“反思是成长的序章。”
- **极简智能 (Minimal Intelligence)**：“智能驱动无限可能。”
- **东方留白与和谐 (Harmony)**：“和谐孕育秩序与平衡。”
- **恒久沉静 (Timeless)**：“恒久设计抵御时光冲刷。”
- **轻盈敏捷 (Lightweight)**：“轻盈架构承载厚重思想。”

## 3. 美学设计原则
- **极致极简**：去除一切多余冗余装饰，归真于纯粹的矢量轮廓。
- **平滑弧线**：连续相切的数学几何曲率，杜绝突兀拐点。
- **黄金分割**：所有半径、厚度与留白边界严格符合 $\phi \approx 1.618$。
- **纯粹黑白**：水墨黑 (`#111111`) 与宣纸白 (`#FAF9F6`)。
- **东方留白**：拥抱静谧、留足呼吸空间与秩序感。
""";
    }

    public string GetConstructionMarkdown(string lang = "zh")
    {
        if (lang == "en")
        {
            return """
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
        }

        return """
# Clight 品牌系统 — 几何构造蓝图与数学规范

## 1. 数学架构与坐标方程
Clight 标识由两个严格黄金比例相交的圆弧构成：

- **主画布尺寸**：$512 \times 512$ 笛卡尔坐标系统。
- **外圆弧参数 (Outer Arc)**：
  - 圆心坐标：$(C_{xo}, C_{yo}) = (256.0, 256.0)$
  - 外半径：$R_{outer} = 220.0\text{ px}$
  - 开口尖角：$\alpha = 46.0^\circ$
  - 上顶点坐标：$(256 + 220 \cos 46^\circ, 256 - 220 \sin 46^\circ) \approx (408.825, 97.745)$
  - 下顶点坐标：$(256 + 220 \cos 46^\circ, 256 + 220 \sin 46^\circ) \approx (408.825, 414.255)$
- **内圆弧参数 (Inner Arc)**：
  - 圆心坐标：$(C_{xi}, C_{yi}) \approx (271.518, 256.0)$
  - 内半径：$R_{inner} \approx 209.518\text{ px}$
  - 峰值厚度：$W = 26.0\text{ px}$

## 2. 黄金分割矩阵 ($\phi = 1.61803398875$)
- **同心能量环**：参考同心圆半径 $R_1 = R_o / \phi \approx 135.97$ 与 $R_2 = R_1 / \phi \approx 84.03$。
- **安全留白基准**：标准保护区 $1X = 1.618 \times W \approx 42\text{ px}$。

## 3. 标准 SVG 路径代码
```xml
<path d="M 408.825 97.745 A 220.000 220.000 0 1 0 408.825 414.255 A 209.518 209.518 0 1 1 408.825 97.745 Z" fill="#111111" />
```
""";
    }

    public string GetApplicationMarkdown(string lang = "zh")
    {
        if (lang == "en")
        {
            return """
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
        }

        return """
# Clight 品牌系统 — 场景应用与治理规范

## 1. 安全留白空间 (1X 准则)
- 标识四周必须保留至少 **$1X$** 的隔离呼吸空间（$X = 26\text{ px}$，标准留白 $42\text{ px}$）。
- 严禁任何文字、装饰图案、边框或按钮侵入安全区域。

## 2. 最小使用尺寸阈值
- **数字屏幕**：最小显示尺寸 $16 \times 16\text{ px}$（高清浏览器 Favicon 与状态微标）。
- **移动端图标**：$180 \times 180\text{ px}$ (iOS) / $192 \times 192\text{ px}$ (Android PWA)。
- **印刷介质**：最小印刷高度 $5.0\text{ mm}$，确保尖角细节在高精度印刷中清晰完整。

## 3. 色彩对比度与背景适配
- **浅色环境**：水墨黑 (`#111111`) 搭配宣纸白 (`#FAF9F6`) 或纯白 (`#FFFFFF`)。
- **深色环境**：纯白 (`#FFFFFF`) 搭配水墨黑 (`#111111`) 或深炭灰。
- **WCAG 对比度**：黑白对比度高达 $> 18.5:1$，远超 WCAG AAA 顶级无障碍标准 ($7:1$)。
""";
    }

    public string GetTypographyMarkdown(string lang = "zh")
    {
        if (lang == "en")
        {
            return """
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
        }

        return """
# Clight 品牌系统 — 字型层级与字体排印规范

## 1. 主界面字体：Inter / Plus Jakarta Sans
- **应用场景**：核心 UI 界面、标题、正文、开发文档及数字产品。
- **特性**：几何无衬线结构，微缩尺寸下仍具备极高易读性，现代而克制。
- **字重规格**：Light (300)、Regular (400)、Medium (500)、SemiBold (600)。

## 2. 叙事与展示字体：Cormorant Garamond
- **应用场景**：社论大标题、品牌哲学故事、诗意语录。
- **特性**：古典高对比衬线字形，散发文学美感与东方留白沉静气质。

## 3. 字阶比例 (1.250 大三度字阶)
- **Display 1 (特大展题)**：48px / 1.1 行高
- **Heading 1 (一级标题)**：32px / 1.2 行高
- **Heading 2 (二级标题)**：24px / 1.3 行高
- **Body (常规正文)**：15px / 1.5 行高
- **Caption / Meta (说明小字)**：12px / 1.4 行高
""";
    }

    public string GetColorsMarkdown(string lang = "zh")
    {
        if (lang == "en")
        {
            return """
# Clight Brand System — Color System Tokens

| Color Name | Hex Code | RGB | CMYK | Usage |
| :--- | :--- | :--- | :--- | :--- |
| **Ink Black** | `#111111` | 17, 17, 17 | 0, 0, 0, 93 | Primary dark brand color, logo, headings |
| **Paper White** | `#FAF9F6` | 250, 249, 246 | 0, 0, 2, 2 | Primary light canvas, backgrounds |
| **Mist Gray** | `#E0E0E0` | 224, 224, 224 | 0, 0, 0, 12 | Hairline dividers, gridlines, inactive borders |
| **Deep Gray** | `#444444` | 68, 68, 68 | 0, 0, 0, 73 | Secondary body text, metadata, captions |
| **Pure White** | `#FFFFFF` | 255, 255, 255 | 0, 0, 0, 0 | Dark-mode foreground glyphs, high contrast |
""";
        }

        return """
# Clight 品牌系统 — 官方色彩规范与调色板令牌

| 色彩名称 | 十六进制 (Hex) | RGB 坐标 | CMYK 印刷值 | 应用场景 |
| :--- | :--- | :--- | :--- | :--- |
| **水墨黑 (Ink Black)** | `#111111` | 17, 17, 17 | 0, 0, 0, 93 | 核心深色品牌主色、Logo、主标题 |
| **宣纸白 (Paper White)** | `#FAF9F6` | 250, 249, 246 | 0, 0, 2, 2 | 浅色画布背景、自然温润纸感 |
| **雾灰 (Mist Gray)** | `#E0E0E0` | 224, 224, 224 | 0, 0, 0, 12 | 结构分割线、辅助网格线、微边框 |
| **深灰 (Deep Gray)** | `#444444` | 68, 68, 68 | 0, 0, 0, 73 | 次级正文、元数据、技术标注 |
| **纯白 (Pure White)** | `#FFFFFF` | 255, 255, 255 | 0, 0, 0, 0 | 深色模式前景主体、最高对比度 |
""";
    }

    public string GetFullGuidelineMarkdown(string lang = "zh")
    {
        return GetLogoMarkdown(lang) + "\n\n---\n\n" +
               GetConstructionMarkdown(lang) + "\n\n---\n\n" +
               GetApplicationMarkdown(lang) + "\n\n---\n\n" +
               GetTypographyMarkdown(lang) + "\n\n---\n\n" +
               GetColorsMarkdown(lang);
    }
}
