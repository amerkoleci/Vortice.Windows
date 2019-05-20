// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.DirectX.Direct3D
{
    public partial class Blob
    {
        public string ConvertToString()
        {
            return Marshal.PtrToStringAnsi(BufferPointer);
        }
    }
}
