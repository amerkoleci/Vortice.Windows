// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.XAudio2
{
    public static partial class XAudio2
    {
        /// <summary>
        /// Create new instance of <see cref="IXAudio2"/> class.
        /// </summary>
        /// <param name="processorSpecifier"></param>
        /// <param name="registerCallback">Whether to register for callback, uses native RegisterForCallbacks.</param>
        /// <returns>New instance of <see cref="IXAudio2"/> class.</returns>
        public static IXAudio2 XAudio2Create(ProcessorSpecifier processorSpecifier = ProcessorSpecifier.UseDefaultProcessor, bool registerCallback = true)
        {
            return new IXAudio2(processorSpecifier, registerCallback);
        }
    }
}
