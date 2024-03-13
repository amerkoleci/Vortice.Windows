// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.XAudio2;

/// <summary>
/// Defines a point of 3D audio reception.
/// </summary>
public partial class Listener
{
    /// <summary>
    /// Cone data.
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
        public Cone* ConePointer;
        public Cone Cone;
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
            @ref.Cone = Cone.Value;
            @ref.ConePointer = (Cone*)Unsafe.AsPointer(ref @ref.Cone);
        }
    }
}
