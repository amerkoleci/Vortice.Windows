// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIDevice
    {
        public IDXGISurface CreateSurface(IntPtr sharedResource)
        {
            if (sharedResource == IntPtr.Zero)
                throw new ArgumentNullException(nameof(sharedResource), "Invalid shared resource handle");

            return CreateSurface(null, 1, 0, new SharedResource { Handle = sharedResource });
        }

        public IDXGISurface CreateSurface(SurfaceDescription description, int numSurfaces, Usage usage)
        {
            return CreateSurface(description, numSurfaces, (int)usage, null);
        }

        public Result QueryResourceResidency(IUnknown[] resources, Residency[] residencyStatus)
        {
            return QueryResourceResidency(resources, residencyStatus, resources.Length);
        }
    }
}
