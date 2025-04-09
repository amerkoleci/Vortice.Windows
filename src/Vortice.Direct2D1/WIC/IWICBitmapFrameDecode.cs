// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public partial class IWICBitmapFrameDecode
{
    /// <summary>
    /// Get the <see cref="IWICColorContext"/> of the image (if any)
    /// </summary>
    /// <param name="imagingFactory">The factory for creating new color contexts</param>
    /// <param name="colorContexts">The color context array, or null</param>
    /// <remarks>
    /// When the image format does not support color contexts,
    /// <see cref="ResultCode.UnsupportedOperation"/> is returned.
    /// </remarks>
    /// <unmanaged>HRESULT IWICBitmapDecoder::GetColorContexts([In] unsigned int cCount,[Out, Buffer, Optional] IWICColorContext** ppIColorContexts,[Out] unsigned int* pcActualCount)</unmanaged>	
    public Result TryGetColorContexts(IWICImagingFactory imagingFactory, out IWICColorContext[] colorContexts)
    {
        return ColorContextsHelper.TryGetColorContexts(GetColorContexts, imagingFactory, out colorContexts);
    }

    /// <summary>
    /// Get the <see cref="IWICColorContext"/> of the image (if any)
    /// </summary>
    /// <returns>
    /// null if the decoder does not support color contexts;
    /// otherwise an array of zero or more ColorContext objects
    /// </returns>
    /// <unmanaged>HRESULT IWICBitmapDecoder::GetColorContexts([In] unsigned int cCount,[Out, Buffer, Optional] IWICColorContext** ppIColorContexts,[Out] </unmanaged>
    public IWICColorContext[] TryGetColorContexts(IWICImagingFactory imagingFactory)
    {
        return ColorContextsHelper.TryGetColorContexts(GetColorContexts, imagingFactory);
    }
}
