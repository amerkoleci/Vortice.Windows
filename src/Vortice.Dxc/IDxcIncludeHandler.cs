// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcIncludeHandler
    {
        public IDxcBlob LoadSource(string fileName)
        {
            LoadSource(fileName, out IDxcBlob blob).CheckError();
            return blob;
        }
    }

#if TODO
    public partial class IDxcIncludeHandlerNative
    {
        //Result IDxcIncludeHandler.LoadSource(string fileName, out IDxcBlob includeSource)
        //{
        //    return LoadSource_(fileName, out includeSource);
        //}
    }

    internal class IDxcIncludeHandlerShadow : ComObjectShadow
    {
        private static readonly IDxcIncludeHandlerVtbl s_vtbl = new IDxcIncludeHandlerVtbl(0);

        protected unsafe class IDxcIncludeHandlerVtbl : ComObjectVtbl
        {
            public IDxcIncludeHandlerVtbl(int numberOfCallbackMethods)
                : base(numberOfCallbackMethods + 1)
            {
                AddMethod(new LoadSourceDelegate(LoadSource));
            }

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int LoadSourceDelegate(IntPtr thisObject, void* arg0, void* arg1);

            private static unsafe int LoadSource(IntPtr thisObject, void* param0, void* param1)
            {
                try
                {
                    IntPtr filenameRef = (IntPtr)param0;
                    IDxcIncludeHandler @this = (IDxcIncludeHandler)ToShadow<Vortice.Dxc.IDxcIncludeHandlerShadow>(thisObject).Callback;
                    string fileName = Marshal.PtrToStringUni(filenameRef);
                    Result result = @this.LoadSource(fileName, out IDxcBlob includeSource);

                    ref IntPtr includeSourceOut = ref Unsafe.AsRef<IntPtr>(param1);
                    includeSourceOut = CppObject.ToCallbackPtr<IDxcBlob>(includeSource);
                    return result.Code;
                }
                catch (Exception ex)
                {
                    IDxcIncludeHandler shadow = (IDxcIncludeHandler)ToShadow<IDxcIncludeHandlerShadow>(thisObject).Callback;
                    (shadow as IExceptionCallback)?.RaiseException(ex);
                    return Result.GetResultFromException(ex).Code;
                }
            }
        }

        protected override CppObjectVtbl Vtbl => s_vtbl;
    } 
#endif
}
