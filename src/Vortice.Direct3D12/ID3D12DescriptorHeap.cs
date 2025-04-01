// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12DescriptorHeap
{
    private uint GetCPUDescriptorHandleForHeapStart__vtbl_index = 9;
    private uint GetGPUDescriptorHandleForHeapStart__vtbl_index = 10;

    /// <include file="Documentation.xml" path="/comments/comment[@id='ID3D12DescriptorHeap::GetCPUDescriptorHandleForHeapStart']/*" />
    /// <unmanaged>D3D12_CPU_DESCRIPTOR_HANDLE ID3D12DescriptorHeap::GetCPUDescriptorHandleForHeapStart()</unmanaged>
    /// <unmanaged-short>ID3D12DescriptorHeap::GetCPUDescriptorHandleForHeapStart</unmanaged-short>
    public CpuDescriptorHandle GetCPUDescriptorHandleForHeapStart()
    {
        // Implement vkd3d-native behavior on Linux
        // See https://github.com/HansKristian-Work/vkd3d-proton/blob/fc2b6f419008ffc5fbdee8564c70cded29abf7bb/libs/vkd3d/resource.c#L7447
        if (PlatformDetection.IsItaniumSystemV)
        {
            CpuDescriptorHandle result;
            return *((delegate* unmanaged[Stdcall]<nint, CpuDescriptorHandle*, CpuDescriptorHandle*>)this[GetCPUDescriptorHandleForHeapStart__vtbl_index])(NativePointer, &result);
        }
        
        return ((delegate* unmanaged[MemberFunction]<nint, CpuDescriptorHandle>)this[GetCPUDescriptorHandleForHeapStart__vtbl_index])(NativePointer);
    }

    /// <include file="Documentation.xml" path="/comments/comment[@id='ID3D12DescriptorHeap::GetGPUDescriptorHandleForHeapStart']/*" />
    /// <unmanaged>D3D12_GPU_DESCRIPTOR_HANDLE ID3D12DescriptorHeap::GetGPUDescriptorHandleForHeapStart()</unmanaged>
    /// <unmanaged-short>ID3D12DescriptorHeap::GetGPUDescriptorHandleForHeapStart</unmanaged-short>
    public GpuDescriptorHandle GetGPUDescriptorHandleForHeapStart()
    {
        // Implement vkd3d-native behavior on Linux
        // See https://github.com/HansKristian-Work/vkd3d-proton/blob/fc2b6f419008ffc5fbdee8564c70cded29abf7bb/libs/vkd3d/resource.c#L7459
        if (PlatformDetection.IsItaniumSystemV)
        {
            GpuDescriptorHandle result;
            return *((delegate* unmanaged[Stdcall]<nint, GpuDescriptorHandle*, GpuDescriptorHandle*>)this[GetGPUDescriptorHandleForHeapStart__vtbl_index])(NativePointer, &result);
        }
        
        return ((delegate* unmanaged[MemberFunction]<nint, GpuDescriptorHandle>)this[GetGPUDescriptorHandleForHeapStart__vtbl_index])(NativePointer);
    }
}
