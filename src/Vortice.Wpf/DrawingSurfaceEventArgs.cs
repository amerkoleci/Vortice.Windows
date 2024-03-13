// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D11;

namespace Vortice.Wpf;

/// <summary>
/// Arguments used for Device related events.
/// </summary>
public class DrawingSurfaceEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new DrawingSurfaceEventArgs.
    /// </summary>
    /// <param name="device">The <see cref="ID3D11Device"/> associated with the event.</param>
    public DrawingSurfaceEventArgs(ID3D11Device device)
    {
        ArgumentNullException.ThrowIfNull(nameof(device));

        Device = device;
    }

    /// <summary>
    /// Gets the <see cref="ID3D11Device"/>>.
    /// </summary>
    public ID3D11Device Device { get; }
}
