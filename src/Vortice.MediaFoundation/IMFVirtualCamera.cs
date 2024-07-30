// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Mathematics;
using Vortice.Win32;

namespace Vortice.MediaFoundation;

public unsafe partial class IMFVirtualCamera
{
    public Result AddProperty(DevPropertyKey key, DevPropertyType type, byte[] data)
    {
        fixed (byte* pbData = data)
            return AddProperty(ref key, (uint)type, pbData, (uint)data.Length);
    }

    public Result AddProperty(DevPropertyKey key, DevPropertyType type, Span<byte> data)
    {
        fixed (byte* pbData = data)
            return AddProperty(ref key, (uint)type, pbData, (uint)data.Length);
    }

    public Result AddProperty<T>(DevPropertyKey key, DevPropertyType type, Span<T> data)
        where T : unmanaged
    {
        fixed (T* pbData = data)
            return AddProperty(ref key, (uint)type, pbData, (uint)(sizeof(T) * data.Length));
    }

    public Result AddRegistryEntry(string entryName, string subkeyPath, int regType, byte[] data)
    {
        fixed (byte* pbData = data)
            return AddRegistryEntry(entryName, subkeyPath, regType, pbData, (uint)data.Length);
    }

    public Result AddRegistryEntry(string entryName, string subkeyPath, int regType, Span<byte> data)
    {
        fixed (byte* pbData = data)
            return AddRegistryEntry(entryName, subkeyPath, regType, pbData, (uint)data.Length);
    }

    public Result AddRegistryEntry<T>(string entryName, string subkeyPath, int regType, Span<T> data)
        where T : unmanaged
    {
        fixed (T* pbData = data)
            return AddRegistryEntry(entryName, subkeyPath, regType, pbData, (uint)(sizeof(T) * data.Length));
    }
}
