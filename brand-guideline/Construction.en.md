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
