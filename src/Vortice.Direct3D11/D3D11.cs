// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.DXGI;
using Vortice.Direct3D;
using SharpGen.Runtime;

namespace Vortice.Direct3D11
{
    public static partial class D3D11
    {
        public static Result D3D11CreateDevice(IDXGIAdapter adapter,
            DriverType driverType,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            out ID3D11Device device)
        {
            return D3D11CreateDevice(adapter, driverType, flags, featureLevels, out device, out var featureLevel, out var context);
        }

        public static Result D3D11CreateDevice(IDXGIAdapter adapter,
            DriverType driverType,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            out ID3D11Device device,
            out ID3D11DeviceContext immediateContext)
        {
            return D3D11CreateDevice(adapter, driverType, flags, featureLevels, out device, out var featureLevel, out immediateContext);
        }

        public static Result D3D11CreateDevice(IDXGIAdapter adapter,
            DriverType driverType,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            out ID3D11Device device,
            out FeatureLevel featureLevel,
            out ID3D11DeviceContext immediateContext)
        {
            var result = D3D11CreateDevice(adapter, driverType, IntPtr.Zero,
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
                device.ImmediateContext__ = immediateContext;
                immediateContext.shouldNotDisposeDevice = true;
                immediateContext.Device__ = device;
            }

            return result;
        }

        public unsafe static Result D3D11On12CreateDevice(
            IUnknown d3d12Device,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            IUnknown[] commandQueues,
            int nodeMask,
            out ID3D11Device device,
            out ID3D11DeviceContext immediateContext,
            out FeatureLevel chosenFeatureLevel)
        {
            var result = D3D11On12CreateDevice(d3d12Device,
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
                device.ImmediateContext__ = immediateContext;
                immediateContext.shouldNotDisposeDevice = true;
                immediateContext.Device__ = device;
            }

            return result;
        }

        /// <summary>
        /// Check if a feature level is supported by a primary adapter.
        /// </summary>
        /// <param name="featureLevel">The feature level.</param>
        /// <returns><c>true</c> if the primary adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
        public static bool IsSupportedFeatureLevel(FeatureLevel featureLevel)
        {
            ID3D11Device device = null;
            ID3D11DeviceContext context = null;

            try
            {
                var result = D3D11CreateDevice(
                    null,
                    DriverType.Hardware,
                    IntPtr.Zero,
                    0,
                    new[] { featureLevel }, 1,
                    SdkVersion, 
                    out device, out var outputLevel, out context);
                return result.Success && outputLevel == featureLevel;
            }
            finally
            {
                context?.Dispose();
                device?.Dispose();
            }
        }

        /// <summary>
        /// Check if a feature level is supported by a particular adapter.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        /// <param name="featureLevel">The feature level.</param>
        /// <returns><c>true</c> if the specified adapter is supporting this feature level; otherwise, <c>false</c>.</returns>
        public static bool IsSupportedFeatureLevel(IDXGIAdapter adapter, FeatureLevel featureLevel)
        {
            ID3D11Device device = null;
            ID3D11DeviceContext context = null;

            try
            {
                var result = D3D11CreateDevice(
                    adapter,
                    DriverType.Unknown,
                    IntPtr.Zero,
                    0,
                    new[] { featureLevel }, 1,
                    SdkVersion,
                    out device, out var outputLevel, out context);
                return result.Success && outputLevel == featureLevel;
            }
            finally
            {
                context?.Dispose();
                device?.Dispose();
            }
        }

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetSupportedFeatureLevel()
        {
            var featureLevel = FeatureLevel.Level_9_1;
            ID3D11Device device = null;
            ID3D11DeviceContext context = null;

            try
            {
                D3D11CreateDevice(
                    null,
                    DriverType.Hardware,
                    IntPtr.Zero,
                    0,
                    null, 0,
                    SdkVersion,
                    out device, out featureLevel, out context);
            }
            finally
            {
                context?.Dispose();
                device?.Dispose();
            }

            return featureLevel;
        }

        /// <summary>
        /// Gets the highest supported hardware feature level of the primary adapter.
        /// </summary>
        /// <param name="adapter">The <see cref="IDXGIAdapter"/>.</param>
        /// <returns>The highest supported hardware feature level.</returns>
        public static FeatureLevel GetSupportedFeatureLevel(IDXGIAdapter adapter)
        {
            var featureLevel = FeatureLevel.Level_9_1;
            ID3D11Device device = null;
            ID3D11DeviceContext context = null;

            try
            {
                D3D11CreateDevice(
                    adapter,
                    DriverType.Unknown,
                    IntPtr.Zero,
                    0,
                    null, 0,
                    SdkVersion,
                    out device, out featureLevel, out context);
            }
            finally
            {
                context?.Dispose();
                device?.Dispose();
            }

            return featureLevel;
        }
    }
}
