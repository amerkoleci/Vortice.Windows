// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public static partial class D3D12
{
    // https://docs.microsoft.com/en-us/windows/win32/direct3d12/constants
    public const byte IndexStripCutValue8Bit = 0xff;
    public const ushort IndexStripCutValue16Bit = 0xffff;
    public const uint IndexStripCutValue32Bit = 0xffffffff;
    public const uint AppendAlignedElement = 0xffffffff;
    public const int ArrayAxisAddressRangeBitCount = 9;
    public const int ClipOrCullDistanceCount = 8;
    public const int ClipOrCullDistanceElementCount = 2;

    public const int CommonShaderConstantBufferApiSlotCount = 14;
    public const int CommonShaderConstantBufferComponents = 4;
    public const int CommonShaderConstantBufferComponentBitCount = 32;
    public const int CommonShaderConstantBufferHWSlotCount = 15;
    public const int CommonShaderConstantBufferPartialUpdateExtentsByteAlignment = 16;
    public const int CommonShaderConstantBufferRegisterComponents = 4;
    public const int CommonShaderConstantBufferRegisterCount = 15;
    public const int CommonShaderConstantBufferRegisterReadsPerInstance = 1;
    public const int CommonShaderConstant_BufferRegisterReadPorts = 1;
    public const int CommonShaderFlowControlNestingLimit = 64;
    public const int CommonShaderImmediateConstantBufferRegisterComponents = 4;
    public const int CommonShaderImmediateConstantBufferRegisterCount = 1;
    public const int CommonShaderImmediateConstantBufferRegisterReadsPerInstance = 1;
    public const int CommonShaderImmediateConstantBufferRegisterReadPorts = 1;
    public const int CommonShaderImmediateValueComponentBitCount = 32;
    public const int CommonShaderInputResourceRegisterComponents = 1;
    public const int CommonShaderInputResourceRegisterCount = 128;
    public const int CommonShaderInputResourceRegisterReadsPerInstance = 1;
    public const int CommonShaderInputResourceRegisterReadPorts = 1;
    public const int CommonShaderInputResourceSlotCount = 128;
    public const int CommonShaderSamplerRegisterComponents = 1;
    public const int CommonShaderSamplerRegisterCount = 16;
    public const int CommonShaderSamplerRegisterReadsPerInstance = 1;
    public const int CommonShaderSamplerRegisterReadPorts = 1;
    public const int CommonShaderSamplerSlotCount = 16;

    public const int CommonShaderSubroutineNestingLimit = 32;

    public const int CommonShaderTempRegisterComponents = 4;
    public const int CommonShaderTempRegisterComponentBitCount = 32;
    public const int CommonShaderTempRegisterCount = 4096;
    public const int CommonShaderTempRegisterReadsPerInstance = 3;
    public const int CommonShaderTempRegisterReadPorts = 3;

    public const int CommonShaderTexcoordRangeReductionMax = 10;
    public const int CommonShaderTexcoordRangeReductionMin = -10;

    public const int CommonShaderTexelOffsetMaxNegative = -8;
    public const int CommonShaderTexelOffsetMaxPositive = 7;

    public const int ConstantBufferDataPlacementAlignment = 256;

    public const int ComputeShader4xBucket00MaxBytesTGSMWriteablePerThread = 256;
    public const int ComputeShader4xBucket00MaxNumThreadsPerGroup = 64;
    public const int ComputeShader4xBucket01MaxBytesTGSMWriteablePerThread = 240;
    public const int ComputeShader4xBucket01MaxNumThreadsPerGroup = 68;
    public const int ComputeShader4xBucket02MaxBytesTGSMWriteablePerThread = 224;
    public const int ComputeShader4xBucket02MaxNumThreadsPerGroup = 72;
    public const int ComputeShader4xBucket03MaxBytesTGSMWriteablePerThread = 208;
    public const int ComputeShader4xBucket03MaxNumThreadsPerGroup = 76;
    public const int ComputeShader4xBucket04MaxBytesTGSMWriteablePerThread = 192;
    public const int ComputeShader4xBucket04MaxNumThreadsPerGroup = 84;
    public const int ComputeShader4xBucket05MaxBytesTGSMWriteablePerThread = 176;
    public const int ComputeShader4xBucket05MaxNumThreadsPerGroup = 92;
    public const int ComputeShader4xBucket06MaxBytesTGSMWriteablePerThread = 160;
    public const int ComputeShader4xBucket06MaxNumThreadsPerGroup = 100;
    public const int ComputeShader4xBucket07MaxBytesTGSMWriteablePerThread = 144;
    public const int ComputeShader4xBucket07MaxNumThreadsPerGroup = 112;
    public const int ComputeShader4xBucket08MaxBytesTGSMWriteablePerThread = 128;
    public const int ComputeShader4xBucket08MaxNumThreadsPerGroupP = 128;
    public const int ComputeShader4xBucket09MaxBytesTGSMWriteablePerThread = 112;
    public const int ComputeShader4xBucket09MaxNumThreadsPerGroup = 144;
    public const int ComputeShader4xBucket10MaxBytesTGSMWriteablePerThread = 96;
    public const int ComputeShader4xBucket10MaxNumThreadsPerGroup = 168;
    public const int ComputeShader4xBucket11MaxBytesTGSMWriteablePerThread = 80;
    public const int ComputeShader4xBucket11MaxNumThreadsPerGroup = 204;
    public const int ComputeShader4xBucket12MaxBytesTGSMWriteablePerThread = 64;
    public const int ComputeShader4xBucket12MaxNumThreadsPerGroup = 256;
    public const int ComputeShader4xBucket13MaxBytesTGSMWriteablePerThread = 48;
    public const int ComputeShader4xBucket13MaxNumThreadsPerGroup = 340;
    public const int ComputeShader4xBucket14MaxBytesTGSMWriteablePerThread = 32;
    public const int ComputeShader4xBucket14MaxNumThreadsPerGroup = 512;
    public const int ComputeShader4xBucket15MaxBytesTGSMWriteablePerThread = 16;
    public const int ComputeShader4xBucket15MaxNumThreadsPerGroup = 768;

    public const int ComputeShader4xDispatchMaxThreadGroupsInZDimension = 1;
    public const int ComputeShader4xRawUnorderedAccessViewByteAlignment = 256;
    public const int ComputeShader4xThreadGroupMaxThreadsPerGroup = 768;
    public const int ComputeShader4xThreadGroupMaxX = 768;
    public const int ComputeShader4xThreadGroupMaxY = 768;
    public const int ComputeShader4xUnorderedAccessViewRegisterCount = 1;

    public const int ComputeShaderDispatchMaxThreadGroupsPerDimension = 65535;

    public const int ComputeShaderTGSMRegisterCount = 8192;
    public const int ComputeShaderTGSMRegisterReadsPerInstance = 1;
    public const int ComputeShaderTGSMResourceRegisterComponents = 1;
    public const int ComputeShaderTGSMResourceRegisterReadPorts = 1;

    public const int ComputeShaderThreadGroupIdRegisterComponents = 3;

    public const int ComputeShaderThreadGroupIdRegisterCount = 1;

    public const int ComputeShaderThreadIdInGroupFlattenedRegisterComponents = 1;

    public const int ComputeShaderThreadIdInGroupFlattenedRegisterCount = 1;

    public const int ComputeShaderThreadIdInGroupRegisterComponents = 3;

    public const int ComputeShaderThreadIdInGroupRegisterCount = 1;

    public const int ComputeShaderThreadIdRegisterComponents = 3;
    public const int ComputeShaderThreadIdRegisterCount = 1;

    public const int ComputeShaderThreadGroupMaxThreadsPerGroup = 1024;
    public const int ComputeShaderThreadGroupMaxX = 1024;
    public const int ComputeShaderThreadGroupMaxY = 1024;
    public const int ComputeShaderThreadGroupMaxZ = 64;
    public const int ComputeShaderThreadGroupMinX = 1;
    public const int ComputeShaderThreadGroupMinY = 1;
    public const int ComputeShaderThreadGroupMinZ = 1;

    public const int ComputeShaderThreadLocalTempRegisterPool = 16384;

    public const float DefaultBlendFactorAlpha = 1.0f;
    public const float DefaultBlendFactorBlue = 1.0f;
    public const float DefaultBlendFactorGreen = 1.0f;
    public const float DefaultBlendFactorRed = 1.0f;
    public const float DefaultBorderColorComponent = 0.0f;
    public const int DefaultDepthBias = 0;
    public const float DefaultDepthBiasClamp = 0.0f;
    public const int DefaultMaxAnisotropy = 16;

    public const float DefaultMipLodBias = 0.0f;
    public const uint DefaultMsaaResourcePlacementAlignment = 4194304;

    public const int DefaultRenderTargetArrayIndex = 0;

    public const int DefaultResourcePlacementAlignment = 65536;

    public const uint DefaultSampleMask = 0xffffffff;

    public const int DefaultScissorEndX = 0;
    public const int DefaultScissorEndY = 0;

    public const int DefaultScissorStartX = 0;
    public const int DefaultScissorStartY = 0;

    public const float DefaultSlopeScaledDepthBias = 0.0f;
    public const byte DefaultStencilReadMask = 0xff;
    public const byte DefaultStencilWriteMask = 0xff;
    public const int DefaultStencilReference = 0;


    public const int DefaultViewportAndScissorRectIndex = 0;
    public const int DefaultViewportHeight = 0;
    public const float DefaultViewportMaxDepth = 0.0f;
    public const float DefaultViewportMinDepth = 0.0f;
    public const int DefaultViewportTopLeftX = 0;
    public const int DefaultViewportTopLeftY = 0;
    public const int DefaultViewportWidth = 0;

    /// <unmanaged>D3D12_DESCRIPTOR_RANGE_OFFSET_APPEND</unmanaged>
    public const uint DescriptorRangeOffsetAppend = 0xffffffff;

    public const uint DriverReservedRegisterSpaceValuesEnd = 0xfffffff7;
    public const uint DriverReservedRegisterSpaceValuesStart = 0xfffffff0;

    public const int DomainShaderInputControlPointsMaxTotalScalars = 3968;
    public const int DomainShaderInputControlPointRegisterComponents = 4;
    public const int DomainShaderInputControlPointRegisterComponentBitCount = 32;
    public const int DomainShaderInputControlPointRegisterCount = 32;
    public const int DomainShaderInputControlPointRegisterReadsPerInstance = 2;
    public const int DomainShaderInputControlPointRegisterReadPorts = 1;
    public const int DomainShaderInputDomainPointRegisterComponents = 3;
    public const int DomainShaderInputDomainPointRegisterComponentBitCount = 32;
    public const int DomainShaderInputDomainPointRegisterCount = 1;
    public const int DomainShaderInputDomainPointRegisterReadsPerInstance = 2;
    public const int DomainShaderInputDomainPointRegisterReadPorts = 1;
    public const int DomainShaderInputPatchConstantRegisterComponents = 4;
    public const int DomainShaderInputPatchConstantRegisterComponentBitCount = 32;
    public const int DomainShaderInputPatchConstantRegisterCount = 32;
    public const int DomainShaderInputPatchConstant_RegisterReadsPerInstances = 2;
    public const int DomainShaderInputPatchConstant_RegisterReadPorts = 1;
    public const int DomainShaderInputPrimitiveIdRegisterComponents = 1;
    public const int DomainShaderInputPrimitiveIdRegisterComponentBitCount = 32;
    public const int DomainShaderInputPrimitiveIdRegisterCount = 1;
    public const int DomainShaderInputPrimitiveIdRegisterReadsPerInstances = 2;
    public const int DomainShaderInputPrimitiveIdRegisterReadPorts = 1;
    public const int DomainShaderOutputRegisterComponents = 4;
    public const int DomainShaderOutputRegisterComponentBitCount = 32;
    public const int DomainShaderOutputRegisterCount = 32;

    public const double Float16FusedToleranceInUlp = 0.6;
    public const float Float32Max = float.MaxValue;
    public const float Float32ToIntegerToleranceInUlp = 0.6f;
    public const float FloatToSrgbExponent_Denominator = 2.4f;
    public const float FloatToSrgbExponent_Numerator = 1.0f;
    public const float FloatToSrgbOffset = 0.055f;
    public const float FloatToSrgbScale1 = 12.92f;
    public const float FloatToSrgbScale2 = 1.055f;
    public const float FloatToSrgbThreshold = 0.0031308f;
    public const float FtoiInstructionMaxInput = 2147483647.999f;
    public const float FtoiInstructionMinInput = -2147483648.999f;
    public const float FtouInstructionMaxInput = 4294967295.999f;
    public const float FtouInstructionMinInput = 0.0f;

    public const int GeometryShaderInputInstanceIdReadsPerInstance = 2;
    public const int GeometryShaderInputInstanceIdReadPorts = 1;
    public const int GeometryShaderInputInstanceIdRegisterComponents = 1;
    public const int GeometryShaderInputInstanceIdRegisterComponentBitCount = 32;
    public const int GeometryShaderInputInstanceIdRegisterCount = 1;
    public const int GeometryShaderInputPrimConstRegisterComponents = 1;
    public const int GeometryShaderInputPrimConstRegisterComponent_Bit_Count = 32;
    public const int GeometryShaderInputPrimConstRegisterCount = 1;
    public const int GeometryShaderInputPrimConstRegisterReadsPerInstance = 2;
    public const int GeometryShaderInputPrimConstRegisterReadPorts = 1;
    public const int GeometryShaderInputRegisterComponents = 4;
    public const int GeometryShaderInputRegisterComponentBitCount = 32;
    public const int GeometryShaderInputRegisterCount = 32;
    public const int GeometryShaderInputRegisterReadsPerInstance = 2;
    public const int GeometryShaderInputRegisterReadPorts = 1;
    public const int GeometryShaderInputRegisterVertices = 32;
    public const int GeometryShaderMaxInstanceCount = 32;
    public const int GeometryShaderMaxOutputVertexCountAcrossInstances = 1024;
    public const int GeometryShaderOutputElements = 32;
    public const int GeometryShaderOutputRegisterComponents = 4;
    public const int GeometryShaderOutputRegisterComponentBitCount = 32;
    public const int GeometryShaderOutputRegisterCount = 32;

    public const int HullShaderControlPointPhaseInputRegister_Count = 32;
    public const int HullShaderControlPointPhaseOutputRegister_Count = 32;
    public const int HullShaderControlPointRegisterComponents = 4;
    public const int HullShaderControlPointRegisterComponentBitCount = 32;
    public const int HullShaderControlPointRegisterReadsPerInstance = 2;
    public const int HullShaderControlPointRegisterReadPorts = 1;

    public const uint HullShaderForkPhaseInstanceCountUpperBound = 0xffffffff;
    public const int HullShaderInputForkInstanceIdRegisterComponents = 1;
    public const int HullShaderInputForkInstanceIdRegisterComponentBitCount = 32;
    public const int HullShaderInputForkInstanceIdRegisterCount = 1;
    public const int HullShaderInputForkInstanceIdRegisterReadsPerInstance = 2;
    public const int HullShaderInputForkInstanceIdRegisterReadPorts = 1;
    public const int HullShaderInputJoinInstanceIdRegisterComponents = 1;
    public const int HullShaderInputJoinInstanceIdRegisterComponentBitCount = 32;
    public const int HullShaderInputJoinInstanceIdRegisterCount = 1;
    public const int HullShaderInputJoinInstanceIdRegisterReadsPerInstance = 2;
    public const int HullShaderInputJoinInstanceIdRegisterReadPorts = 1;
    public const int HullShaderInputPrimitiveIdRegisterComponents = 1;
    public const int HullShaderInputPrimitiveIdRegisterComponent_Bit_Count = 32;
    public const int HullShaderInputPrimitiveIdRegisterCount = 1;
    public const int HullShaderInputPrimitiveIdRegisterReadsPerInstance = 2;
    public const int HullShaderInputPrimitiveIdRegisterReadPorts = 1;
    public const uint HullShaderJoinPhaseInstanceCountUpperBound = 0xffffffff;
    public const float HullShaderMaxTessFactorLowerBound = 1.0f;
    public const float HullShaderMaxTessFactorUpperBound = 64.0f;
    public const int HullShaderOutputControlPointsMaxTotalScalars = 3968;
    public const int HullShaderOutputControlPointIdRegisterComponents = 1;
    public const int HullShaderOutputControlPointIdRegisterComponentBitCount = 32;
    public const int HullShaderOutputControlPointIdRegisterCount = 1;
    public const int HullShaderOutputControlPointIdRegisterReadsPerInstance = 2;
    public const int HullShaderOutputControlPointIdRegisterReadPorts = 1;
    public const int HullShaderOutputPatchConstantRegisterComponents = 4;
    public const int HullShaderOutputPatchConstantRegisterComponentBitCount = 32;
    public const int HullShaderOutputPatchConstantRegisterCount = 32;
    public const int HullShaderOutputPatchConstantRegisterReadsPerInstance = 2;
    public const int HullShaderOutputPatchConstantRegisterReadPorts = 1;
    public const int HullShaderOutputPatchConstantRegisterScalarComponents = 128;

    public const int InputAssemblerDefaultIndexBufferOffsetInBytes = 0;
    public const int InputAssemblerDefaultPrimitiveTopology = 0;
    public const int InputAssemblerDefaultVertexBufferOffsetInBytes = 0;
    public const int InputAssemblerIndexInputResourceSlot_Count = 1;
    public const int InputAssemblerInstanceIdBitCount = 32;
    public const int InputAssemblerIntegerArithmeticBitCount = 32;
    public const int InputAssemblerPatchMaxControlPointCount = 32;
    public const int InputAssemblerPrimitiveIdBitCount = 32;
    public const int InputAssemblerVertexIdBitCount = 32;
    public const int InputAssemblerVertexInputResourceSlotCount = 32;
    public const int InputAssemblerVertexInputStructureElementsComponents = 128;
    public const int InputAssemblerVertexInputStructureElementCount = 32;

    public const uint IntegerDivideByZeroQuotient = 0xffffffff;
    public const uint IntegerDivideByZeroRemained = 0xffffffff;

    public const uint KeepRenderTargetsAndDepthStencil = 0xffffffff;
    public const uint KeepUnorderedAccessViews = 0xffffffff;

    public const float LinearGamma = 1.0f;
    public const int MajorVersion = 12;

    public const float MaxBorderColorComponent = 1.0f;
    public const float MaxDepth = 1.0f;
    public const int MaxLiveStaticSamplers = 2032;

    public const int MaxMaxAnisotropy = 16;

    public const int MaxMultisampleSampleCount = 32;

    public const float MaxPositionValue = float.MaxValue;
    public const int MaxRootCost = 64;

    public const int MaxShaderVisibleDescriptorHeapSizeTier1 = 1000000;
    public const int MaxShaderVisibleDescriptorHeapSizeTier2 = 1000000;
    public const int MaxShaderVisibleSamplerHeapSize = 2048;

    public const int MaxTextureDimension2ToExp = 17;

    public const int MaxViewInstanceCount = 4;

    public const int MinorVersion = 0;

    public const float MinBorderColorComponent = 0.0f;
    public const float MinDepth = 0.0f;
    public const int MinMaxAnisotropy = 0;

    public const float MipLodBiasMax = 15.99f;
    public const float MipLodBiasMin = -16.0f;
    public const int MipLodFractionalBitCount = 8;

    public const int MipLodRangeBitCount = 8;

    public const float MultisampleAntialiasLineWidth = 1.4f;
    public const int NonSampleFetchOutOfRangeAccessResult = 0;

    public const uint PackedTile = 0xffffffff;

    public const int PixelAddressRangeBit_Count = 15;

    public const int PreScissorPixelAddressRangeBitCount = 16;

    public const int PixelShaderComputeShaderUnorderedAccessViewRegisterComponents = 1;
    public const int PixelShaderComputeShaderUnorderedAccessViewRegisterCount = 8;
    public const int PixelShaderComputeShaderUnorderedAccessViewRegisterReadsPerInstance = 1;
    public const int PixelShaderComputeShaderUnorderedAccessViewRegisterReadPorts = 1;

    public const uint PixelShaderFrontFacingDefaultValue = 0xffffffff;
    public const uint PixelShaderFrontFacingFalseValue = 0;
    public const uint PixelShaderFrontFacingTrueValue = 0xffffffff;

    public const int PixelShaderInputRegisterComponents = 4;
    public const int PixelShaderInputRegisterComponentBitCount = 32;
    public const int PixelShaderInputRegisterCount = 32;
    public const int PixelShaderInputRegisterReadsPerInstance = 2;
    public const int PixelShaderInputRegisterReadPorts = 1;

    public const float PixelShaderLegacyPixelCenterFractionalComponent = 0.0f;
    public const int PixelShaderOutputDepthRegisterComponents = 1;
    public const int PixelShaderOutputDepthRegisterComponentBitCount = 32;
    public const int PixelShaderOutputDepthRegisterCount = 1;
    public const int PixelShaderOutputMaskRegisterComponents = 1;
    public const int PixelShaderOutputMaskRegisterComponentBitCount = 32;
    public const int PixelShaderOutputMaskRegisterCount = 1;
    public const int PixelShaderOutputRegisterComponents = 4;
    public const int PixelShaderOutputRegisterComponentBitCount = 32;
    public const int PixelShaderOutputRegisterCount = 8;

    public const float PixelShaderPixelCenterFractionalComponent = 0.5f;
    public const int RawUnorderedAccessViewShaderResourceViewByteAlignment = 16;

    public const int RaytracingAABBByteAlignment = 8;
    public const int RaytracingAccelerationStructureByteAlignment = 256;
    public const int RaytracingInstanceDescByteAlignment = 16;
    public const int RaytracingMaxAttributeSizeInBytes = 32;
    public const int RaytracingMaxDeclarableTraceRecursionDepth = 31;
    public const int RaytracingMaxGeometriesPerBottomLevelAccelerationStructure = 16777216;
    public const int RaytracingMaxInstancesPerTopLevelAccelerationStructure = 16777216;
    public const int RaytracingMaxPrimitivesPerBottomLevelAccelerationStructure = 536870912;
    public const int RaytracingMaxrRayGenerationShaderThreads = 1073741824;
    public const int RaytracingMaxShaderRecordStride = 4096;
    public const int RaytracingShaderRecordByteAlignment = 32;
    public const int RaytracingShaderTableByteAlignment = 64;
    public const int RaytracingTransform3x4ByteAlignment = 16;

    public const int RequestBlendObjectCountPerDevice = 4096;
    public const int RequestBufferResourceTexelCount2ToExp = 27;
    public const int RequestConstantBufferElementCount = 4096;
    public const int RequestDepthStencilObjectCountPerDevice = 4096;
    public const int RequestDrawIndexedIndexCount2ToExp = 32;
    public const int RequestDrawVertexCount2ToExp = 32;
    public const int RequestFilteringHardwareAddressableResourceDimension = 16384;
    public const int RequestGeometryShaderInvocation32BitOutputComponentLimit = 1024;
    public const int RequestImmediateConstantBufferElementCount = 4096;
    public const int RequestMaxAnisotropy = 16;
    public const int RequestMipLevels = 15;
    public const int RequestMultiElementStructureSizeInBytes = 2048;
    public const int RequestRasterizerObjectCountPerDevice = 4096;
    public const int RequestRenderToBufferWindowWidth = 16384;
    public const int RequestResourceSizeInMegaBytesExpressionATerm = 128;
    public const float RequestResourceSizeInMegaBytesExpressionBTerm = 0.25f;
    public const int RequestResourceSizeInMegaBytesExpressionCTerm = 2048;
    public const int RequestResourceViewCountPerDevice2ToExp = 20;
    public const int RequestSamplerObjectCountPerDevice = 4096;
    public const int RequestSubResources = 30720;
    public const int RequestTexture1DArrayAxisDimension = 2048;
    public const int RequestTexture1DUDimension = 16384;
    public const int RequestTexture2DArrayAxisDimension = 2048;
    public const int RequestTexture2DUOrVDimension = 16384;
    public const int RequestTexture3DUVOrWDimension = 2048;
    public const int RequestTextureCubeDimension = 16384;

    public const int ResInfoInstructionMissingComponentReturnValue = 0;

    /// <unmanaged>D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES</unmanaged>
    public const uint ResourceBarrierAllSubResources = 0xffffffff;

    public const int RSSetShadingRateCombinerCount = 2;

    public const uint ShaderIdentifierSizeInBytes = 32;
    public const int ShaderMajorVersion = 5;
    public const int ShaderMaxInstances = 65535;
    public const int ShaderMaxInterfaces = 253;
    public const int ShaderMaxInterfaceCallSites = 4096;
    public const int ShaderMaxTypes = 65535;
    public const int ShaderMinorVersion = 1;

    public const int ShiftInstructionPadValue = 0;
    public const int ShiftInstructionShiftValueBitCount = 5;

    public const int SimultaneousRenderTargetCount = 8;

    public const uint SmallMsaaResourcePlacementAlignment = 65536;
    public const uint SmallResourcePlacementAlignment = 4096;

    public const int StreamOutputBufferMaxStrideInBytes = 2048;
    public const int StreamOutputBufferMaxWriteWindowInBytes = 512;
    public const int StreamOutputBufferSlotCount = 4;
    public const uint StreamOutputDdiRegisterIndexDenotingGap = 0xffffffff;
    public const uint StreamOutputNoRasterizedStream = 0xffffffff;
    public const int StreamOutputOutputComponentCount = 128;
    public const int StreamOutputStreamCount = 4;

    public const int SpecDateDay = 14;
    public const int SpecDateMonth = 11;
    public const int SpecDateYes = 2014;
    public const float SpecVersion = 1.16f;

    public const float SrgbGamma = 2.2f;
    public const float SrgbToFloatDenominator1 = 12.92f;
    public const float SrgbToFloatDenominator2 = 1.055f;
    public const float SrgbToFloatExponent = 2.4f;
    public const float SrgbToFloatOffset = 0.055f;
    public const float SrgbToFloatThreshold = 0.04045f;
    public const float SrgbToFloatToleranceInUlp = 0.5f;

    public const int StandardComponentBitCount = 32;
    public const int StandardComponentBitCountDoubled = 64;
    public const int StandardMaximumElementAlignmentByteMultiple = 4;
    public const int StandardPixelComponentCount = 128;
    public const int StandardPixelElementCount = 32;
    public const int StandardVectorSize = 4;
    public const int StandardVertexElementCount = 32;
    public const int StandardVertexTotalComponentCount = 64;

    public const int SubpixelFractionalBitCount = 8;
    public const int SubtexelFractionalBitCount = 8;

    public const uint SystemReservedRegisterSpaceValuesEnd = 0xffffffff;
    public const uint SystemReservedRegisterSpaceValuesStart = 0xfffffff0;

    public const int TessellatorMaxEvenTessellationFactor = 64;
    public const int TessellatorMaxIsolineDensityTessellationFactor = 64;
    public const int TessellatorMaxOddTessellationFactor = 63;
    public const int TessellatorMaxTessellationFactor = 64;
    public const int TessellatorMinEvenTessellationFactor = 2;
    public const int TessellatorMinIsolineDensityTessellationFactor = 1;
    public const int TessellatorMinOddTessellationFactor = 1;

    public const int TexelAddressRangeBitCount = 16;

    public const int TextureDataPitchAlignment = 256;
    public const int TextureDataPlacementAlignment = 512;

    public const int TiledResourceTileSizeBytes = 65536;

    public const int TrackedWorkloadMaxInstances = 32;

    public const int UnorderedAccessViewCounterPlacementAlignment = 4096;

    public const int UnorderedAccessViewSlotCount = 64;

    public const int UnboundMemoryAccessResult = 0;

    public const int VideoDecodeMaxArguments = 10;
    public const int VideoDecodeMaxHistogramComponents = 4;
    public const int VideoDecodeMinBitStreamOffsetAlignment = 256;
    public const int VideoDecodeMinHistogramOffsetAlignment = 256;
    public const uint VideoDecodeStatusMacroblocksAffectedUnknown = 0xffffffff;
    public const int VideoProcessMaxFilter = 32;
    public const int VideoProcessStereoViews = 2;

    public const int ViewportAndScissorRectMaxIndex = 15;
    public const int ViewportAndScissorRectObjectCountPerPipeline = 16;
    public const int ViewportBoundsMax = 32767;
    public const int ViewportBoundsMin = -32768;

    public const int VertexShaderInputRegisterComponents = 4;
    public const int VertexShaderInputRegisterComponentBitCount = 32;
    public const int VertexShaderInputRegisterCount = 32;
    public const int VertexShaderInputRegisterReadsPerInstances = 2;
    public const int VertexShaderInputRegisterReadPorts = 1;
    public const int VertexShaderOutputRegisterComponent = 4;
    public const int VertexShaderOutputRegisterComponentBitCount = 32;
    public const int VertexShaderOutputRegisterCount = 32;

    public const int WhqlContextCountForResourceLimit = 10;
    public const int WhqlDrawIndexedIndexCount2ToExp = 25;
    public const int WhqlDrawVertexCount2ToExp = 25;
}
