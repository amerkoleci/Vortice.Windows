// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes the properties of a ray dispatch operation initiated with a call to <see cref="ID3D12GraphicsCommandList4.DispatchRays(DispatchRaysDescription)"/>.
    /// </summary>
    public partial struct DispatchRaysDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchRaysDescription"/> struct.
        /// </summary>
        /// <param name="rayGenerationShaderRecord">
        /// The shader record for the ray generation shader to use.
        /// The memory pointed to must be in state <see cref="ResourceStates.NonPixelShaderResource"/>.
        /// The address must be aligned to 64 bytes, defined as <see cref="D3D12.RaytracingShaderTableByteAlignment"/> and in the range [0...4096] bytes.
        /// </param>
        /// <param name="missShaderTable">
        /// The shader table for miss shaders.
        /// The stride is record stride, and must be aligned to 32 bytes, defined as <see cref="D3D12.RaytracingShaderRecordByteAlignment"/> and in the range [0...4096] bytes. 0 is allowed.
        /// The memory pointed to must be in state <see cref="ResourceStates.NonPixelShaderResource"/>.
        /// The address must be aligned to 64 bytes, defined as <see cref="D3D12.RaytracingShaderRecordByteAlignment"/>.
        /// </param>
        /// <param name="hitGroupTable">
        /// The shader table for hit groups.
        /// The stride is record stride, and must be aligned to 32 bytes, defined as <see cref="D3D12.RaytracingShaderRecordByteAlignment"/> and in the range [0...4096] bytes. 0 is allowed.
        /// The memory pointed to must be in state <see cref="ResourceStates.NonPixelShaderResource"/>.
        /// The address must be aligned to 64 bytes, defined as <see cref="D3D12.RaytracingShaderTableByteAlignment"/>.
        /// </param>
        /// <param name="callableShaderTable">
        /// The shader table for callable shaders.
        /// The stride is record stride, and must be aligned to 32 bytes, defined as <see cref="D3D12.RaytracingShaderRecordByteAlignment"/> 0 is allowed.
        /// The memory pointed to must be in state <see cref="ResourceStates.NonPixelShaderResource"/>.
        /// The address must be aligned to 64 bytes, defined as <see cref="D3D12.RaytracingShaderTableByteAlignment"/>.
        /// </param>
        /// <param name="width">The width of the generation shader thread grid.</param>
        /// <param name="height">The height of the generation shader thread grid.</param>
        /// <param name="depth">The depth of the generation shader thread grid.</param>
        public DispatchRaysDescription(
            GpuVirtualAddressRange rayGenerationShaderRecord,
            GpuVirtualAddressRangeAndStride missShaderTable,
            GpuVirtualAddressRangeAndStride hitGroupTable,
            GpuVirtualAddressRangeAndStride callableShaderTable,
            int width,
            int height,
            int depth
            )
        {
            RayGenerationShaderRecord = rayGenerationShaderRecord;
            MissShaderTable = missShaderTable;
            HitGroupTable = hitGroupTable;
            CallableShaderTable = callableShaderTable;
            Width = width;
            Height = height;
            Depth = depth;
        }
    }
}
