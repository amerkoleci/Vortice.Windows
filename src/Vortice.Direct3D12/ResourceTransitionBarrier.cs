// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial struct ResourceTransitionBarrier
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceTransitionBarrier"/> struct.
    /// </summary>
    /// <param name="resource">The <see cref="ID3D12Resource"/>.</param>
    /// <param name="stateBefore">The state before.</param>
    /// <param name="stateAfter">The state after.</param>
    /// <param name="subresource">The subresource.</param>
    /// <exception cref="System.ArgumentNullException">resource</exception>
    public ResourceTransitionBarrier(ID3D12Resource resource, ResourceStates stateBefore, ResourceStates stateAfter, int subresource = D3D12.ResourceBarrierAllSubResources)
    {
        ResourcePointer = resource.NativePointer;
        Subresource = subresource;
        StateBefore = stateBefore;
        StateAfter = stateAfter;
    }
}
