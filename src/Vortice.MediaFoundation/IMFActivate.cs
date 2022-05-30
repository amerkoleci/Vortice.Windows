// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace Vortice.MediaFoundation;

public unsafe partial class IMFActivate
{
    public T ActivateObject<T>() where T : ComObject
    {
        ActivateObject(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result ActivateObject<T>(out T? @object) where T : ComObject
    {
        Result result = ActivateObject(typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            @object = null;
            return result;
        }

        @object = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
