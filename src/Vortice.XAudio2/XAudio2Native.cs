// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.XAudio2
{
    internal static unsafe class XAudio2Native
    {
        private static readonly IntPtr s_xaudioLibrary = LoadXAudio2();

        private static IntPtr LoadXAudio2()
        {
            if (VorticePlatformDetection.IsUAP)
            {
                s_XAudio2CreateCallback = XAudio29.XAudio2Create;
                s_CreateAudioVolumeMeter = XAudio29.XAudio2CreateVolumeMeter;
                s_CreateAudioReverb = XAudio29.XAudio2CreateReverb;
                s_X3DAudioInitialize = XAudio29.X3DAudioInitialize;
                s_X3DAudioCalculate = XAudio29.X3DAudioCalculate;
                return IntPtr.Zero;
            }
            else
            {
                IntPtr nativeLib = IntPtr.Zero;
                string rid = Environment.Is64BitProcess ? "win-x64" : "win-x86";
                string assemblyLocation = Path.GetDirectoryName(typeof(XAudio2Native).Assembly.Location) ?? "./";

                if (nativeLib == IntPtr.Zero)
                    nativeLib = LoadLibraryW(Path.Combine(assemblyLocation, "native", rid, "xaudio2_9redist.dll"));

                if (nativeLib == IntPtr.Zero)
                {
                    if (Environment.Is64BitProcess)
                        nativeLib = LoadLibraryW(Path.Combine(assemblyLocation, "x64/xaudio2_9redist.dll"));
                    else
                        nativeLib = LoadLibraryW(Path.Combine(assemblyLocation, "x86/xaudio2_9redist.dll"));

                    if (nativeLib == IntPtr.Zero)
                        nativeLib = LoadLibraryW(Path.Combine(assemblyLocation, "../../runtimes", rid, "native/xaudio2_9redist.dll"));

                    if (nativeLib == IntPtr.Zero)
                        nativeLib = LoadLibraryW(Path.Combine(assemblyLocation, "runtimes", rid, "native/xaudio2_9redist.dll"));

                    // Last try from executable folder.
                    if (nativeLib == IntPtr.Zero)
                        nativeLib = LoadLibraryW(Path.Combine(assemblyLocation, "xaudio2_9redist.dll"));
                }

                if (nativeLib == IntPtr.Zero)
                {
                    throw new PlatformNotSupportedException("Cannot load native libraries on this platform: " + RuntimeInformation.OSDescription);
                }

                s_XAudio2CreateCallback = LoadFunction<XAudio2CreateDelegate>(nativeLib, "XAudio2Create");
                s_CreateAudioReverb = LoadFunction<CreateAudioReverbDelegate>(nativeLib, "CreateAudioReverb");
                s_CreateAudioVolumeMeter = LoadFunction<CreateAudioVolumeMeterDelegate>(nativeLib, "CreateAudioVolumeMeter");
                s_X3DAudioInitialize = LoadFunction<X3DAudioInitializeDelegate>(nativeLib, "X3DAudioInitialize");
                s_X3DAudioCalculate = LoadFunction<X3DAudioCalculateDelegate>(nativeLib, "X3DAudioCalculate");
                return nativeLib;

            }
        }

        public static IntPtr XAudio2Create(int flags, ProcessorSpecifier processorSpecifier)
        {
            IntPtr nativePtr = IntPtr.Zero;
            Result result = s_XAudio2CreateCallback(&nativePtr, flags, (int)processorSpecifier);
            result.CheckError();
            return nativePtr;
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

        public static Result CreateAudioVolumeMeter(out ComObject volumeMeter) 
        {
            unsafe
            {
                IntPtr nativePtr = IntPtr.Zero;
                Result result = s_CreateAudioVolumeMeter(&nativePtr, 0u);
                if (result.Success)
                {
                    volumeMeter = new ComObject(nativePtr);
                    return result;
                }

                volumeMeter = null;
                return result;
            }
        }

        public static Result CreateAudioReverb(out ComObject reverb) 
        {
            unsafe
            {
                IntPtr nativePtr = IntPtr.Zero;
                Result result = s_CreateAudioReverb(&nativePtr, 0u);
                if (result.Success)
                {
                    reverb = new ComObject(nativePtr);
                    return result;
                }

                reverb = null;
                return result;
            }
        }

        private delegate int XAudio2CreateDelegate(void* arg0, int arg1, int arg2);
        private delegate int CreateAudioReverbDelegate(void* arg0, uint flags);
        private delegate int CreateAudioVolumeMeterDelegate(void* arg0, uint flags);
        private delegate int X3DAudioInitializeDelegate(int arg0, float arg1, void* arg2);
        private delegate void X3DAudioCalculateDelegate(void* arg0, void* arg1, void* arg2, int arg3, void* arg4);

        private static XAudio2CreateDelegate s_XAudio2CreateCallback;
        private static CreateAudioReverbDelegate s_CreateAudioReverb;
        private static CreateAudioVolumeMeterDelegate s_CreateAudioVolumeMeter;
        private static X3DAudioInitializeDelegate s_X3DAudioInitialize;
        private static X3DAudioCalculateDelegate s_X3DAudioCalculate;

        private static T LoadFunction<T>(IntPtr module, string name)
        {
            IntPtr functionPtr = GetProcAddress(module, name);
            if (functionPtr == IntPtr.Zero)
            {
                throw new InvalidOperationException($"No function was found with the name {name}.");
            }

            return Marshal.GetDelegateForFunctionPointer<T>(functionPtr);
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibraryW(string lpszLib);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
    }
}
