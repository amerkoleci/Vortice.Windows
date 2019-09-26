// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice.Direct3D12
{
    public static partial class D3D12
    {
        /// <summary>
        /// Test to create <see cref="ID3D12Device"/> without actually creating it.
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="minFeatureLevel"></param>
        /// <returns></returns>
        public static Result D3D12CreateDevice(IUnknown adapter, FeatureLevel minFeatureLevel)
        {
            return D3D12CreateDevice(adapter, minFeatureLevel, typeof(ID3D12Device).GUID, out var nativePtr);
        }

        public static Result D3D12CreateDevice(IUnknown adapter, FeatureLevel minFeatureLevel, out ID3D12Device device)
        {
            var result = D3D12CreateDevice(adapter, minFeatureLevel, typeof(ID3D12Device).GUID, out var nativePtr);
            if (result.Failure)
            {
                device = null;
                return result;
            }

            device = new ID3D12Device(nativePtr);
            return result;
        }

        internal static unsafe Result D3D12CreateDeviceNoDevice(IUnknown adapter, FeatureLevel minFeatureLevel)
        {
            var adapterPtr = CppObject.ToCallbackPtr<IUnknown>(adapter);
            var riid = typeof(ID3D12Device).GUID;
            var result = D3D12CreateDevice_((void*)adapterPtr, (int)minFeatureLevel, &riid, null);
            GC.KeepAlive(adapter);
            return result;
        }

        public static Result D3D12GetDebugInterface<T>(out T debugInterface) where T : ComObject
        {
            var result = D3D12GetDebugInterface(typeof(T).GUID, out var nativePtr);

            if (result.Failure)
            {
                debugInterface = null;
                return result;
            }

            debugInterface = CppObject.FromPointer<T>(nativePtr);
            return result;
        }

        public static string D3D12SerializeVersionedRootSignature(VersionedRootSignatureDescription description, out Blob blob)
        {
            var result = D3D12SerializeVersionedRootSignature(description, out blob, out Blob errorBlob);
            if (result.Failure)
            {
                return errorBlob.ConvertToString();
            }

            return string.Empty;
        }

        public static readonly Guid D3D12ExperimentalShaderModels = new Guid("76f5573e-f13a-40f5-b297-81ce9e18933f");
        public static readonly Guid D3D12TiledResourceTier4 = new Guid("c9c4725f-a81a-4f56-8c5b-c51039d694fb");
        public static readonly Guid D3D12MetaCommand = new Guid("C734C97E-8077-48C8-9FDC-D9D1DD31DD77");
        public static readonly Guid D3D12ComputeOnlyDevices = new Guid("50f7ab08-4b6d-4e14-89a5-5d16cd272594");

        public static void D3D12EnableExperimentalFeatures(params Guid[] features)
        {
            D3D12EnableExperimentalFeatures(features.Length, features, IntPtr.Zero, null);
        }
    }
}
