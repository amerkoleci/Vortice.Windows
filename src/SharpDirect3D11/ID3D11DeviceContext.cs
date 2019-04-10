// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Mathematics;
using System.Runtime.CompilerServices;
using SharpDXGI;
using SharpGen.Runtime;

namespace SharpDirect3D11
{
    public partial class ID3D11DeviceContext
    {
        public unsafe void RSSetViewport(Viewport viewport)
        {
            RSSetViewports(1, new IntPtr(&viewport));
        }

        public void RSSetViewports(params Viewport[] viewports)
        {
            unsafe
            {
                fixed (void* pViewPorts = viewports)
                {
                    RSSetViewports(viewports.Length, (IntPtr)pViewPorts);
                }
            }
        }

        public unsafe void RSSetScissorRect(InteropRect rectangle)
        {
            RSSetScissorRects(1, new IntPtr(&rectangle));
        }

        public unsafe void RSSetScissorRects(params InteropRect[] rectangles)
        {
            fixed (void* pRects = rectangles)
            {
                RSSetScissorRects(rectangles.Length, (IntPtr)pRects);
            }
        }

        public unsafe void OMSetRenderTargets(ID3D11RenderTargetView renderTargetView, ID3D11DepthStencilView depthStencilView = null)
        {
            Guard.NotNull(renderTargetView, nameof(renderTargetView));
            var renderTargetViewPtr = renderTargetView.NativePointer;
            OMSetRenderTargets(1, new IntPtr(&renderTargetViewPtr), depthStencilView);
        }

        public unsafe void OMSetRenderTargets(int renderTargetViewsCount, ID3D11RenderTargetView[] renderTargetViews, ID3D11DepthStencilView depthStencilView = null)
        {
            IntPtr* renderTargetViewsPtr = (IntPtr*)0;
            if (renderTargetViewsCount > 0)
            {
                var tempPtr = stackalloc IntPtr[renderTargetViewsCount];
                renderTargetViewsPtr = tempPtr;
                for (int i = 0; i < renderTargetViewsCount; i++)
                {
                    renderTargetViewsPtr[i] = (renderTargetViews[i] == null) ? IntPtr.Zero : renderTargetViews[i].NativePointer;
                }
            }

            OMSetRenderTargets(renderTargetViewsCount, (IntPtr)renderTargetViewsPtr, depthStencilView);
        }

        public unsafe void OMSetRenderTargets(ID3D11RenderTargetView[] renderTargetViews, ID3D11DepthStencilView depthStencilView = null)
        {
            var renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
            }

            OMSetRenderTargets(renderTargetViews.Length, (IntPtr)renderTargetViewsPtr, depthStencilView);
        }

        public unsafe void OMSetRenderTargetsAndUnorderedAccessViews(
            ID3D11RenderTargetView renderTargetView,
            ID3D11DepthStencilView depthStencilView,
            int startSlot,
            ID3D11UnorderedAccessView[] unorderedAccessViews)
        {
            Guard.NotNull(renderTargetView, nameof(renderTargetView));
            Guard.NotNullOrEmpty(unorderedAccessViews, nameof(unorderedAccessViews));

            // Marshal array.
            var renderTargetViewsPtr = renderTargetView.NativePointer;

            var unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
            int* uavInitialCounts = stackalloc int[unorderedAccessViews.Length];
            for (int i = 0; i < unorderedAccessViews.Length; i++)
            {
                unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
                uavInitialCounts[i] = -1;
            }

            OMSetRenderTargetsAndUnorderedAccessViews(1, renderTargetViewsPtr,
                depthStencilView,
                startSlot, unorderedAccessViews.Length, (IntPtr)unorderedAccessViewsPtr,
                (IntPtr)uavInitialCounts);
        }

        public unsafe void OMSetRenderTargetsAndUnorderedAccessViews(
            ID3D11RenderTargetView[] renderTargetViews,
            ID3D11DepthStencilView depthStencilView,
            int startSlot,
            ID3D11UnorderedAccessView[] unorderedAccessViews)
        {
            Guard.NotNullOrEmpty(renderTargetViews, nameof(renderTargetViews));
            Guard.NotNullOrEmpty(unorderedAccessViews, nameof(unorderedAccessViews));

            // Marshal array.
            var renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
            }

            var unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
            int* uavInitialCounts = stackalloc int[unorderedAccessViews.Length];
            for (int i = 0; i < unorderedAccessViews.Length; i++)
            {
                unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
                uavInitialCounts[i] = -1;
            }

