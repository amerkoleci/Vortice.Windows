// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D11;

namespace Vortice.Wpf;

/// <summary>
/// Provides data for the Draw event.
/// </summary>
public class DrawEventArgs : DrawingSurfaceEventArgs
{
    public DrawEventArgs(DrawingSurface surface, ID3D11Device device)
        : base(device)
    {
        ArgumentNullException.ThrowIfNull(nameof(surface));

        Surface = surface;
    }

    public DrawingSurface Surface { get; }

    public void InvalidateSurface()
    {
        Surface.Invalidate();
    }
}
