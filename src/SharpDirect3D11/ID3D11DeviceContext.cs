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

        public ID3D11BlendState OMGetBlendState(out float blendFactor)
        {
            OMGetBlendState(out var blendState, out blendFactor, out var sampleMask);
            return blendState;
        }

        public ID3D11BlendState OMGetBlendState(out float blendFactor, out int sampleMask)
        {
            OMGetBlendState(out var blendState, out blendFactor, out sampleMask);
            return blendState;
        }

        public unsafe void RSGetViewports(Viewport[] viewports)
        {
            Guard.NotNullOrEmpty(viewports, nameof(viewports));
            int numViewports = viewports.Length;
            RSGetViewports(ref numViewports, (IntPtr)Unsafe.AsPointer(ref viewports[0]));
        }

        public unsafe void RSGetViewports(int count, Viewport[] viewports)
        {
            Guard.NotNullOrEmpty(viewports, nameof(viewports));
            RSGetViewports(ref count, (IntPtr)Unsafe.AsPointer(ref viewports[0]));
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
        #endregion
    }
}
