using Clight.Asset.Generator.Encoders;
using Xunit;

namespace Clight.Brand.Tests.Generator;

public class IcoEncoderTests
{
    [Fact]
    public void EncodeIco_ValidFrames_GeneratesValidIcoBinaryHeader()
    {
        // Generate mock PNG byte streams for 16, 32, 48
        byte[] mockPng16 = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x01];
        byte[] mockPng32 = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x02, 0x03];
        byte[] mockPng48 = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x04, 0x05, 0x06];

        var dict = new Dictionary<int, byte[]>
        {
            [16] = mockPng16,
            [32] = mockPng32,
            [48] = mockPng48
        };

        byte[] icoBytes = IcoEncoder.EncodeIco(dict);

        Assert.NotNull(icoBytes);
        Assert.True(icoBytes.Length > 6 + (3 * 16));

        // Verify ICONDIR Header
        Assert.Equal(0, icoBytes[0]); // Reserved
        Assert.Equal(0, icoBytes[1]);
        Assert.Equal(1, icoBytes[2]); // Type = 1 (ICO)
        Assert.Equal(0, icoBytes[3]);
        Assert.Equal(3, icoBytes[4]); // Image Count = 3
        Assert.Equal(0, icoBytes[5]);

        // Verify Entry 1 (16x16)
        Assert.Equal(16, icoBytes[6]); // Width
        Assert.Equal(16, icoBytes[7]); // Height
    }
}
