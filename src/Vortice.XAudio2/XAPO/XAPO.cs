// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XAPO;

public static unsafe partial class XAPO
{
    public static IUnknown CreateFX(Guid clsId)
    {
        CreateFX(clsId, out IUnknown effect, (void*)null, 0).CheckError();
        return effect;
    }

    public static Result CreateFX(Guid clsId, out IUnknown? effect)
    {
        return CreateFX(clsId, out effect, (void*)null, 0);
    }

    public static IUnknown CreateFX(Guid clsId, IntPtr initData, uint initDataByteSize)
    {
        CreateFX(clsId, out IUnknown effect, initData.ToPointer(), initDataByteSize).CheckError();
        return effect;
    }

    public static Result CreateFX(Guid clsId, IntPtr initData, uint initDataByteSize, out IUnknown? effect)
    {
        return CreateFX(clsId, out effect, initData.ToPointer(), initDataByteSize);
    }

    public static IUnknown CreateFX<T>(Guid clsId, T initData) where T : unmanaged
    {
        CreateFX(clsId, out IUnknown effect, &initData, (uint)sizeof(T)).CheckError();
        return effect;
    }

    public static Result CreateFX<T>(Guid clsId, T initData, out IUnknown? effect) where T : unmanaged
    {
        return CreateFX(clsId, out effect, &initData, (uint)sizeof(T));
    }
}
