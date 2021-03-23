// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.DXGI.WinUI
{
    public partial class ISurfaceImageSourceNativeWithD2D
    {
#if false
        public T BeginDraw<T>(RawRect? updateRect, out Point updateOffset) where T : ComObject
        {
            BeginDraw(updateRect.GetValueOrDefault(), typeof(T).GUID, out IntPtr updateObjectPtr, out updateOffset).CheckError();
            return FromPointer<T>(updateObjectPtr);
        }
#endif
    }
}
