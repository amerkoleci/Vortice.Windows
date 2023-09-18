// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;
using SharpGen.Runtime;
using static Vortice.DirectSound.DSound;

namespace Vortice.DirectSound;

public partial class IDirectSound
{
    /// <summary>
    /// Retrieves the speaker configuration of the device.
    /// </summary>
    /// <param name="speakerSet" />
    /// <param name="geometry" />
    public Result GetSpeakerConfiguration(out SpeakerConfiguration speakerSet, out SpeakerGeometry geometry)
    {
        Result result = GetSpeakerConfig(out int speakerConfig);
        speakerSet = (SpeakerConfiguration)(speakerConfig & 0xFFFF);
        geometry = (SpeakerGeometry)(speakerConfig >> 16);
        return result;
    }

    /// <summary>
    /// Sets the speaker configuration of the device.
    /// </summary>
    /// <param name="speakerSet" />
    /// <param name="geometry" />
    public Result SetSpeakerConfiguration(SpeakerConfiguration speakerSet, SpeakerGeometry geometry)
    {
        return SetSpeakerConfig(((int)speakerSet) | (((int)geometry) << 16));
    }


    /// <summary>
    /// Enumerates the DirectSound devices installed in the system.
    /// </summary>
    /// <returns>A collection of the devices found.</returns>
    public static List<DeviceInformation> GetDevices()
    {
        EnumDelegateCallback callback = new();
        DirectSoundEnumerateW(callback.NativePointer, IntPtr.Zero);
        return callback.Informations;
    }

    /// <summary>
    /// Duplicates the sound buffer.
    /// </summary>
    /// <param name="sourceBuffer">The source buffer.</param>
    /// <returns>A duplicate of this soundBuffer.</returns>
    /// <unmanaged>HRESULT IDirectSound::DuplicateSoundBuffer([In] IDirectSoundBuffer* pDSBufferOriginal,[Out] void** ppDSBufferDuplicate)</unmanaged>
    ///   <unmanaged-short>IDirectSound::DuplicateSoundBuffer</unmanaged-short>
    public IDirectSoundBuffer? DuplicateSoundBuffer(IDirectSoundBuffer sourceBuffer)
    {
        Result result = DuplicateSoundBuffer(sourceBuffer, out nint soundBufferPtr);
        IDirectSoundBuffer? soundBuffer = null;

        if (result.Success && soundBufferPtr != 0)
        {
            soundBuffer = MarshallingHelpers.FromPointer<IDirectSoundBuffer>(soundBufferPtr);
        }

        if (soundBuffer != null)
        {
            Marshal.Release(soundBufferPtr);
        }

        return soundBuffer;
    }
}
