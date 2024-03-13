// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using Vortice.Multimedia;

namespace Vortice.XAPO;

public partial struct LockParameters
{
    /// <summary>
    /// Gets or sets the waveformat.
    /// </summary>
    /// <value>The format.</value>
    public WaveFormat Format { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct __Native
    {
        public IntPtr pFormat;
        public int MaxFrameCount;

        internal void __MarshalFree()
        {
            if (pFormat != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(pFormat);
            }
        }
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.pFormat = IntPtr.Zero;
        if (Format != null)
        {
            int sizeOfFormat = Marshal.SizeOf(Format);
            @ref.pFormat = Marshal.AllocCoTaskMem(sizeOfFormat);
            Marshal.StructureToPtr(Format, @ref.pFormat, false);
        }
        @ref.MaxFrameCount = this.MaxFrameCount;
    }
    #endregion
}
