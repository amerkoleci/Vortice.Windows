// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.DirectComposition;

public partial class IDCompositionDevice2
{
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

    public IDCompositionSurface CreateSurface(int width, int height, Format pixelFormat, AlphaMode alphaMode)
    {
        CreateSurface(width, height, pixelFormat, alphaMode, out IDCompositionSurface surface).CheckError();
        return surface;
    }

    public IDCompositionSurfaceFactory CreateSurfaceFactory(IUnknown renderingDevice)
    {
        CreateSurfaceFactory(renderingDevice, out IDCompositionSurfaceFactory surfaceFactory).CheckError();
        return surfaceFactory;
    }

    public IDCompositionTransform CreateTransformGroup(IDCompositionTransform[] transforms)
    {
        CreateTransformGroup(transforms, transforms.Length, out IDCompositionTransform transformGroup).CheckError();
        return transformGroup;
    }

    public IDCompositionTransform CreateTransformGroup(IDCompositionTransform[] transforms, int count)
    {
        CreateTransformGroup(transforms, count, out IDCompositionTransform transformGroup).CheckError();
        return transformGroup;
    }

    public IDCompositionTransform3D CreateTransform3DGroup(IDCompositionTransform3D[] transforms3D)
    {
        CreateTransform3DGroup(transforms3D, transforms3D.Length, out IDCompositionTransform3D transform3DGroup).CheckError();
        return transform3DGroup;
    }

    public IDCompositionTransform3D CreateTransform3DGroup(IDCompositionTransform3D[] transforms3D, int count)
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

    public IDCompositionVisual2 CreateVisual()
    {
        CreateVisual(out IDCompositionVisual2 visual).CheckError();
        return visual;
    }

    public IDCompositionVirtualSurface CreateVirtualSurface(int initialWidth, int initialHeight, Format pixelFormat, AlphaMode alphaMode)
    {
        CreateVirtualSurface(initialWidth, initialHeight, pixelFormat, alphaMode, out IDCompositionVirtualSurface virtualSurface).CheckError();
        return virtualSurface;
    }
}
