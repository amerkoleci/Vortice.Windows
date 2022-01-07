// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.XAudio2;

internal static unsafe class XAudio2Native
{
    private static readonly IntPtr s_xaudioLibrary;

    private static readonly delegate* unmanaged[Stdcall]<void*, uint, uint, int> s_XAudio2CreateCallback;
    private static readonly delegate* unmanaged[Stdcall]<void*, uint, int> s_CreateAudioReverb;
    private static readonly delegate* unmanaged[Stdcall]<void*, uint, int> s_CreateAudioVolumeMeter;
    private static readonly delegate* unmanaged[Stdcall]<int, float, void*, int> s_X3DAudioInitialize;
    private static readonly delegate* unmanaged[Stdcall]<void*, void*, void*, int, void*, void> s_X3DAudioCalculate;

    static XAudio2Native()
    {
        s_xaudioLibrary = LoadXAudioLibrary();
        s_XAudio2CreateCallback = (delegate* unmanaged[Stdcall]<void*, uint, uint, int>)GetExport(nameof(XAudio2Create));
        s_CreateAudioReverb = (delegate* unmanaged[Stdcall]<void*, uint, int>)GetExport(nameof(CreateAudioReverb));
        s_CreateAudioVolumeMeter = (delegate* unmanaged[Stdcall]<void*, uint, int>)GetExport(nameof(CreateAudioVolumeMeter));
        s_X3DAudioInitialize = (delegate* unmanaged[Stdcall]<int, float, void*, int>)GetExport(nameof(X3DAudioInitialize));
        s_X3DAudioCalculate = (delegate* unmanaged[Stdcall]<void*, void*, void*, int, void*, void>)GetExport(nameof(X3DAudioCalculate));
    }

    private static IntPtr LoadXAudioLibrary()
    {
#if NET5_0_OR_GREATER
        IntPtr libraryHandle;
        if(NativeLibrary.TryLoad("xaudio2_9.dll", out libraryHandle))
        {
            return libraryHandle;
        }
        else if (NativeLibrary.TryLoad("xaudio2_8.dll", out libraryHandle))
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
            libraryPath = GetLibraryPath("xaudio2_8.dll");
            handle = Win32.LoadLibrary(libraryPath);
            if (handle == IntPtr.Zero)
            {
                libraryPath = GetLibraryPath("xaudio2_9redist.dll");
                handle = Win32.LoadLibrary(libraryPath);
                if (handle == IntPtr.Zero)
                {
                    throw new DllNotFoundException("Unable to load xaudio2_9.dll or xaudio2_9redist.dll library.");
                }
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

    public static IntPtr XAudio2Create(uint flags, ProcessorSpecifier processorSpecifier)
    {
        IntPtr nativePtr = IntPtr.Zero;
        Result result = s_XAudio2CreateCallback(&nativePtr, flags, (uint)processorSpecifier);
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

    public static Result CreateAudioVolumeMeter(uint flags, IntPtr* reverb) => s_CreateAudioVolumeMeter(reverb, flags);
    public static Result CreateAudioReverb(uint flags, IntPtr* reverb) => s_CreateAudioReverb(reverb, flags);

    public static IntPtr GetExport(string name)
    {
#if NET5_0_OR_GREATER
        return NativeLibrary.GetExport(s_xaudioLibrary, name);
#else
        return Win32.GetProcAddress(s_xaudioLibrary, name);
#endif
    }

    private static T LoadFunction<T>(string name)
    {
#if NET5_0_OR_GREATER
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
#if !NET5_0_OR_GREATER
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
