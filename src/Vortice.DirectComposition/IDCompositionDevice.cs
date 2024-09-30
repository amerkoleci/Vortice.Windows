// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.DirectComposition;

public partial class IDCompositionDevice
{
    public bool CheckDeviceState()
    {
        CheckDeviceState(out RawBool result).CheckError();
        return result;
    }

    public IDCompositionAnimation CreateAnimation()
    {
        CreateAnimation(out IDCompositionAnimation animation).CheckError();
        return animation;
    }

    public IDCompositionEffectGroup CreateEffectGroup()
    {
        CreateEffectGroup(out IDCompositionEffectGroup effectGroup).CheckError();
        return effectGroup;
    }

    public IDCompositionMatrixTransform CreateMatrixTransform()
    {
        CreateMatrixTransform(out IDCompositionMatrixTransform matrixTransform).CheckError();
        return matrixTransform;
    }

    public IDCompositionMatrixTransform3D CreateMatrixTransform3D()
    {
        CreateMatrixTransform3D(out IDCompositionMatrixTransform3D matrixTransform).CheckError();
        return matrixTransform;
    }

    public IDCompositionRectangleClip CreateRectangleClip()
    {
        CreateRectangleClip(out IDCompositionRectangleClip clip).CheckError();
        return clip;
    }

    public IDCompositionRotateTransform CreateRotateTransform()
    {
        CreateRotateTransform(out IDCompositionRotateTransform rotateTransform).CheckError();
        return rotateTransform;
    }

    public IDCompositionRotateTransform3D CreateRotateTransform3D()
    {
        CreateRotateTransform3D(out IDCompositionRotateTransform3D rotateTransform).CheckError();
        return rotateTransform;
    }

    public IDCompositionScaleTransform CreateScaleTransform()
    {
        CreateScaleTransform(out IDCompositionScaleTransform scaleTransform).CheckError();
        return scaleTransform;
    }

    public IDCompositionScaleTransform3D CreateScaleTransform3D()
    {
        CreateScaleTransform3D(out IDCompositionScaleTransform3D scaleTransform).CheckError();
        return scaleTransform;
    }

    public IDCompositionSkewTransform CreateSkewTransform()
    {
        CreateSkewTransform(out IDCompositionSkewTransform skewTransform).CheckError();
        return skewTransform;
    }

    public IUnknown CreateSurfaceFromHandle(IntPtr handle)
    {
        CreateSurfaceFromHandle(handle, out IUnknown surface).CheckError();
        return surface;
    }

    public IUnknown CreateSurfaceFromHwnd(IntPtr hwnd)
    {
        CreateSurfaceFromHwnd(hwnd, out IUnknown surface).CheckError();
        return surface;
    }

    public IDCompositionTarget CreateSurfaceFromHwnd(IntPtr hwnd, bool topmost)
    {
        CreateTargetForHwnd(hwnd, topmost, out IDCompositionTarget target).CheckError();
        return target;
    }

    public IDCompositionTransform CreateTransformGroup(IDCompositionTransform[] transforms)
    {
        CreateTransformGroup(transforms, (uint)transforms.Length, out IDCompositionTransform transformGroup).CheckError();
        return transformGroup;
    }

    public IDCompositionTransform CreateTransformGroup(IDCompositionTransform[] transforms, uint count)
    {
        CreateTransformGroup(transforms, count, out IDCompositionTransform transformGroup).CheckError();
        return transformGroup;
    }

    public IDCompositionTransform3D CreateTransform3DGroup(IDCompositionTransform3D[] transforms3D)
    {
        CreateTransform3DGroup(transforms3D, (uint)transforms3D.Length, out IDCompositionTransform3D transform3DGroup).CheckError();
        return transform3DGroup;
    }

    public IDCompositionTransform3D CreateTransform3DGroup(IDCompositionTransform3D[] transforms3D, uint count)
    {
        CreateTransform3DGroup(transforms3D, count, out IDCompositionTransform3D transform3DGroup).CheckError();
        return transform3DGroup;
    }

    public IDCompositionTranslateTransform CreateTranslateTransform()
    {
        CreateTranslateTransform(out IDCompositionTranslateTransform translateTransform).CheckError();
        return translateTransform;
    }

    public IDCompositionTranslateTransform3D CreateTranslateTransform3D()
    {
        CreateTranslateTransform3D(out IDCompositionTranslateTransform3D translateTransform).CheckError();
        return translateTransform;
    }

    public IDCompositionVisual CreateVisual()
    {
        CreateVisual(out IDCompositionVisual visual).CheckError();
        return visual;
    }

    public IDCompositionVirtualSurface CreateVirtualSurface(uint initialWidth, uint initialHeight, Format pixelFormat, AlphaMode alphaMode)
    {
        CreateVirtualSurface(initialWidth, initialHeight, pixelFormat, alphaMode, out IDCompositionVirtualSurface virtualSurface).CheckError();
        return virtualSurface;
    }
}
