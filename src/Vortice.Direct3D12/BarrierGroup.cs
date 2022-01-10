// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;
/// <summary>
/// Describes a group of barrier of a given type
/// </summary>
public partial class BarrierGroup
{
    /// <summary>
    /// Type of barriers in the group, see <see cref="BarrierType"/>
    /// </summary>
    public readonly BarrierType Type;

    /// <summary>
    /// Number of barriers in the group
    /// </summary>
    public readonly int NumBarriers;

    /// <summary>
    /// Array of <see cref="GlobalBarrier"/> if <see cref="Type"/> is <see cref="BarrierType.Global"/>.
    /// </summary>
    public GlobalBarrier[]? GlobalBarriers { get; }

    /// <summary>
    /// Array of <see cref="TextureBarrier"/> if <see cref="Type"/> is <see cref="BarrierType.Texture"/>.
    /// </summary>
    public TextureBarrier[]? TextureBarriers { get; }

    /// <summary>
    /// Array of <see cref="BufferBarrier"/> if <see cref="Type"/> is <see cref="BarrierType.Buffer"/>.
    /// </summary>
    public BufferBarrier[]? BufferBarriers { get; }

    public BarrierGroup(GlobalBarrier[] globalBarriers)
    {
        Type = BarrierType.Global;
        NumBarriers = globalBarriers.Length;
        GlobalBarriers = globalBarriers;
    }

    public BarrierGroup(TextureBarrier[] textureBarriers)
    {
        Type = BarrierType.Texture;
        NumBarriers = textureBarriers.Length;
        TextureBarriers = textureBarriers;
    }

    public BarrierGroup(BufferBarrier[] bufferBarriers)
    {
        Type = BarrierType.Buffer;
        NumBarriers = bufferBarriers.Length;
        BufferBarriers = bufferBarriers;
    }

    #region Marshal
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct Union
    {
        [FieldOffset(0)]
        public GlobalBarrier* pGlobalBarriers;

        [FieldOffset(0)]
        public TextureBarrier.__Native* pTextureBarriers;

        [FieldOffset(0)]
        public BufferBarrier.__Native* pBufferBarriers;

        [FieldOffset(0)]
        public ResourceStateBarrier.__Native* pStateBarriers;
    }

    internal struct __Native
    {
        public BarrierType Type;
        public int NumBarriers;
        public Union Union;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        switch (Type)
        {
            case BarrierType.Global:
                MemoryHelpers.FreeMemory(@ref.Union.pGlobalBarriers);
                break;

            case BarrierType.Texture:
                for (int i = 0; i < NumBarriers; ++i)
                {
                    TextureBarriers![i].__MarshalFree(ref @ref.Union.pTextureBarriers[i]);
                }
                MemoryHelpers.FreeMemory(@ref.Union.pTextureBarriers);
                break;

            case BarrierType.Buffer:
                for (int i = 0; i < NumBarriers; ++i)
                {
                    BufferBarriers![i].__MarshalFree(ref @ref.Union.pBufferBarriers[i]);
                }
                MemoryHelpers.FreeMemory(@ref.Union.pBufferBarriers);
                break;
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Type;
        @ref.NumBarriers = NumBarriers;
        switch (Type)
        {
            case BarrierType.Global:
                @ref.Union.pGlobalBarriers = (GlobalBarrier*)MemoryHelpers.AllocateMemory((nuint)(NumBarriers * sizeof(GlobalBarrier)));
                fixed (GlobalBarrier* dataPtr = &GlobalBarriers![0])
                {
                    Unsafe.CopyBlockUnaligned(@ref.Union.pGlobalBarriers, dataPtr, (uint)sizeof(GlobalBarrier));
                }
                break;

            case BarrierType.Texture:
                @ref.Union.pTextureBarriers = (TextureBarrier.__Native*)MemoryHelpers.AllocateMemory((nuint)(NumBarriers * sizeof(TextureBarrier.__Native)));
                for (int i = 0; i < NumBarriers; ++i)
                {
                    TextureBarriers![i].__MarshalTo(ref @ref.Union.pTextureBarriers[i]);
                }
                break;

            case BarrierType.Buffer:
                @ref.Union.pBufferBarriers = (BufferBarrier.__Native*)MemoryHelpers.AllocateMemory((nuint)(NumBarriers * sizeof(BufferBarrier.__Native)));
                for (int i = 0; i < NumBarriers; ++i)
                {
                    BufferBarriers![i].__MarshalTo(ref @ref.Union.pBufferBarriers[i]);
                }
                break;
        }
    }
    #endregion
}
