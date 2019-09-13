// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DirectX;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Represents a subobject with in a state object description. 
    /// Use with <see cref="StateObjectDescription"/>.
    /// </summary>
    public partial class StateSubObject
    {
        public StateSubObjectType Type => Description.SubObjectType;

        public IStateSubObjectDescription Description { get; set; }

        //public StateSubObject(StateObjectConfig config)
        //{
        //    Type = StateSubObjectType.StateObjectConfig;
        //    unsafe
        //    {
        //        Description = Interop.Alloc<StateObjectConfig>();
        //        Unsafe.WriteUnaligned(Description.ToPointer(), config);
        //    }
        //}

        //public StateSubObject(GlobalRootSignature globalRootSignature)
        //{
        //    Type = StateSubObjectType.GlobalRootSignature;
        //    unsafe
        //    {
        //        Description = Marshal.AllocHGlobal(IntPtr.Size);
        //        Unsafe.WriteUnaligned(Description.ToPointer(), globalRootSignature.RootSignature.NativePointer);
        //    }
        //}

        //public StateSubObject(LocalRootSignature localRootSignature)
        //{
        //    Type = StateSubObjectType.LocalRootSignature;
        //    unsafe
        //    {
        //        Description = Marshal.AllocHGlobal(IntPtr.Size);
        //        Unsafe.WriteUnaligned(Description.ToPointer(), localRootSignature.RootSignature.NativePointer);
        //    }
        //}

        //public StateSubObject(NodeMask nodeMask)
        //{
        //    Type = StateSubObjectType.NodeMask;
        //    unsafe
        //    {
        //        Description = Marshal.AllocHGlobal(Unsafe.SizeOf<NodeMask>());
        //        Unsafe.WriteUnaligned(Description.ToPointer(), nodeMask);
        //    }
        //}

        //public StateSubObject(DxilLibraryDescription libraryDescription)
        //{
        //    Type = StateSubObjectType.DxilLibrary;
        //    unsafe
        //    {
        //        var nativeDescription = new DxilLibraryDescription.__Native();
        //        libraryDescription.__MarshalTo(ref nativeDescription);

        //        Description = Marshal.AllocHGlobal(Unsafe.SizeOf<DxilLibraryDescription.__Native>());
        //        MemoryHelpers.CopyMemory(
        //           Description,
        //           (IntPtr)Unsafe.AsPointer(ref nativeDescription),
        //           sizeof(DxilLibraryDescription.__Native));

        //        libraryDescription.__MarshalFree(ref nativeDescription);
        //    }
        //}

        //public StateSubObject(ExistingCollectionDescription existingCollectionDescription)
        //{
        //    Type = StateSubObjectType.ExistingCollection;
        //    unsafe
        //    {
        //        var nativeDescription = new ExistingCollectionDescription.__Native();
        //        existingCollectionDescription.__MarshalTo(ref nativeDescription);

        //        Description = Marshal.AllocHGlobal(Unsafe.SizeOf<ExistingCollectionDescription.__Native>());
        //        MemoryHelpers.CopyMemory(
        //           Description,
        //           (IntPtr)Unsafe.AsPointer(ref nativeDescription),
        //           sizeof(ExistingCollectionDescription.__Native));

        //        existingCollectionDescription.__MarshalFree(ref nativeDescription);
        //    }
        //}

        //public StateSubObject(SubObjectToExportsAssociation subObjectToExportsAssociation)
        //{
        //    Type = StateSubObjectType.SubObjectToExportsAssociation;
        //    unsafe
        //    {
        //        var nativeDescription = new SubObjectToExportsAssociation.__Native();
        //        subObjectToExportsAssociation.__MarshalTo(ref nativeDescription);

        //        Description = Marshal.AllocHGlobal(Unsafe.SizeOf<SubObjectToExportsAssociation.__Native>());
        //        MemoryHelpers.CopyMemory(
        //           Description,
        //           (IntPtr)Unsafe.AsPointer(ref nativeDescription),
        //           sizeof(SubObjectToExportsAssociation.__Native));

        //        subObjectToExportsAssociation.__MarshalFree(ref nativeDescription);
        //    }
        //}

        //public StateSubObject(DxilSubObjectToExportsAssociation subObjectToExportsAssociation)
        //{
        //    Type = StateSubObjectType.DxilSubObjectToExportsAssociation;
        //    unsafe
        //    {
        //        var nativeDescription = new DxilSubObjectToExportsAssociation.__Native();
        //        subObjectToExportsAssociation.__MarshalTo(ref nativeDescription);

        //        Description = Marshal.AllocHGlobal(Unsafe.SizeOf<DxilSubObjectToExportsAssociation.__Native>());
        //        MemoryHelpers.CopyMemory(
        //           Description,
        //           (IntPtr)Unsafe.AsPointer(ref nativeDescription),
        //           sizeof(DxilSubObjectToExportsAssociation.__Native));

        //        subObjectToExportsAssociation.__MarshalFree(ref nativeDescription);
        //    }
        //}

        //public StateSubObject(RaytracingShaderConfig raytracingShaderConfig)
        //{
        //    Type = StateSubObjectType.RaytracingShaderConfig;
        //    unsafe
        //    {
        //        Description = Marshal.AllocHGlobal(Unsafe.SizeOf<RaytracingShaderConfig>());
        //        Unsafe.WriteUnaligned(Description.ToPointer(), raytracingShaderConfig);
        //    }
        //}

        //public StateSubObject(RaytracingPipelineConfig raytracingPipelineConfig)
        //{
        //    Type = StateSubObjectType.RaytracingPipelineConfig;
        //    unsafe
        //    {
        //        Description = Marshal.AllocHGlobal(Unsafe.SizeOf<RaytracingPipelineConfig>());
        //        Unsafe.WriteUnaligned(Description.ToPointer(), raytracingPipelineConfig);
        //    }
        //}

        public StateSubObject(IStateSubObjectDescription description)
        {
            Description = description;
        }

        //public StateSubObject(HitGroupDescription hitGroupDescription)
        //{
        //    Type = StateSubObjectType.HitGroup;
        //    unsafe
        //    {
        //        var nativeDescription = new HitGroupDescription.__Native();
        //        hitGroupDescription.__MarshalTo(ref nativeDescription);
        //        Description = Interop.Alloc<HitGroupDescription.__Native>();
        //        MemoryHelpers.CopyMemory(
        //            Description,
        //            (IntPtr)Unsafe.AsPointer(ref nativeDescription),
        //            sizeof(HitGroupDescription.__Native));
        //        hitGroupDescription.__MarshalFree(ref nativeDescription);
        //    }
        //}

        //internal unsafe void __MarshalFree()
        //{
        //    if (Description != IntPtr.Zero)
        //    {
        //        Marshal.FreeHGlobal(Description);
        //    }
        //}

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal unsafe struct __Native
        {
            public StateSubObjectType Type;
            public IntPtr pDesc;
        }

        internal void __MarshalFree(ref __Native @ref)
        {
            if (Description is IStateSubObjectDescriptionMarshal descriptionMarshal)
            {
                descriptionMarshal.__MarshalFree(ref @ref.pDesc);
            }
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.Type = Type;
            if (Description is IStateSubObjectDescriptionMarshal descriptionMarshal)
            {
                @ref.pDesc = descriptionMarshal.__MarshalAlloc();
            }
        }
        #endregion
    }

    public interface IStateSubObjectDescription
    {
        StateSubObjectType SubObjectType { get; }
    }

    internal interface IStateSubObjectDescriptionMarshal
    {
        IntPtr __MarshalAlloc();
        void __MarshalFree(ref IntPtr pDesc);
    }
}
