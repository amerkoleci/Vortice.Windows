// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIDevice
{
    public int GPUThreadPriority
    {
        get
        {
            GetGPUThreadPriority(out int priority).CheckError();
            return priority;
        }
        set
        {
            SetGPUThreadPriority(value).CheckError();
        }
    }

    public IDXGIAdapter GetAdapter()
    {
        GetAdapter(out IDXGIAdapter adapter).CheckError();
        return adapter;
    }

    public IDXGISurface CreateSurface(IntPtr sharedResource)
    {
        if (sharedResource == IntPtr.Zero)
            throw new ArgumentNullException(nameof(sharedResource), "Invalid shared resource handle");

        return CreateSurface(null, 1, 0, new SharedResource { Handle = sharedResource });
    }

    public IDXGISurface CreateSurface(SurfaceDescription description, uint numSurfaces, Usage usage)
    {
        return CreateSurface(description, numSurfaces, (uint)usage, null);
    }

    public Result QueryResourceResidency(IUnknown[] resources, Residency[] residencyStatus)
    {
        return QueryResourceResidency(resources, residencyStatus, (uint)resources.Length);
    }
}
