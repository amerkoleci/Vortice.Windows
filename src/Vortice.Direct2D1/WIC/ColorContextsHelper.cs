// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;

namespace Vortice.WIC;

internal static class ColorContextsHelper
{
    public delegate Result ColorContextsProvider(uint count, IWICColorContext[]? colorContexts, out uint actualCountRef);

    public static Result TryGetColorContexts(
        ColorContextsProvider getColorContexts,
        IWICImagingFactory imagingFactory,
        out IWICColorContext[]? colorContexts)
    {
        colorContexts = default;

        Result result = getColorContexts(0, default, out uint count);

        if (result.Success)
        {
            colorContexts = new IWICColorContext[(int)count];

            if (count > 0)
            {
                // http://msdn.microsoft.com/en-us/library/windows/desktop/ee690135(v=vs.85).aspx
                // The ppIColorContexts array must be filled with valid data: each IWICColorContext* in the array must 
                // have been created using IWICImagingFactory::CreateColorContext.

                for (uint i = 0; i < count; i++)
                {
                    colorContexts[i] = imagingFactory.CreateColorContext();
                }

                getColorContexts(count, colorContexts, out uint actualCount);
                Debug.Assert(count == actualCount);
            }
        }

        return result;
    }

    public static IWICColorContext[] TryGetColorContexts(ColorContextsProvider getColorContexts, IWICImagingFactory imagingFactory)
    {
        Result result = TryGetColorContexts(getColorContexts, imagingFactory, out IWICColorContext[]? colorContexts);

        if (ResultCode.UnsupportedOperation != result)
            result.CheckError();

        return colorContexts;
    }

    public static IWICColorContext[] GetColorContexts(ColorContextsProvider getColorContexts, IWICImagingFactory imagingFactory)
    {
        Result result = TryGetColorContexts(getColorContexts, imagingFactory, out IWICColorContext[]? colorContexts);
        result.CheckError();
        return colorContexts;
    }
}
