// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectStorage;

[StructLayout(LayoutKind.Explicit, Pack = 0, CharSet = CharSet.Unicode)]
public partial struct RequestOptions
{
    [FieldOffset(0)]
    internal CompressionFormat _CompressionFormat;

    public CompressionFormat CompressionFormat
    {
        get => _CompressionFormat;
        set => _CompressionFormat = value;
    }

    [FieldOffset(8)]
    internal ulong _Type;

    public RequestSourceType SourceType
    {
        get => (RequestSourceType)(_Type & 1);
        set => _Type = (_Type & ~1ul) | ((ulong)value & 1ul);
    }

    public RequestDestinationType DestinationType
    {
        get => (RequestDestinationType)((_Type & 254ul) >> 1);
        set => _Type = (_Type & ~254ul) | (((ulong)value << 1) & 254ul);
    }
}
