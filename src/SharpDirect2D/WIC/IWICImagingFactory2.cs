// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpDXGI;

namespace SharpDirect2D.WIC
{
    public partial class IWICImagingFactory2
    {
        public IWICImagingFactory2()
        {
            ComUtilities.CreateComInstance(
                WICImagingFactoryClsid,
                ComUtilities.CLSCTX.ClsctxInprocServer,
                typeof(IWICImagingFactory2).GUID,
                this);
        }
    }
}
