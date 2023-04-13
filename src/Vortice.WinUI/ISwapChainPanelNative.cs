// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;
#if WINDOWS
using WinRT;
using Microsoft.UI.Xaml.Controls;
#endif

namespace Vortice.WinUI;

[Guid("63aad0b8-7c24-40ff-85a8-640d944cc325")]
public unsafe partial class ISwapChainPanelNative : ComObject
{
    public ISwapChainPanelNative(IntPtr nativePtr)
        : base(nativePtr)
    {
    }

    public static explicit operator ISwapChainPanelNative?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new ISwapChainPanelNative(nativePtr);

#if WINDOWS
    public ISwapChainPanelNative(SwapChainPanel owner)
        : base(((IWinRTObject)owner).NativeObject.GetRef())
    {
    }

    public static explicit operator ISwapChainPanelNative(SwapChainPanel owner) => new(owner);
#endif

    public IDXGISwapChain SwapChain
    {
        set => SetSwapChain(value).CheckError();
    }

    public Result SetSwapChain(IDXGISwapChain swapChain)
    {
        IntPtr swapChain_ = MarshallingHelpers.ToCallbackPtr<IDXGISwapChain>(swapChain);
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[3])(NativePointer, (void*)swapChain_);
        GC.KeepAlive(swapChain);
        return result;
    }
}
