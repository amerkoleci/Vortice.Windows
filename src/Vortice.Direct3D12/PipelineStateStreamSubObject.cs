// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    public interface IPipelineStateStreamSubObject
    {
        PipelineStateSubObjectType Type { get; }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    internal struct AlignedSubobjectType<T> where T : unmanaged
    {
        [FieldOffset(0)]
        public IntPtr Ptr;

        [FieldOffset(0)]
        public PipelineStateSubObjectType Type;

        [FieldOffset(4)]
        public T Inner;
    }

    public class PipelineStateStreamFlags
    {
        private readonly AlignedSubobjectType<PipelineStateFlags> _union;

        public PipelineStateStreamFlags(PipelineStateFlags flags = PipelineStateFlags.None)
        {
            _union.Type = PipelineStateSubObjectType.Flags;
            _union.Inner = flags;
        }
    }
}
