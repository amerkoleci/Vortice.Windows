// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.DirectInput
{
    public partial class Envelope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Envelope"/> class.
        /// </summary>
        public unsafe Envelope()
        {
            Size = sizeof(__Native);
        }

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
            Size = @ref.Size;
            AttackLevel = @ref.AttackLevel;
            AttackTime = @ref.AttackTime;
            FadeLevel = @ref.FadeLevel;
            FadeTime = @ref.FadeTime;
        }

        internal void __MarshalTo(ref __Native @ref)
        {
            @ref.Size = Size;
            @ref.AttackLevel = AttackLevel;
            @ref.AttackTime = AttackTime;
            @ref.FadeLevel = FadeLevel;
            @ref.FadeTime = FadeTime;
        }
        #endregion Marshal
    }
}
