// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XAudio2;

public static partial class XAudio2
{
    /// <summary>
    /// Create new instance of <see cref="IXAudio2"/> class.
    /// </summary>
    /// <param name="processorSpecifier"></param>
    /// <param name="registerCallback">Whether to register for callback, uses native RegisterForCallbacks.</param>
    /// <returns>New instance of <see cref="IXAudio2"/> class.</returns>
    public static IXAudio2 XAudio2Create(ProcessorSpecifier processorSpecifier = ProcessorSpecifier.DefaultProcessor, bool registerCallback = true)
    {
        return new IXAudio2(processorSpecifier, registerCallback);
    }

    public static Result XAudio2Create(out IXAudio2? XAudio2)
    {
        return XAudio2Create(ProcessorSpecifier.DefaultProcessor, true, out XAudio2);
    }

    public static Result XAudio2Create(ProcessorSpecifier processor, out IXAudio2? XAudio2)
    {
        return XAudio2Create(processor, true, out XAudio2);
    }

    public static unsafe Result XAudio2Create(ProcessorSpecifier processor, bool registerCallback, out IXAudio2? XAudio2)
    {
        Result result = XAudio2Native.XAudio2Create(processor, out IntPtr nativePtr);
        if (result.Failure)
        {
            XAudio2 = default;
            return result;
        }

        XAudio2 = new IXAudio2(nativePtr, registerCallback);
        return result;
    }
}
