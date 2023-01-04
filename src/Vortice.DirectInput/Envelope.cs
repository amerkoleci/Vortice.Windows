// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.DirectInput;

public partial class Envelope
{
    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal partial struct __Native
    {
        public int Size;
        public int AttackLevel;
        public int AttackTime;
        public int FadeLevel;
        public int FadeTime;
    }

    internal static unsafe __Native __NewNative()
    {
        __Native native = default;
        native.Size = sizeof(__Native);
        return native;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
    }

    internal void __MarshalFrom(ref __Native @ref)
    {
        AttackLevel = @ref.AttackLevel;
        AttackTime = @ref.AttackTime;
        FadeLevel = @ref.FadeLevel;
        FadeTime = @ref.FadeTime;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Size = sizeof(__Native);
        @ref.AttackLevel = AttackLevel;
        @ref.AttackTime = AttackTime;
        @ref.FadeLevel = FadeLevel;
        @ref.FadeTime = FadeTime;
    }
    #endregion Marshal
}
