// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IVirtualSurfaceImageSourceNative
{
    /// <summary>
    /// Gets the set of regions that must be updated on the shared surface.
    /// </summary>
    public RawRect[] UpdateRectangles
    {
        get
        {
            if (GetUpdateRectCount(out int count).Failure)
            {
                return Array.Empty<RawRect>();
            }

            RawRect[] regionToUpdate = new RawRect[count];
            GetUpdateRects(regionToUpdate, count);
            return regionToUpdate;
        }
    }
}
