// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.DirectX.Direct3D;

namespace Vortice.Direct3D12
{
    public partial class ID3D12GraphicsCommandList4
    {
        public void BeginRenderPass(RenderPassRenderTargetDescription[] renderTargets, RenderPassDepthStencilDescription? depthStencil, RenderPassFlags flags = RenderPassFlags.None)
        {
            BeginRenderPass(renderTargets.Length, renderTargets, depthStencil, flags);
        }

        /// <summary>
        /// Performs a raytracing acceleration structure build on the GPU and optionally outputs post-build information immediately after the build.
        /// </summary>
        /// <param name="description">Description of the acceleration structure to build.</param>
        public void BuildRaytracingAccelerationStructure(BuildRaytracingAccelerationStructureDescription description)
        {
            BuildRaytracingAccelerationStructure(ref description, 0, null);
        }

        /// <summary>
        /// Performs a raytracing acceleration structure build on the GPU and optionally outputs post-build information immediately after the build.
        /// </summary>
        /// <param name="description">Description of the acceleration structure to build.</param>
        /// <param name="postbuildInfoDescriptions">Array of descriptions for post-build info to generate describing properties of the acceleration structure that was built.</param>
        public void BuildRaytracingAccelerationStructure(BuildRaytracingAccelerationStructureDescription description, RaytracingAccelerationStructurePostbuildInfoDescription[] postbuildInfoDescriptions)
        {
            BuildRaytracingAccelerationStructure(ref description, postbuildInfoDescriptions.Length, postbuildInfoDescriptions);
        }

        public void InitializeMetaCommand(ID3D12MetaCommand metaCommand)
        {
            InitializeMetaCommand(metaCommand, IntPtr.Zero, PointerSize.Zero);
        }

        public void InitializeMetaCommand(ID3D12MetaCommand metaCommand, Blob initializationParametersData)
        {
            InitializeMetaCommand(metaCommand, initializationParametersData.BufferPointer, initializationParametersData.BufferSize);
        }

        public void ExecuteMetaCommand(ID3D12MetaCommand metaCommand)
        {
            ExecuteMetaCommand(metaCommand, IntPtr.Zero, PointerSize.Zero);
        }

        public void ExecuteMetaCommand(ID3D12MetaCommand metaCommand, Blob executionParametersData)
        {
            ExecuteMetaCommand(metaCommand, executionParametersData.BufferPointer, executionParametersData.BufferSize);
        }

        public void EmitRaytracingAccelerationStructurePostbuildInfo(
            RaytracingAccelerationStructurePostbuildInfoDescription description,
            long[] sourceAccelerationStructureData)
        {
            EmitRaytracingAccelerationStructurePostbuildInfo(description, sourceAccelerationStructureData.Length, sourceAccelerationStructureData);
        }
    }
}
