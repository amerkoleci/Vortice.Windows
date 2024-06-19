// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.WinUI;

[Guid("e8e84ac7-b7b8-40f4-b033-f877a756c52b")]
[Vtbl(typeof(IVirtualSurfaceUpdatesCallbackNativeVtbl))]
public interface IVirtualSurfaceUpdatesCallbackNative : IUnknown, ICallbackable, IDisposable
{
    Result UpdatesNeeded();
}

internal static class IVirtualSurfaceUpdatesCallbackNativeVtbl
{
    public static readonly unsafe nint[] Vtbl = [(nint)(delegate* unmanaged[Stdcall]<nint, int>)(&UpdatesNeededImpl_)];

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int UpdatesNeededImpl_(nint thisObject)
    {
        IVirtualSurfaceUpdatesCallbackNative @this = CppObjectShadow.ToCallback<IVirtualSurfaceUpdatesCallbackNative>(thisObject);
        try
        {
            Result result = @this.UpdatesNeeded();
            return (int)result;
        }
        catch (Exception ex)
        {
            (@this as IExceptionCallback)?.RaiseException(ex);
            return Result.GetResultFromException(ex).Code;
        }
    }
}

public abstract class IVirtualSurfaceUpdatesCallbackNativeBase : CallbackBase, IVirtualSurfaceUpdatesCallbackNative
{
    public abstract Result UpdatesNeeded();
}
