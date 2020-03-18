// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Device2
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ImmediateContext2__ != null)
                {
                    ImmediateContext2__.Dispose();
                    ImmediateContext2__ = null;
                }
            }

            base.Dispose(disposing);
        }

        public unsafe ID3D11DeviceContext2 CreateDeferredContext2()
        {
            return CreateDeferredContext2(0);
        }

        public int CheckMultisampleQualityLevels1(Format format, int sampleCount)
        {
            return CheckMultisampleQualityLevels1(format, sampleCount, CheckMultisampleQualityLevelsFlags.TiledResource);
        }
    }
}
