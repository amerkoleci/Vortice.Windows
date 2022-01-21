// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Win32;

[StructLayout(LayoutKind.Sequential)]
public struct BitmapInfoHeader
{
    public int SizeInBytes;
    public int Width;
    public int Height;
    public short PlaneCount;
    public short BitCount;
    public int Compression;
    public int SizeImage;
    public int XPixelsPerMeter;
    public int YPixelsPerMeter;
    public int ColorUsedCount;
    public int ColorImportantCount;
}
