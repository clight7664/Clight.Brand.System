# Clight Brand System & Logo Studio (月 · 兰 · C)

> **AI 原生参数化品牌标识系统与资产交互生成器**  
> **AI-Native Parametric Logo Design System & Brand Asset Studio**  
> *核心意象：月 · 兰 · C (Moon · Orchid · Letter C) — 反思启思 · 极简智能 · 东方留白*

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor WASM](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![CI/CD GitHub Pages](https://github.com/clight7664/Clight.Brand.System/actions/workflows/deploy.yml/badge.svg)](https://github.com/clight7664/Clight.Brand.System/actions/workflows/deploy.yml)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

**简体中文** | [**English**](README.en.md)

---

## 📖 项目概述 (Executive Summary)

**Clight Brand System** 是一套面向 AI 原生时代的企业级品牌标识系统与交互式 WebAssembly 设计工作台。项目基于 **SVG-First** 纯矢量数学架构、**黄金分割比例 ($\phi \approx 1.618$)** 曲线调和，并将东方美学的“留白与静谧”与现代数字极简主义（Linear / Vercel 风格）深度融合。

在线工作台采用 **C# 12 / .NET 8 Blazor WebAssembly** 构建，具备纯前端零服务端依赖的实时参数几何计算、多分辨率矢量转位图栅格化、以及一键打包导出能力。

---

## 🏛️ 品牌哲学与核心意象 (Brand Philosophy & Symbolism)

```
                       🌙 月魄 (The Crescent Moon)
                       反思 · 静谧 · 晨昏之光
                                  │
                                  ▼
 🌸 幽兰 (The Orchid Petal) ─────┼───── 🔤 字母 C (The Letter C)
  有机生长 · 东方留白之韵        │      Clight · 计算 · 演化 · 链接
                                  ▼
                   ✨ Clight 品牌超级符号 (Symbol)
```

- **反思启思 (Reflection)**: *“反思是成长的序章。”* — 如同明月静照大千，智能的本质始于深邃的观察与克制的省思。
- **极简智能 (Minimal Intelligence)**: *“智能驱动无限可能。”* — 去除一切多余修饰，以纯粹的数学弧线凝聚高密度信息与算力之美。
- **东方留白与和谐 (Harmony & Negative Space)**: *“和谐孕育秩序与平衡。”* — 兰花花瓣的有机流线与几何圆弧完美平衡，虚实相生。
- **恒久沉静 (Timeless)**: *“恒久设计抵御时光冲刷。”* — 基于古典几何比例与黑白高对比度，历久弥新。
- **轻盈敏捷 (Lightweight)**: *“轻盈架构承载厚重思想。”* — 毫秒级矢量渲染，极小资源占用，全端敏捷响应。

---

## 📐 几何构造与数学规范 (Geometric & Mathematical Specs)

Clight 符号由两个精密相交的黄金比例圆弧构成，所有关键尺寸均在 $512 \times 512$ 标准画布上严谨标定：

```
                    (408.825, 97.745)  Top Tip (开口尖角 α=46°)
                           ╭─────────╮
                          │           │
   Outer Crest ───────────┤           │  Outer Radius R_out = 220.0px
   (36.0, 256.0)          │           │  Inner Radius R_in = 209.52px
     Stroke Width W=26.0px ╰─────────╯
                    (408.825, 414.255) Bottom Tip
```

| 参数名称 (Parameter) | 符号 | 标准值 (512 Grid) | 比例与几何关系 |
| :--- | :--- | :--- | :--- |
| **主画布尺寸 (ViewBox)** | $V$ | $512 \times 512\text{ px}$ | 基准坐标空间 |
| **外圆半径 (Outer Radius)** | $R_{out}$ | $220.0\text{ px}$ | 外轮廓主圆弧 |
| **内圆半径 (Inner Radius)** | $R_{in}$ | $209.52\text{ px}$ | 由端点切线与峰值厚度精确求解 |
| **开口尖角 (Tip Angle)** | $\alpha$ | $46.0^\circ$ | 上顶点 $(408.825, 97.745)$，下顶点 $(408.825, 414.255)$ |
| **峰值厚度 (Crest Width)** | $W$ | $26.0\text{ px}$ | 黄金比例阶数对应厚度 |
| **安全空间 (Clear Space)** | $1X$ | $42.0\text{ px}$ | $W \times \phi$ 绝对隔离保护区 |

### 标准 SVG 代码 (Canonical SVG)
```xml
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" width="512" height="512">
  <path d="M 408.825 97.745 A 220.000 220.000 0 1 0 408.825 414.255 A 209.518 209.518 0 1 1 408.825 97.745 Z" fill="#111111" />
</svg>
```

---

## 🖥️ 交互式 Logo Studio 功能架构 (Interactive Studio)

工作台提供 8 大核心功能模块：

1. **01 Dashboard (仪表盘)**: 系统概览、核心哲学五维矩阵、快捷导出条（1-Click PNG/SVG/ZIP）。
2. **02 Preview (多模预览)**:
   - 支持 **Light** (`#FAF9F6`)、**Dark** (`#111111`)、**Transparent** (棋盘透明)、**Paper** (`#F5F2EB`) 四大主题。
   - **4 种专业工程图层**: Standard (标准)、Construction (几何构造蓝图)、Grid (32px 模块化网格)、1X Safe Space (安全呼吸空间)。
   - $50\% - 300\%$ 无级平滑缩放、Favicon & iOS App Icon 实时微缩图。
   - 16px 至 1024px 全规格尺寸矩阵 (Size Matrix)。
3. **04 Generator (参数生成器)**:
   - 实时滑块调节：外半径 $R_{out}$、峰值厚度 $W$、开口尖角 $\alpha$、XY 偏移量。
   - $\phi$ 黄金比例锁定开关、粗细预设 (Thin 16px / Regular 26px / Bold 42px)、水平/垂直镜像。
   - 实时生成 SVG `d` 路径代码并支持一键复制。
4. **05 Export (资源导出中心)**:
   - 10 种分辨率多选矩阵 (16, 32, 48, 64, 128, 180, 192, 256, 512, 1024)。
   - 浏览器端纯前端批量渲染与打包导出 ZIP。
   - 独立标准矢量 SVG 及 Windows 多画格 ICO 单击即下。
5. **06 Evolution (演化脉络)**: 从原始多重构造圆、兰花曲线探索、月魄弧度标定到最终定稿的 4 阶段演化复盘。
6. **07 Guideline (设计规范)**: 构造线、安全留白、最小尺寸阈值 (16px / 5mm)、色彩禁忌规范与 Markdown 文档导出。
7. **08 Applications (应用场景)**: 浏览器标签、官网 Header、iOS/macOS 图标、开发文档侧边栏、移动端开屏、社交头像、烫黑商务名片全景模拟。
8. **09 Color & Typography (色彩字型)**: 水墨黑 (`#111111`)、纸白 (`#FAF9F6`)、雾灰 (`#E0E0E0`)、深灰 (`#444444`) 色彩令牌与 Inter / Cormorant Garamond 字体标本及使用规范。

---

## 🧩 代码架构与工程目录 (Solution Architecture)

```
Clight.Brand.System/
├── Clight.Brand.System.sln          # .NET 8 / C# 12 Master Solution
├── Directory.Build.props            # 全局编译与可空引用类型规则
├── .gitignore                       # 完整的 .NET / IDE / 构建产物忽略规则
├── .github/
│   └── workflows/
│       └── deploy.yml               # GitHub Actions 自动化构建与 GitHub Pages 部署工作流
├── README.md                        # 项目中英文完整技术与设计文档
├── brand-guideline/                 # 品牌规范 Markdown 手册
│   ├── Logo.md                      # 标识释义与意象
│   ├── Construction.md              # 数学推导与坐标求解
│   ├── Application.md               # 留白、尺寸与对比度
│   ├── Typography.md                # 字体层级搭配
│   └── Colors.md                    # 官方色彩规范
├── assets/                          # 预构建全规格资产库 (SVG / PNG / ICO / Web)
├── src/
│   ├── Clight.Logo.Core/            # 核心数学模型、几何计算引擎 (LogoCalculator)
│   ├── Clight.Logo.Renderer/        # SVG 渲染引擎、工程蓝图与网格生成器
│   ├── Clight.Asset.Generator/      # 二进制 ICO 编码器、Web Manifest 与资产目录管理
│   ├── Clight.Brand.Guideline/      # 设计规范提供者服务与 Markdown 解析
│   └── Clight.LogoStudio.Wasm/      # 交互式 Blazor WebAssembly 客户端应用
└── tests/
    └── Clight.Brand.Tests/          # xUnit 自动化单元测试集 (100% 覆盖核心几何与渲染)
```

---

## 🛠️ 本地运行与构建指南 (Local Development)

### 环境要求 (Prerequisites)
- 安装 [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### 1. 克隆与还原依赖
```bash
git clone https://github.com/clight7664/Clight.Brand.System.git
cd Clight.Brand.System
dotnet restore Clight.Brand.System.sln
```

### 2. 运行单元测试
```bash
dotnet test tests/Clight.Brand.Tests/Clight.Brand.Tests.csproj --verbosity normal
```

### 3. 启动交互式设计工作台
```bash
dotnet run --project src/Clight.LogoStudio.Wasm/Clight.LogoStudio.Wasm.csproj
```
在浏览器中访问：`http://localhost:5000` 或 `https://localhost:5001`。

### 4. 生产环境编译发布
```bash
dotnet publish src/Clight.LogoStudio.Wasm/Clight.LogoStudio.Wasm.csproj -c Release -o publish/wasm
```

---

## 🚀 CI/CD 与 GitHub Pages 部署指南 (GitHub Pages CI/CD)

项目已内置完整的 GitHub Actions 自动化工作流 [`.github/workflows/deploy.yml`](.github/workflows/deploy.yml)。

### 自动化流水线流程：
1. **代码拉取与环境配置**：自动载入 Ubuntu 环境并配置 .NET 8.0 SDK。
2. **质量门禁 (Quality Gate)**：全量运行 22 项 xUnit 单元测试，确保几何计算与渲染逻辑 100% 正确。
3. **WebAssembly 编译发布**：执行 `dotnet publish -c Release` 生成静态资产。
4. **SPA 路由与 Pages 适配**：
   - 自动生成 `.nojekyll` 确保 `_framework` 目录正常被静态服务器托管；
   - 注入 `<base href="/Clight.Brand.System/" />` 适配 GitHub 仓库路径；
   - 将 `index.html` 同步生成 `404.html`，实现 Blazor 前端路由在刷新时不出现 404 错误。
5. **部署至 Pages**：调用官方 `actions/deploy-pages@v4` 发布至 GitHub Pages。

### 启用步骤 (Repository Setup)：
1. 在 GitHub 仓库页面点击 **Settings** -> **Pages**；
2. 在 **Build and deployment** -> **Source** 下拉框中选择 **GitHub Actions**；
3. 推送代码至 `main` 分支，GitHub Actions 将自动触发构建并部署上线！

---

## 📦 官方资产交付清单 (Delivered Brand Assets)

| 文件路径 | 格式 | 尺寸 / 规格 | 说明 |
| :--- | :--- | :--- | :--- |
| `assets/svg/clight-logo.svg` | SVG | 纯矢量 | 官方标准透明底黑线矢量图 |
| `assets/svg/clight-logo-black.svg` | SVG | 纯矢量 | 纸白底黑线矢量图 (`#FAF9F6`) |
| `assets/svg/clight-logo-white.svg` | SVG | 纯矢量 | 纯黑底白线矢量图 (`#111111`) |
| `assets/svg/clight-logo-construction.svg`| SVG | 纯矢量 | 黄金比例工程构造蓝图 |
| `assets/svg/clight-logo-grid.svg` | SVG | 纯矢量 | 32px 模块化坐标网格图 |
| `assets/png/clight-logo-{size}.png` | PNG | 16 ~ 1024px | 10 种国际标准分辨率位图 |
| `assets/ico/favicon.ico` | ICO | 16/32/48/64/128/256 | Windows / 浏览器多画格高清图标 |
| `assets/web/apple-touch-icon.png` | PNG | 180×180 px | iOS 主屏幕触控图标 |
| `assets/web/manifest.json` | JSON | PWA 配置 | Progressive Web App 部署配置 |

---

## 📄 授权与版权 (License & Attribution)

Copyright © 2026 Clight Inc. All rights reserved.  
Engineered with precision for the Clight Brand Identity System.
