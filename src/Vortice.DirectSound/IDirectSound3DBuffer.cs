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

using System.Numerics;
using SharpGen.Runtime;

namespace Vortice.DirectSound;

public partial class IDirectSound3DBuffer
{
    /// <summary>
    /// Default cone angle, in degrees.
    /// </summary>
    public const float DefaultConeAngle = 360f;
    /// <summary>
    /// Default outside cone volume. Volume levels are expressed as attenuation, in hundredths of a decibel.
    /// </summary>
    public const int DefaultConeOutsideVolume = 0;
    /// <summary>
    /// Default maximum distance, in meters.
    /// </summary>
    public const float DefaultMaxDistance = 1E+09f;
    /// <summary>
    /// Default minimum distance, in meters.
    /// </summary>
    public const float DefaultMinDistance = 1f;
    /// <summary>
    /// Maximum cone angle, in degrees.
    /// </summary>
    public const float MaxConeAngle = 360f;
    /// <summary>
    /// Minimum cone angle, in degrees.
    /// </summary>
    public const float MinConeAngle = 0f;

    /// <summary>
    /// Initializes a new instance of the <see cref="IDirectSound3DBuffer" /> class.
    /// </summary>
    /// <param name="soundBuffer" />
    /// <returns />
    public IDirectSound3DBuffer(IDirectSoundBuffer soundBuffer)
    {
        QueryInterfaceFrom(soundBuffer);
    }

    /// <summary>
    /// Gets or sets all the  parameters of a buffer
    /// </summary>
    public Buffer3DSettings AllParameters
    {
        get => GetAllParameters();
        set
        {
            SetAllParameters(value, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// The orientation of the sound projection cone.
    /// </summary>
    public Vector3 ConeOrientation
    {
        get => GetConeOrientation();
        set
        {
            SetConeOrientation(value.X, value.Y, value.Z, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// The volume of the sound outside the outside angle of the sound projection cone.
    /// </summary>
    public int ConeOutsideVolume
    {
        get
        {
            NativeLong temp =  GetConeOutsideVolume();
            return (int)temp;
        }
        set
        {
            SetConeOutsideVolume(value, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// Settings are not applied until the application calls the SoundListener3D.CommitDeferredSettings() if true.
    /// </summary>
    public bool Deferred { get; set; }

    /// <summary>
    /// The inside angle of the sound projection cone.
    /// </summary>
    public int InsideConeAngle
    {
        get
        {
            GetConeAngles(out int insideCondeAngle, out int _);
            return insideCondeAngle;
        }
        set
        {
            SetConeAngles(value, OutsideConeAngle, Deferred ? 1 : 0);
        }

    }

    /// <summary>
    /// The maximum distance, which is the distance from the listener beyond which sounds in this buffer are no longer attenuated.
    /// </summary>
    public float MaxDistance
    {
        get => GetMaxDistance();
        set
        {
            SetMaxDistance(value, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// The minimum distance, which is the distance from the listener at which sounds in this buffer begin to be attenuated.
    /// </summary>
    public float MinDistance
    {
        get => GetMinDistance();
        set
        {
            SetMinDistance(value, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// The operation mode for 3-D sound processing.
    /// </summary>
    public Mode3D Mode
    {
        get
        {
            return (Mode3D)GetMode();
        }
        set
        {
            SetMode((int)value, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// The outside angle of the sound projection cone.
    /// </summary>
    public int OutsideConeAngle
    {
        get
        {
            GetConeAngles(out int _, out int outsideConeAngle);
            return outsideConeAngle;
        }
        set
        {
            SetConeAngles(InsideConeAngle, value, Deferred ? 1 : 0);
        }
    }
    /// <summary>
    /// The position of the sound source.
    /// </summary>
    public Vector3 Position
    {
        get => GetPosition();
        set
        {
            SetPosition(value.X, value.Y, value.Z, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// The velocity of the sound source.
    /// </summary>
    public Vector3 Velocity
    {
        get => GetVelocity();
        set
        {
            SetVelocity(value.X, value.Y, value.Z, Deferred ? 1 : 0);
        }
    }
}
