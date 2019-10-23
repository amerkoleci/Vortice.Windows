// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using Vortice.Direct3D;

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
            CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), sizeof(T));
            return featureSupport;
        }

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
        {
            return CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), sizeof(T)).Success;
        }

        /// <summary>
        /// Check if this device is supporting threading.
        /// </summary>
        /// <param name="supportsConcurrentResources">Support concurrent resources.</param>
        /// <param name="supportsCommandLists">Support command lists.</param>
        /// <returns>
        /// A <see cref="SResult"/> object describing the result of the operation.
        /// </returns>
        public unsafe Result CheckThreadingSupport(out bool supportsConcurrentResources, out bool supportsCommandLists)
        {
            var support = default(FeatureDataThreading);
            var result = CheckFeatureSupport(Feature.Threading, new IntPtr(&support), Unsafe.SizeOf<FeatureDataThreading>());

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

            return CreateBuffer(description, new SubresourceData((IntPtr)Unsafe.AsPointer(ref data)));
        }

        public unsafe ID3D11Buffer CreateBuffer<T>(T[] data, BufferDescription description) where T : unmanaged
        {
            if (description.SizeInBytes == 0)
                description.SizeInBytes = sizeof(T) * data.Length;

            return CreateBuffer(description, new SubresourceData((IntPtr)Unsafe.AsPointer(ref data[0])));
        }

        public unsafe ID3D11VertexShader CreateVertexShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateVertexShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11PixelShader CreatePixelShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreatePixelShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11GeometryShader CreateGeometryShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateGeometryShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11HullShader CreateHullShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateHullShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11DomainShader CreateDomainShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateDomainShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11ComputeShader CreateComputeShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateComputeShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11InputLayout CreateInputLayout(InputElementDescription[] inputElements, byte[] shaderBytecode)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateInputLayout(inputElements, inputElements.Length, (IntPtr)pBuffer, shaderBytecode.Length);
            }
        }

        public ID3D11InputLayout CreateInputLayout(InputElementDescription[] inputElements, Blob blob)
        {
            return CreateInputLayout(inputElements, inputElements.Length, blob.BufferPointer, blob.BufferPointer);
        }

        /// <summary>
        /// Give a device access to a shared resource created on a different device.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ID3D11Resource"/> </typeparam>
        /// <param name="handle">A handle to the resource to open.</param>
        /// <returns>Instance of <see cref="ID3D11Resource"/>.</returns>
        public T OpenSharedResource<T>(IntPtr handle) where T : ID3D11Resource
        {
            var result = OpenSharedResource(handle, typeof(T).GUID, out var nativePtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }
    }
}
