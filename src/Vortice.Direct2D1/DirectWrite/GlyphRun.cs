// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public partial class GlyphRun : IDisposable
{
    /// <summary>
    /// The physical font face object to draw with.
    /// </summary>
    public IDWriteFontFace? FontFace { set; get; }

    /// <summary>
    /// The logical size of the font in DIPs (equals 1/96 inch), not points.
    /// </summary>
    public float FontEmSize { get; set; }

    /// <summary>
    /// An array of indices to render for the glyph run.
    /// </summary>
    public ushort[]? Indices { get; set; }

    /// <summary>
    /// An array of glyph advance widths for the glyph run.
    /// </summary>
    public float[]? Advances { get; set; }

    /// <summary>
    /// An array containing glyph offsets for the glyph run.
    /// </summary>
    public GlyphOffset[]? Offsets { get; set; }

    /// <summary>
    /// If true, specifies that glyphs are rotated 90 degrees to the left and vertical metrics are used. Vertical writing is achieved by specifying isSideways = true and rotating the entire run 90 degrees to the right via a rotate transform.
    /// </summary>
    public bool IsSideways { get; set; }

    /// <summary>
    /// The implicit resolved bidi level of the run. Odd levels indicate right-to-left languages like Hebrew and Arabic, while even levels indicate left-to-right languages like English and Japanese (when written horizontally). For right-to-left languages, the text origin is on the right, and text should be drawn to the left.
    /// </summary>
    public int BidiLevel { get; set; }

    public void Dispose()
    {
        if (FontFace != null)
        {
            FontFace.Dispose();
            FontFace = null;
        }
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal partial struct __Native
    {
        public IntPtr FontFace;
        public float FontEmSize;
        public int GlyphCount;
        public IntPtr GlyphIndices;
        public IntPtr GlyphAdvances;
        public IntPtr GlyphOffsets;
        public RawBool IsSideways;
        public int BidiLevel;

        internal unsafe void __MarshalFree()
        {
            if (GlyphIndices != IntPtr.Zero)
                Marshal.FreeHGlobal(GlyphIndices);
            if (GlyphAdvances != IntPtr.Zero)
                Marshal.FreeHGlobal(GlyphAdvances);
            if (GlyphOffsets != IntPtr.Zero)
                Marshal.FreeHGlobal(GlyphOffsets);
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        FontFace = (@ref.FontFace == IntPtr.Zero) ? null : new IDWriteFontFace(@ref.FontFace);
        if (FontFace != null)
            FontFace.AddRef();

        FontEmSize = @ref.FontEmSize;
        if (@ref.GlyphIndices != IntPtr.Zero)
        {
            Indices = new ushort[@ref.GlyphCount];
            if (@ref.GlyphCount > 0)
            {
                UnsafeUtilities.Read(@ref.GlyphIndices, Indices, @ref.GlyphCount);
            }
        }

        if (@ref.GlyphAdvances != IntPtr.Zero)
        {
            Advances = new float[@ref.GlyphCount];
            if (@ref.GlyphCount > 0)
            {
                UnsafeUtilities.Read(@ref.GlyphAdvances, Advances, @ref.GlyphCount);
            }
        }

        if (@ref.GlyphOffsets != IntPtr.Zero)
        {
            Offsets = new GlyphOffset[@ref.GlyphCount];
            if (@ref.GlyphCount > 0)
            {
                UnsafeUtilities.Read(@ref.GlyphOffsets, Offsets, @ref.GlyphCount);
            }
        }

        IsSideways = @ref.IsSideways;
        BidiLevel = @ref.BidiLevel;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.FontFace = FontFace == null ? IntPtr.Zero : FontFace.NativePointer;
        @ref.FontEmSize = FontEmSize;
        @ref.GlyphCount = -1;
        @ref.GlyphIndices = IntPtr.Zero;
        @ref.GlyphAdvances = IntPtr.Zero;
        @ref.GlyphOffsets = IntPtr.Zero;

        if (Indices != null)
        {
            @ref.GlyphCount = Indices.Length;
            @ref.GlyphIndices = Marshal.AllocHGlobal(Indices.Length * sizeof(ushort));
            if (Indices.Length > 0)
            {
                UnsafeUtilities.Write(@ref.GlyphIndices, Indices, 0, Indices.Length);
            }
        }

        if (Advances != null)
        {
            if (@ref.GlyphCount >= 0 && @ref.GlyphCount != Advances.Length)
            {
                throw new InvalidOperationException(
                    $"Invalid length for array Advances [{Advances.Length}] and Indices [{@ref.GlyphCount}]. Indices, Advances and Offsets array must have same size - or may be null"
                    );
            }

            @ref.GlyphCount = Advances.Length;
            @ref.GlyphAdvances = Marshal.AllocHGlobal(Advances.Length * sizeof(float));
            if (Advances.Length > 0)
            {
                UnsafeUtilities.Write(@ref.GlyphAdvances, Advances, 0, Advances.Length);
            }
        }

        if (Offsets != null)
        {
            if (@ref.GlyphCount >= 0 && @ref.GlyphCount != Offsets.Length)
            {
                throw new InvalidOperationException($"Invalid length for array Offsets [{Offsets.Length}]. Indices, Advances and Offsets array must have same size (Current is [{@ref.GlyphCount}]- or may be null");
            }

            @ref.GlyphCount = this.Offsets.Length;
            @ref.GlyphOffsets = Marshal.AllocHGlobal(this.Offsets.Length * sizeof(GlyphOffset));
            if (this.Offsets.Length > 0)
            {
                UnsafeUtilities.Write(@ref.GlyphOffsets, Offsets, 0, this.Offsets.Length);
            }
        }

        if (@ref.GlyphCount < 0)
            @ref.GlyphCount = 0;

        @ref.IsSideways = IsSideways;
        @ref.BidiLevel = BidiLevel;
    }
    #endregion Marshal
}
