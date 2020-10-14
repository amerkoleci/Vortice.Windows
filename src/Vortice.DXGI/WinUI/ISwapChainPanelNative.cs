// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI.WinUI
{
    [Guid("63aad0b8-7c24-40ff-85a8-640d944cc325")]
    public partial class ISwapChainPanelNative : ComObject
    {
        public ISwapChainPanelNative(IntPtr nativePtr) : base(nativePtr)
        {
        }

        public static explicit operator ISwapChainPanelNative(IntPtr nativePtr)
        {
            return (nativePtr == IntPtr.Zero) ? null : new ISwapChainPanelNative(nativePtr);
        }

        public unsafe Result SetSwapChain(IDXGISwapChain swapChain)
        {
            IntPtr swapChainPtr = swapChain.NativePointer;
            Result result = LocalInterop.CalliStdCallint(_nativePointer, (void*)swapChainPtr, (*(void***)_nativePointer)[3]);
            GC.KeepAlive(swapChain);
            return result;
        }
    }
}
