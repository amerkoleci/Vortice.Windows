// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes bindings (fixed for the duration of the render pass) to one or more render target views (RTVs), as well as their beginning and ending access characteristics.
    /// </summary>
    public partial struct RenderPassRenderTargetDescription
    {
        /// <summary>
        /// Initialize new instance of <see cref="RenderPassRenderTargetDescription"/> struct.
        /// </summary>
        /// <param name="cpuDescriptor">The CPU <see cref="CpuDescriptorHandle"/> handle corresponding to the render target view(s) (RTVs).</param>
        /// <param name="beginningAccess">The access to the RTV(s) requested at the transition into a render pass.</param>
        /// <param name="endingAccess">The access to the RTV(s) requested at the transition out of a render pass.</param>
        public RenderPassRenderTargetDescription(
            CpuDescriptorHandle cpuDescriptor, 
            RenderPassBeginningAccess beginningAccess,
            RenderPassEndingAccess endingAccess)
        {
            CpuDescriptor = cpuDescriptor;
            BeginningAccess = beginningAccess;
            EndingAccess = endingAccess;
        }
    }
}
