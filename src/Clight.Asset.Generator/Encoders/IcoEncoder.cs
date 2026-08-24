using System.Buffers.Binary;

namespace Clight.Asset.Generator.Encoders;

/// <summary>
/// High performance, pure managed binary encoder for Windows Icon (.ico) files,
/// supporting multi-resolution PNG-compressed icon entries (16, 32, 48, 64, 128, 256).
/// </summary>
public static class IcoEncoder
{
    /// <summary>
    /// Encodes a dictionary of (dimension, PNG byte data) into a valid multi-frame ICO file stream.
    /// </summary>
    /// <param name="pngFrames">Key is width/height in pixels (e.g. 16, 32, 48, 64, 128, 256), Value is raw PNG byte array.</param>
    /// <returns>Complete ICO binary byte array.</returns>
    public static byte[] EncodeIco(IReadOnlyDictionary<int, byte[]> pngFrames)
    {
        ArgumentNullException.ThrowIfNull(pngFrames);
        if (pngFrames.Count == 0)
        {
            throw new ArgumentException("At least one frame is required to encode an ICO file.", nameof(pngFrames));
        }

        var sortedFrames = pngFrames.OrderBy(kvp => kvp.Key).ToList();
        int frameCount = sortedFrames.Count;

        // ICO Header = 6 bytes
        // Each Directory Entry = 16 bytes
        int headerSize = 6;
        int dirEntriesSize = frameCount * 16;
        int currentOffset = headerSize + dirEntriesSize;

        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        // 1. Write ICONDIR Header (6 bytes)
        writer.Write((ushort)0); // Reserved (must be 0)
        writer.Write((ushort)1); // Resource type (1 for ICO, 2 for CUR)
        writer.Write((ushort)frameCount); // Number of images

        // 2. Write ICONDIRENTRY list (16 bytes each)
        foreach (var frame in sortedFrames)
        {
            int size = frame.Key;
            byte[] pngData = frame.Value;

            byte bWidth = size >= 256 ? (byte)0 : (byte)size;
            byte bHeight = size >= 256 ? (byte)0 : (byte)size;

            writer.Write(bWidth); // Width (0 means 256)
            writer.Write(bHeight); // Height (0 means 256)
            writer.Write((byte)0); // Color count (0 for 256+ colors / PNG)
            writer.Write((byte)0); // Reserved
            writer.Write((ushort)1); // Color planes
            writer.Write((ushort)32); // Bits per pixel (32-bit RGBA)
            writer.Write((uint)pngData.Length); // Image data size in bytes
            writer.Write((uint)currentOffset); // Offset from beginning of file

            currentOffset += pngData.Length;
        }

        // 3. Write raw PNG image data blocks
        foreach (var frame in sortedFrames)
        {
            writer.Write(frame.Value);
        }

        writer.Flush();
        return ms.ToArray();
    }
}
