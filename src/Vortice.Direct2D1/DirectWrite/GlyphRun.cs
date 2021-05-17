// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DirectWrite
{
    public partial class GlyphRun : IDisposable
    {
        public IDWriteFontFace? FontFace { set; get; }
        public ushort[]? GlyphIndices { get; set; }
        public float[]? GlyphAdvances { get; set; }
        public GlyphOffset[]? GlyphOffsets { get; set; }

        public void Dispose()
        {
            FontFace?.Dispose();
            FontFace = null;
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
            FontFace?.AddRef();

            FontEmSize = @ref.FontEmSize;
            GlyphCount = @ref.GlyphCount;
            GlyphIndices = null;
            GlyphAdvances = null;
            GlyphOffsets = null;
            IsSideways = @ref.IsSideways;
            BidiLevel = @ref.BidiLevel;

            if (@ref.GlyphIndices != IntPtr.Zero)
            {
                GlyphIndices = new ushort[@ref.GlyphCount];
                if (@ref.GlyphCount > 0)
                    fixed (void* indicesPtr = &GlyphIndices[0])
                    {
                        Unsafe.CopyBlock(indicesPtr, @ref.GlyphIndices.ToPointer(),
                            (uint)(sizeof(ushort) * @ref.GlyphCount));
                    }
            }

            if (@ref.GlyphAdvances != IntPtr.Zero)
            {
                GlyphAdvances = new float[@ref.GlyphCount];
                if (@ref.GlyphCount > 0)
                    fixed (void* advancesPtr = &GlyphAdvances[0])
                    {
                        Unsafe.CopyBlock(
                            advancesPtr,
                            @ref.GlyphAdvances.ToPointer(),
                            (uint)(sizeof(float) * @ref.GlyphCount));
                    }
            }

            if (@ref.GlyphOffsets != IntPtr.Zero)
            {
                GlyphOffsets = new GlyphOffset[@ref.GlyphCount];
                if (@ref.GlyphCount > 0)
                    fixed (void* offsetsPtr = &GlyphOffsets[0])
                    {
                        Unsafe.CopyBlock(offsetsPtr, @ref.GlyphOffsets.ToPointer(), (uint)(sizeof(GlyphOffset) * @ref.GlyphCount));
                    }
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.FontFace = FontFace == null ? IntPtr.Zero : FontFace.NativePointer;
            @ref.FontEmSize = FontEmSize;
            @ref.GlyphCount = GlyphCount;
            @ref.GlyphIndices = IntPtr.Zero;
            @ref.GlyphAdvances = IntPtr.Zero;
            @ref.GlyphOffsets = IntPtr.Zero;
            @ref.IsSideways = IsSideways;
            @ref.BidiLevel = BidiLevel;

            if (GlyphIndices != null)
            {
                @ref.GlyphIndices = Marshal.AllocHGlobal(GlyphIndices.Length * sizeof(ushort));
                if (GlyphCount > 0)
                {
                    fixed (void* glyphIndicesPtr = &GlyphIndices[0])
                    {
                        Unsafe.CopyBlock(@ref.GlyphIndices.ToPointer(),
                            glyphIndicesPtr,
                            (uint)(sizeof(ushort) * GlyphCount));
                    }
                }

            }

            if (GlyphAdvances != null)
            {
                @ref.GlyphAdvances = Marshal.AllocHGlobal(GlyphAdvances.Length * sizeof(float));
                if (GlyphCount > 0)
                {
                    fixed (void* glyphAdvancesPtr = &GlyphAdvances[0])
                    {
                        Unsafe.CopyBlock(@ref.GlyphAdvances.ToPointer(),
                            glyphAdvancesPtr,
                            (uint)(sizeof(float) * GlyphCount));
                    }
                }
            }

            if (GlyphOffsets != null)
            {
                @ref.GlyphOffsets = Marshal.AllocHGlobal(GlyphOffsets.Length * sizeof(GlyphOffset));
                if (GlyphCount > 0)
                {
                    fixed (void* offsetsPtr = &GlyphOffsets[0])
                    {
                        Unsafe.CopyBlock(@ref.GlyphOffsets.ToPointer(),
                            offsetsPtr,
                            (uint)(sizeof(GlyphOffset) * GlyphCount));
                    }
                }
            }
        }
        #endregion Marshal
    }
}
