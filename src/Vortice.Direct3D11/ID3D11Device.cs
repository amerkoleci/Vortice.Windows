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
        public static Result TryCreate(
            IDXGIAdapter adapter,
            DriverType driverType,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            out ID3D11Device device,
            out ID3D11DeviceContext immediateContext)
        {
            var result = D3D11.CreateDevice(
                adapter,
                driverType,
                IntPtr.Zero,
                (int)flags,
                featureLevels,
                (featureLevels != null) ? featureLevels.Length : 0,
                D3D11.SdkVersion,
                out device,
                out FeatureLevel featureLevel,
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

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : struct
        {
            return CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), Interop.SizeOf<T>()).Success;
        }
    }
}
