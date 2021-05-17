// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Device
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ImmediateContext__ != null)
                {
                    ImmediateContext__.Dispose();
                    ImmediateContext__ = null;
                }
            }

            base.Dispose(disposing);
        }

        public unsafe T CheckFeatureSupport<T>(Feature feature) where T : unmanaged
        {
            T featureSupport = default;
            CheckFeatureSupport(feature, new IntPtr(&featureSupport), sizeof(T));
            return featureSupport;
        }

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
        {
            fixed (void* featureSupportPtr = &featureSupport)
            {
                return CheckFeatureSupport(feature, (IntPtr)featureSupportPtr, sizeof(T)).Success;
            }
        }

        /// <summary>
        /// Check if this device is supporting threading.
        /// </summary>
        /// <param name="supportsConcurrentResources">Support concurrent resources.</param>
        /// <param name="supportsCommandLists">Support command lists.</param>
        /// <returns>
        /// A <see cref="Result"/> object describing the result of the operation.
        /// </returns>
        public unsafe Result CheckThreadingSupport(out bool supportsConcurrentResources, out bool supportsCommandLists)
        {
            var support = default(FeatureDataThreading);
            Result result = CheckFeatureSupport(Feature.Threading, new IntPtr(&support), Unsafe.SizeOf<FeatureDataThreading>());

            if (result.Failure)
            {
                supportsConcurrentResources = false;
                supportsCommandLists = false;
            }
            else
            {
                supportsConcurrentResources = support.DriverConcurrentCreates;
                supportsCommandLists = support.DriverCommandLists;
            }

            return result;
        }

        public FeatureDataThreading CheckFeatureThreading()
        {
            return CheckFeatureSupport<FeatureDataThreading>(Feature.Threading);
        }

        public FeatureDataDoubles CheckFeatureDoubles()
        {
            return CheckFeatureSupport<FeatureDataDoubles>(Feature.Doubles);
        }

        public FeatureDataD3D11Options CheckFeatureOptions()
        {
            return CheckFeatureSupport<FeatureDataD3D11Options>(Feature.D3D11Options);
        }

        public FeatureDataArchitectureInfo CheckFeatureArchitectureInfo()
        {
            return CheckFeatureSupport<FeatureDataArchitectureInfo>(Feature.ArchitectureInfo);
        }

        public FeatureDataShaderMinPrecisionSupport CheckFeatureShaderMinPrecisionSupport()
        {
            return CheckFeatureSupport<FeatureDataShaderMinPrecisionSupport>(Feature.ShaderMinPrecisionSupport);
        }

        public FeatureDataD3D11Options1 CheckFeatureOptions1()
        {
            return CheckFeatureSupport<FeatureDataD3D11Options1>(Feature.D3D11Options1);
        }

        public FeatureDataD3D11Options2 CheckFeatureOptions2()
        {
            return CheckFeatureSupport<FeatureDataD3D11Options2>(Feature.D3D11Options2);
        }

        public FeatureDataD3D11Options3 CheckFeatureOptions3()
        {
            return CheckFeatureSupport<FeatureDataD3D11Options3>(Feature.D3D11Options3);
        }

        public FeatureDataD3D11Options4 CheckFeatureOptions4()
        {
            return CheckFeatureSupport<FeatureDataD3D11Options4>(Feature.D3D11Options4);
        }

        public FeatureDataD3D11Options5 CheckFeatureOptions5()
        {
            return CheckFeatureSupport<FeatureDataD3D11Options5>(Feature.D3D11Options5);
        }

        public FeatureDataGpuVirtualAddressSupport CheckFeatureGpuVirtualAddressSupport()
        {
            return CheckFeatureSupport<FeatureDataGpuVirtualAddressSupport>(Feature.GpuVirtualAddressSupport);
        }

        public FeatureDataShaderCache CheckFeatureShaderCache()
        {
            return CheckFeatureSupport<FeatureDataShaderCache>(Feature.ShaderCache);
        }

        public unsafe FormatSupport CheckFeatureFormatSupport(Format format)
        {
            FeatureDataFormatSupport support = default;
            support.InFormat = format;
            if (CheckFeatureSupport(Feature.FormatSupport, new IntPtr(&support), Unsafe.SizeOf<FeatureDataFormatSupport2>()).Failure)
            {
                return FormatSupport.None;
            }

            return support.OutFormatSupport;
        }

        public unsafe FormatSupport2 CheckFeatureFormatSupport2(Format format)
        {
            FeatureDataFormatSupport2 support = default;
            support.InFormat = format;
            if (CheckFeatureSupport(Feature.FormatSupport2, new IntPtr(&support), Unsafe.SizeOf<FeatureDataFormatSupport2>()).Failure)
            {
                return FormatSupport2.None;
            }

            return support.OutFormatSupport2;
        }

        public ID3D11DeviceContext CreateDeferredContext()
        {
            return CreateDeferredContext(0);
        }

        public ID3D11Buffer CreateBuffer(BufferDescription description, IntPtr dataPointer)
        {
            return CreateBuffer(description, new SubresourceData(dataPointer));
        }

        public unsafe ID3D11Buffer CreateBuffer<T>(ref T data, BufferDescription description) where T : unmanaged
        {
            if (description.SizeInBytes == 0)
                description.SizeInBytes = sizeof(T);

            fixed (void* dataPtr = &data)
            {
                return CreateBuffer(description, new SubresourceData((IntPtr)dataPtr));
            }
        }

        public unsafe ID3D11Buffer CreateBuffer<T>(T[] data, BufferDescription description) where T : unmanaged
        {
            if (description.SizeInBytes == 0)
                description.SizeInBytes = sizeof(T) * data.Length;

            fixed (void* dataPtr = &data[0])
            {
                return CreateBuffer(description, new SubresourceData((IntPtr)dataPtr));
            }
        }

        public unsafe ID3D11VertexShader CreateVertexShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateVertexShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11PixelShader CreatePixelShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreatePixelShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11GeometryShader CreateGeometryShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateGeometryShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11HullShader CreateHullShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateHullShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11DomainShader CreateDomainShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateDomainShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11ComputeShader CreateComputeShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateComputeShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        /// <summary>
        /// Create an input-layout object to describe the input-buffer data for the input-assembler stage.
        /// </summary>
        /// <param name="inputElements">An array of the input-assembler stage input data types; each type is described by an element description</param>
        /// <param name="shaderBytecode">A pointer to the compiled shader. The compiled shader code contains a input signature which is validated against the array of elements.</param>
        /// <returns>New instance of <see cref="ID3D11InputLayout"/> or throws exception.</returns>
        public ID3D11InputLayout CreateInputLayout(InputElementDescription[] inputElements, byte[] shaderBytecode)
        {
            unsafe
            {
                fixed (void* pBuffer = shaderBytecode)
                {
                    return CreateInputLayout(inputElements, inputElements.Length, (IntPtr)pBuffer, shaderBytecode.Length);
                }
            }
        }

        /// <summary>
        /// Create an input-layout object to describe the input-buffer data for the input-assembler stage.
        /// </summary>
        /// <param name="inputElements">An array of the input-assembler stage input data types; each type is described by an element description</param>
        /// <param name="blob">A pointer to the compiled shader. The compiled shader code contains a input signature which is validated against the array of elements.</param>
        /// <returns>New instance of <see cref="ID3D11InputLayout"/> or throws exception.</returns>
        public ID3D11InputLayout CreateInputLayout(InputElementDescription[] inputElements, Blob blob)
        {
            return CreateInputLayout(inputElements, inputElements.Length, blob.BufferPointer, blob.BufferSize);
        }

        /// <summary>
        /// Give a device access to a shared resource created on a different device.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ID3D11Resource"/> </typeparam>
        /// <param name="handle">A handle to the resource to open.</param>
        /// <returns>Instance of <see cref="ID3D11Resource"/>.</returns>
        public T? OpenSharedResource<T>(IntPtr handle) where T : ID3D11Resource
        {
            Result result = OpenSharedResource(handle, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                return default;
            }

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        /// <summary>
        /// Give a device access to a shared resource created on a different device.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ID3D11Resource"/> </typeparam>
        /// <param name="handle">A handle to the resource to open.</param>
        /// <param name="resource">Instance of <see cref="ID3D11Resource"/>.</param>
        /// <returns>The operation result.</returns>
        public Result OpenSharedResource<T>(IntPtr handle, out T? resource) where T : ID3D11Resource
        {
            Result result = OpenSharedResource(handle, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Success)
            {
                resource = MarshallingHelpers.FromPointer<T>(nativePtr);
                return result;
            }

            resource = default;
            return result;
        }
    }
}
