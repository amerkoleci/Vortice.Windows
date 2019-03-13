// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
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
        public ResourceTransitionBarrier(ID3D12Resource resource, ResourceStates stateBefore, ResourceStates stateAfter, int subresource = -1)
        {
            Guard.NotNull(resource, nameof(resource));
            ResourcePointer = resource.NativePointer;
            Subresource = subresource;
            StateBefore = stateBefore;
            StateAfter = stateAfter;
            Subresource = subresource;
        }
    }
}
