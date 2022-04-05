// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public partial class ColorGlyphRun 
{
    /// <summary>
    /// Glyph run to draw for this layer.
    /// </summary>
    public GlyphRun GlyphRun;

    /// <summary>
    /// Pointer to the glyph run description for this layer.
    /// This may be NULL.
    /// For example, when the original glyph run is split into multiple layers, one layer might have a description and the others have none.
    /// </summary>
    public GlyphRunDescription? GlyphRunDescription;

    /// <summary>
    /// X coordinate of the baseline origin for the layer.
    /// </summary>
    public float BaselineOriginX;

    /// <summary>
    /// Y coordinate of the baseline origin for the layer.
    /// </summary>
    public float BaselineOriginY;

    /// <summary>
    /// Color value of the run; if all members are zero, the run should be drawn using the current brush.
    /// </summary>
    public Color4 RunColor;

    /// <summary>
    /// Zero-based index into the font’s color palette; if this is 0xFFFF, the run should be drawn using the current brush.
    /// </summary>
    public ushort PaletteIndex;

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    internal unsafe struct __Native
    {
        public GlyphRun.__Native GlyphRun;
        public GlyphRunDescription.__Native* GlyphRunDescription;
        public float BaselineOriginX;
        public float BaselineOriginY;
        public Color4 RunColor;
        public ushort PaletteIndex;

        internal unsafe void __MarshalFree()
        {
            GlyphRun.__MarshalFree();
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        GlyphRun = new GlyphRun();
        GlyphRun.__MarshalFrom(ref @ref.GlyphRun);

        if (@ref.GlyphRunDescription == null)
        {
            GlyphRunDescription = null;
        }
        else
        {
            GlyphRunDescription = new GlyphRunDescription();
            GlyphRunDescription.__MarshalFrom(ref *@ref.GlyphRunDescription);
        }

        BaselineOriginX = @ref.BaselineOriginX;
        BaselineOriginY = @ref.BaselineOriginY;
        RunColor = @ref.RunColor;
        PaletteIndex = @ref.PaletteIndex;
    }
    #endregion Marshal
}
