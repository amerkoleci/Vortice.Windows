// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.DirectInput;

public partial class DeviceImageHeader
{
    public DeviceImage[]? Images { get; private set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal partial struct __Native
    {
        public int Size;
        public int SizeImageInfo;
        public int ViewCount;
        public int ButtonCount;
        public int AxeCount;
        public int PovCount;
        public int BufferSize;
        public int BufferUsed;
        public IntPtr ImageInfoArrayPointer;

        internal void __MarshalFree()
        {
        }
    }

    internal static unsafe __Native __NewNative()
    {
        __Native native = default;
        native.Size = sizeof(__Native);
        native.SizeImageInfo = sizeof(DeviceImage.__Native);
        return native;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Size = sizeof(__Native);
        @ref.SizeImageInfo = sizeof(DeviceImage.__Native);
        @ref.ViewCount = ViewCount;
        @ref.ButtonCount = ButtonCount;
        @ref.AxeCount = AxeCount;
        @ref.PovCount = PovCount;
        @ref.BufferSize = BufferSize;
        @ref.BufferUsed = BufferUsed;
        @ref.ImageInfoArrayPointer = ImageInfoArrayPointer;

    }
    #endregion Marshal
}
