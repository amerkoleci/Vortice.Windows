// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.X3DAudio
{
    /// <summary>
    /// Receives the results from a call to <see cref="X3DAudio.Calculate(Listener, Emitter, CalculateFlags, int, int)"/>.
    /// </summary>
    public partial class DspSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DspSettings" /> class.
        /// </summary>
        /// <param name="sourceChannelCount">The source channel count.</param>
        /// <param name="destinationChannelCount">The destination channel count.</param>
        public DspSettings(int sourceChannelCount, int destinationChannelCount)
        {
            SourceChannelCount = sourceChannelCount;
            DestinationChannelCount = destinationChannelCount;

            MatrixCoefficients = new float[sourceChannelCount * destinationChannelCount];
            DelayTimes = new float[destinationChannelCount];
        }

        /// <summary>
        /// The matrix coefficients
        /// </summary>
        public readonly float[] MatrixCoefficients;

        /// <summary>
        /// The delay times
        /// </summary>
        public readonly float[] DelayTimes;

        // Internal native struct used for marshalling
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct __Native
        {
            public IntPtr MatrixCoefficientsPointer;
            public IntPtr DelayTimesPointer;
            public int SrcChannelCount;
            public int DstChannelCount;
            public float LPFDirectCoefficient;
            public float LPFReverbCoefficient;
            public float ReverbLevel;
            public float DopplerFactor;
            public float EmitterToListenerAngle;
            public float EmitterToListenerDistance;
            public float EmitterVelocityComponent;
            public float ListenerVelocityComponent;
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            LpfDirectCoefficient = @ref.LPFDirectCoefficient;
            LpfReverbCoefficient = @ref.LPFReverbCoefficient;
            ReverbLevel = @ref.ReverbLevel;
            DopplerFactor = @ref.DopplerFactor;
            EmitterToListenerAngle = @ref.EmitterToListenerAngle;
            EmitterToListenerDistance = @ref.EmitterToListenerDistance;
            EmitterVelocityComponent = @ref.EmitterVelocityComponent;
            ListenerVelocityComponent = @ref.ListenerVelocityComponent;
        }
    }
}
