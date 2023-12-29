// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS
using WinRT;
using Microsoft.UI.Xaml.Controls;
#endif

namespace Vortice.WinUI;

[Guid("88fd8248-10da-4810-bb4c-010dd27faea9")]
public unsafe partial class ISwapChainPanelNative2 : ISwapChainPanelNative
{
    public ISwapChainPanelNative2(IntPtr nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ISwapChainPanelNative2?(IntPtr nativePtr) => nativePtr == IntPtr.Zero ? null : new ISwapChainPanelNative2(nativePtr);

#if WINDOWS
    public ISwapChainPanelNative2(SwapChainPanel owner)
        : base(((IWinRTObject)owner).NativeObject.GetRef())
    {
    }

    public static explicit operator ISwapChainPanelNative2(SwapChainPanel owner) => new(owner);
#endif

    public IntPtr SwapChainHandle
    {
        set => SetSwapChainHandle(value).CheckError();
    }

    public Result SetSwapChainHandle(IntPtr swapChainHandle)
    {
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[4])(NativePointer, (void*)swapChainHandle);
        return result;
    }
}
