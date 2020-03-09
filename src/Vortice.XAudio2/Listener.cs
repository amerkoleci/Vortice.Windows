// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.XAudio2
{
    /// <summary>
    /// Defines a point of 3D audio reception.
    /// </summary>
    public partial class Listener
    {
        /// <summary>
        /// Reference to Cone data.
        /// </summary>
        public Cone? Cone;

        // Internal native struct used for marshalling
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal unsafe struct __Native
        {
            public Vector3 OrientFront;
            public Vector3 OrientTop;
            public Vector3 Position;
            public Vector3 Velocity;
            public void* ConePointer;
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.OrientFront = OrientFront;
            @ref.OrientTop = OrientTop;
            @ref.Position = Position;
            @ref.Velocity = Velocity;
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
