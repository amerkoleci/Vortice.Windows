// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.WinUI;

[Guid("63aad0b8-7c24-40ff-85a8-640d944cc325")]
public unsafe partial class ISwapChainPanelNative : ComObject
{
    public ISwapChainPanelNative(nint nativePtr)
        : base(nativePtr)
    {
    }

    public static explicit operator ISwapChainPanelNative?(nint nativePtr) => nativePtr == 0 ? default : new ISwapChainPanelNative(nativePtr);

#if WINDOWS
    public ISwapChainPanelNative(Microsoft.UI.Xaml.Controls.SwapChainPanel panel)
        : base(WinUIHelpers.GetNativeObject(typeof(ISwapChainPanelNative).GUID, panel))
    {
    }

    public static explicit operator ISwapChainPanelNative(Microsoft.UI.Xaml.Controls.SwapChainPanel panel) => new(panel);
#endif

    public Result SetSwapChain(IDXGISwapChain? swapChain)
    {
        void* swapChainHandle = swapChain != null ? swapChain.NativePointer.ToPointer() : default;
        Result result = ((delegate* unmanaged<IntPtr, void*, int>)this[3])(NativePointer, swapChainHandle);
        GC.KeepAlive(swapChain);
        return result;
    }
}
