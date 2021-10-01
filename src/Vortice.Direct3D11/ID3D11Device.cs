﻿// Copyright (c) Amer Koleci and Contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Device
    {
        /// <summary>
        /// Gets or sets the debug-name for this object.
        /// </summary>
        public unsafe string DebugName
        {
            get
            {
                byte* pname = stackalloc byte[1024];
                int size = 1024 - 1;
                if (GetPrivateData(CommonGuid.DebugObjectName, ref size, new IntPtr(pname)).Failure)
                {
                    return string.Empty;
                }

                pname[size] = 0;
                return Marshal.PtrToStringAnsi(new IntPtr(pname));
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    SetPrivateData(CommonGuid.DebugObjectName, 0, IntPtr.Zero);
                }
                else
                {
                    var namePtr = Marshal.StringToHGlobalAnsi(value);
                    SetPrivateData(CommonGuid.DebugObjectName, value.Length, namePtr);
                    Marshal.FreeHGlobal(namePtr);
                }
            }
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
            return CreateBuffer(description, dataPointer != IntPtr.Zero ? new SubresourceData(dataPointer) : (SubresourceData?)default);
        }

        public ID3D11Buffer CreateBuffer(BufferDescription description, DataStream data)
        {
            return CreateBuffer(description, new SubresourceData(data.PositionPointer, 0, 0));
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

        /// <summary>
        /// Creates a new instance of the <see cref="ID3D11Buffer"/> class.
        /// </summary>
        /// <typeparam name="T">Type of the data to upload</typeparam>
        /// <param name="bindFlags">Flags specifying how the buffer will be bound to the pipeline.</param>
        /// <param name="data">Initial data used to initialize the buffer.</param>
        /// <param name="sizeInBytes">The size, in bytes, of the buffer. If 0 is specified, sizeof(T) * data.Length is used.</param>
        /// <param name="usage">The usage pattern for the buffer.</param>
        /// <param name="accessFlags">Flags specifying how the buffer will be accessible from the CPU.</param>
        /// <param name="optionFlags">Miscellaneous resource options.</param>
        /// <param name="structureByteStride">The size (in bytes) of the structure element for structured buffers.</param>
        /// <returns>An initialized buffer</returns>
        /// <msdn-id>ff476501</msdn-id>	
        /// <unmanaged>HRESULT ID3D11Device::CreateBuffer([In] const D3D11_BUFFER_DESC* pDesc,[In, Optional] const D3D11_SUBRESOURCE_DATA* pInitialData,[Out, Fast] ID3D11Buffer** ppBuffer)</unmanaged>	
        /// <unmanaged-short>ID3D11Device::CreateBuffer</unmanaged-short>	
        public unsafe ID3D11Buffer CreateBuffer<T>(BindFlags bindFlags, T[] data,
            int sizeInBytes = 0, ResourceUsage usage = ResourceUsage.Default, CpuAccessFlags accessFlags = CpuAccessFlags.None, ResourceOptionFlags optionFlags = ResourceOptionFlags.None, int structureByteStride = 0) where T : unmanaged
        {
            var description = new BufferDescription()
            {
                BindFlags = bindFlags,
                CpuAccessFlags = accessFlags,
                OptionFlags = optionFlags,
                SizeInBytes = sizeInBytes == 0 ? sizeof(T) * data.Length : sizeInBytes,
                Usage = usage,
                StructureByteStride = structureByteStride
            };

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

        public ID3D11VertexShader CreateVertexShader(Blob shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            return CreateVertexShader(shaderBytecode.BufferPointer, shaderBytecode.BufferSize, classLinkage);
        }

        public unsafe ID3D11PixelShader CreatePixelShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreatePixelShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public ID3D11PixelShader CreatePixelShader(Blob shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            return CreatePixelShader(shaderBytecode.BufferPointer, shaderBytecode.BufferSize, classLinkage);
        }

        public unsafe ID3D11GeometryShader CreateGeometryShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateGeometryShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public ID3D11GeometryShader CreateGeometryShader(Blob shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            return CreateGeometryShader(shaderBytecode.BufferPointer, shaderBytecode.BufferSize, classLinkage);
        }

        public unsafe ID3D11HullShader CreateHullShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateHullShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public ID3D11HullShader CreateHullShader(Blob shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            return CreateHullShader(shaderBytecode.BufferPointer, shaderBytecode.BufferSize, classLinkage);
        }

        public unsafe ID3D11DomainShader CreateDomainShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateDomainShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public ID3D11DomainShader CreateDomainShader(Blob shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            return CreateDomainShader(shaderBytecode.BufferPointer, shaderBytecode.BufferSize, classLinkage);
        }

        public unsafe ID3D11ComputeShader CreateComputeShader(byte[] shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            fixed (void* pBuffer = shaderBytecode)
            {
                return CreateComputeShader((IntPtr)pBuffer, shaderBytecode.Length, classLinkage);
            }
        }

        public ID3D11ComputeShader CreateComputeShader(Blob shaderBytecode, ID3D11ClassLinkage? classLinkage = default)
        {
            return CreateComputeShader(shaderBytecode.BufferPointer, shaderBytecode.BufferSize, classLinkage);
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

        public ID3D11Query CreateQuery(QueryType queryType, QueryFlags miscFlags = QueryFlags.None)
        {
            return CreateQuery(new QueryDescription(queryType, miscFlags));
        }

        public ID3D11Texture2D CreateTexture2D(in Texture2DDescription description, DataRectangle[] data)
        {
            SubresourceData[] subResourceDatas = new SubresourceData[data.Length];
            for (int i = 0; i < subResourceDatas.Length; i++)
            {
                subResourceDatas[i].DataPointer = data[i].DataPointer;
                subResourceDatas[i].RowPitch = data[i].Pitch;
            }

            return CreateTexture2D(description, subResourceDatas);
        }

        public ID3D11Texture1D CreateTexture1D(int width, Format format = Format.R8G8B8A8_UNorm, int arraySize = 1, int mipLevels = 0, SubresourceData[]? initialData = null, BindFlags bindFlags = BindFlags.ShaderResource)
        {
            return CreateTexture1D(new Texture1DDescription(width, format, arraySize, mipLevels, bindFlags), initialData);
        }

        public ID3D11Texture2D CreateTexture2D(int width, int height, Format format = Format.R8G8B8A8_UNorm, int arraySize = 1, int mipLevels = 0, SubresourceData[]? initialData = null, BindFlags bindFlags = BindFlags.ShaderResource)
        {
            return CreateTexture2D(new Texture2DDescription(width, height, format, arraySize, mipLevels, bindFlags), initialData);
        }

        public ID3D11Texture2D CreateTexture2DMultisample(int width, int height, int sampleCount, Format format = Format.R8G8B8A8_UNorm, int arraySize = 1, BindFlags bindFlags = BindFlags.ShaderResource)
        {
            if (sampleCount < 1)
                throw new ArgumentException(nameof(sampleCount));

            return CreateTexture2D(new Texture2DDescription(width, height, format, arraySize, 1, bindFlags, ResourceUsage.Default, CpuAccessFlags.None, sampleCount, 0), null);
        }

        public ID3D11Texture3D CreateTexture3D(int width, int height, int depth, Format format = Format.R8G8B8A8_UNorm, int mipLevels = 0, SubresourceData[]? initialData = null, BindFlags bindFlags = BindFlags.ShaderResource)
        {
            return CreateTexture3D(new Texture3DDescription(width, height, depth, format, mipLevels, bindFlags), initialData);
        }

        public ID3D11Texture2D CreateTextureCube(int size, Format format = Format.R8G8B8A8_UNorm, int arraySize = 1, int mipLevels = 0, SubresourceData[]? initialData = null, BindFlags bindFlags = BindFlags.ShaderResource)
        {
            var description = new Texture2DDescription(size, size, format, arraySize * 6, mipLevels, bindFlags, ResourceUsage.Default, CpuAccessFlags.None, 1, 0, ResourceOptionFlags.TextureCube);

            return CreateTexture2D(description, initialData);
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
