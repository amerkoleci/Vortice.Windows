// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.XAudio2
{
    internal static unsafe class XAudio29
    {
        public static IntPtr XAudio2Create(int flags, ProcessorSpecifier processorSpecifier)
        {
            var nativePtr = IntPtr.Zero;
            Result result = XAudio2Create_(&nativePtr, flags, (int)processorSpecifier);
            result.CheckError();
            return nativePtr;
        }

        [DllImport("xaudio2_9.dll", EntryPoint = "XAudio2Create", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int XAudio2Create_(void* arg0, int arg1, int arg2);
    }
}
