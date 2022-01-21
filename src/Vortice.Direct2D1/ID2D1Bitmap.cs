// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct2D1;

public unsafe partial class ID2D1Bitmap
{
    public Size Dpi
    {
        get
        {
            GetDpi(out float dpiX, out float dpiY);
            return new(dpiX, dpiY);
        }
    }

    public void CopyFromBitmap(ID2D1Bitmap sourceBitmap)
    {
        CopyFromBitmap(null, sourceBitmap, null);
    }

    public void CopyFromBitmap(PointI destinationPoint, ID2D1Bitmap sourceBitmap)
    {
        CopyFromBitmap(destinationPoint, sourceBitmap, null);
    }

    public void CopyFromBitmap(PointI destinationPoint, ID2D1Bitmap sourceBitmap, RectangleI sourceArea)
    {
        RawRect rawSourceArea = sourceArea;
        CopyFromBitmap(destinationPoint, sourceBitmap, rawSourceArea);
    }

    public void CopyFromMemory(IntPtr pointer, int pitch)
    {
        CopyFromMemory(null, pointer, pitch);
    }

    public unsafe void CopyFromMemory(byte[] data, int pitch)
    {
        fixed (void* dataPtr = &data[0])
        {
            CopyFromMemory(null, new IntPtr(dataPtr), pitch);
        }
    }

    public void CopyFromMemory<T>(T[] data, int pitch) where T : unmanaged
    {
        fixed (void* dataPtr = data)
        {
            CopyFromMemory(null, (IntPtr)dataPtr, pitch);
        }
    }

    public void CopyFromMemory<T>(Span<T> data, int pitch) where T : unmanaged
    {
        fixed (void* dataPtr = data)
        {
            CopyFromMemory(null, new IntPtr(dataPtr), pitch);
        }
    }

    public void CopyFromMemory(RectangleI destinationArea, byte[] data, int pitch)
    {
        fixed (void* dataPtr = &data[0])
        {
            RawRect rawDestinationArea = destinationArea;
            CopyFromMemory(rawDestinationArea, new IntPtr(dataPtr), pitch);
        }
    }

    public void CopyFromMemory<T>(RectangleI destinationArea, T[] data, int pitch) where T : unmanaged
    {
        fixed (void* dataPtr = data)
        {
            RawRect rawDestinationArea = destinationArea;
            CopyFromMemory(destinationArea, (IntPtr)dataPtr, pitch);
        }
    }

    public void CopyFromMemory<T>(RectangleI destinationArea, Span<T> data, int pitch) where T : unmanaged
    {
        fixed (void* dataPtr = data)
        {
            RawRect rawDestinationArea = destinationArea;
            CopyFromMemory(rawDestinationArea, new IntPtr(dataPtr), pitch);
        }
    }

    public void CopyFromRenderTarget(ID2D1RenderTarget renderTarget)
    {
        CopyFromRenderTarget(null, renderTarget, null);
    }

    public void CopyFromRenderTarget(PointI destinationPoint, ID2D1RenderTarget renderTarget)
    {
        CopyFromRenderTarget(destinationPoint, renderTarget, null);
    }

    public void CopyFromRenderTarget(PointI destinationPoint, ID2D1RenderTarget renderTarget, RectangleI sourceArea)
    {
        RawRect rawSourceArea = sourceArea;
        CopyFromRenderTarget(destinationPoint, renderTarget, rawSourceArea);
    }
}
