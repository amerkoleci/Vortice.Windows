// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime.Win32;

namespace Vortice.DirectWrite
{
    public partial class GlyphRun : IDisposable
    {
        public IDWriteFontFace FontFace { set; get; }
        public short[] GlyphIndices { get; set; }
        public float[] GlyphAdvances { get; set; }
        public GlyphOffset[] GlyphOffsets { get; set; }

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
                GlyphIndices = new short[@ref.GlyphCount];
                if (@ref.GlyphCount > 0)
                    Unsafe.CopyBlock(
                        Unsafe.AsPointer(ref GlyphIndices[0]),
                        @ref.GlyphIndices.ToPointer(),
                        (uint)(sizeof(short) * @ref.GlyphCount));
            }

            if (@ref.GlyphAdvances != IntPtr.Zero)
            {
                GlyphAdvances = new float[@ref.GlyphCount];
                if (@ref.GlyphCount > 0)
                    Unsafe.CopyBlock(
                        Unsafe.AsPointer(ref GlyphAdvances[0]),
                        @ref.GlyphAdvances.ToPointer(),
                        (uint)(sizeof(float) * @ref.GlyphCount));
            }

            if (@ref.GlyphOffsets != IntPtr.Zero)
            {
                GlyphOffsets = new GlyphOffset[@ref.GlyphCount];
                if (@ref.GlyphCount > 0)
                    Unsafe.CopyBlock(
                        Unsafe.AsPointer(ref GlyphOffsets[0]),
                        @ref.GlyphOffsets.ToPointer(),
                        (uint)(sizeof(GlyphOffset) * @ref.GlyphCount));
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
                @ref.GlyphIndices = Marshal.AllocHGlobal(GlyphIndices.Length * sizeof(short));
                if (GlyphCount > 0)
                {
                    Unsafe.CopyBlock(@ref.GlyphIndices.ToPointer(),
                        Unsafe.AsPointer(ref GlyphIndices[0]),
                        (uint)(sizeof(short) * GlyphCount));
                }

            }

            if (GlyphAdvances != null)
            {
                @ref.GlyphAdvances = Marshal.AllocHGlobal(GlyphAdvances.Length * sizeof(float));
                if (GlyphCount > 0)
                {
                    Unsafe.CopyBlock(@ref.GlyphAdvances.ToPointer(),
                        Unsafe.AsPointer(ref GlyphAdvances[0]),
                        (uint)(sizeof(float) * GlyphCount));
                }
            }

            if (GlyphOffsets != null)
            {
                @ref.GlyphOffsets = Marshal.AllocHGlobal(GlyphOffsets.Length * sizeof(GlyphOffset));
                if (GlyphCount > 0)
                {
                    Unsafe.CopyBlock(@ref.GlyphOffsets.ToPointer(), 
                        Unsafe.AsPointer(ref GlyphOffsets[0]),
                        (uint)(sizeof(GlyphOffset) * GlyphCount));
                }
            }
        }
        #endregion Marshal
    }
}
