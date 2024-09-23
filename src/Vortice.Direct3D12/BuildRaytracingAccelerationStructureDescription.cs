// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a raytracing acceleration structure. Pass this structure into <see cref="ID3D12GraphicsCommandList4.BuildRaytracingAccelerationStructure(BuildRaytracingAccelerationStructureDescription)"/> to describe the acceleration structure to be built.
/// </summary>
public partial struct BuildRaytracingAccelerationStructureDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildRaytracingAccelerationStructureDescription"/> struct.
    /// </summary>
    /// <param name="destinationAccelerationStructureData">
    /// Location to store resulting acceleration structure. <see cref="ID3D12Device5.GetRaytracingAccelerationStructurePrebuildInfo(BuildRaytracingAccelerationStructureInputs)"/> reports the amount of memory required for the result here given a set of acceleration structure build parameters.
    /// The address must be aligned to 256 bytes, defined as <see cref="D3D12.RaytracingAccelerationStructureByteAlignment"/>.
    /// The memory pointed to must be in state <see cref="ResourceStates.RaytracingAccelerationStructure"/>.
    /// </param>
    /// <param name="inputs">Description of the input data for the acceleration structure build. This is data is stored in a separate structure because it is also used with <see cref="ID3D12Device5.GetRaytracingAccelerationStructurePrebuildInfo(BuildRaytracingAccelerationStructureInputs)"/>.</param>
    /// <param name="sourceAccelerationStructureData">Address of an existing acceleration structure if an acceleration structure update (an incremental build) is being requested, by setting <see cref="RaytracingAccelerationStructureBuildFlags.PerformUpdate"/> in the Flags parameter. Otherwise this address must be 0.</param>
    /// <param name="scratchAccelerationStructureData">Location where the build will store temporary data. <see cref="ID3D12Device5.GetRaytracingAccelerationStructurePrebuildInfo(BuildRaytracingAccelerationStructureInputs)"/> reports the amount of scratch memory the implementation will need for a given set of acceleration structure build parameters.</param>
    public BuildRaytracingAccelerationStructureDescription(
        ulong destinationAccelerationStructureData,
        BuildRaytracingAccelerationStructureInputs inputs,
        ulong sourceAccelerationStructureData,
        ulong scratchAccelerationStructureData)
    {
        DestinationAccelerationStructureData = destinationAccelerationStructureData;
        Inputs = inputs;
        SourceAccelerationStructureData = sourceAccelerationStructureData;
        ScratchAccelerationStructureData = scratchAccelerationStructureData;
    }
}
