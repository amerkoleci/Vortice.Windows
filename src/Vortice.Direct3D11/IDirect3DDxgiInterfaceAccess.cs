// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class IDirect3DDxgiInterfaceAccess
{
    public T GetInterface<T>() where T : ComObject
    {
        GetInterface(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result GetInterface<T>(out T? @interface) where T: ComObject
    {
        Result result = GetInterface(typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            @interface = default;
            return result;
        }

        @interface = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
