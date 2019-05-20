// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.DirectX.Direct3D12
{
    public partial struct ResourceBarrier
    {
        /// <summary>
        /// Specifies the barrier type, see <see cref="ResourceBarrierType"/>
        /// </summary>
        public ResourceBarrierType Type;

        /// <summary>	
        /// Specifies a <see cref="ResourceBarrierFlags"/> enumeration constant such as for "begin only" or "end only".
        /// </summary>	
        public ResourceBarrierFlags Flags;

        private Union _union;

        public ResourceTransitionBarrier Transition
        {
            get => _union.Transition;
            set => _union.Transition = value;
        }

        public ResourceAliasingBarrier Aliasing
        {
            get => _union.Aliasing;
            set => _union.Aliasing = value;
        }

        public ResourceUnorderedAccessViewBarrier UnorderedAccessView
        {
            get => _union.UnorderedAccessView;
            set => _union.UnorderedAccessView = value;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 0)]
        private struct Union
        {
            [FieldOffset(0)]
            public ResourceTransitionBarrier Transition;

            [FieldOffset(0)]
            public ResourceAliasingBarrier Aliasing;

            [FieldOffset(0)]
            public ResourceUnorderedAccessViewBarrier UnorderedAccessView;
        }
    }
}
