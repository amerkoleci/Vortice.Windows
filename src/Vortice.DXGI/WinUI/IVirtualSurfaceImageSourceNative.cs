// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI.WinUI
{
    public partial class IVirtualSurfaceImageSourceNative
    {
#if false
        /// <summary>
        /// Gets the set of regions that must be updated on the shared surface.
        /// </summary>
        public RawRect[] UpdateRectangles
        {
            get
            {
                if (GetUpdateRectCount(out int count).Failure)
                    return Array.Empty<RawRect>();

                var regionToUpdate = new RawRect[count];
                GetUpdateRects(regionToUpdate, count);
                return regionToUpdate;
            }
        }
#endif
    }
}
