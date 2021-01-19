// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcCompilerArgs
    {
        public string Arguments
        {
            get => Marshal.PtrToStringUni(GetArguments());
        }

        private unsafe IntPtr GetArguments()
        {
            return LocalInterop.CalliStdCallSystemIntPtr(_nativePointer, (*(void***)_nativePointer)[3]);
        }

        public Result AddArguments(string[] arguments)
        {
            return AddArguments(arguments, arguments.Length);
        }

        public unsafe Result AddArguments(string[] arguments, int argumentsCount)
        {
            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                return AddArguments((IntPtr)argumentsPtr, argumentsCount);
            }
            finally
            {
                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }

        public Result AddArgumentsUTF8(string[] arguments)
        {
            return AddArgumentsUTF8(arguments, arguments.Length);
        }

        public unsafe Result AddArgumentsUTF8(string[] arguments, int argumentsCount)
        {
            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                return AddArgumentsUTF8((IntPtr)argumentsPtr, argumentsCount);
            }
            finally
            {
                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }

        public Result AddDefines(DxcDefine[] defines)
        {
            return AddDefines(defines, defines != null ? defines.Length : 0);
        }
    }
}
