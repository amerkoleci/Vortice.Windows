// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;
public unsafe partial class ID2D1EffectContext
{
    public Vector2 Dpi
    {
        get
        {
            GetDpi(out float dpiX, out float dpiY);
            return new Vector2(dpiX, dpiY);
        }
    }

    public T CheckFeatureSupport<T>(Feature feature) where T : unmanaged
    {
        T featureSupport = default;
        CheckFeatureSupport(feature, &featureSupport, sizeof(T));
        return featureSupport;
    }

    public bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
    {
        fixed (T* featureSupportPtr = &featureSupport)
        {
            return CheckFeatureSupport(feature, featureSupportPtr, sizeof(T)).Success;
        }
    }
}
