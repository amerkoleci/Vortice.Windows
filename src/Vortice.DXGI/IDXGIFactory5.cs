// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public unsafe partial class IDXGIFactory5
{
    /// <summary>
    /// Gets if tearing is allowed during present.
    /// </summary>
    public RawBool PresentAllowTearing
    {
        get
        {
            RawBool allowTearing;
            CheckFeatureSupport(Feature.PresentAllowTearing, &allowTearing, sizeof(RawBool));
            return allowTearing;
        }
    }

    public bool CheckFeatureSupport<T>(Feature feature, T featureSupport) where T : unmanaged
    {
        return CheckFeatureSupport(feature, &featureSupport, sizeof(T)).Success;
    }
}
