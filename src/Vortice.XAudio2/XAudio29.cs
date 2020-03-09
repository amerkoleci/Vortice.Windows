// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.XAudio2
{
    internal static unsafe class XAudio29
    {
        
        [DllImport("xaudio2_9.dll", EntryPoint = "XAudio2Create", CallingConvention = CallingConvention.StdCall)]
        public static extern int XAudio2Create(void* arg0, int arg1, int arg2);

        [DllImport("xaudio2_9.dll", EntryPoint = "CreateAudioReverb", CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern int CreateAudioReverb(void* arg0);

        [DllImport("xaudio2_9.dll", EntryPoint = "CreateAudioVolumeMeter", CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern int CreateAudioVolumeMeter(void* arg0);

        [DllImport("xaudio2_9.dll", EntryPoint = "X3DAudioInitialize", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern int X3DAudioInitialize(int arg0, float arg1, void* arg2);

        [DllImport("xaudio2_9.dll", EntryPoint = "X3DAudioCalculate", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void X3DAudioCalculate(void* arg0, void* arg1, void* arg2, int arg3, void* arg4);
    }
}
