// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.D3D12MemoryAllocator;

[Flags]
public enum AllocatorFlags
{
    None = 0,
    SingleThreaded = 0x1,
    AlwaysCommitted = 0x2,
    DefaultPoolsNotZeroed = 0x4,
    MSAATexturesAlwaysCommitted = 0x8,
    DontPreferSmallBuffersCommitted = 0x10
}
