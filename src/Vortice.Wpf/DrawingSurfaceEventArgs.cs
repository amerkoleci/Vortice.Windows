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
    /// <param name="device">The <see cref="ID3D11Device1"/> associated with the event.</param>
    /// <param name="context">The <see cref="ID3D11DeviceContext1"/> associated with the event.</param>
    public DrawingSurfaceEventArgs(ID3D11Device1 device, ID3D11DeviceContext1 context)
    {
        ArgumentNullException.ThrowIfNull(nameof(device));
        ArgumentNullException.ThrowIfNull(nameof(context));

        Device = device;
        Context = context;
    }

    /// <summary>
    /// Gets the <see cref="ID3D11Device1"/>>.
    /// </summary>
    public ID3D11Device1 Device { get; }

    /// <summary>
    /// Gets the <see cref="ID3D11DeviceContext1"/>>.
    /// </summary>
    public ID3D11DeviceContext1 Context { get; }
}
