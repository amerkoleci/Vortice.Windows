// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIAdapter
{
    public bool CheckInterfaceSupport<T>() where T : ComObject
    {
        return CheckInterfaceSupport(typeof(T), out _);
    }

    public bool CheckInterfaceSupport<T>(out long userModeVersion) where T : ComObject
    {
        return CheckInterfaceSupport(typeof(T), out userModeVersion);
    }

    public bool CheckInterfaceSupport(Type type, out long userModeDriverVersion)
    {
        return CheckInterfaceSupport(type.GUID, out userModeDriverVersion).Success;
    }
}
