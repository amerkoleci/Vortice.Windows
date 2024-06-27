// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12VersionedRootSignatureDeserializer
{
    public VersionedRootSignatureDescription GetRootSignatureDescAtVersion(RootSignatureVersion convertToVersion)
    {
        IntPtr ptr = GetRootSignatureDescAtVersion_(convertToVersion);

        // Marshal the result.
        var result = new VersionedRootSignatureDescription();
        result.__MarshalFrom(ref Unsafe.AsRef<VersionedRootSignatureDescription.__Native>(ptr.ToPointer()));
        return result;
    }
}
