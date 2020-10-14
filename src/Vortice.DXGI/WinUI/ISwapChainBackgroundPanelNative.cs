// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI.WinUI
{
    [Guid("24d43d84-4246-4aa7-9774-8604cb73d90d")]
    public partial class ISwapChainBackgroundPanelNative : ComObject
    {
        public ISwapChainBackgroundPanelNative(IntPtr nativePtr) : base(nativePtr)
        {
        }

        public static explicit operator ISwapChainBackgroundPanelNative(IntPtr nativePtr) => (nativePtr == IntPtr.Zero) ? null : new ISwapChainBackgroundPanelNative(nativePtr);

        public unsafe Result SetSwapChain(IDXGISwapChain swapChain)
        {
            Result result = LocalInterop.CalliStdCallint(_nativePointer, (void*)swapChain.NativePointer.ToPointer(), (*(void***)_nativePointer)[3]);
            GC.KeepAlive(swapChain);
            return result;
        }
    }
}
