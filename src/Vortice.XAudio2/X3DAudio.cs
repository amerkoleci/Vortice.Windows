// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Multimedia;

namespace Vortice.XAudio2
{
    public partial class X3DAudio
    {
        /// <summary>
        /// Speed of sound in meters per second for dry air at approximately 20C.
        /// </summary>
        public const float SpeedOfSound = 343.5f;


        private delegate void X3DAudioCalculateDelegate(
            ref X3DAudioHandle instance,
            Listener listener, Emitter emitter,
            CalculateFlags flags, IntPtr dSPSettings);

        private X3DAudioHandle _handle;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="X3DAudio" /> class.
        /// </summary>
        /// <param name="speakers">The speakers config.</param>
        public X3DAudio(Speakers speakers) 
            : this(speakers, SpeedOfSound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X3DAudio"/> class.
        /// </summary>
        /// <param name="speakers">The speakers config.</param>
        /// <param name="speedOfSound">The speed of sound.</param>
        public X3DAudio(Speakers speakers, float speedOfSound)
        {
            XAudio2Native.X3DAudioInitialize(speakers, speedOfSound, out _handle).CheckError();
        }

        /// <summary>
        /// Calculates DSP settings for the specified listener and emitter.
        /// </summary>
        /// <param name="listener">The <see cref="Listener"/>.</param>
        /// <param name="emitter">The <see cref="Emitter"/>.</param>
        /// <param name="flags">The <see cref="CalculateFlags"/>.</param>
        /// <param name="sourceChannelCount">The source channel count.</param>
        /// <param name="destinationChannelCount">The destination channel count.</param>
        /// <returns>An instance of <see cref="DspSettings"/> class.</returns>
        public DspSettings Calculate(Listener listener, Emitter emitter, CalculateFlags flags, int sourceChannelCount, int destinationChannelCount)
        {
            var settings = new DspSettings(sourceChannelCount, destinationChannelCount);
            Calculate(listener, emitter, flags, settings);
            return settings;
        }

        /// <summary>
        /// Calculates DSP settings for the specified listener and emitter. See remarks.
        /// </summary>
        /// <param name="listener">The <see cref="Listener"/>.</param>
        /// <param name="emitter">The <see cref="Emitter"/>.</param>
        /// <param name="flags">The <see cref="CalculateFlags"/>.</param>
        /// <param name="settings">The <see cref="DspSettings"/>.</param>
        /// <remarks>The source and destination channel count must be set on <see cref="DspSettings" /> before calling this method.</remarks>
        public void Calculate(Listener listener, Emitter emitter, CalculateFlags flags, DspSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            DspSettings.__Native settingsNative;
            settingsNative.SrcChannelCount = settings.SourceChannelCount;
            settingsNative.DstChannelCount = settings.DestinationChannelCount;

            unsafe
            {
                fixed (void* pMatrix = settings.MatrixCoefficients)
                {
                    fixed (void* pDelays = settings.DelayTimes)
                    {
                        settingsNative.MatrixCoefficientsPointer = (IntPtr)pMatrix;
                        settingsNative.DelayTimesPointer = (IntPtr)pDelays;

                        XAudio2Native.X3DAudioCalculate(ref _handle, listener, emitter, flags, new IntPtr(&settingsNative));
                    }
                }
            }

            settings.__MarshalFrom(ref settingsNative);
        }
    }
}
