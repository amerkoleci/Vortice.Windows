// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectStorage;

[StructLayout(LayoutKind.Explicit, Pack = 0, CharSet = CharSet.Unicode)]
public partial struct RequestOptions
{
    [FieldOffset(0)]
    internal byte _CompressionFormat;
    public CompressionFormat CompressionFormat
    {
        get => (CompressionFormat)((_CompressionFormat >> 0) & 255);
        set => _CompressionFormat = (byte)((_CompressionFormat & ~(255 << 0)) | (((byte)value & 255) << 0));
    }

    [FieldOffset(0)]
    internal long _SourceType;

    public RequestSourceType SourceType
    {
        get => (RequestSourceType)((_SourceType >> 8) & 1);
        set => _SourceType = (_SourceType & ~(1 << 8)) | (((long)value & 1) << 8);
    }

    [FieldOffset(0)]
    internal long _DestinationType;
    public RequestDestinationType DestinationType
    {
        get => (RequestDestinationType)((_DestinationType >> 9) & 127);
        set => _DestinationType = (long)((_DestinationType & ~(127 << 9)) | (((long)value & 127) << 9));
    }

    [FieldOffset(0)]
    internal ulong _Reserved;
    internal ulong Reserved
    {
        get => (_Reserved >> 16) & 65535;
        set => _Reserved = (ulong)((_Reserved & ~(65535 << 16)) | ((value & 65535) << 16));
    }
}
