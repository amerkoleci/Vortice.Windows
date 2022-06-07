// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Collections;
using System.Runtime.InteropServices;

namespace Vortice.MediaFoundation;

public unsafe class IMFActivateCollection : IEnumerable<IMFActivate>, IDisposable
{
    private IntPtr* _ptrs;
    private readonly List<IMFActivate> _list;

    internal IMFActivateCollection(IntPtr ptrs, int count)
    {
        _ptrs = (IntPtr*)ptrs;
        _list = new List<IMFActivate>();

        for (int i = 0; i < count; i++)
        {
            _list.Add(new IMFActivate(*_ptrs));
            _ptrs++;
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            _list[i].Dispose();
        }
        //Marshal.FreeCoTaskMem((IntPtr)_ptrs);
    }

    public IEnumerator<IMFActivate> GetEnumerator() => _list.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
}
