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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace SharpGen.Runtime
{
    /// <summary>
    /// A COM Interface Callback
    /// </summary>
    public class ComObjectShadow : CppObjectShadow
    {
        private Result QueryInterface(Guid guid, out IntPtr output)
        {
            output = Callback.Shadow.Find(guid);

            if (output == IntPtr.Zero)
            {
                return Result.NoInterface.Code;
            }

            ((IUnknown)Callback).AddRef();

            return Result.Ok.Code;

        }

        protected override CppObjectVtbl Vtbl { get; } = new ComObjectVtbl(0);

        protected class ComObjectVtbl : CppObjectVtbl
        {
            public ComObjectVtbl(int numberOfCallbackMethods)
                : base(numberOfCallbackMethods + 3)
            {
                AddMethod(new QueryInterfaceDelegate(QueryInterfaceImpl));
                AddMethod(new AddRefDelegate(AddRefImpl));
                AddMethod(new ReleaseDelegate(ReleaseImpl));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate int QueryInterfaceDelegate(IntPtr thisObject, IntPtr guid, out IntPtr output);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint AddRefDelegate(IntPtr thisObject);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint ReleaseDelegate(IntPtr thisObject);

            protected unsafe static int QueryInterfaceImpl(IntPtr thisObject, IntPtr guid, out IntPtr output)
            {
                var shadow = ToShadow<ComObjectShadow>(thisObject);

                return shadow.QueryInterface(*(Guid*)guid, out output).Code;
            }

            protected static uint AddRefImpl(IntPtr thisObject)
            {
                var shadow = ToShadow<ComObjectShadow>(thisObject);

                var obj = (IUnknown)shadow.Callback;

                return obj.AddRef();
            }

            protected static uint ReleaseImpl(IntPtr thisObject)
            {
                var shadow = ToShadow<ComObjectShadow>(thisObject);

                var obj = (IUnknown)shadow.Callback;

                return obj.Release();
            }
        }
    }
}