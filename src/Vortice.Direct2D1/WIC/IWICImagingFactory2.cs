// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DirectX;
using SharpGen.Runtime;

namespace Vortice.WIC
{
    public partial class IWICImagingFactory2
    {
        public IWICImagingFactory2()
        {
            ComUtilities.CreateComInstance(
                WICImagingFactoryClsid,
                ComContext.InprocServer,
                typeof(IWICImagingFactory2).GUID,
                this);
        }
    }
}
