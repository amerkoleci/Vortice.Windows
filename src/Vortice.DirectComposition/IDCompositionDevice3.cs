// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectComposition;

public partial class IDCompositionDevice3
{
    public IDCompositionGaussianBlurEffect CreateGaussianBlurEffect()
    {
        CreateGaussianBlurEffect(out IDCompositionGaussianBlurEffect gaussianBlurEffect).CheckError();
        return gaussianBlurEffect;
    }

    public IDCompositionBrightnessEffect CreateBrightnessEffect()
    {
        CreateBrightnessEffect(out IDCompositionBrightnessEffect brightnessEffect).CheckError();
        return brightnessEffect;
    }

    public IDCompositionColorMatrixEffect CreateColorMatrixEffect()
    {
        CreateColorMatrixEffect(out IDCompositionColorMatrixEffect colorMatrixEffect).CheckError();
        return colorMatrixEffect;
    }

    public IDCompositionShadowEffect CreateShadowEffect()
    {
        CreateShadowEffect(out IDCompositionShadowEffect shadowEffect).CheckError();
        return shadowEffect;
    }

    public IDCompositionHueRotationEffect CreateHueRotationEffect()
    {
        CreateHueRotationEffect(out IDCompositionHueRotationEffect hueRotationEffect).CheckError();
        return hueRotationEffect;
    }

    public IDCompositionSaturationEffect CreateSaturationEffect()
    {
        CreateSaturationEffect(out IDCompositionSaturationEffect saturationEffect).CheckError();
        return saturationEffect;
    }

    public IDCompositionTurbulenceEffect CreateTurbulenceEffect()
    {
        CreateTurbulenceEffect(out IDCompositionTurbulenceEffect turbulenceEffect).CheckError();
        return turbulenceEffect;
    }

    public IDCompositionLinearTransferEffect CreateLinearTransferEffect()
    {
        CreateLinearTransferEffect(out IDCompositionLinearTransferEffect linearTransferEffect).CheckError();
        return linearTransferEffect;
    }

    public IDCompositionTableTransferEffect CreateTableTransferEffect()
    {
        CreateTableTransferEffect(out IDCompositionTableTransferEffect tableTransferEffect).CheckError();
        return tableTransferEffect;
    }

    public IDCompositionCompositeEffect CreateCompositeEffect()
    {
        CreateCompositeEffect(out IDCompositionCompositeEffect compositeEffect).CheckError();
        return compositeEffect;
    }

    public IDCompositionBlendEffect CreateBlendEffect()
    {
        CreateBlendEffect(out IDCompositionBlendEffect blendEffect).CheckError();
        return blendEffect;
    }

    public IDCompositionArithmeticCompositeEffect CreateArithmeticCompositeEffect()
    {
        CreateArithmeticCompositeEffect(out IDCompositionArithmeticCompositeEffect arithmeticCompositeEffect);
        return arithmeticCompositeEffect;
    }

    public IDCompositionAffineTransform2DEffect CreateAffineTransform2DEffect()
    {
        CreateAffineTransform2DEffect(out IDCompositionAffineTransform2DEffect affineTransform2dEffect).CheckError();
        return affineTransform2dEffect;
    }
}
