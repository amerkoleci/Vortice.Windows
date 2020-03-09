// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using NativeLibraryLoader;
using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.XAudio2
{
    internal static unsafe class XAudio2Native
    {
        private static readonly NativeLibrary s_xaudioLibrary = LoadXAudio2();

        private static NativeLibrary LoadXAudio2()
        {
            if (PlatformDetection.IsUAP)
            {
                s_XAudio2CreateCallback = XAudio29.XAudio2Create;
                s_CreateAudioReverb = XAudio29.CreateAudioReverb;
                s_CreateAudioVolumeMeter = XAudio29.CreateAudioVolumeMeter;
                s_X3DAudioInitialize = XAudio29.X3DAudioInitialize;
                s_X3DAudioCalculate = XAudio29.X3DAudioCalculate;
                return null;
            }
            else
            {
                var nativeLibrary = new NativeLibrary("xaudio2_9redist.dll");
                s_XAudio2CreateCallback = s_xaudioLibrary.LoadFunction<XAudio2CreateDelegate>("XAudio2Create");
                s_CreateAudioReverb = s_xaudioLibrary.LoadFunction<CreateAudioReverbDelegate>("CreateAudioReverb");
                s_CreateAudioVolumeMeter = s_xaudioLibrary.LoadFunction<CreateAudioVolumeMeterDelegate>("CreateAudioVolumeMeter");
                s_X3DAudioInitialize = s_xaudioLibrary.LoadFunction<X3DAudioInitializeDelegate>("X3DAudioInitialize");
                s_X3DAudioCalculate = s_xaudioLibrary.LoadFunction<X3DAudioCalculateDelegate>("X3DAudioCalculate");
                return nativeLibrary;
            }
        }

        public static IntPtr XAudio2Create(int flags, ProcessorSpecifier processorSpecifier)
        {
            var nativePtr = IntPtr.Zero;
            Result result = s_XAudio2CreateCallback(&nativePtr, flags, (int)processorSpecifier);
            result.CheckError();
            return nativePtr;
        }

        public static Result CreateAudioReverb<T>(out T reverb) where T : ComObject
        {
            unsafe
            {
                var nativePtr = IntPtr.Zero;
                Result result = s_CreateAudioReverb(&nativePtr);
                if (result.Success)
                {
                    reverb = CppObject.FromPointer<T>(nativePtr);
                    return result;
                }

                reverb = null;
                return result;
            }
        }

        public static Result CreateAudioVolumeMeter<T>(out T reverb) where T : ComObject
        {
            unsafe
            {
                var nativePtr = IntPtr.Zero;
                Result result = s_CreateAudioVolumeMeter(&nativePtr);
                if (result.Success)
                {
                    reverb = CppObject.FromPointer<T>(nativePtr);
                    return result;
                }

                reverb = null;
                return result;
            }
        }


        public static Result X3DAudioInitialize(Speakers speakerChannelMask, float speedOfSound, out X3DAudioHandle instance)
        {
            instance = new X3DAudioHandle();
            Result result;
            fixed (void* instance_ = &instance)
            {
                result = s_X3DAudioInitialize((int)speakerChannelMask, speedOfSound, instance_);
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
                s_X3DAudioCalculate(instance_, &nativeListener, &nativeEmitter, (int)flags, (void*)dSPSettings);
            }

            emitter.__MarshalFree(ref nativeEmitter);
        }

        private delegate int XAudio2CreateDelegate(void* arg0, int arg1, int arg2);
        private delegate int CreateAudioReverbDelegate(void* arg0);
        private delegate int CreateAudioVolumeMeterDelegate(void* arg0);
        private delegate int X3DAudioInitializeDelegate(int arg0, float arg1, void* arg2);
        private delegate void X3DAudioCalculateDelegate(void* arg0, void* arg1, void* arg2, int arg3, void* arg4);

        private static XAudio2CreateDelegate s_XAudio2CreateCallback;
        private static CreateAudioReverbDelegate s_CreateAudioReverb;
        private static CreateAudioVolumeMeterDelegate s_CreateAudioVolumeMeter;
        private static X3DAudioInitializeDelegate s_X3DAudioInitialize;
        private static X3DAudioCalculateDelegate s_X3DAudioCalculate;
    }
}
