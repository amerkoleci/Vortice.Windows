// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DDevice9Ex
    {
        public IDirect3DSurface9 CreateRenderTargetEx(int width, int height, Format format, MultisampleType multiSample, int multisampleQuality, bool lockable, Usage usage)
        {
            return CreateRenderTargetEx_(width, height, format, multiSample, multisampleQuality, lockable, IntPtr.Zero, usage);
        }

        public unsafe IDirect3DSurface9 CreateRenderTargetEx(int width, int height, Format format, MultisampleType multiSample, int multisampleQuality, bool lockable, ref IntPtr sharedHandle, Usage usage)
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

        public unsafe IDirect3DSurface9 CreateDepthStencilSurfaceEx(int width, int height, Format format, MultisampleType multiSample, int multisampleQuality, bool discard, ref IntPtr sharedHandle, Usage usage)
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

        public unsafe IDirect3DSurface9 CreateOffscreenPlainSurfaceEx(int width, int height, Format format, Pool pool, ref IntPtr sharedHandle, Usage usage)
        {
            fixed (void* pSharedHandle = &sharedHandle)
            {
                return CreateOffscreenPlainSurfaceEx_(width, height, format, pool, new IntPtr(pSharedHandle), usage);
            }
        }

        public unsafe void PresentEx(Present flags)
        {
            PresentEx(null, null, IntPtr.Zero, null, (int)flags);
        }

        public void PresentEx(Present flags, RawRect sourceRectangle, RawRect destinationRectangle)
        {
            PresentEx(flags, sourceRectangle, destinationRectangle, IntPtr.Zero);
        }

        public unsafe void PresentEx(Present flags, RawRect sourceRectangle, RawRect destinationRectangle, IntPtr windowOverride)
        {
            PresentEx(&sourceRectangle, &destinationRectangle, windowOverride, null, (int)flags);
        }

        public unsafe void PresentEx(Present flags, RawRect sourceRectangle, RawRect destinationRectangle, IntPtr windowOverride, IntPtr dirtyRegionRGNData)
        {
            PresentEx(&sourceRectangle, &destinationRectangle, windowOverride, dirtyRegionRGNData.ToPointer(), (int)flags);
        }

        public unsafe void ResetEx(ref PresentParameters presentationParameters)
        {
            ResetEx(ref presentationParameters, null);
        }

        public unsafe void ResetEx(ref PresentParameters presentationParameters, DisplayModeEx fullScreenDisplayMode)
        {
            ResetEx(ref presentationParameters, &fullScreenDisplayMode);
        }
    }
}
