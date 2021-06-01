// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectWrite
{
    public partial class GlyphRun : IDisposable
    {
        public IDWriteFontFace? FontFace { set; get; }
        public ushort[]? Indices { get; set; }
        public float[]? Advances { get; set; }
        public GlyphOffset[]? Offsets { get; set; }

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
                ((IUnknown)FontFace).AddRef();

            FontSize = @ref.FontEmSize;
            GlyphCount = @ref.GlyphCount;
            GlyphCount = @ref.GlyphCount;
            if (@ref.GlyphIndices != IntPtr.Zero)
            {
                Indices = new ushort[GlyphCount];
                if (GlyphCount > 0)
                    UnsafeUtilities.Read(@ref.GlyphIndices, Indices, GlyphCount);
            }

            if (@ref.GlyphAdvances != IntPtr.Zero)
            {
                Advances = new float[GlyphCount];
                if (GlyphCount > 0)
                    UnsafeUtilities.Read(@ref.GlyphAdvances, Advances, GlyphCount);
            }

            if (@ref.GlyphOffsets != IntPtr.Zero)
            {
                Offsets = new GlyphOffset[GlyphCount];
                if (GlyphCount > 0)
                    UnsafeUtilities.Read(@ref.GlyphOffsets, Offsets, GlyphCount);
            }

            IsSideways = @ref.IsSideways;
            BidiLevel = @ref.BidiLevel;
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.FontFace = FontFace == null ? IntPtr.Zero : FontFace.NativePointer;
            @ref.FontEmSize = FontSize;
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

            // Update GlyphCount only for debug purpose
            GlyphCount = @ref.GlyphCount;

            @ref.IsSideways = this.IsSideways;
            @ref.BidiLevel = this.BidiLevel;
        }
        #endregion Marshal
    }
}
