// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12GraphicsCommandList4
{
    public void BeginRenderPass(in RenderPassRenderTargetDescription renderTarget, RenderPassDepthStencilDescription? depthStencil = null, RenderPassFlags flags = RenderPassFlags.None)
    {
        RenderPassRenderTargetDescription.__Native renderTargetNative = default;
        renderTarget.__MarshalTo(ref renderTargetNative);
        BeginRenderPass(1, &renderTargetNative, depthStencil, flags);
        renderTarget.__MarshalFree(ref renderTargetNative);
    }

    /// <summary>
    /// Marks the beginning of a render pass by binding a set of output resources for the duration of the render pass.
    /// These bindings are to one or more render target views (RTVs), and/or to a depth stencil view (DSV).
    /// </summary>
    /// <param name="renderTargetCount">The number of render targets being bound.</param>
    /// <param name="renderTargets">An array of <see cref="RenderPassRenderTargetDescription"/>, which describes bindings (fixed for the duration of the render pass) to one or more render target views (RTVs), as well as their beginning and ending access characteristics.</param>
    /// <param name="depthStencil">An optional <see cref="RenderPassDepthStencilDescription"/>, which describes a binding (fixed for the duration of the render pass) to a depth stencil view (DSV), as well as its beginning and ending access characteristics.</param>
    /// <param name="flags">The nature/requirements of the render pass; for example, whether it is a suspending or a resuming render pass, or whether it wants to write to unordered access view(s).</param>
    public void BeginRenderPass(int renderTargetCount, RenderPassRenderTargetDescription[] renderTargets, RenderPassDepthStencilDescription? depthStencil = null, RenderPassFlags flags = RenderPassFlags.None)
    {
        Span<RenderPassRenderTargetDescription.__Native> renderTargetsNative = stackalloc RenderPassRenderTargetDescription.__Native[renderTargetCount];
        for (int i = 0; i < renderTargetCount; ++i)
        {
            renderTargets[i].__MarshalTo(ref renderTargetsNative[i]);
        }

        fixed (RenderPassRenderTargetDescription.__Native* renderTargetsPtr = renderTargetsNative)
        {
            BeginRenderPass(renderTargetCount, renderTargetsPtr, depthStencil, flags);
        }

        for (int i = 0; i < renderTargetCount; ++i)
        {
            renderTargets[i].__MarshalFree(ref renderTargetsNative[i]);
        }
    }

    /// <summary>
    /// Marks the beginning of a render pass by binding a set of output resources for the duration of the render pass.
    /// These bindings are to one or more render target views (RTVs), and/or to a depth stencil view (DSV).
    /// </summary>
    /// <param name="renderTargets">An array of <see cref="RenderPassRenderTargetDescription"/>, which describes bindings (fixed for the duration of the render pass) to one or more render target views (RTVs), as well as their beginning and ending access characteristics.</param>
    /// <param name="depthStencil">An optional <see cref="RenderPassDepthStencilDescription"/>, which describes a binding (fixed for the duration of the render pass) to a depth stencil view (DSV), as well as its beginning and ending access characteristics.</param>
    /// <param name="flags">The nature/requirements of the render pass; for example, whether it is a suspending or a resuming render pass, or whether it wants to write to unordered access view(s).</param>
    public void BeginRenderPass(RenderPassRenderTargetDescription[] renderTargets, RenderPassDepthStencilDescription? depthStencil = null, RenderPassFlags flags = RenderPassFlags.None)
    {
        BeginRenderPass(renderTargets.Length, renderTargets, depthStencil, flags);
    }

    /// <summary>
    /// Marks the beginning of a render pass by binding a set of output resources for the duration of the render pass.
    /// These bindings are to one or more render target views (RTVs), and/or to a depth stencil view (DSV).
    /// </summary>
    /// <param name="renderTargetCount">The number of render targets being bound.</param>
    /// <param name="renderTargets">An array of <see cref="RenderPassRenderTargetDescription"/>, which describes bindings (fixed for the duration of the render pass) to one or more render target views (RTVs), as well as their beginning and ending access characteristics.</param>
    /// <param name="depthStencil">An optional <see cref="RenderPassDepthStencilDescription"/>, which describes a binding (fixed for the duration of the render pass) to a depth stencil view (DSV), as well as its beginning and ending access characteristics.</param>
    /// <param name="flags">The nature/requirements of the render pass; for example, whether it is a suspending or a resuming render pass, or whether it wants to write to unordered access view(s).</param>
    public void BeginRenderPass(int renderTargetCount, Span<RenderPassRenderTargetDescription> renderTargets, RenderPassDepthStencilDescription? depthStencil = null, RenderPassFlags flags = RenderPassFlags.None)
    {
        Span<RenderPassRenderTargetDescription.__Native> renderTargetsNative = stackalloc RenderPassRenderTargetDescription.__Native[renderTargetCount];
        for (int i = 0; i < renderTargetCount; ++i)
        {
            renderTargets[i].__MarshalTo(ref renderTargetsNative[i]);
        }

        fixed (RenderPassRenderTargetDescription.__Native* renderTargetsPtr = renderTargetsNative)
        {
            BeginRenderPass(renderTargetCount, renderTargetsPtr, depthStencil, flags);
        }

        for (int i = 0; i < renderTargetCount; ++i)
        {
            renderTargets[i].__MarshalFree(ref renderTargetsNative[i]);
        }
    }

    /// <summary>
    /// Marks the beginning of a render pass by binding a set of output resources for the duration of the render pass.
    /// These bindings are to one or more render target views (RTVs), and/or to a depth stencil view (DSV).
    /// </summary>
    /// <param name="renderTargets">An array of <see cref="RenderPassRenderTargetDescription"/>, which describes bindings (fixed for the duration of the render pass) to one or more render target views (RTVs), as well as their beginning and ending access characteristics.</param>
    /// <param name="depthStencil">An optional <see cref="RenderPassDepthStencilDescription"/>, which describes a binding (fixed for the duration of the render pass) to a depth stencil view (DSV), as well as its beginning and ending access characteristics.</param>
    /// <param name="flags">The nature/requirements of the render pass; for example, whether it is a suspending or a resuming render pass, or whether it wants to write to unordered access view(s).</param>
    public void BeginRenderPass(Span<RenderPassRenderTargetDescription> renderTargets, RenderPassDepthStencilDescription? depthStencil = null, RenderPassFlags flags = RenderPassFlags.None)
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
    public void BuildRaytracingAccelerationStructure(BuildRaytracingAccelerationStructureDescription description, RaytracingAccelerationStructurePostBuildInfoDescription[] postbuildInfoDescriptions)
    {
        BuildRaytracingAccelerationStructure(ref description, postbuildInfoDescriptions.Length, postbuildInfoDescriptions);
    }

    public void InitializeMetaCommand(ID3D12MetaCommand metaCommand)
    {
        InitializeMetaCommand(metaCommand, nint.Zero, nuint.Zero);
    }

    public void InitializeMetaCommand(ID3D12MetaCommand metaCommand, Blob initializationParametersData)
    {
        InitializeMetaCommand(metaCommand, initializationParametersData.BufferPointer, initializationParametersData.BufferSize);
    }

    public void ExecuteMetaCommand(ID3D12MetaCommand metaCommand)
    {
        ExecuteMetaCommand(metaCommand, nint.Zero, nuint.Zero);
    }

    public void ExecuteMetaCommand(ID3D12MetaCommand metaCommand, Blob executionParametersData)
    {
        ExecuteMetaCommand(metaCommand, executionParametersData.BufferPointer, executionParametersData.BufferSize);
    }

    public void EmitRaytracingAccelerationStructurePostbuildInfo(
        RaytracingAccelerationStructurePostBuildInfoDescription description,
        ulong[] sourceAccelerationStructureData)
    {
        EmitRaytracingAccelerationStructurePostbuildInfo(description, sourceAccelerationStructureData.Length, sourceAccelerationStructureData);
    }
}
