// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.WinUI;

[Guid("24d43d84-4246-4aa7-9774-8604cb73d90d")]
public partial class ISwapChainBackgroundPanelNative : ComObject
{
    public ISwapChainBackgroundPanelNative(IntPtr nativePtr)
        : base(nativePtr)
    {
    }

    public static explicit operator ISwapChainBackgroundPanelNative?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new ISwapChainBackgroundPanelNative(nativePtr);

    public IDXGISwapChain SwapChain
    {
        set => SetSwapChain(value).CheckError();
    }

    public unsafe Result SetSwapChain(IDXGISwapChain swapChain)
    {
        IntPtr swapChain_ = MarshallingHelpers.ToCallbackPtr<IDXGISwapChain>(swapChain);
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[3])(NativePointer, (void*)swapChain_);
        GC.KeepAlive(swapChain);
        return result;
    }
}
