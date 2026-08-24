using Clight.Logo.Core.Models;

namespace Clight.Brand.Guideline.Models;

/// <summary>
/// Complete design system color token specification.
/// </summary>
public record ColorSpecification
{
    public BrandColor InkBlack => BrandColor.InkBlack;
    public BrandColor PaperWhite => BrandColor.PaperWhite;
    public BrandColor MistGray => BrandColor.MistGray;
    public BrandColor DeepGray => BrandColor.DeepGray;
    public BrandColor PureWhite => BrandColor.PureWhite;
}
