// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    public static partial class D3D11
    {
        public static Result D3D11CreateDevice(IDXGIAdapter adapter, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
            out ID3D11Device? device)
        {
            return RawD3D11CreateDeviceNoContext(
                adapter != null ? adapter.NativePointer : IntPtr.Zero,
                driverType,
                flags,
                featureLevels,
                out device,
                out _);
        }

        public static Result D3D11CreateDevice(IDXGIAdapter adapter, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
            out ID3D11Device device, out ID3D11DeviceContext immediateContext)
        {
            return D3D11CreateDevice(adapter, driverType, flags, featureLevels, out device, out _, out immediateContext);
        }

        public static Result D3D11CreateDevice(IDXGIAdapter adapter, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
            out ID3D11Device device, out FeatureLevel featureLevel, out ID3D11DeviceContext immediateContext)
        {
            Result result = D3D11CreateDevice(
                adapter != null ? adapter.NativePointer : IntPtr.Zero,
                driverType,
                IntPtr.Zero,
                (int)flags,
                featureLevels,
                (featureLevels != null) ? featureLevels.Length : 0,
                SdkVersion,
                out device,
                out featureLevel,
                out immediateContext);

            if (result.Failure)
            {
                return result;
            }

            if (immediateContext != null)
            {
                device.AddRef();
                device.ImmediateContext__ = immediateContext;
                immediateContext.Device__ = device;
            }

            return result;
        }

        public static Result D3D11CreateDevice(IntPtr adapterPtr, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
            out ID3D11Device? device)
        {
            return RawD3D11CreateDeviceNoContext(
                adapterPtr,
                driverType,
                flags,
                featureLevels,
                out device,
                out _);
        }

        public static Result D3D11CreateDevice(IntPtr adapterPtr, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
            out ID3D11Device device, out ID3D11DeviceContext immediateContext)
        {
            return D3D11CreateDevice(adapterPtr, driverType, flags, featureLevels, out device, out _, out immediateContext);
        }

        public static Result D3D11CreateDevice(IntPtr adapterPtr, DriverType driverType, DeviceCreationFlags flags, FeatureLevel[] featureLevels,
            out ID3D11Device device, out FeatureLevel featureLevel, out ID3D11DeviceContext immediateContext)
        {
            Result result = D3D11CreateDevice(adapterPtr, driverType, IntPtr.Zero,
                (int)flags,
                featureLevels,
                (featureLevels != null) ? featureLevels.Length : 0,
                SdkVersion,
                out device,
                out featureLevel,
                out immediateContext);

            if (result.Failure)
            {
                return result;
            }

            if (immediateContext != null)
            {
                device.AddRef();
                device.ImmediateContext__ = immediateContext;
                immediateContext.Device__ = device;
            }

            return result;
        }

        public static unsafe Result D3D11On12CreateDevice(
            IUnknown d3d12Device,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            IUnknown[] commandQueues,
            int nodeMask,
            out ID3D11Device device,
            out ID3D11DeviceContext immediateContext,
            out FeatureLevel chosenFeatureLevel)
        {
            Result result = D3D11On12CreateDevice(d3d12Device,
                flags,
                featureLevels, featureLevels.Length,
                commandQueues, commandQueues.Length,
                nodeMask,
                out device, out immediateContext, out chosenFeatureLevel);

            if (result.Failure)
            {
                return result;
            }

            if (immediateContext != null)
            {
                device.AddRef();
                device.ImmediateContext__ = immediateContext;
                immediateContext.Device__ = device;
            }

            return result;
        }

        /// <summary>
        /// Check if a feature level is supported by a primary adapter.
        /// </summary>
        /// <param name="featureLevel">The feature level.</param>
        /// <param name="flags">The <see cref="DeviceCreationFlags"/> flags.</param>
        /// <returns><c>true</c> if the primary adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
        public static bool IsSupportedFeatureLevel(FeatureLevel featureLevel, DeviceCreationFlags flags = DeviceCreationFlags.None)
        {
            Result result = RawD3D11CreateDeviceNoDeviceAndContext(
                IntPtr.Zero,
                DriverType.Hardware,
                flags,
                new[] { featureLevel },
                out FeatureLevel outputLevel);
            return result.Success && outputLevel == featureLevel;
        }

        /// <summary>
        /// Check if a feature level is supported by a particular adapter.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        /// <param name="featureLevel">The feature level.</param>
        /// <param name="flags">The <see cref="DeviceCreationFlags"/> flags.</param>
        /// <returns><c>true</c> if the specified adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
        public static unsafe bool IsSupportedFeatureLevel(
            IDXGIAdapter adapter,
            FeatureLevel featureLevel,
            DeviceCreationFlags flags = DeviceCreationFlags.None)
        {
            if (adapter == null)
                throw new ArgumentNullException(nameof(adapter), "Invalid adapter");

            Result result = RawD3D11CreateDeviceNoDeviceAndContext(
                adapter.NativePointer,
                DriverType.Unknown,
                flags,
                new[] { featureLevel },
                out FeatureLevel outputLevel);
            return result.Success && outputLevel == featureLevel;
        }

        /// <summary>
        /// Check if a feature level is supported by a particular adapter.
        /// </summary>
        /// <param name="adapterPtr">The native handle of <see cref="IDXGIAdapter"/>.</param>
        /// <param name="featureLevel">The feature level.</param>
        /// <param name="flags">The <see cref="DeviceCreationFlags"/> flags.</param>
        /// <returns><c>true</c> if the specified adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
        public static unsafe bool IsSupportedFeatureLevel(
            IntPtr adapterPtr,
            FeatureLevel featureLevel,
            DeviceCreationFlags flags = DeviceCreationFlags.None)
        {
            if (adapterPtr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(adapterPtr), "Invalid adapter handle");

            Result result = RawD3D11CreateDeviceNoDeviceAndContext(
                adapterPtr,
                DriverType.Unknown,
                flags,
                new[] { featureLevel },
                out FeatureLevel outputLevel);
            return result.Success && outputLevel == featureLevel;
        }

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetSupportedFeatureLevel()
        {
            RawD3D11CreateDeviceNoDeviceAndContext(
                IntPtr.Zero,
                DriverType.Hardware,
                DeviceCreationFlags.None,
                null,
                out FeatureLevel featureLevel);
            return featureLevel;
        }

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <param name="adapter">The <see cref="IDXGIAdapter"/>.</param>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetSupportedFeatureLevel(IDXGIAdapter adapter)
        {
            if (adapter == null)
                throw new ArgumentNullException(nameof(adapter), "Invalid adapter");

            RawD3D11CreateDeviceNoDeviceAndContext(
                adapter.NativePointer,
                DriverType.Unknown,
                DeviceCreationFlags.None,
                null,
                out FeatureLevel featureLevel);
            return featureLevel;
        }

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <param name="adapterPtr">The native handle of <see cref="IDXGIAdapter"/>.</param>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetSupportedFeatureLevel(IntPtr adapterPtr)
        {
            if (adapterPtr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(adapterPtr), "Invalid adapter handle");

            RawD3D11CreateDeviceNoDeviceAndContext(
                adapterPtr,
                DriverType.Unknown,
                DeviceCreationFlags.None,
                null,
                out FeatureLevel featureLevel);
            return featureLevel;
        }

        /// <summary>
        /// Check for SDK Layer support.
        /// </summary>
        /// <returns>True if available, false otherwise.</returns>
        public static bool SdkLayersAvailable()
        {
            unsafe
            {
                Result result = D3D11CreateDevice_(
                    null, (int)DriverType.Null,
                    null, (int)DeviceCreationFlags.Debug,
                    null, 0,
                    SdkVersion,
                    null, null, null);
                return result.Success;
            }
        }

        #region RawD3D11CreateDevice Methods
        private static Result RawD3D11CreateDeviceNoContext(
            IntPtr adapterPtr,
            DriverType driverType,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            out ID3D11Device? device,
            out FeatureLevel featureLevel)
        {
            unsafe
            {
                device = default;
                fixed (void* featureLevelsPtr = &featureLevels[0])
                fixed (void* featureLevelPtr = &featureLevel)
                {
                    IntPtr devicePtr = IntPtr.Zero;
                    Result result = D3D11CreateDevice_(
                        (void*)adapterPtr,
                        (int)driverType,
                        null,
                        (int)flags,
                        featureLevels != null && featureLevels.Length > 0 ? featureLevelsPtr : null,
                        featureLevels?.Length ?? 0,
                        SdkVersion,
                        &devicePtr,
                        featureLevelPtr,
                        null);

                    if (result.Success && devicePtr != IntPtr.Zero)
                    {
                        device = new ID3D11Device(devicePtr);
                    }
                    return result;
                }
            }
        }

        private static unsafe Result RawD3D11CreateDeviceNoDeviceAndContext(
            IntPtr adapterPtr,
            DriverType driverType,
            DeviceCreationFlags flags,
            FeatureLevel[]? featureLevels,
            out FeatureLevel featureLevel)
        {
            if (featureLevels != null && featureLevels.Length > 0)
            {
                fixed (FeatureLevel* featureLevelsPtr = &featureLevels[0])
                fixed (void* featureLevelPtr = &featureLevel)
                {
                    Result result = D3D11CreateDevice_(
                        (void*)adapterPtr,
                        (int)driverType,
                        null,
                        (int)flags,
                        featureLevelsPtr,
                        featureLevels.Length,
                        SdkVersion,
                        null,
                        featureLevelPtr,
                        null);
                    return result;
                }
            }
            else
            {
                fixed (void* featureLevelPtr = &featureLevel)
                {
                    Result result = D3D11CreateDevice_(
                        (void*)adapterPtr,
                        (int)driverType,
                        null,
                        (int)flags,
                        null,
                        0,
                        SdkVersion,
                        null,
                        featureLevelPtr,
                        null);
                    return result;
                }
            }
        }
        #endregion
    }
}
