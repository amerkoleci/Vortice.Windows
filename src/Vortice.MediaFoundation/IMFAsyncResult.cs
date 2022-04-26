// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.MediaFoundation;

public partial class IMFAsyncResult
{
    private object _state;
    private bool _isStateVerified;

    public Result Status
    {
        get => GetStatus();
        set => SetStatus(value);
    }

    public object State
    {
        get
        {
            if (!_isStateVerified)
            {
                GetState(out IntPtr statePtr);
                if (statePtr != IntPtr.Zero)
                {
                    _state = Marshal.GetObjectForIUnknown(statePtr);
                    Marshal.Release(statePtr);
                }
                _isStateVerified = true;
            }

            return _state;
        }
    }
}
