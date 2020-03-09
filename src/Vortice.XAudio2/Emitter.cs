// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.XAudio2
{
    /// <summary>
    /// Defines a single-point or multiple-point 3D audio source that is used with an arbitrary number of sound channels.
    /// </summary>
    public partial class Emitter
    {
        /// <summary>
        /// Reference to Cone data.
        /// </summary>
        public Cone? Cone;

        public float[] ChannelAzimuths;

        public CurvePoint[] VolumeCurve;

        public CurvePoint[] LfeCurve;

        public CurvePoint[] LpfDirectCurve;

        public CurvePoint[] LpfReverbCurve;

        public CurvePoint[] ReverbCurve;

        // Internal native struct used for marshalling
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal unsafe struct __Native
        {
            public void* ConePointer;
            public Vector3 OrientFront;
            public Vector3 OrientTop;
            public Vector3 Position;
            public Vector3 Velocity;
            public float InnerRadius;
            public float InnerRadiusAngle;
            public int ChannelCount;
            public float ChannelRadius;
            public IntPtr ChannelAzimuthsPointer;
            public IntPtr VolumeCurvePointer;
            public IntPtr LFECurvePointer;
            public IntPtr LPFDirectCurvePointer;
            public IntPtr LPFReverbCurvePointer;
            public IntPtr ReverbCurvePointer;
            public float CurveDistanceScaler;
            public float DopplerScaler;

            internal unsafe void __MarshalFree()
            {
                if (ChannelAzimuthsPointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(ChannelAzimuthsPointer);
                if (VolumeCurvePointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(VolumeCurvePointer);
                if (LFECurvePointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(LFECurvePointer);
                if (LPFDirectCurvePointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(LPFDirectCurvePointer);
                if (LPFReverbCurvePointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(LPFReverbCurvePointer);
                if (ReverbCurvePointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(ReverbCurvePointer);
            }
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            @ref.__MarshalFree();
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.OrientFront = OrientFront;
            @ref.OrientTop = OrientTop;
            @ref.Position = Position;
            @ref.Velocity = Velocity;
            @ref.InnerRadius = InnerRadius;
            @ref.InnerRadiusAngle = InnerRadiusAngle;
            @ref.ChannelCount = ChannelCount;
            @ref.ChannelRadius = ChannelRadius;

            if (ChannelAzimuths != null 
                && ChannelAzimuths.Length > 0 && ChannelCount > 0)
            {
                @ref.ChannelAzimuthsPointer = Marshal.AllocHGlobal(sizeof(float) * Math.Min(ChannelCount, ChannelAzimuths.Length));
                MemoryHelpers.Write(@ref.ChannelAzimuthsPointer, new Span<float>(ChannelAzimuths), ChannelCount);
            }

            @ref.VolumeCurvePointer = DistanceCurve.FromCurvePoints(VolumeCurve);
            @ref.LFECurvePointer = DistanceCurve.FromCurvePoints(LfeCurve);
            @ref.LPFDirectCurvePointer = DistanceCurve.FromCurvePoints(LpfDirectCurve);
            @ref.LPFReverbCurvePointer = DistanceCurve.FromCurvePoints(LpfReverbCurve);
            @ref.ReverbCurvePointer = DistanceCurve.FromCurvePoints(ReverbCurve);
            @ref.CurveDistanceScaler = CurveDistanceScaler;
            @ref.DopplerScaler = DopplerScaler;

            if (Cone == null)
            {
                @ref.ConePointer = null;
            }
            else
            {
                var coneValue = Cone.Value;
                @ref.ConePointer = Unsafe.AsPointer(ref coneValue);
            }
        }
    }
}
