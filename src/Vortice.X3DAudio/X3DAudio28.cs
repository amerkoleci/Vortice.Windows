// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.X3DAudio
{
    internal unsafe static class X3DAudio28
    {
        public static Result X3DAudioInitialize(Speakers speakerChannelMask, float speedOfSound, out X3DAudioHandle instance)
        {
            instance = new X3DAudioHandle();
            Result result;
            fixed (void* instance_ = &instance)
            {
                result = X3DAudioInitialize_((int)speakerChannelMask, speedOfSound, instance_);
            }

            return result;
        }

        public static void X3DAudioCalculate(ref X3DAudioHandle instance, Listener listener, Emitter emitter, CalculateFlags flags, IntPtr dSPSettings)
        {
            var nativeListener = new Listener.__Native();
            listener.__MarshalTo(ref nativeListener);
            var nativeEmitter = new Emitter.__Native();
            emitter.__MarshalTo(ref nativeEmitter);
            fixed (void* instance_ = &instance)
            {
                X3DAudioCalculate_(instance_, &nativeListener, &nativeEmitter, (int)flags, (void*)dSPSettings);
            }

            emitter.__MarshalFree(ref nativeEmitter);
        }

        [DllImport("xaudio2_8.dll", EntryPoint = "X3DAudioInitialize", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern int X3DAudioInitialize_(int arg0, float arg1, void* arg2);

        [DllImport("xaudio2_8.dll", EntryPoint = "X3DAudioCalculate", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern void X3DAudioCalculate_(void* arg0, void* arg1, void* arg2, int arg3, void* arg4);
    }

}
