// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <summary>
/// Defines the inputs for a raytracing acceleration structure build operation.
/// This is used by <see cref="ID3D12GraphicsCommandList4.BuildRaytracingAccelerationStructure(BuildRaytracingAccelerationStructureDescription)"/> and <see cref="ID3D12Device5.GetRaytracingAccelerationStructurePrebuildInfo(BuildRaytracingAccelerationStructureInputs)"/>.
/// </summary>
public partial class BuildRaytracingAccelerationStructureInputs
{
    public RaytracingAccelerationStructureType Type { get; set; }

    public RaytracingAccelerationStructureBuildFlags Flags { get; set; }

    public int DescriptorsCount { get; set; }

    public ElementsLayout Layout { get; set; }

    public ulong InstanceDescriptions { get; set; }

    public RaytracingGeometryDescription[]? GeometryDescriptions { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public RaytracingAccelerationStructureType Type;
        public RaytracingAccelerationStructureBuildFlags Flags;
        public int NumDescs;
        public ElementsLayout DescsLayout;
        public Union Union;

        internal void __MarshalFree()
        {
            if (Type == RaytracingAccelerationStructureType.BottomLevel
                && Union.pGeometryDescs != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Union.pGeometryDescs);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 0)]
    internal unsafe struct Union
    {
        [FieldOffset(0)]
        public ulong InstanceDescs;

        [FieldOffset(0)]
        public IntPtr pGeometryDescs;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        Type = @ref.Type;
        Flags = @ref.Flags;
        DescriptorsCount = @ref.NumDescs;
        Layout = @ref.DescsLayout;

        if (@ref.NumDescs > 0)
        {
            if (@ref.Type == RaytracingAccelerationStructureType.TopLevel)
            {
                InstanceDescriptions = Unsafe.Read<ulong>(@ref.Union.pGeometryDescs.ToPointer());
            }
            else
            {
                GeometryDescriptions = new RaytracingGeometryDescription[@ref.NumDescs];
                UnsafeUtilities.Read(@ref.Union.pGeometryDescs, GeometryDescriptions);
            }
        }
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Type;
        @ref.Flags = Flags;
        @ref.NumDescs = DescriptorsCount;
        @ref.DescsLayout = Layout;

        if (GeometryDescriptions != null
            && GeometryDescriptions.Length > 0)
        {
            @ref.Union.pGeometryDescs = UnsafeUtilities.AllocToPointer(GeometryDescriptions);
        }
        else
        {
            @ref.Union.InstanceDescs = InstanceDescriptions;
        }
    }
    #endregion
}
