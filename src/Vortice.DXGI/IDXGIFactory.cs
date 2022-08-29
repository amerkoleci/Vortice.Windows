// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIFactory
{
    private List<IDXGIAdapter>? _adapters;

    protected override void NativePointerUpdated(IntPtr oldNativePointer)
    {
        base.NativePointerUpdated(oldNativePointer);
        if (oldNativePointer != IntPtr.Zero)
        {
            if (_adapters != null)
            {
                foreach (IDXGIAdapter adapter in _adapters)
                {
                    MemoryHelpers.Dispose(adapter, true);
                }

                _adapters.Clear();
                _adapters = default;
            }
        }
    }

    public IEnumerable<IDXGIAdapter> EnumAdapters()
    {
        if (_adapters == null)
        {
            _adapters = new List<IDXGIAdapter>();
            while (true)
            {
                Result result = EnumAdapters(_adapters.Count, out IDXGIAdapter? adapter);
                if (result.Failure || adapter == null)
                {
                    break;
                }

                _adapters.Add(adapter);
            }
        }

        return _adapters!;
    }
}
