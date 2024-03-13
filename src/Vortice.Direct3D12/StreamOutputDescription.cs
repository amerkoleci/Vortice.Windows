// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using static Vortice.UnsafeUtilities;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a streaming output buffer.
/// </summary>
public partial class StreamOutputDescription
{
    public StreamOutputDescription() { }

    public StreamOutputDescription(params StreamOutputElement[] elements)
    {
        Elements = elements;
    }

    /// <summary>	
    /// An array of <see cref="StreamOutputElement"/>.
    /// </summary>	
    public StreamOutputElement[]? Elements { get; set; }

    /// <summary>
    /// An array of buffer strides; each stride is the size of an element for that buffer.
    /// </summary>
    public uint[]? Strides { get; set; }

    /// <summary>
    /// The index number of the stream to be sent to the rasterizer stage.
    /// </summary>
    public uint RasterizedStream { get; set; }

    /// <summary>
    /// Implicitely converts to an <see cref="StreamOutputDescription"/> from an array of <see cref="StreamOutputElement"/>
    /// </summary>
    /// <param name="elements">Array of <see cref="StreamOutputElement"/>.</param>
    public static implicit operator StreamOutputDescription(StreamOutputElement[] elements)
    {
        return new StreamOutputDescription(elements);
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public StreamOutputElement.__Native* pSODeclaration;
        public int NumEntries;
        public uint* pBufferStrides;
        public int NumStrides;
        public uint RasterizedStream;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (@ref.pSODeclaration != null)
        {
            for (int i = 0; i < @ref.NumEntries; i++)
            {
                Elements![i].__MarshalFree(ref @ref.pSODeclaration[i]);
            }

            Free(@ref.pSODeclaration);
        }

        if (@ref.pBufferStrides != null)
        {
            Free(@ref.pBufferStrides);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.NumEntries = Elements?.Length ?? 0;
        if (@ref.NumEntries > 0)
        {
            var nativeElements = Alloc<StreamOutputElement.__Native>(@ref.NumEntries);
            for (int i = 0; i < @ref.NumEntries; i++)
            {
                Elements![i].__MarshalTo(ref nativeElements[i]);
            }

            @ref.pSODeclaration = nativeElements;
        }

        @ref.NumStrides = 0;
        @ref.pBufferStrides = null;
        if (Strides != null && Strides.Length > 0)
        {
            @ref.NumStrides = Strides.Length;
            @ref.pBufferStrides = AllocWithData(Strides);
        }

        @ref.RasterizedStream = RasterizedStream;
    }
    #endregion
}
