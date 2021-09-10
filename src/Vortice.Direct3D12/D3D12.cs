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

        public static readonly Guid ExperimentalShaderModels = new("76f5573e-f13a-40f5-b297-81ce9e18933f");
        public static readonly Guid TiledResourceTier4 = new("c9c4725f-a81a-4f56-8c5b-c51039d694fb");
        public static readonly Guid MetaCommand = new("C734C97E-8077-48C8-9FDC-D9D1DD31DD77");
        public static readonly Guid ComputeOnlyDevices = new("50f7ab08-4b6d-4e14-89a5-5d16cd272594");

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetMaxSupportedFeatureLevel(FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            ID3D12Device? device = null;

            try
            {
                D3D12CreateDevice(null, minFeatureLevel, out device).CheckError();
                return device!.CheckMaxSupportedFeatureLevel(FeatureLevels);
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
            ID3D12Device? device = null;

            try
            {
                D3D12CreateDevice(adapter, minFeatureLevel, out device).CheckError();
                return device!.CheckMaxSupportedFeatureLevel(FeatureLevels);
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
        /// <param name="adapterPtr">The native handle of <see cref="IDXGIAdapter"/>.</param>
        /// <param name="minFeatureLevel">Thje</param>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetMaxSupportedFeatureLevel(IntPtr adapterPtr, FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            ID3D12Device? device = null;

            try
            {
                D3D12CreateDevice(adapterPtr, minFeatureLevel, out device).CheckError();
                return device!.CheckMaxSupportedFeatureLevel(FeatureLevels);
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
                return D3D12CreateDeviceNoDevice(IntPtr.Zero, minFeatureLevel).Success;
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
            if (adapter == null)
                throw new ArgumentNullException(nameof(adapter));

            try
            {
                return D3D12CreateDeviceNoDevice(adapter.NativePointer, minFeatureLevel).Success;
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
        /// <param name="adapterPtr">The native handle of <see cref="IDXGIAdapter"/>.</param>
        /// <param name="minFeatureLevel">Minimum feature level.</param>
        /// <returns>True if supported, false otherwise.</returns>
        public static bool IsSupported(IntPtr adapterPtr, FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            if (adapterPtr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(adapterPtr));

            try
            {
                return D3D12CreateDeviceNoDevice(adapterPtr, minFeatureLevel).Success;
            }
            catch (DllNotFoundException)
            {
                // On pre Windows 10 d3d12.dll is not present and therefore not supported.
                return false;
            }
        }

        public static Result D3D12CreateDevice<T>(IDXGIAdapter? adapter, out T? device) where T : ID3D12Device
        {
            return D3D12CreateDevice(adapter, FeatureLevel.Level_11_0, out device);
        }

        public static Result D3D12CreateDevice<T>(IDXGIAdapter? adapter, FeatureLevel minFeatureLevel, out T? device) where T : ID3D12Device
        {
            Result result = D3D12CreateDevice(
                adapter != null ? adapter.NativePointer : IntPtr.Zero,
                minFeatureLevel,
                typeof(T).GUID,
                out IntPtr nativePtr);

            if (result.Failure)
            {
                device = default;
                return result;
            }

            device = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        public static T D3D12CreateDevice<T>(IDXGIAdapter? adapter, FeatureLevel minFeatureLevel) where T : ID3D12Device
        {
            D3D12CreateDevice(
                adapter != null ? adapter.NativePointer : IntPtr.Zero,
                minFeatureLevel,
                typeof(T).GUID,
                out IntPtr nativePtr).CheckError();

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public static Result D3D12CreateDevice<T>(IntPtr adapterPtr, out T? device) where T : ID3D12Device
        {
            return D3D12CreateDevice(adapterPtr, FeatureLevel.Level_11_0, out device);
        }

        public static Result D3D12CreateDevice<T>(IntPtr adapterPtr, FeatureLevel minFeatureLevel, out T? device) where T : ID3D12Device
        {
            Result result = D3D12CreateDevice(adapterPtr, minFeatureLevel, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                device = default;
                return result;
            }

            device = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        public static T D3D12CreateDevice<T>(IntPtr adapterPtr, FeatureLevel minFeatureLevel) where T : ID3D12Device
        {
            D3D12CreateDevice(adapterPtr, minFeatureLevel, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        private static unsafe Result D3D12CreateDeviceNoDevice(IntPtr adapterPtr, FeatureLevel minFeatureLevel)
        {
            Guid riid = typeof(ID3D12Device).GUID;
            Result result = D3D12CreateDevice_((void*)adapterPtr, (int)minFeatureLevel, &riid, null);
            return result;
        }

        public static Result D3D12GetDebugInterface<T>(out T? debugInterface) where T : ComObject
        {
            Result result = D3D12GetDebugInterface(typeof(T).GUID, out IntPtr nativePtr);

            if (result.Failure)
            {
                debugInterface = null;
                return result;
            }

            debugInterface = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        public static T D3D12GetDebugInterface<T>() where T : ComObject
        {
            D3D12GetDebugInterface(typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public static string D3D12SerializeVersionedRootSignature(VersionedRootSignatureDescription description, out Blob blob)
        {
            string errorString = string.Empty;
            if (D3D12SerializeVersionedRootSignature(description, out blob, out Blob errorBlob).Failure)
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
