using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Device
    {
        public static bool TryCreate(
            IDXGIAdapter adapter,
            DriverType driverType,
            DeviceCreationFlags flags,
            FeatureLevel[] featureLevels,
            out ID3D11Device device,
            out ID3D11DeviceContext immediateContext)
        {
            FeatureLevel featureLevelRef;
            var result = D3D11.CreateDevice(
                adapter,
                driverType,
                IntPtr.Zero,
                (uint)flags,
                featureLevels,
                (featureLevels != null) ? (uint)featureLevels.Length : 0,
                D3D11.SdkVersion,
                out device,
                out featureLevelRef,
                out immediateContext);

            if (result.Failure)
            {
                return false;
            }

            if (immediateContext != null)
            {
                device.AddRef();
                device.ImmediateContext__ = immediateContext;
                immediateContext.Device__ = device;
            }

            return true;
        }

        public bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : struct
        {
            unsafe
            {
                return CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), (uint)Interop.SizeOf<T>()).Success;
            }
        }


    }
}
