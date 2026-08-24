# Clight Brand System & Logo Studio (Moon · Orchid · Letter C)

> **AI-Native Parametric Logo Design System & Interactive Brand Asset Studio**  
> *Core Symbol: 月 · 兰 · C (Moon · Orchid · Letter C) — Reflection, Minimal Intelligence, Eastern Harmony.*

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor WASM](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![CI/CD GitHub Pages](https://github.com/clight7664/Clight.Brand.System/actions/workflows/deploy.yml/badge.svg)](https://github.com/clight7664/Clight.Brand.System/actions/workflows/deploy.yml)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

[**简体中文**](README.md) | **English**

---

## 📖 Executive Summary

**Clight Brand System** is an enterprise-grade, production-ready brand identity engine and interactive Blazor WebAssembly studio. Designed with an **SVG-First Architecture**, mathematical **Golden Ratio ($\phi \approx 1.618$)** calibration, and an aesthetic synthesis of Eastern negative space (*留白*) and modern digital minimalism (Linear / Vercel style).

The interactive studio is built on **C# 12 / .NET 8 Blazor WebAssembly**, featuring real-time client-side parametric geometry calculation, multi-resolution rasterization, and one-click ZIP packaging without server dependencies.

---

## 🏛️ Brand Philosophy & Core Symbol

```
                       🌙 The Crescent Moon (月)
                       Reflection · Quiet Illumination
                                  │
                                  ▼
 🌸 The Orchid Petal (兰) ────────┼──────── 🔤 The Letter C (C)
  Fluid Grace · Eastern 留白      │         Clight · Computation · Evolution
                                  ▼
                    ✨ Clight Brand System Symbol
```

- **Reflection (反思 · 启思)**: *"Reflection is the beginning of growth."* — Like the moon reflecting celestial light, true intelligence begins with deep observation and reflective restraint.
- **Minimal Intelligence (极简智能)**: *"Intelligence drives innovation forward."* — AI-native computing distilled into fluid, mathematical harmony with zero decorative clutter.
- **Harmony (东方留白与和谐)**: *"Harmony brings balance and beauty."* — The organic curve of an orchid petal meeting the universal letter C in equilibrium.
- **Timeless (恒久沉静)**: *"Timeless design creates lasting value."* — Built upon enduring geometric principles and monochrome clarity that resist fleeting trends.
- **Lightweight (轻盈敏捷)**: *"Lightweight design for powerful ideas."* — Ultra-low footprint, sub-millisecond vector rendering, and instantaneous asset compilation.

---

## 📐 Geometric & Mathematical Construction

The Clight symbol is formed by two intersecting circular arcs with golden ratio proportions:

| Parameter | Symbol | Value (512 Grid) | Proportional Relation |
| :--- | :--- | :--- | :--- |
| **Canvas ViewBox** | $V$ | $512 \times 512\text{ px}$ | Master Coordinate Space |
| **Outer Arc Radius** | $R_{outer}$ | $220.0\text{ px}$ | Primary Circular Boundary |
| **Inner Arc Radius** | $R_{inner}$ | $209.52\text{ px}$ | Calculated via Tangent Tips |
| **Tip Opening Angle** | $\alpha$ | $46.0^\circ$ | Top Tip: $(408.825, 97.745)$ |
| **Crest Stroke Width** | $W$ | $26.0\text{ px}$ | Proportional to $R_o / \phi^4 \times 10$ |
| **Clear Space Margin** | $1X$ | $42.0\text{ px}$ | $W \times \phi$ Safe Exclusion Zone |

### Master SVG Path
```xml
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" width="512" height="512">
  <path d="M 408.825 97.745 A 220.000 220.000 0 1 0 408.825 414.255 A 209.518 209.518 0 1 1 408.825 97.745 Z" fill="#111111" />
</svg>
```

---

## 🖥️ Interactive Logo Studio (Blazor WebAssembly)

The Studio provides 8 dedicated modules accessible via sidebar navigation:

1. **01 Dashboard (`/`)**: Welcome Hero, 5-Pillar Philosophy cards, Quick Export Bar (1-Click PNG/SVG/ZIP).
2. **02 Preview (`/preview`)**: Live rendering with Light, Dark, Transparent, and Paper themes; 4 layer blueprints (Standard, Construction, Grid, 1X Safe Space); zoom $50\% - 300\%$; Favicon & App Icon micro previews; 16px-1024px size matrix.
3. **04 Generator (`/generator`)**: Parametric sliders for outer radius, stroke width, tip angle, and center offset; $\phi$ golden ratio lock; symmetry mirroring; real-time SVG path code generation & copy.
4. **05 Export (`/export`)**: Multi-size selector matrix (16 to 1024px), batch ZIP packaging, standalone SVG and Windows ICO downloads.
5. **06 Evolution (`/evolution`)**: 4-stage iteration storyboard from blueprint to final approved symbol.
6. **07 Guideline (`/guideline`)**: Construction specs, clear space, minimum size limits, and downloadable Markdown guidelines (Chinese & English).
7. **08 Applications (`/applications`)**: Realistic design mockups across web headers, tabs, app icons, docs, and stationery.
8. **09 Color & Typography (`/typography`)**: WCAG AAA color token cards, typographic specimens, and logo governance rules.

---

## 🧩 Solution Architecture

```
Clight.Brand.System/
├── Clight.Brand.System.sln          # Master .NET 8 / C# 12 Visual Studio Solution
├── Directory.Build.props            # Global Roslyn & Nullable configuration
├── .gitignore                       # .NET / IDE / Build output ignore rules
├── .github/
│   └── workflows/
│       └── deploy.yml               # GitHub Actions CI/CD for GitHub Pages
├── README.md                        # Chinese Documentation
├── README.en.md                     # English Documentation
├── brand-guideline/                 # Comprehensive Markdown Guidelines (Bilingual)
│   ├── Logo.zh.md / Logo.en.md
│   ├── Construction.zh.md / Construction.en.md
│   ├── Application.zh.md / Application.en.md
│   ├── Typography.zh.md / Typography.en.md
│   └── Colors.zh.md / Colors.en.md
├── assets/                          # Pre-compiled high-resolution brand assets
├── src/
│   ├── Clight.Logo.Core/            # Mathematical models & LogoCalculator
│   ├── Clight.Logo.Renderer/        # SVG Renderer & Engineering Blueprints
│   ├── Clight.Asset.Generator/      # Binary ICO encoder & Web Manifest
│   ├── Clight.Brand.Guideline/      # Multilingual guideline provider service
│   └── Clight.LogoStudio.Wasm/      # Interactive Blazor WASM Studio (i18n: zh, en, ja, ko)
└── tests/
    └── Clight.Brand.Tests/          # xUnit Test Suite (100% Core Coverage)
```

---

## 🛠️ Build, Test & Run Instructions

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### 1. Restore & Build
```bash
git clone https://github.com/clight7664/Clight.Brand.System.git
cd Clight.Brand.System
dotnet restore Clight.Brand.System.sln
dotnet build Clight.Brand.System.sln --configuration Release
```

### 2. Run Comprehensive Unit Tests
```bash
dotnet test tests/Clight.Brand.Tests/Clight.Brand.Tests.csproj --verbosity normal
```

### 3. Launch Blazor WebAssembly Studio
```bash
dotnet run --project src/Clight.LogoStudio.Wasm/Clight.LogoStudio.Wasm.csproj
```
Open `http://localhost:5000` or `https://localhost:5001`.

### 4. Publish for Production Deployment
```bash
dotnet publish src/Clight.LogoStudio.Wasm/Clight.LogoStudio.Wasm.csproj -c Release -o publish/wasm
```

---

## 🚀 CI/CD & GitHub Pages Deployment

The repository includes an automated GitHub Actions pipeline [`.github/workflows/deploy.yml`](.github/workflows/deploy.yml):
1. **Quality Gate**: Runs all 22 unit tests.
2. **WASM Compilation**: Compiles release artifacts.
3. **SPA Routing**: Generates `.nojekyll`, fixes `<base href="/Clight.Brand.System/" />`, and provides `404.html` fallback.
4. **Deploy**: Automatically deploys to GitHub Pages via `actions/deploy-pages@v4`.

---

## 📄 License & Attribution
Copyright © 2026 Clight Inc. All rights reserved.  
Engineered with precision for the Clight Brand Identity System.
