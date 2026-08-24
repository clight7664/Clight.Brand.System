# Clight Brand System — Geometric Construction Blueprint

## 1. Mathematical Foundation
The Clight Logo is defined by two intersecting circular arcs in a master $512 \times 512$ coordinate system:

- **Master ViewBox**: `0 0 512 512`
- **Outer Arc**:
  - Center: $(C_{xo}, C_{yo}) = (256.0, 256.0)$
  - Radius: $R_{outer} = 220.0$
  - Tip Opening Angle: $\alpha = 46.0^\circ$
  - Top Tip: $(408.825, 97.745)$
  - Bottom Tip: $(408.825, 414.255)$
  - Left Crest: $(36.0, 256.0)$
- **Inner Arc**:
  - Center: $(C_{xi}, C_{yi}) = (271.518, 256.0)$
  - Radius: $R_{inner} = 209.518$
  - Inner Crest: $(62.0, 256.0)$
  - Crest Thickness: $W = 26.0\text{ px}$

## 2. Construction Diagram & Golden Spiral Radii
```
                 (408.8, 97.7) Top Tip
                      * .
                   .     \
                 .         \
               .             \ (R_out = 220.0)
     (36,256) |               | Center (256, 256)
     Outer    |   (62,256)    |
     Crest    |   Inner       |
               .  Crest      /
                 .         /
                   .     /
                      * ' (408.8, 414.3) Bottom Tip
```

## 3. Golden Ratio Harmonic Ratios
- **Outer / Inner Ratio**: $R_{outer} / R_{inner} = 220.0 / 209.518 \approx 1.050$
- **Golden Guides**: Concentric circles at $R_1 = R_o / \phi \approx 135.97$, $R_2 = R_1 / \phi \approx 84.03$.
- **Clear Space Margin**: $1X = W \times \phi \approx 42\text{ px}$.

## 4. Master SVG Vector Definition
```xml
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" width="512" height="512">
  <path d="M 408.825 97.745 A 220.000 220.000 0 1 0 408.825 414.255 A 209.518 209.518 0 1 1 408.825 97.745 Z" fill="#111111" />
</svg>
```
