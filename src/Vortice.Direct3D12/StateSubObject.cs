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
    public partial struct StateSubObject
    {
        public readonly StateSubObjectType Type;

        public readonly IntPtr Description;

        public StateSubObject(StateObjectConfig config)
        {
            Type = StateSubObjectType.StateObjectConfig;
            unsafe
            {
                Description = Interop.Alloc<StateObjectConfig>();
                Unsafe.WriteUnaligned(Description.ToPointer(), config);
            }
        }

        public StateSubObject(GlobalRootSignature globalRootSignature)
        {
            Type = StateSubObjectType.GlobalRootSignature;
            unsafe
            {
                Description = Marshal.AllocHGlobal(IntPtr.Size);
                Unsafe.WriteUnaligned(Description.ToPointer(), globalRootSignature.RootSignature.NativePointer);
            }
        }

        public StateSubObject(LocalRootSignature localRootSignature)
        {
            Type = StateSubObjectType.LocalRootSignature;
            unsafe
            {
                Description = Marshal.AllocHGlobal(IntPtr.Size);
                Unsafe.WriteUnaligned(Description.ToPointer(), localRootSignature.RootSignature.NativePointer);
            }
        }

        public StateSubObject(NodeMask nodeMask)
        {
            Type = StateSubObjectType.NodeMask;
            unsafe
            {
                Description = Marshal.AllocHGlobal(Unsafe.SizeOf<NodeMask>());
                Unsafe.WriteUnaligned(Description.ToPointer(), nodeMask);
            }
        }

        public StateSubObject(DxilLibraryDescription libraryDescription)
        {
            // TODO: Marshal DxilLibraryDescription
            Type = StateSubObjectType.DxilLibrary;
            unsafe
            {
                Description = Marshal.AllocHGlobal(Unsafe.SizeOf<DxilLibraryDescription>());
                Unsafe.WriteUnaligned(Description.ToPointer(), libraryDescription);
            }
        }

        public StateSubObject(ExistingCollectionDescription existingCollectionDescription)
        {
            // TODO: Marshal ExistingCollectionDescription
            Type = StateSubObjectType.ExistingCollection;
            unsafe
            {
                Description = Marshal.AllocHGlobal(Unsafe.SizeOf<ExistingCollectionDescription>());
                Unsafe.WriteUnaligned(Description.ToPointer(), existingCollectionDescription);
            }
        }

        public StateSubObject(SubObjectToExportsAssociation subObjectToExportsAssociation)
        {
            // TODO: Marshal SubObjectToExportsAssociation
            Type = StateSubObjectType.SubObjectToExportsAssociation;
            unsafe
            {
                Description = Marshal.AllocHGlobal(Unsafe.SizeOf<SubObjectToExportsAssociation>());
                Unsafe.WriteUnaligned(Description.ToPointer(), subObjectToExportsAssociation);
            }
        }

        public StateSubObject(DxilSubObjectToExportsAssociation subObjectToExportsAssociation)
        {
            // TODO: Marshal DxilSubObjectToExportsAssociation
            Type = StateSubObjectType.DxilSubObjectToExportsAssociation;
            unsafe
            {
                Description = Marshal.AllocHGlobal(Unsafe.SizeOf<DxilSubObjectToExportsAssociation>());
                Unsafe.WriteUnaligned(Description.ToPointer(), subObjectToExportsAssociation);
            }
        }

        public StateSubObject(RaytracingShaderConfig raytracingShaderConfig)
        {
            Type = StateSubObjectType.RaytracingShaderConfig;
            unsafe
            {
                Description = Marshal.AllocHGlobal(Unsafe.SizeOf<RaytracingShaderConfig>());
                Unsafe.WriteUnaligned(Description.ToPointer(), raytracingShaderConfig);
            }
        }

        public StateSubObject(RaytracingPipelineConfig raytracingPipelineConfig)
        {
            Type = StateSubObjectType.RaytracingPipelineConfig;
            unsafe
            {
                Description = Marshal.AllocHGlobal(Unsafe.SizeOf<RaytracingPipelineConfig>());
                Unsafe.WriteUnaligned(Description.ToPointer(), raytracingPipelineConfig);
            }
        }

        public StateSubObject(HitGroupDescription hitGroupDescription)
        {
            Type = StateSubObjectType.HitGroup;
            unsafe
            {
                var nativeDescription = new HitGroupDescription.__Native();
                hitGroupDescription.__MarshalTo(ref nativeDescription);
                Description = Interop.Alloc<HitGroupDescription.__Native>();
                MemoryHelpers.CopyMemory(
                    Description,
                    (IntPtr)Unsafe.AsPointer(ref nativeDescription),
                    sizeof(HitGroupDescription.__Native));
            }
        }
    }
}
