// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;

namespace HelloDirect3D11
{
    public static class Program
    {
        private static readonly FeatureLevel[] _featureLevels = new FeatureLevel[]
        {
                    FeatureLevel.Level_11_1,
                    FeatureLevel.Level_11_0
        };

        public static void Main()
        {
            if (IDXGIFactory1.TryCreate(out var factory))
            {
                var adapter = factory.EnumerateAdapters();
                var result = ID3D11Device.TryCreate(
                     null,
                     DriverType.Hardware,
                     DeviceCreationFlags.None,
                     _featureLevels,
                     out var device,
                     out var context);
            }
        }
    }
}
