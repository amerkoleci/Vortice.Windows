// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using Vortice.Multimedia;

namespace Vortice.XAudio2;

internal static unsafe class XAudio2Native
{
    private static readonly IntPtr s_xaudioLibrary;

    private static readonly delegate* unmanaged[Stdcall]<out IntPtr, uint, uint, int> s_XAudio2CreateCallback;
    private static readonly delegate* unmanaged[Stdcall]<void*, uint, int> s_CreateAudioReverb;
    private static readonly delegate* unmanaged[Stdcall]<void*, uint, int> s_CreateAudioVolumeMeter;
    private static readonly delegate* unmanaged[Stdcall]<int, float, out X3DAudioHandle, int> s_X3DAudioInitialize;
    private static readonly delegate* unmanaged[Stdcall]<void*, void*, void*, int, void*, void> s_X3DAudioCalculate;

    static XAudio2Native()
    {
        s_xaudioLibrary = LoadXAudioLibrary();
        s_XAudio2CreateCallback = (delegate* unmanaged[Stdcall]<out IntPtr, uint, uint, int>)GetExport(nameof(XAudio2Create));
        s_CreateAudioReverb = (delegate* unmanaged[Stdcall]<void*, uint, int>)GetExport(nameof(CreateAudioReverb));
        s_CreateAudioVolumeMeter = (delegate* unmanaged[Stdcall]<void*, uint, int>)GetExport(nameof(CreateAudioVolumeMeter));
        s_X3DAudioInitialize = (delegate* unmanaged[Stdcall]<int, float, out X3DAudioHandle, int>)GetExport(nameof(X3DAudioInitialize));
        s_X3DAudioCalculate = (delegate* unmanaged[Stdcall]<void*, void*, void*, int, void*, void>)GetExport(nameof(X3DAudioCalculate));
    }

    private static IntPtr LoadXAudioLibrary()
    {
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
    }

    public static Result XAudio2Create(ProcessorSpecifier processorSpecifier, out IntPtr nativePtr)
    {
        return s_XAudio2CreateCallback(out nativePtr, 0u, (uint)processorSpecifier);
    }

    public static Result X3DAudioInitialize(int speakerChannelMask, float speedOfSound, out X3DAudioHandle instance)
    {
        return s_X3DAudioInitialize(speakerChannelMask, speedOfSound, out instance);
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
        return NativeLibrary.GetExport(s_xaudioLibrary, name);
    }

    private static T LoadFunction<T>(string name)
    {
        IntPtr functionPtr = NativeLibrary.GetExport(s_xaudioLibrary, name);
        if (functionPtr == IntPtr.Zero)
        {
            throw new InvalidOperationException($"No function was found with the name {name}.");
        }

        return Marshal.GetDelegateForFunctionPointer<T>(functionPtr);
    }
}
