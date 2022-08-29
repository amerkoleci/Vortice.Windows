// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIFactory1
{
    private List<IDXGIAdapter1>? _adapters1;

    protected override void NativePointerUpdated(IntPtr oldNativePointer)
    {
        base.NativePointerUpdated(oldNativePointer);
        if (oldNativePointer != IntPtr.Zero)
        {
            if (_adapters1 != null)
            {
                foreach (IDXGIAdapter1 adapter1 in _adapters1)
                {
                    MemoryHelpers.Dispose(adapter1, true);
                }

                _adapters1.Clear();
                _adapters1 = default;
            }
        }
    }

    public IEnumerable<IDXGIAdapter1> EnumAdapters1()
    {
        if (_adapters1 == null)
        {
            _adapters1 = new List<IDXGIAdapter1>();
            while (true)
            {
                Result result = EnumAdapters1(_adapters1.Count, out IDXGIAdapter1? adapter);
                if (result.Failure || adapter == null)
                {
                    break;
                }

                _adapters1.Add(adapter);
            }
        }

        return _adapters1!;
    }
}
