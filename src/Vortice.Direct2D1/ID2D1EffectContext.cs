// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.Direct2D1;
public partial class ID2D1EffectContext
{
    public Vector2 Dpi
    {
        get
        {
            GetDpi(out float dpiX, out float dpiY);
            return new Vector2(dpiX, dpiY);
        }
    }

    public unsafe T CheckFeatureSupport<T>(Feature feature) where T : unmanaged
    {
        T featureSupport = default;
        CheckFeatureSupport(feature, &featureSupport, sizeof(T));
        return featureSupport;
    }

    public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
    {
        fixed (T* featureSupportPtr = &featureSupport)
        {
            return CheckFeatureSupport(feature, featureSupportPtr, sizeof(T)).Success;
        }
    }
}
