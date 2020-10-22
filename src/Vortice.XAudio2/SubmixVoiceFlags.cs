// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.XAudio2
{
    [Flags]
    public enum SubmixVoiceFlags
    {
        None = VoiceFlags.None,
        UseFilter = VoiceFlags.UseFilter
    }
}
