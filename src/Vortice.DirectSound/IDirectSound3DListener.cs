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

namespace Vortice.DirectSound;

public partial class IDirectSound3DListener
{
    /// <summary>
    /// Default distance factor. The default value is (1.0).
    /// </summary>
    public const float DefaultDistanceFactor = 1f;
    /// <summary>
    /// Default Doppler factor. The default value is (1.0).
    /// </summary>
    public const float DefaultDopplerFactor = 1f;
    /// <summary>
    /// Default rolloff factor. The default value is (1.0).
    /// </summary>
    public const float DefaultRolloffFactor = 1f;
    /// <summary>
    /// Maximum distance factor. The default value is (1.0).
    /// </summary>
    public const float MaxDistanceFactor = float.MaxValue;
    /// <summary>
    /// Maximum Doppler factor. The default value is (1.0).
    /// </summary>
    public const float MaxDopplerFactor = 10f;
    /// <summary>
    /// Maximum rolloff factor. The default value is (1.0).
    /// </summary>
    public const float MaxRolloffFactor = 10f;
    /// <summary>
    /// Minimum distance factor. The default value is (1.0).
    /// </summary>
    public const float MinDistanceFactor = 1.175494E-38f;
    /// <summary>
    /// Minimum Doppler factor. The default value is (1.0).
    /// </summary>
    public const float MinDopplerFactor = 0f;
    /// <summary>
    /// Minimum rolloff factor. The default value is (1.0).
    /// </summary>
    public const float MinRolloffFactor = 0f;

    /// <summary>
    /// Initializes a new instance of the <see cref="IDirectSound3DListener" /> class with a previously created sound buffer.
    /// </summary>
    /// <param name="soundBuffer">The SlimDX::DirectSound::SoundBuffer object.</param>
    public IDirectSound3DListener(IDirectSoundBuffer soundBuffer)
    {
        QueryInterfaceFrom(soundBuffer);
    }

    /// <summary>
    /// Determines if settings are set immediately or deferred.
    /// </summary>
    public bool Deferred { get; set; }

    /// <summary>
    /// Gets or sets the distance factor, which is the number of meters in a vector unit.
    /// </summary>
    public float DistanceFactor
    {
        get => GetDistanceFactor();
        set
        {
            SetDistanceFactor(value, Deferred ? 1 : 0);
        }
    }
    /// <summary>
    /// Gets or sets the multiplier for the Doppler effect.
    /// </summary>
    public float DopplerFactor
    {
        get => GetDopplerFactor();
        set
        {
            SetDopplerFactor(value, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// Describes the listener's front orientation.
    /// </summary>
    public Vector3 FrontOrientation
    {
        get
        {
            GetOrientation(out Vector3 frontOrientation, out Vector3 _);
            return frontOrientation;
        }
        set
        {
            Vector3 topOrientation = TopOrientation;
            SetOrientation(value.X, value.Y, value.Z, topOrientation.X, topOrientation.Y, topOrientation.Z, Deferred ? 1 : 0);
        }

    }

    /// <summary>
    /// Gets or sets the listener's position.
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
    /// Gets or sets the rolloff factor, which determines the rate of attenuation over distance.
    /// </summary>
    public float RolloffFactor
    {
        get => GetRolloffFactor();
        set
        {
            SetRolloffFactor(value, Deferred ? 1 : 0);
        }
    }
    /// <summary>
    /// Describes the listener's top orientation.
    /// </summary>
    public Vector3 TopOrientation
    {
        get
        {
            GetOrientation(out Vector3 _, out Vector3 topOrientation);
            return topOrientation;
        }
        set
        {
            Vector3 frontOrientation = FrontOrientation;
            SetOrientation(FrontOrientation.X, frontOrientation.Y, frontOrientation.Z, value.X, value.Y, value.Z, Deferred ? 1 : 0);
        }
    }

    /// <summary>
    /// Gets or sets the listener's velocity.
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
