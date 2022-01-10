// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIFactory
{
    /// <summary>
    /// Get an instance of <see cref="IDXGIAdapter"/> or null if not found.
    /// </summary>
    /// <remarks>
    /// Make sure to dispose the <see cref="IDXGIAdapter"/> instance.
    /// </remarks>
    /// <param name="index">The index to get from.</param>
    /// <returns>Instance of <see cref="IDXGIAdapter"/> or null if not found.</returns>
    public IDXGIAdapter GetAdapter(int index)
    {
        EnumAdapters(index, out IDXGIAdapter adapter).CheckError();
        return adapter;
    }
}
