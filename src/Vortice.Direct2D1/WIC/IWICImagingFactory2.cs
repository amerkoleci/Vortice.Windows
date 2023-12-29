// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

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
