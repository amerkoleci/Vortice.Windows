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
// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.DirectSound;

public partial class NotificationPosition
{
    /// <unmanaged>DSBPOSITIONNOTIFY::dwOffset</unmanaged>
    /// <unmanaged-short>dwOffset</unmanaged-short>
    public int Offset;

    /// <summary>
    /// Gets or sets the wait handle.
    /// </summary>
    /// <value>The wait handle.</value>
    public WaitHandle? WaitHandle { get; set; }

    // Internal native struct used for marshalling
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal partial struct __Native
    {
        public int dwOffset;
        public IntPtr hEventNotify;

        internal unsafe void __MarshalFree()
        {
        }
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    // Method to marshal from managed struct tot native
    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.dwOffset = Offset;
        if (WaitHandle != null)
            @ref.hEventNotify = WaitHandle.GetSafeWaitHandle().DangerousGetHandle();
        else
            @ref.hEventNotify = IntPtr.Zero;
    }
}
