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
using System;
using System.Runtime.InteropServices;

namespace SharpGen.Runtime.Win32
{
    [Shadow(typeof(IStreamBaseShadow))]
    public partial interface IStreamBase
    {
        /// <summary>
        /// Reads a specified number of bytes from the stream object into memory starting at the current seek pointer.
        /// </summary>
        /// <param name="buffer">The read buffer.</param>
        /// <param name="numberOfBytesToRead">The number of bytes to read.</param>
        /// <returns>The actual number of bytes read from the stream object. </returns>
        uint Read(IntPtr buffer, uint numberOfBytesToRead);

        /// <summary>
        /// Writes a specified number of bytes into the stream object starting at the current seek pointer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="numberOfBytesToRead">The number of bytes to read.</param>
        /// <returns>The actual number of bytes written to the stream object</returns>
        uint Write(IntPtr buffer, uint numberOfBytesToRead);
    }

    internal class IStreamBaseShadow : ComObjectShadow
    {
        private static readonly ComStreamBaseVtbl _vTable = new ComStreamBaseVtbl(0);

        protected override CppObjectVtbl Vtbl => _vTable;

        protected class ComStreamBaseVtbl : ComObjectVtbl
        {
            public ComStreamBaseVtbl(int numberOfMethods)
                : base(numberOfMethods + 2)
            {
                AddMethod(new ReadDelegate(ReadImpl));
                AddMethod(new WriteDelegate(WriteImpl));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int ReadDelegate(IntPtr thisPtr, IntPtr buffer, uint sizeOfBytes, out uint bytesRead);
            private static int ReadImpl(IntPtr thisPtr, IntPtr buffer, uint sizeOfBytes, out uint bytesRead)
            {
                bytesRead = 0;
                try
                {
                    var shadow = ToShadow<IStreamBaseShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    bytesRead = callback.Read(buffer, sizeOfBytes);
                }
                catch (Exception exception)
                {
                    return (int)Result.GetResultFromException(exception);
                }
                return Result.Ok.Code;
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int WriteDelegate(IntPtr thisPtr, IntPtr buffer, uint sizeOfBytes, out uint bytesWrite);
            private static int WriteImpl(IntPtr thisPtr, IntPtr buffer, uint sizeOfBytes, out uint bytesWrite)
            {
                bytesWrite = 0;
                try
                {
                    var shadow = ToShadow<IStreamBaseShadow>(thisPtr);
                    var callback = ((IStream)shadow.Callback);
                    bytesWrite = callback.Write(buffer, sizeOfBytes);
                }
                catch (Exception exception)
                {
                    return (int)Result.GetResultFromException(exception);
                }
                return Result.Ok.Code;
            }
        }
    }
}
