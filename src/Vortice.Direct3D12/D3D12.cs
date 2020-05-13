// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D12
{
    public static partial class D3D12
    {
        public static readonly FeatureLevel[] FeatureLevels = new[]
        {
            FeatureLevel.Level_12_1,
            FeatureLevel.Level_12_0,
            FeatureLevel.Level_11_1,
            FeatureLevel.Level_11_0
        };

        public static readonly Guid ExperimentalShaderModels = new Guid("76f5573e-f13a-40f5-b297-81ce9e18933f");
        public static readonly Guid TiledResourceTier4 = new Guid("c9c4725f-a81a-4f56-8c5b-c51039d694fb");
        public static readonly Guid MetaCommand = new Guid("C734C97E-8077-48C8-9FDC-D9D1DD31DD77");
        public static readonly Guid ComputeOnlyDevices = new Guid("50f7ab08-4b6d-4e14-89a5-5d16cd272594");

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetMaxSupportedFeatureLevel(FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            ID3D12Device device = null;

            try
            {
                D3D12CreateDevice(null, minFeatureLevel, out device);
                return device.CheckMaxSupportedFeatureLevel(FeatureLevels);
            }
            catch
            {
                return FeatureLevel.Level_9_1;
            }
            finally
            {
                device?.Dispose();
            }
        }

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <param name="adapter">The <see cref="IDXGIAdapter"/>.</param>
        /// <param name="minFeatureLevel">Thje</param>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetMaxSupportedFeatureLevel(IDXGIAdapter adapter, FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            ID3D12Device device = null;

            try
            {
                D3D12CreateDevice(adapter, minFeatureLevel, out device);
                return device.CheckMaxSupportedFeatureLevel(FeatureLevels);
            }
            catch
            {
                return FeatureLevel.Level_9_1;
            }
            finally
            {
                device?.Dispose();
            }
        }

        /// <summary>
        /// Checks whether Direct3D12 is supported with default adapter and minimum feature level.
        /// </summary>
        /// <param name="minFeatureLevel">Minimum feature level.</param>
        /// <returns>True if supported, false otherwise.</returns>
        public static bool IsSupported(FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            try
            {
                return D3D12CreateDeviceNoDevice(null, minFeatureLevel).Success;
            }
            catch (DllNotFoundException)
            {
                // On pre Windows 10 d3d12.dll is not present and therefore not supported.
                return false;
            }
        }

        /// <summary>
        /// Checks whether Direct3D12 is supported with given adapter and minimum feature level.
        /// </summary>
        /// <param name="adapter">The <see cref="IDXGIAdapter"/> to use.</param>
        /// <param name="minFeatureLevel">Minimum feature level.</param>
        /// <returns>True if supported, false otherwise.</returns>
        public static bool IsSupported(IDXGIAdapter adapter, FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            try
            {
                return D3D12CreateDeviceNoDevice(adapter, minFeatureLevel).Success;
            }
            catch (DllNotFoundException)
            {
                // On pre Windows 10 d3d12.dll is not present and therefore not supported.
                return false;
            }
        }

        public static Result D3D12CreateDevice(IDXGIAdapter adapter, out ID3D12Device device)
        {
            return D3D12CreateDevice(adapter, FeatureLevel.Level_11_0, out device);
        }

        public static Result D3D12CreateDevice(IDXGIAdapter adapter, FeatureLevel minFeatureLevel, out ID3D12Device device)
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

        internal static unsafe Result D3D12CreateDeviceNoDevice(IDXGIAdapter adapter, FeatureLevel minFeatureLevel)
        {
            var adapterPtr = CppObject.ToCallbackPtr<IDXGIAdapter>(adapter);
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
            var errorString = string.Empty;
            if (D3D12SerializeVersionedRootSignature(description, out blob, out var errorBlob).Failure)
            {
                errorString = errorBlob.ConvertToString();
            }

            errorBlob?.Dispose();
            return errorString;
        }

        public static void D3D12EnableExperimentalFeatures(params Guid[] features)
        {
            D3D12EnableExperimentalFeatures(features.Length, features, IntPtr.Zero, null);
        }
    }
}
