// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DirectX.DXGI
{
    public partial class IDXGIObject
    {
        public T GetParent<T>() where T : ComObject
        {
            if (GetParent(typeof(T).GUID, out IntPtr nativePtr).Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }
    }
}
