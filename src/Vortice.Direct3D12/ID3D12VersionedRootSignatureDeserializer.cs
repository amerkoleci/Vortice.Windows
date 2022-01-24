// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public partial class ID3D12VersionedRootSignatureDeserializer
{
    public unsafe VersionedRootSignatureDescription GetRootSignatureDescAtVersion(RootSignatureVersion convertToVersion)
    {
        IntPtr ptr = GetRootSignatureDescAtVersion_(convertToVersion);

        // Marshal the result.
        var result = new VersionedRootSignatureDescription();
        result.__MarshalFrom(ref Unsafe.AsRef<VersionedRootSignatureDescription.__Native>(ptr.ToPointer()));
        return result;
    }
}
