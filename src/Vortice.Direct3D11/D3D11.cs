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
            return D3D11CreateDevice(adapter, driverType, flags, featureLevels, out device, out var featureLevel, out var immediateContext);
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
                device.AddRef();
                device.ImmediateContext__ = immediateContext;
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
                device.AddRef();
                device.ImmediateContext__ = immediateContext;
                immediateContext.Device__ = device;
            }

            return result;
        }
    }
}
