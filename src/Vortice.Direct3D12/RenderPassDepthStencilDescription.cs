// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a binding (fixed for the duration of the render pass) to a depth stencil view (DSV), as well as its beginning and ending access characteristics.
    /// </summary>
    public partial struct RenderPassDepthStencilDescription
    {
        /// <summary>
        /// Initialize new instance of <see cref="RenderPassDepthStencilDescription"/> struct.
        /// </summary>
        /// <param name="cpuDescriptor">The CPU <see cref="CpuDescriptorHandle"/> handle corresponding to the depth stencil view (DSV).</param>
        /// <param name="depthBeginningAccess">The access to the depth buffer requested at the transition into a render pass.</param>
        /// <param name="depthEndingAccess">The access to the depth buffer requested at the transition out of a render pass.</param>
        public RenderPassDepthStencilDescription(
            CpuDescriptorHandle cpuDescriptor,
            RenderPassBeginningAccess depthBeginningAccess,
            RenderPassEndingAccess depthEndingAccess)
        {
            CpuDescriptor = cpuDescriptor;
            DepthBeginningAccess = depthBeginningAccess;
            StencilBeginningAccess = new RenderPassBeginningAccess(RenderPassBeginningAccessType.NoAccess);
            DepthEndingAccess = depthEndingAccess;
            StencilEndingAccess = new RenderPassEndingAccess(RenderPassEndingAccessType.NoAccess);
        }

        /// <summary>
        /// Initialize new instance of <see cref="RenderPassDepthStencilDescription"/> struct.
        /// </summary>
        /// <param name="cpuDescriptor">The CPU <see cref="CpuDescriptorHandle"/> handle corresponding to the depth stencil view (DSV).</param>
        /// <param name="depthBeginningAccess">The access to the depth buffer requested at the transition into a render pass.</param>
        /// <param name="stencilBeginningAccess">The access to the stencil buffer requested at the transition into a render pass.</param>
        /// <param name="depthEndingAccess">The access to the depth buffer requested at the transition out of a render pass.</param>
        /// <param name="stencilEndingAccess">The access to the stencil buffer requested at the transition out of a render pass.</param>
        public RenderPassDepthStencilDescription(
            CpuDescriptorHandle cpuDescriptor,
            RenderPassBeginningAccess depthBeginningAccess,
            RenderPassBeginningAccess stencilBeginningAccess,
            RenderPassEndingAccess depthEndingAccess,
            RenderPassEndingAccess stencilEndingAccess)
        {
            CpuDescriptor = cpuDescriptor;
            DepthBeginningAccess = depthBeginningAccess;
            StencilBeginningAccess = stencilBeginningAccess;
            DepthEndingAccess = depthEndingAccess;
            StencilEndingAccess = stencilEndingAccess;
        }
    }
}
