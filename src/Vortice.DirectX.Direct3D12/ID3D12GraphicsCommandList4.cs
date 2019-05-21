// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.DirectX.Direct3D;

namespace Vortice.DirectX.Direct3D12
{
    public partial class ID3D12GraphicsCommandList4
    {
        public void BeginRenderPass(RenderPassRenderTargetDescription[] renderTargets, RenderPassDepthStencilDescription? depthStencil, RenderPassFlags flags = RenderPassFlags.None)
        {
            BeginRenderPass(renderTargets.Length, renderTargets, depthStencil, flags);
        }

        public void BuildRaytracingAccelerationStructure(BuildRaytracingAccelerationStructureDescription description)
        {
            BuildRaytracingAccelerationStructure(ref description, 0, null);
        }

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
            ulong[] sourceAccelerationStructureData)
        {
            EmitRaytracingAccelerationStructurePostbuildInfo(description, sourceAccelerationStructureData.Length, sourceAccelerationStructureData);
        }
    }
}
