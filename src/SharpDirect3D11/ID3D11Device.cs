// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpDXGI;
using SharpDXGI.Direct3D;

namespace SharpDirect3D11
{
    public partial class ID3D11Device
    {
        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : struct
        {
            return CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), Interop.SizeOf<T>()).Success;
        }

        public ID3D11Buffer CreateBuffer(BufferDescription description, IntPtr dataPointer)
        {
            Guard.IsTrue(dataPointer != IntPtr.Zero, nameof(dataPointer), "Invalid data pointer");

            return CreateBuffer(description, new SubresourceData(dataPointer));
        }

        public unsafe ID3D11Buffer CreateBuffer<T>(ref T data, BufferDescription description) where T : struct
        {
            if (description.ByteWidth == 0)
                description.ByteWidth = Unsafe.SizeOf<T>();

            return CreateBuffer(description, new SubresourceData((IntPtr)Unsafe.AsPointer(ref data)));
        }

        public unsafe ID3D11Buffer CreateBuffer<T>(T[] data, BufferDescription description) where T : struct
        {
            if (description.ByteWidth == 0)
                description.ByteWidth = Unsafe.SizeOf<T>() * data.Length;

            return CreateBuffer(description, new SubresourceData((IntPtr)Unsafe.AsPointer(ref data[0])));
        }

        public unsafe ID3D11VertexShader CreateVertexShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            Guard.NotNull(shaderBytecode, nameof(shaderBytecode));

            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateVertexShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11PixelShader CreatePixelShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            Guard.NotNull(shaderBytecode, nameof(shaderBytecode));

            fixed (void* pBuffer = shaderBytecode)
            {
                return CreatePixelShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11GeometryShader CreateGeometryShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            Guard.NotNull(shaderBytecode, nameof(shaderBytecode));

            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateGeometryShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11HullShader CreateHullShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            Guard.NotNull(shaderBytecode, nameof(shaderBytecode));

            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateHullShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11DomainShader CreateDomainShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            Guard.NotNull(shaderBytecode, nameof(shaderBytecode));

            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateDomainShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11ComputeShader CreateComputeShader(byte[] shaderBytecode, ID3D11ClassLinkage classLinkage = null)
        {
            Guard.NotNull(shaderBytecode, nameof(shaderBytecode));

            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateComputeShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public unsafe ID3D11InputLayout CreateInputLayout(InputElementDescription[] inputElements, byte[] shaderBytecode)
        {
            Guard.NotNull(inputElements, nameof(inputElements));
            Guard.NotNull(shaderBytecode, nameof(shaderBytecode));

            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateInputLayout(inputElements, inputElements.Length, (IntPtr)pBuffer, shaderBytecode.Length);
            }
        }

        public ID3D11InputLayout CreateInputLayout(InputElementDescription[] inputElements, Blob blob)
        {
            Guard.NotNull(inputElements, nameof(inputElements));
            Guard.NotNull(blob, nameof(blob));

            return CreateInputLayout(inputElements, inputElements.Length, blob.BufferPointer, blob.BufferPointer);
        }
    }
}
