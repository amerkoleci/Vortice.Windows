// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public partial struct DispatchGraphDescription
{
    public DispatchMode Mode;
    public _Anonymous_e__Union Anonymous;

    [UnscopedRef]
    public ref NodeCpuInput NodeCPUInput
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref Anonymous.NodeCPUInput;
        }
    }

    [UnscopedRef]
    public ref ulong NodeGPUInput
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref Anonymous.NodeGPUInput;
        }
    }

    [UnscopedRef]
    public ref MultiNodeCpuInput MultiNodeCPUInput
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref Anonymous.MultiNodeCPUInput;
        }
    }

    [UnscopedRef]
    public ref ulong MultiNodeGPUInput
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref Anonymous.MultiNodeGPUInput;
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    public struct _Anonymous_e__Union
    {
        [FieldOffset(0)]
        public NodeCpuInput NodeCPUInput;

        [FieldOffset(0)]
        public ulong NodeGPUInput;

        [FieldOffset(0)]
        public MultiNodeCpuInput MultiNodeCPUInput;

        [FieldOffset(0)]
        public ulong MultiNodeGPUInput;
    }
}
