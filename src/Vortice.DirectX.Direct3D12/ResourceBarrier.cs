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
        public readonly ResourceBarrierType Type;

        /// <summary>	
        /// Specifies a <see cref="ResourceBarrierFlags"/> enumeration constant such as for "begin only" or "end only".
        /// </summary>	
        public readonly ResourceBarrierFlags Flags;

        private readonly Union _union;

        public ResourceTransitionBarrier Transition
        {
            get => _union.Transition;
        }

        public ResourceAliasingBarrier Aliasing
        {
            get => _union.Aliasing;
        }

        public ResourceUnorderedAccessViewBarrier UnorderedAccessView
        {
            get => _union.UnorderedAccessView;
        }

        /// <summary>
        /// Initializes a new transition instance of <see cref="ResourceBarrier"/> struct.
        /// </summary>
        /// <param name="transition">The transition barrier.</param>
        /// <param name="flags"></param>
        public ResourceBarrier(ResourceTransitionBarrier transition, ResourceBarrierFlags flags = ResourceBarrierFlags.None)
        {
            Type = ResourceBarrierType.Transition;
            Flags = flags;
            _union = new Union { Transition = transition };
        }

        /// <summary>
        /// Initializes a new aliasing instance of <see cref="ResourceBarrier"/> struct.
        /// </summary>
        /// <param name="aliasing">The aliasing.</param>
        public ResourceBarrier(ResourceAliasingBarrier aliasing)
        {
            Type = ResourceBarrierType.Aliasing;
            Flags = ResourceBarrierFlags.None;
            _union = new Union { Aliasing = aliasing };
        }

        /// <summary>
        /// Initializes a new UAV instance of the <see cref="ResourceBarrier"/> struct.
        /// </summary>
        /// <param name="unorderedAccessView">The unordered access view.</param>
        public ResourceBarrier(ResourceUnorderedAccessViewBarrier unorderedAccessView)
        {
            Type = ResourceBarrierType.UnorderedAccessView;
            Flags = ResourceBarrierFlags.None;
            _union = new Union { UnorderedAccessView = unorderedAccessView };
        }

        /// <summary>
        /// Create a new transition resource barrier.
        /// </summary>
        /// <param name="resource">The transition resource.</param>
        /// <param name="stateBefore">The state before.</param>
        /// <param name="stateAfter">The state after.</param>
        /// <param name="subresource">The subresource.</param>
        /// <param name="flags">The transition flags.</param>
        /// <returns>New intance of <see cref="ResourceBarrier"/> struct.</returns>
        public static ResourceBarrier BarrierTransition(
            ID3D12Resource resource,
            ResourceStates stateBefore,
            ResourceStates stateAfter,
            int subresource = AllSubResources,
            ResourceBarrierFlags flags = ResourceBarrierFlags.None)
        {
            return new ResourceBarrier(
                new ResourceTransitionBarrier(resource, stateBefore, stateAfter, subresource),
                flags);
        }

        /// <summary>
        /// Create a new aliasing resource barrier.
        /// </summary>
        /// <param name="resourceBefore">The resource before.</param>
        /// <param name="resourceAfter">The resource after.</param>
        /// <returns>New intance of <see cref="ResourceBarrier"/> struct.</returns>
        public static ResourceBarrier BarrierAliasing(ID3D12Resource resourceBefore, ID3D12Resource resourceAfter)
        {
            return new ResourceBarrier(new ResourceAliasingBarrier(resourceBefore, resourceAfter));
        }

        /// <summary>
        /// Create a new UAV resource barrier instance.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>New intance of <see cref="ResourceBarrier"/> struct.</returns>
        public static ResourceBarrier BarrierUnorderedAccessView(ID3D12Resource resource)
        {
            return new ResourceBarrier(new ResourceUnorderedAccessViewBarrier(resource));
        }

        #region Nested
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
        #endregion
    }
}
