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
        private static readonly IntPtr s_xaudioLibrary = LoadXAudioLibrary();

        private static IntPtr LoadXAudioLibrary()
        {
#if NETCOREAPP3_0_OR_GREATER
            IntPtr libraryHandle;
            if(NativeLibrary.TryLoad("xaudio2_9.dll", out libraryHandle))
            {
                return libraryHandle;
            }
            else if(NativeLibrary.TryLoad("xaudio2_9redist.dll", out libraryHandle))
            {
                return libraryHandle;
            }

            return IntPtr.Zero;
#else
            string libraryPath = GetLibraryPath("xaudio2_9.dll");

            IntPtr handle = Win32.LoadLibrary(libraryPath);
            if (handle == IntPtr.Zero)
            {
                libraryPath = GetLibraryPath("xaudio2_9redist.dll");
                handle = Win32.LoadLibrary(libraryPath);
                if (handle == IntPtr.Zero)
                {
                    throw new DllNotFoundException("Unable to load xaudio2_9.dll or xaudio2_9redist.dll library.");
                }
            }

            return handle;

            static string GetLibraryPath(string libraryName)
            {
                bool isArm = RuntimeInformation.ProcessArchitecture == Architecture.Arm || RuntimeInformation.ProcessArchitecture == Architecture.Arm64;

                var arch = Environment.Is64BitProcess
                    ? isArm ? "arm64" : "x64"
                    : isArm ? "arm" : "x86";

                // 1. try alongside managed assembly
                var path = typeof(XAudio2Native).Assembly.Location;
                if (!string.IsNullOrEmpty(path))
                {
                    path = Path.GetDirectoryName(path);
                    // 1.1 in platform sub dir
                    var lib = Path.Combine(path, arch, libraryName);
                    if (File.Exists(lib))
                        return lib;
                    // 1.2 in root
                    lib = Path.Combine(path, libraryName);
                    if (File.Exists(lib))
                        return lib;
                }

                // 2. try current directory
                path = Directory.GetCurrentDirectory();
                if (!string.IsNullOrEmpty(path))
                {
                    // 2.1 in platform sub dir
                    var lib = Path.Combine(path, arch, libraryName);
                    if (File.Exists(lib))
                        return lib;
                    // 2.2 in root
                    lib = Path.Combine(lib, libraryName);
                    if (File.Exists(lib))
                        return lib;
                }

                // 3. try app domain
                try
                {
                    if (AppDomain.CurrentDomain is AppDomain domain)
                    {
                        // 3.1 RelativeSearchPath
                        path = domain.RelativeSearchPath;
                        if (!string.IsNullOrEmpty(path))
                        {
                            // 3.1.1 in platform sub dir
                            var lib = Path.Combine(path, arch, libraryName);
                            if (File.Exists(lib))
                                return lib;
                            // 3.1.2 in root
                            lib = Path.Combine(lib, libraryName);
                            if (File.Exists(lib))
                                return lib;
                        }

                        // 3.2 BaseDirectory
                        path = domain.BaseDirectory;
                        if (!string.IsNullOrEmpty(path))
                        {
                            // 3.2.1 in platform sub dir
                            string? lib = Path.Combine(path, arch, libraryName);
                            if (File.Exists(lib))
                                return lib;
                            // 3.2.2 in root
                            lib = Path.Combine(lib, libraryName);
                            if (File.Exists(lib))
                                return lib;
                        }
                    }
                }
                catch
                {
                    // no-op as there may not be any domain or path
                }

                // 4. use PATH or default loading mechanism
                return libraryName;
            }
#endif
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

        public static Result CreateAudioVolumeMeter(out ComObject? volumeMeter)
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

        public static Result CreateAudioReverb(out ComObject? reverb)
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

        private static XAudio2CreateDelegate s_XAudio2CreateCallback = LoadFunction<XAudio2CreateDelegate>(nameof(XAudio2Create));
        private static CreateAudioReverbDelegate s_CreateAudioReverb = LoadFunction<CreateAudioReverbDelegate>(nameof(CreateAudioReverb));
        private static CreateAudioVolumeMeterDelegate s_CreateAudioVolumeMeter = LoadFunction<CreateAudioVolumeMeterDelegate>(nameof(CreateAudioVolumeMeter));
        private static X3DAudioInitializeDelegate s_X3DAudioInitialize = LoadFunction<X3DAudioInitializeDelegate>(nameof(X3DAudioInitialize));
        private static X3DAudioCalculateDelegate s_X3DAudioCalculate = LoadFunction<X3DAudioCalculateDelegate>(nameof(X3DAudioCalculate));

        private static T LoadFunction<T>(string name)
        {
#if NETCOREAPP3_0_OR_GREATER
            IntPtr functionPtr = NativeLibrary.GetExport(s_xaudioLibrary, name);
#else
            IntPtr functionPtr = Win32.GetProcAddress(s_xaudioLibrary, name);
#endif
            if (functionPtr == IntPtr.Zero)
            {
                throw new InvalidOperationException($"No function was found with the name {name}.");
            }

            return Marshal.GetDelegateForFunctionPointer<T>(functionPtr);
        }

#pragma warning disable IDE1006 // Naming Styles
#if !NETCOREAPP3_0_OR_GREATER
        private static class Win32
        {
            private const string SystemLibrary = "Kernel32.dll";

            [DllImport(SystemLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern IntPtr LoadLibrary(string lpFileName);

            [DllImport(SystemLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

            [DllImport(SystemLibrary, SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern void FreeLibrary(IntPtr hModule);
        }
#endif
#pragma warning restore IDE1006 // Naming Styles
    }
}
