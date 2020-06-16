// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIDeviceSubObject
    {
        public T GetDevice<T>() where T : ComObject
        {
            if (GetDevice(typeof(T).GUID, out IntPtr nativePtr).Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }
    }
}
