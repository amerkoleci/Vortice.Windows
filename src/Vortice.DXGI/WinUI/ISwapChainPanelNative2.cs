// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI.WinUI
{
    [Guid("88fd8248-10da-4810-bb4c-010dd27faea9")]
    public partial class ISwapChainPanelNative2 : ISwapChainPanelNative
    {
        public ISwapChainPanelNative2(IntPtr nativePtr) : base(nativePtr)
        {
        }

        public static explicit operator ISwapChainPanelNative2(IntPtr nativePtr)
        {
            return (nativePtr == IntPtr.Zero) ? null : new ISwapChainPanelNative2(nativePtr);
        }

        public unsafe Result SetSwapChainHandle(IntPtr swapChainHandle)
        {
            return LocalInterop.CalliStdCallint(_nativePointer, (void*)swapChainHandle, (*(void***)_nativePointer)[4]);
        }
    }
}
