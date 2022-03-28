// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIFactory1
{
    /// <summary>
    /// Get an instance of <see cref="IDXGIAdapter1"/> or null if not found.
    /// </summary>
    /// <remarks>
    /// Make sure to dispose the <see cref="IDXGIAdapter1"/> instance.
    /// </remarks>
    /// <param name="index">The index to get from.</param>
    /// <returns>Instance of <see cref="IDXGIAdapter1"/> or null if not found.</returns>
    public IDXGIAdapter1 GetAdapter1(int index)
    {
        EnumAdapters1(index, out IDXGIAdapter1 adapter).CheckError();
        return adapter;
    }

    /// <summary>
    /// Gets the number of available adapters from this factory.
    /// </summary>
    /// <returns>The number of adapters</returns>
    public int GetAdapterCount1()
    {
        int count = 0;

        for (int adapterIndex = 0; EnumAdapters1(adapterIndex, out IDXGIAdapter1 adapter).Success; adapterIndex++)
        {
            count++;
            adapter.Dispose();
        }

        return count;
    }
}
