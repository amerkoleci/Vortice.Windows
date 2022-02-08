// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial struct ResourceUnorderedAccessViewBarrier
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceUnorderedAccessViewBarrier"/> struct.
    /// </summary>
    /// <param name="resource">The resource.</param>
    /// <exception cref="System.ArgumentNullException">resourceBefore</exception>
    public ResourceUnorderedAccessViewBarrier(ID3D12Resource? resource)
    {
        ResourcePointer = resource != null ? resource.NativePointer : IntPtr.Zero;
    }
}
