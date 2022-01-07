// Copyright © Amer Koleci and Contributors.
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
}