            OMSetRenderTargetsAndUnorderedAccessViews(renderTargetViews.Length, (IntPtr)renderTargetViewsPtr,
                depthStencilView,
                startSlot, unorderedAccessViews.Length, (IntPtr)unorderedAccessViewsPtr,
                (IntPtr)uavInitialCounts);
        }

        public unsafe void OMSetRenderTargetsAndUnorderedAccessViews(
            ID3D11RenderTargetView[] renderTargetViews,
            ID3D11DepthStencilView depthStencilView,
            int startSlot,
            ID3D11UnorderedAccessView[] unorderedAccessViews,
            int[] uavInitialCounts)
        {
            Guard.NotNullOrEmpty(renderTargetViews, nameof(renderTargetViews));
            Guard.NotNullOrEmpty(unorderedAccessViews, nameof(unorderedAccessViews));
            Guard.NotNullOrEmpty(uavInitialCounts, nameof(uavInitialCounts));

            // Marshal array.
            var renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
            }

            var unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
            for (int i = 0; i < unorderedAccessViews.Length; i++)
            {
                unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
            }

            OMSetRenderTargetsAndUnorderedAccessViews(renderTargetViews.Length, (IntPtr)renderTargetViewsPtr,
                depthStencilView,
                startSlot, unorderedAccessViews.Length, (IntPtr)unorderedAccessViewsPtr,
                (IntPtr)Unsafe.AsPointer(ref uavInitialCounts[0]));
        }

        public ID3D11CommandList FinishCommandList(bool restoreState)
        {
            var result = new ID3D11CommandList();
            FinishCommandListInternal(restoreState, result).CheckError();
            return result;
        }

        public Result FinishCommandList(bool restoreState, ID3D11CommandList commandList)
        {
            return FinishCommandListInternal(restoreState, commandList);
        }

        public bool IsDataAvailable(ID3D11Asynchronous data)
        {
            return GetData(data, IntPtr.Zero, 0, AsyncGetDataFlags.None) == Result.Ok;
        }

        public bool IsDataAvailable(ID3D11Asynchronous data, AsyncGetDataFlags flags)
        {
            return GetData(data, IntPtr.Zero, 0, flags) == Result.Ok;
        }

        public T GetData<T>(ID3D11Asynchronous data, AsyncGetDataFlags flags) where T : struct
        {
            GetData(data, flags, out T result);
            return result;
        }

        public unsafe bool GetData<T>(ID3D11Asynchronous data, AsyncGetDataFlags flags, out T result) where T : struct
        {
            result = default;
            return GetData(data, (IntPtr)Unsafe.AsPointer(ref result), Unsafe.SizeOf<T>(), flags) == Result.Ok;
        }

        public ID3D11BlendState OMGetBlendState()
        {
            OMGetBlendState(out var blendState, out var blendFactor, out var sampleMask);
            return blendState;
        }

        public ID3D11BlendState OMGetBlendState(out Color4 blendFactor)
        {
            OMGetBlendState(out var blendState, out blendFactor, out var sampleMask);
            return blendState;
        }

        public ID3D11BlendState OMGetBlendState(out Color4 blendFactor, out int sampleMask)
        {
            OMGetBlendState(out var blendState, out blendFactor, out sampleMask);
            return blendState;
        }

        public unsafe Viewport RSGetViewport()
        {
            int numViewports = 1;
            var viewport = new Viewport();
            RSGetViewports(ref numViewports, (IntPtr)Unsafe.AsPointer(ref viewport));
            return viewport;
        }

        public unsafe void RSGetViewport(ref Viewport viewport)
        {
            int numViewports = 1;
            RSGetViewports(ref numViewports, (IntPtr)Unsafe.AsPointer(ref viewport));
        }

        public unsafe void RSGetViewports(Viewport[] viewports)
        {
            int numViewports = viewports.Length;
            RSGetViewports(ref numViewports, (IntPtr)Unsafe.AsPointer(ref viewports[0]));
        }

        public unsafe void RSGetViewports(int count, Viewport[] viewports)
        {
            RSGetViewports(ref count, (IntPtr)Unsafe.AsPointer(ref viewports[0]));
        }

        public unsafe InteropRect RSGetScissorRect()
        {
            int numRects = 1;
            var rect = new InteropRect();
            RSGetScissorRects(ref numRects, (IntPtr)Unsafe.AsPointer(ref rect));
            return rect;
        }

        public unsafe void RSGetScissorRect(ref InteropRect rect)
        {
            int numRects = 1;
            RSGetScissorRects(ref numRects, (IntPtr)Unsafe.AsPointer(ref rect));
        }

        public unsafe void RSGetScissorRects(InteropRect[] rects)
        {
            int numRects = rects.Length;
            RSGetScissorRects(ref numRects, (IntPtr)Unsafe.AsPointer(ref rects[0]));
        }

        public unsafe void RSGetScissorRects(int count, InteropRect[] rects)
        {
            RSGetScissorRects(ref count, (IntPtr)Unsafe.AsPointer(ref rects[0]));
        }

        /// <summary>
        /// Set the target output buffers for the stream-output stage of the pipeline.
        /// </summary>
        /// <param name="targets">The array of output buffers <see cref="ID3D11Buffer"/> to bind to the device. The buffers must have been created with the <see cref="BindFlags.StreamOutput"/> flag.</param>
        /// <param name="strides">Array of offsets to the output buffers from ppSOTargets, one offset for each buffer. The offset values must be in bytes.</param>
        public void SOSetTargets(ID3D11Buffer[] targets, int[] strides = null)
        {
            SOSetTargets(targets.Length, targets, strides);
        }

        /// <summary>
        /// Set the target output buffers for the stream-output stage of the pipeline.
        /// </summary>
        /// <param name="buffersCount">The number of buffer to bind to the device. A maximum of four output buffers can be set. If less than four are defined by the call, the remaining buffer slots are set to null.</param>
        /// <param name="targets">The array of output buffers <see cref="ID3D11Buffer"/> to bind to the device. The buffers must have been created with the <see cref="BindFlags.StreamOutput"/> flag.</param>
        /// <param name="strides">Array of offsets to the output buffers from ppSOTargets, one offset for each buffer. The offset values must be in bytes.</param>
        public unsafe void SOSetTargets(int buffersCount, ID3D11Buffer[] targets, int[] strides = null)
        {
            var targetsPtr = stackalloc IntPtr[buffersCount];
            for (int i = 0; i < buffersCount; i++)
            {
                targetsPtr[i] = targets[i] != null ? targets[i].NativePointer : IntPtr.Zero;
            }

            SOSetTargets(buffersCount, (IntPtr)targetsPtr,
                strides?.Length > 0 ? (IntPtr)Unsafe.AsPointer(ref strides[0]) : IntPtr.Zero
                );
        }

        #region VertexShader
        public void VSSetShader(ID3D11VertexShader vertexShader)
        {
            VSSetShader(ToCallbackPtr<ID3D11VertexShader>(vertexShader), IntPtr.Zero, 0);
        }

        public void VSSetShader(ID3D11VertexShader vertexShader, ID3D11ClassInstance[] classInstances)
        {
            VSSetShader(vertexShader, classInstances, classInstances.Length);
        }

        public void VSSetShader(ID3D11VertexShader vertexShader, int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            VSSetShader(vertexShader, classInstances, classInstancesCount);
        }

        public unsafe void VSSetConstantBuffer(int startSlot, ID3D11Buffer constantBuffer)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            VSSetConstantBuffers(startSlot, 1, new IntPtr(&constantBufferPtr));
        }

        public void VSSetConstantBuffers(int startSlot, params ID3D11Buffer[] constantBuffers)
        {
            VSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public unsafe void VSSetSampler(int startSlot, ID3D11SamplerState sampler)
        {
            var samplerPtr = sampler.NativePointer;
            VSSetSamplers(startSlot, 1, new IntPtr(&samplerPtr));
        }

        public void VSSetSamplers(int startSlot, params ID3D11SamplerState[] samplers)
        {
            VSSetSamplers(startSlot, samplers.Length, samplers);
        }

        public unsafe void VSSetShaderResource(int startSlot, ID3D11ShaderResourceView shaderResourceView)
        {
            var viewPtr = shaderResourceView.NativePointer;
            VSSetShaderResources(startSlot, 1, new IntPtr(&viewPtr));
        }

        public void VSSetShaderResources(int startSlot, params ID3D11ShaderResourceView[] shaderResourceViews)
        {
            VSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }

        public ID3D11VertexShader VSGetShader()
        {
            var count = 0;
            VSGetShader(out var shader, null, ref count);
            return shader;
        }

        public ID3D11VertexShader VSGetShader(ID3D11ClassInstance[] classInstances)
        {
            var count = classInstances.Length;
            VSGetShader(out var shader, classInstances, ref count);
            return shader;
        }

        public ID3D11VertexShader VSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            VSGetShader(out var shader, classInstances, ref classInstancesCount);
            return shader;
        }

        public void VSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
        {
            VSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public void VSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
        {
            VSGetSamplers(startSlot, samplers.Length, samplers);
        }

        public void VSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
        {
            VSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }
        #endregion

        #region PixelShader
        public void PSSetShader(ID3D11PixelShader pixelShader)
        {
            PSSetShader(ToCallbackPtr<ID3D11PixelShader>(pixelShader), IntPtr.Zero, 0);
        }

        public void PSSetShader(ID3D11PixelShader pixelShader, ID3D11ClassInstance[] classInstances)
        {
            PSSetShader(pixelShader, classInstances, classInstances.Length);
        }

        public void PSSetShader(ID3D11PixelShader pixelShader, int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            PSSetShader(pixelShader, classInstances, classInstancesCount);
        }

        public unsafe void PSSetConstantBuffer(int startSlot, ID3D11Buffer constantBuffer)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            PSSetConstantBuffers(startSlot, 1, new IntPtr(&constantBufferPtr));
        }

        public void PSSetConstantBuffers(int startSlot, params ID3D11Buffer[] constantBuffers)
        {
            PSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public unsafe void PSSetSampler(int startSlot, ID3D11SamplerState sampler)
        {
            var samplerPtr = sampler.NativePointer;
            PSSetSamplers(startSlot, 1, new IntPtr(&samplerPtr));
        }

        public void PSSetSamplers(int startSlot, params ID3D11SamplerState[] samplers)
        {
            PSSetSamplers(startSlot, samplers.Length, samplers);
        }

        public unsafe void PSSetShaderResource(int startSlot, ID3D11ShaderResourceView shaderResourceView)
        {
            var viewPtr = shaderResourceView.NativePointer;
            PSSetShaderResources(startSlot, 1, new IntPtr(&viewPtr));
        }

        public void PSSetShaderResources(int startSlot, params ID3D11ShaderResourceView[] shaderResourceViews)
        {
            PSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }

        public ID3D11PixelShader PSGetShader()
        {
            var count = 0;
            PSGetShader(out var shader, null, ref count);
            return shader;
        }

        public ID3D11PixelShader PSGetShader(ID3D11ClassInstance[] classInstances)
        {
            var count = classInstances.Length;
            PSGetShader(out var shader, classInstances, ref count);
            return shader;
        }

        public ID3D11PixelShader PSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            PSGetShader(out var shader, classInstances, ref classInstancesCount);
            return shader;
        }

        public void PSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
        {
            PSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public void PSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
        {
            PSGetSamplers(startSlot, samplers.Length, samplers);
        }

        public void PSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
        {
            PSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }
        #endregion

        #region DomainShader
        public void DSSetShader(ID3D11DomainShader domainShader)
        {
            DSSetShader(ToCallbackPtr<ID3D11DomainShader>(domainShader), IntPtr.Zero, 0);
        }

        public void DSSetShader(ID3D11DomainShader domainShader, ID3D11ClassInstance[] classInstances)
        {
            DSSetShader(domainShader, classInstances, classInstances.Length);
        }

        public void DSSetShader(ID3D11DomainShader domainShader, int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            DSSetShader(domainShader, classInstances, classInstancesCount);
        }

        public unsafe void DSSetConstantBuffers(int startSlot, ID3D11Buffer constantBuffer)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            DSSetConstantBuffers(startSlot, 1, new IntPtr(&constantBufferPtr));
        }

        public void DSSetConstantBuffers(int startSlot, params ID3D11Buffer[] constantBuffers)
        {
            DSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public unsafe void DSSetSampler(int startSlot, ID3D11SamplerState sampler)
        {
            var samplerPtr = sampler.NativePointer;
            DSSetSamplers(startSlot, 1, new IntPtr(&samplerPtr));
        }

        public void DSSetSamplers(int startSlot, params ID3D11SamplerState[] samplers)
        {
            DSSetSamplers(startSlot, samplers.Length, samplers);
        }

        public unsafe void DSSetShaderResource(int startSlot, ID3D11ShaderResourceView shaderResourceView)
        {
            var viewPtr = shaderResourceView.NativePointer;
            DSSetShaderResources(startSlot, 1, new IntPtr(&viewPtr));
        }

        public void DSSetShaderResources(int startSlot, params ID3D11ShaderResourceView[] shaderResourceViews)
        {
            DSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }

        public ID3D11DomainShader DSGetShader()
        {
            var count = 0;
            DSGetShader(out var shader, null, ref count);
            return shader;
        }

        public ID3D11DomainShader DSGetShader(ID3D11ClassInstance[] classInstances)
        {
            var count = classInstances.Length;
            DSGetShader(out var shader, classInstances, ref count);
            return shader;
        }

        public ID3D11DomainShader DSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            DSGetShader(out var shader, classInstances, ref classInstancesCount);
            return shader;
        }

        public void DSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
        {
            DSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public void DSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
        {
            DSGetSamplers(startSlot, samplers.Length, samplers);
        }

        public void DSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
        {
            DSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }
        #endregion

        #region HullShader
        public void HSSetShader(ID3D11HullShader hullShader)
        {
            HSSetShader(ToCallbackPtr<ID3D11HullShader>(hullShader), IntPtr.Zero, 0);
        }

        public void HSSetShader(ID3D11HullShader hullShader, ID3D11ClassInstance[] classInstances)
        {
            HSSetShader(hullShader, classInstances, classInstances.Length);
        }

        public void HSSetShader(ID3D11HullShader hullShader, int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            HSSetShader(hullShader, classInstances, classInstancesCount);
        }

        public unsafe void HSSetConstantBuffers(int startSlot, ID3D11Buffer constantBuffer)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            HSSetConstantBuffers(startSlot, 1, new IntPtr(&constantBufferPtr));
        }

        public void HSSetConstantBuffers(int startSlot, params ID3D11Buffer[] constantBuffers)
        {
            HSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public unsafe void HSSetSampler(int startSlot, ID3D11SamplerState sampler)
        {
            var samplerPtr = sampler.NativePointer;
            HSSetSamplers(startSlot, 1, new IntPtr(&samplerPtr));
        }

        public void HSSetSamplers(int startSlot, params ID3D11SamplerState[] samplers)
        {
            HSSetSamplers(startSlot, samplers.Length, samplers);
        }

        public unsafe void HSSetShaderResource(int startSlot, ID3D11ShaderResourceView shaderResourceView)
        {
            var viewPtr = shaderResourceView.NativePointer;
            HSSetShaderResources(startSlot, 1, new IntPtr(&viewPtr));
        }

        public void HSSetShaderResources(int startSlot, params ID3D11ShaderResourceView[] shaderResourceViews)
        {
            HSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }

        public ID3D11HullShader HSGetShader()
        {
            var count = 0;
            HSGetShader(out var shader, null, ref count);
            return shader;
        }

        public ID3D11HullShader HSGetShader(ID3D11ClassInstance[] classInstances)
        {
            var count = classInstances.Length;
            HSGetShader(out var shader, classInstances, ref count);
            return shader;
        }

        public ID3D11HullShader HSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            HSGetShader(out var shader, classInstances, ref classInstancesCount);
            return shader;
        }

        public void HSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
        {
            HSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public void HSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
        {
            HSGetSamplers(startSlot, samplers.Length, samplers);
        }

        public void HSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
        {
            HSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }
        #endregion

        #region GeometryShader
        public void GSSetShader(ID3D11GeometryShader geometryShader)
        {
            GSSetShader(ToCallbackPtr<ID3D11GeometryShader>(geometryShader), IntPtr.Zero, 0);
        }

        public void GSSetShader(ID3D11GeometryShader geometryShader, ID3D11ClassInstance[] classInstances)
        {
            GSSetShader(geometryShader, classInstances, classInstances.Length);
        }

        public void GSSetShader(ID3D11GeometryShader geometryShader, int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            GSSetShader(geometryShader, classInstances, classInstancesCount);
        }

        public unsafe void GSSetConstantBuffers(int startSlot, ID3D11Buffer constantBuffer)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            GSSetConstantBuffers(startSlot, 1, new IntPtr(&constantBufferPtr));
        }

        public void GSSetConstantBuffers(int startSlot, params ID3D11Buffer[] constantBuffers)
        {
            GSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public unsafe void GSSetSampler(int startSlot, ID3D11SamplerState sampler)
        {
            var samplerPtr = sampler.NativePointer;
            GSSetSamplers(startSlot, 1, new IntPtr(&samplerPtr));
        }

        public void GSSetSamplers(int startSlot, params ID3D11SamplerState[] samplers)
        {
            GSSetSamplers(startSlot, samplers.Length, samplers);
        }

        public unsafe void GSSetShaderResource(int startSlot, ID3D11ShaderResourceView shaderResourceView)
        {
            var viewPtr = shaderResourceView.NativePointer;
            GSSetShaderResources(startSlot, 1, new IntPtr(&viewPtr));
        }

        public void GSSetShaderResources(int startSlot, params ID3D11ShaderResourceView[] shaderResourceViews)
        {
            GSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }

        public ID3D11GeometryShader GSGetShader()
        {
            var count = 0;
            GSGetShader(out var shader, null, ref count);
            return shader;
        }

        public ID3D11GeometryShader GSGetShader(ID3D11ClassInstance[] classInstances)
        {
            var count = classInstances.Length;
            GSGetShader(out var shader, classInstances, ref count);
            return shader;
        }

        public ID3D11GeometryShader GSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            GSGetShader(out var shader, classInstances, ref classInstancesCount);
            return shader;
        }

        public void GSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
        {
            GSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public void GSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
        {
            GSGetSamplers(startSlot, samplers.Length, samplers);
        }

        public void GSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
        {
            GSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }
        #endregion

        #region ComputeShader
        public void CSSetShader(ID3D11ComputeShader computeShader)
        {
            CSSetShader(ToCallbackPtr<ID3D11ComputeShader>(computeShader), IntPtr.Zero, 0);
        }

        public void CSSetShader(ID3D11ComputeShader computeShader, ID3D11ClassInstance[] classInstances)
        {
            CSSetShader(computeShader, classInstances, classInstances.Length);
        }

        public void CSSetShader(ID3D11ComputeShader computeShader, int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            CSSetShader(computeShader, classInstances, classInstancesCount);
        }

        public unsafe void CSSetConstantBuffers(int startSlot, ID3D11Buffer constantBuffer)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            CSSetConstantBuffers(startSlot, 1, new IntPtr(&constantBufferPtr));
        }

        public void CSSetConstantBuffers(int startSlot, params ID3D11Buffer[] constantBuffers)
        {
            CSSetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public unsafe void CSSetSampler(int startSlot, ID3D11SamplerState sampler)
        {
            var samplerPtr = sampler.NativePointer;
            CSSetSamplers(startSlot, 1, new IntPtr(&samplerPtr));
        }

        public void CSSetSamplers(int startSlot, params ID3D11SamplerState[] samplers)
        {
            CSSetSamplers(startSlot, samplers.Length, samplers);
        }

        public unsafe void CSSetShaderResource(int startSlot, ID3D11ShaderResourceView shaderResourceView)
        {
            var viewPtr = shaderResourceView.NativePointer;
            CSSetShaderResources(startSlot, 1, new IntPtr(&viewPtr));
        }

        public void CSSetShaderResources(int startSlot, params ID3D11ShaderResourceView[] shaderResourceViews)
        {
            CSSetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }

        public ID3D11ComputeShader CSGetShader()
        {
            var count = 0;
            CSGetShader(out var shader, null, ref count);
            return shader;
        }

        public ID3D11ComputeShader CSGetShader(ID3D11ClassInstance[] classInstances)
        {
            var count = classInstances.Length;
            CSGetShader(out var shader, classInstances, ref count);
            return shader;
        }

        public ID3D11ComputeShader CSGetShader(ref int classInstancesCount, ID3D11ClassInstance[] classInstances)
        {
            CSGetShader(out var shader, classInstances, ref classInstancesCount);
            return shader;
        }

        public void CSGetConstantBuffers(int startSlot, ID3D11Buffer[] constantBuffers)
        {
            CSGetConstantBuffers(startSlot, constantBuffers.Length, constantBuffers);
        }

        public void CSGetSamplers(int startSlot, ID3D11SamplerState[] samplers)
        {
            CSGetSamplers(startSlot, samplers.Length, samplers);
        }

        public void CSGetShaderResources(int startSlot, ID3D11ShaderResourceView[] shaderResourceViews)
        {
            CSGetShaderResources(startSlot, shaderResourceViews.Length, shaderResourceViews);
        }
        #endregion
    }
}
