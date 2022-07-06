// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DDevice9Ex
{
    public IDirect3DSurface9 CreateRenderTargetEx(int width, int height, Format format, MultisampleType multiSample, int multisampleQuality, bool lockable, Usage usage)
    {
        return CreateRenderTargetEx_(width, height, format, multiSample, multisampleQuality, lockable, IntPtr.Zero, usage);
    }

    public IDirect3DSurface9 CreateRenderTargetEx(int width, int height, Format format, MultisampleType multiSample, int multisampleQuality, bool lockable, ref IntPtr sharedHandle, Usage usage)
    {
        fixed (void* pSharedHandle = &sharedHandle)
        {
            return CreateRenderTargetEx_(width, height, format, multiSample, multisampleQuality, lockable, new IntPtr(pSharedHandle), usage);
        }
    }

    public IDirect3DSurface9 CreateDepthStencilSurfaceEx(int width, int height, Format format, MultisampleType multiSample, int multisampleQuality, bool discard, Usage usage)
    {
        return CreateDepthStencilSurfaceEx_(width, height, format, multiSample, multisampleQuality, discard, IntPtr.Zero, usage);
    }

    public IDirect3DSurface9 CreateDepthStencilSurfaceEx(int width, int height, Format format, MultisampleType multiSample, int multisampleQuality, bool discard, ref IntPtr sharedHandle, Usage usage)
    {
        fixed (void* pSharedHandle = &sharedHandle)
        {
            return CreateDepthStencilSurfaceEx_(width, height, format, multiSample, multisampleQuality, discard, new IntPtr(pSharedHandle), usage);
        }
    }

    public IDirect3DSurface9 CreateOffscreenPlainSurfaceEx(int width, int height, Format format, Pool pool, Usage usage)
    {
        return CreateOffscreenPlainSurfaceEx_(width, height, format, pool, IntPtr.Zero, usage);
    }

    public IDirect3DSurface9 CreateOffscreenPlainSurfaceEx(int width, int height, Format format, Pool pool, ref IntPtr sharedHandle, Usage usage)
    {
        fixed (void* pSharedHandle = &sharedHandle)
        {
            return CreateOffscreenPlainSurfaceEx_(width, height, format, pool, new IntPtr(pSharedHandle), usage);
        }
    }

    public void PresentEx(Present flags)
    {
        PresentEx(null, null, IntPtr.Zero, null, (int)flags);
    }

    public void PresentEx(Present flags, Rect sourceRectangle, Rect destinationRectangle)
    {
        PresentEx(flags, sourceRectangle, destinationRectangle, IntPtr.Zero);
    }

    public void PresentEx(Present flags, Rect sourceRectangle, Rect destinationRectangle, IntPtr windowOverride)
    {
        PresentEx(&sourceRectangle, &destinationRectangle, windowOverride, null, (int)flags);
    }

    public void PresentEx(Present flags, Rect sourceRectangle, Rect destinationRectangle, IntPtr windowOverride, IntPtr dirtyRegionRGNData)
    {
        PresentEx(&sourceRectangle, &destinationRectangle, windowOverride, dirtyRegionRGNData.ToPointer(), (int)flags);
    }

    public void ResetEx(ref PresentParameters presentationParameters)
    {
        ResetEx(ref presentationParameters, null);
    }

    public void ResetEx(ref PresentParameters presentationParameters, DisplayModeEx fullScreenDisplayMode)
    {
        ResetEx(ref presentationParameters, &fullScreenDisplayMode);
    }

    public Result CheckResourceResidency(IDirect3DResource9[] resources)
    {
        return CheckResourceResidency(resources, resources.Length);
    }
}
