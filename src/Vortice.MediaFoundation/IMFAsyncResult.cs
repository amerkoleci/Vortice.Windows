// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.Win32;

namespace Vortice.MediaFoundation
{
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
}
