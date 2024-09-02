// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace Vortice.MediaFoundation;

public unsafe partial class IMFGetService
{
    public Result GetService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid guidService, out T? service)
        where T : ComObject
    {
        Result result = GetService(guidService,typeof(T).GUID, out IntPtr devicePtr);
        if (result.Failure)
        {
            service = default;
            return result;
        }

        service = MarshallingHelpers.FromPointer<T>(devicePtr);
        return result;
    }

    public T GetService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid guidService)
        where T : ComObject
    {
        GetService(guidService, typeof(T).GUID, out IntPtr devicePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(devicePtr)!;
    }
}
