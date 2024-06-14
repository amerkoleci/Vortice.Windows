// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.


namespace Vortice.WinUI;

[Guid("88fd8248-10da-4810-bb4c-010dd27faea9")]
public unsafe partial class ISwapChainPanelNative2 : ISwapChainPanelNative
{
    public ISwapChainPanelNative2(nint nativePtr) : base(nativePtr)
    {
    }

    public static explicit operator ISwapChainPanelNative2?(nint nativePtr) => nativePtr == 0 ? default : new ISwapChainPanelNative2(nativePtr);

#if WINDOWS
    public ISwapChainPanelNative2(Microsoft.UI.Xaml.Controls.SwapChainPanel panel)
        : base(WinUIHelpers.GetNativeObject(typeof(ISwapChainPanelNative2).GUID, panel))
    {
    }

    public static explicit operator ISwapChainPanelNative2(Microsoft.UI.Xaml.Controls.SwapChainPanel panel) => new(panel);
#endif

    public IntPtr SwapChainHandle
    {
        set => SetSwapChainHandle(value).CheckError();
    }

    public Result SetSwapChainHandle(nint swapChainHandle)
    {
        Result result = ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)this[4])(NativePointer, (void*)swapChainHandle);
        return result;
    }
}
