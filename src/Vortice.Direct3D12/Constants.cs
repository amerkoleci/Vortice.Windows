// Copyright =c; Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    public static partial class D3D12
    {
        // https://docs.microsoft.com/en-us/windows/win32/direct3d12/constants
        public const byte IndexStripCutValue8Bit = 0xff;
        public const short IndexStripCutValue16Bit = unchecked((short)0xffff);
        public const int IndexStripCutValue32Bit = unchecked((int)0xffffffff);
        public const int AppendAlignedElement = unchecked((int)0xffffffff);
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
        public const int CommonShaderSamplerSlot_ount = 16;

        public const int CommonShaderSUBROUTINE_NESTING_LIMIT = 32;

        public const int CommonShader_TEMP_Register_Components = 4;

        public const int CommonShader_TEMP_Register_Component_Bit_Count = 32;

        public const int CommonShader_TEMP_Register_Count = 4096;

        public const int CommonShader_TEMP_Register_Reads_PER_INST = 3;

        public const int CommonShader_TEMP_Register_READ_Ports = 3;

        public const int CommonShader_TEXCOORD_RANGE_REDUCTION_Max = 10;

        public const int CommonShader_TEXCOORD_RANGE_REDUCTION_Min = -10;

        public const int CommonShader_TEXEL_Offset_Max_NEGATIVE = -8;

        public const int CommonShader_TEXEL_Offset_Max_POSITIVE = 7;

        public const int Constant_Buffer_DATA_PLACEMENT_ALIGNMENT = 256;

        public const int ComputeShader4xBUCKET00_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 256;
        public const int ComputeShader4xBUCKET00_Max_NUM_THReads_PER_GROUP = 64;
        public const int ComputeShader4xBUCKET01_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 240;
        public const int ComputeShader4xBUCKET01_Max_NUM_THReads_PER_GROUP = 68;
        public const int ComputeShader4xBUCKET02_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 224;
        public const int ComputeShader4xBUCKET02_Max_NUM_THReads_PER_GROUP = 72;
        public const int ComputeShader4xBUCKET03_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 208;
        public const int ComputeShader4xBUCKET03_Max_NUM_THReads_PER_GROUP = 76;
        public const int ComputeShader4xBUCKET04_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 192;
        public const int ComputeShader4xBUCKET04_Max_NUM_THReads_PER_GROUP = 84;
        public const int ComputeShader4xBUCKET05_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 176;
        public const int ComputeShader4xBUCKET05_Max_NUM_THReads_PER_GROUP = 92;
        public const int ComputeShader4xBUCKET06_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 160;
        public const int ComputeShader4xBUCKET06_Max_NUM_THReads_PER_GROUP = 100;
        public const int ComputeShader4xBUCKET07_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 144;
        public const int ComputeShader4xBUCKET07_Max_NUM_THReads_PER_GROUP = 112;
        public const int ComputeShader4xBUCKET08_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 128;
        public const int ComputeShader4xBUCKET08_Max_NUM_THReads_PER_GROUP = 128;
        public const int ComputeShader4xBUCKET09_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 112;

        public const int ComputeShader4xBUCKET09_Max_NUM_THReads_PER_GROUP = 144;
        public const int ComputeShader4xBUCKET10_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 96;
        public const int ComputeShader4xBUCKET10_Max_NUM_THReads_PER_GROUP = 168;
        public const int ComputeShader4xBUCKET11_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 80;
        public const int ComputeShader4xBUCKET11_Max_NUM_THReads_PER_GROUP = 204;
        public const int ComputeShader4xBUCKET12_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 64;
        public const int ComputeShader4xBUCKET12_Max_NUM_THReads_PER_GROUP = 256;
        public const int ComputeShader4xBUCKET13_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 48;
        public const int ComputeShader4xBUCKET13_Max_NUM_THReads_PER_GROUP = 340;
        public const int ComputeShader4xBUCKET14_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 32;
        public const int ComputeShader4xBUCKET14_Max_NUM_THReads_PER_GROUP = 512;
        public const int ComputeShader4xBUCKET15_Max_Bytes_TGeometryShaderM_WRITABLE_PER_THREAD = 16;
        public const int ComputeShader4xBUCKET15_Max_NUM_THReads_PER_GROUP = 768;
        public const int ComputeShader4xDispatchMaxThreadGroupsInZDimension = 1;
        public const int ComputeShader4xRAW_UnorderedAccessView_BYTE_ALIGNMENT = 256;
        public const int ComputeShader4xTHREAD_GROUP_Max_THReads_PER_GROUP = 768;
        public const int ComputeShader4xTHREAD_GROUP_Max_X = 768;
        public const int ComputeShader4xTHREAD_GROUP_Max_Y = 768;
        public const int ComputeShader4xUnorderedAccessViewRegisterCount = 1;

        public const int ComputeShaderDispatchMaxThreadGroupsPerDimension = 65535;

        public const int ComputeShaderTGSMRegisterCount = 8192;
        public const int ComputeShaderTGSMRegisterReadsPerInstance = 1;
        public const int ComputeShaderTGSMResourceRegisterComponents = 1;
        public const int ComputeShaderTGSMResourceRegisterReadPorts = 1;

        public const int ComputeShader_ThreadGroupId_Register_Components = 3;

        public const int ComputeShader_ThreadGroupId_Register_Count = 1;

        public const int ComputeShader_THREADIDINGROUPFLATTENED_RegisterComponents = 1;

        public const int ComputeShader_THREADIDINGROUPFLATTENED_RegisterCount = 1;

        public const int ComputeShader_THREADIDINGROUP_RegisterComponents = 3;

        public const int ComputeShader_THREADIDINGROUP_RegisterCount = 1;

        public const int ComputeShader_THREADID_RegisterComponents = 3;

        public const int ComputeShader_THREADID_Register_Count = 1;

        public const int ComputeShader_THREAD_GROUP_Max_THReads_PER_GROUP = 1024;

        public const int ComputeShader_THREAD_GROUP_Max_X = 1024;

        public const int ComputeShader_THREAD_GROUP_Max_Y = 1024;

        public const int ComputeShader_THREAD_GROUP_Max_Z = 64;

        public const int ComputeShader_THREAD_GROUP_Min_X = 1;

        public const int ComputeShader_THREAD_GROUP_Min_Y = 1;

        public const int ComputeShader_THREAD_GROUP_Min_Z = 1;

        public const int ComputeShader_THREAD_LOCAL_TEMP_Register_POOL = 16384;

        public const float DefaultBlendFactorAlpha = 1.0f;
        public const float DefaultBlendFactorBlue = 1.0f;
        public const float DefaultBlendFactorGreen = 1.0f;
        public const float DefaultBlendFactorRed = 1.0f;
        public const float DefaultBorderColorComponent = 0.0f;
        public const int DefaultDepthBias = 0;
        public const float DefaultDepthBiasClamp = 0.0f;
        public const int DefaultMaxAnisotropy = 16;

        public const float DefaultMipLodBias = 0.0f;
        public const int DefaultMsaaResourcePlacementAlignment = 4194304;

        public const int DefaultRenderTargetArrayIndex = 0;

        public const int DefaultResourcePlacementAlignment = 65536;

        public const int DefaultSampleMas = unchecked((int)0xffffffff);

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

        public const int DescriptorRangeOffsetAppend = unchecked((int)0xffffffff);

        public const int Driver_Reserved_Register_SPACE_VALUES_END = unchecked((int)0xfffffff7);

        public const int Driver_Reserved_Register_SPACE_VALUES_START = unchecked((int)0xfffffff0);

        public const int DS_Input_Control_Points_Max_TOTAL_ScalarS = 3968;

        public const int DS_Input_Control_Point_Register_Components = 4;

        public const int DS_Input_Control_Point_Register_Component_Bit_Count = 32;

        public const int DS_Input_Control_Point_Register_Count = 32;

        public const int DS_Input_Control_Point_Register_Reads_PER_INST = 2;

        public const int DS_Input_Control_Point_Register_READ_Ports = 1;

        public const int DS_Input_Domain_Point_Register_Components = 3;

        public const int DS_Input_Domain_Point_Register_Component_Bit_Count = 32;

        public const int DS_Input_Domain_Point_Register_Count = 1;

        public const int DS_Input_Domain_Point_Register_Reads_PER_INST = 2;

        public const int DS_Input_Domain_Point_Register_READ_Ports = 1;

        public const int DS_Input_Patch_Constant_RegisterComponents = 4;

        public const int DS_Input_Patch_Constant_RegisterComponentBitCount = 32;

        public const int DS_Input_Patch_Constant_Register_Count = 32;

        public const int DS_Input_Patch_Constant_Register_Reads_PER_INST = 2;

        public const int DS_Input_Patch_Constant_Register_READ_Ports = 1;

        public const int DS_Input_Primitive_ID_RegisterComponents = 1;

        public const int DS_Input_Primitive_ID_RegisterComponentBitCount = 32;

        public const int DS_Input_Primitive_ID_Register_Count = 1;

        public const int DS_Input_Primitive_ID_Register_Reads_PER_INST = 2;

        public const int DS_Input_Primitive_ID_Register_READ_Ports = 1;

        public const int DS_Output_RegisterComponents = 4;

        public const int DS_Output_RegisterComponentBitCount = 32;

        public const int DS_Output_RegisterCount = 32;

        public const double Float16FusedToleranceInUlp = 0.6;
        public const float Float32Max = float.MaxValue;
        public const float Float32ToIntegerToleranceInUlp = 0.6f;
        public const float FloatToSrgbExponent_Denominator = 2.4f;
        public const float FloatToSrgbExponent_Numerator = 1.0f;
        public const float FloatToSrgbOffset = 0.055f;
        public const float FloatToSrgbSCALE_1 = 12.92f;
        public const float FloatToSrgbSCALE_2 = 1.055f;
        public const float FloatToSrgbTHRESHOLD = 0.0031308f;
        public const float FTOI_Instruction_Max_Input = 2147483647.999f;
        public const float FTOI_Instruction_Min_Input = -2147483648.999f;
        public const float FTOU_Instruction_Max_Input = 4294967295.999f;
        public const float FTOU_Instruction_Min_Input = 0.0f;
        public const int GeometryShader_Input_Instance_ID_Reads_PER_INST = 2;

        public const int GeometryShader_Input_Instance_ID_READ_Ports = 1;

        public const int GeometryShader_Input_Instance_ID_Register_Components = 1;

        public const int GeometryShader_Input_Instance_ID_Register_Component_Bit_Count = 32;

        public const int GeometryShader_Input_Instance_ID_Register_Count = 1;

        public const int GeometryShader_Input_PRIM_CONST_Register_Components = 1;

        public const int GeometryShader_Input_PRIM_CONST_Register_Component_Bit_Count = 32;

        public const int GeometryShader_Input_PRIM_CONST_Register_Count = 1;

        public const int GeometryShader_Input_PRIM_CONST_Register_Reads_PER_INST = 2;

        public const int GeometryShader_Input_PRIM_CONST_Register_READ_Ports = 1;

        public const int GeometryShader_Input_Register_Components = 4;

        public const int GeometryShader_Input_Register_Component_Bit_Count = 32;

        public const int GeometryShader_Input_Register_Count = 32;

        public const int GeometryShader_Input_Register_Reads_PER_INST = 2;

        public const int GeometryShader_Input_Register_READ_Ports = 1;

        public const int GeometryShader_Input_Register_Vertices = 32;

        public const int GeometryShader_Max_Instance_Count = 32;

        public const int GeometryShader_Max_Output_Vertex_Count_ACROSS_Instances = 1024;

        public const int GeometryShader_Output_Elements = 32;

        public const int GeometryShader_Output_Register_Components = 4;

        public const int GeometryShader_Output_Register_Component_Bit_Count = 32;

        public const int GeometryShader_Output_Register_Count = 32;

        public const int HullShader_Control_Point_PHASE_Input_Register_Count = 32;

        public const int HullShader_Control_Point_PHASE_Output_Register_Count = 32;

        public const int HullShader_Control_Point_Register_Components = 4;

        public const int HullShader_Control_Point_Register_Component_Bit_Count = 32;

        public const int HullShader_Control_Point_Register_Reads_PER_INST = 2;

        public const int HullShader_Control_Point_Register_READ_Ports = 1;

        public const int HullShader_FORK_PHASE_Instance_Count_UPPER_BOUND = unchecked((int)0xffffffff);

        public const int HullShader_Input_FORK_Instance_ID_Register_Components = 1;

        public const int HullShader_Input_FORK_Instance_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_Input_FORK_Instance_ID_Register_Count = 1;

        public const int HullShader_Input_FORK_Instance_ID_Register_Reads_PER_INST = 2;

        public const int HullShader_Input_FORK_Instance_ID_Register_READ_Ports = 1;

        public const int HullShader_Input_JOIN_Instance_ID_Register_Components = 1;

        public const int HullShader_Input_JOIN_Instance_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_Input_JOIN_Instance_ID_Register_Count = 1;

        public const int HullShader_Input_JOIN_Instance_ID_Register_Reads_PER_INST = 2;

        public const int HullShader_Input_JOIN_Instance_ID_Register_READ_Ports = 1;

        public const int HullShader_Input_Primitive_ID_Register_Components = 1;

        public const int HullShader_Input_Primitive_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_Input_Primitive_ID_Register_Count = 1;

        public const int HullShader_Input_Primitive_ID_Register_Reads_PER_INST = 2;

        public const int HullShader_Input_Primitive_ID_Register_READ_Ports = 1;

        public const int HullShader_JOIN_PHASE_Instance_Count_UPPER_BOUND = unchecked((int)0xffffffff);

        public const float HullShaderMaxTessFactorLowerBound = 1.0f;
        public const float HullShaderMaxTessFactorUpperBound = 64.0f;
        public const int HullShader_Output_Control_Points_Max_TOTAL_Scalars = 3968;

        public const int HullShader_Output_Control_Point_ID_Register_Components = 1;

        public const int HullShader_Output_Control_Point_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_Output_Control_Point_ID_Register_Count = 1;

        public const int HullShader_Output_Control_Point_ID_Register_Reads_PER_INST = 2;

        public const int HullShader_Output_Control_Point_ID_Register_READ_Ports = 1;

        public const int HullShader_Output_Patch_Constant_Register_Components = 4;

        public const int HullShader_Output_Patch_Constant_Register_Component_Bit_Count = 32;

        public const int HullShader_Output_Patch_Constant_Register_Count = 32;

        public const int HullShader_Output_Patch_Constant_Register_Reads_PER_INST = 2;

        public const int HullShader_Output_Patch_Constant_Register_READ_Ports = 1;

        public const int HullShader_Output_Patch_Constant_Register_Scalar_Components = 128;

        public const int InputAssemblerDefaultIndexBufferOffsetInBytes = 0;
        public const int InputAssemblerDefaultPrimitive_TOPOLOGY = 0;
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

        public const int IntegerDivideByZeroQuotient = unchecked((int)0xffffffff);
        public const int IntegerDivideByZeroRemained = unchecked((int)0xffffffff);

        public const int KeepRenderTargetsAndDepthStencil = unchecked((int)0xffffffff);
        public const int KeepUnorderedAccessViews = unchecked((int)0xffffffff);

        public const float LinearGamma = 1.0f;
        public const int MajorVersion = 12;

        public const float MaxBorderColorComponent = 1.0f;
        public const float MaxDepth = 1.0f;
        public const int MaxLiveStaticSamplers = 2032;

        public const int MaxMaxAnisotropy = 16;

        public const int MaxMultisampleSampleCount = 32;

        public const float MaxPositionValue = float.MaxValue;
        public const int MaxRootCost = 64;

        public const int Max_SHADER_VISIBLE_DESCRIPTOR_HEAP_SIZE_TIER_1 = 1000000;

        public const int Max_SHADER_VISIBLE_DESCRIPTOR_HEAP_SIZE_TIER_2 = 1000000;

        public const int Max_SHADER_VISIBLE_Sampler_HEAP_SIZE = 2048;

        public const int Max_TEXTURE_DIMENSION_2_TO_EXP = 17;

        public const int MaxViewInstanceCount = 4;

        public const int MinorVersion = 0;

        public const float MinBorderColorComponent = 0.0f;
        public const float MinDepth = 0.0f;
        public const int MinMaxAnisotropy = 0;

        public const float MipLodBiasMax = 15.99f;
        public const float MipLodBiasMin = -16.0f;
        public const int MIP_LOD_FRACTIONAL_Bit_Count = 8;

        public const int MIP_LOD_RANGE_Bit_Count = 8;

        public const float MultisampleAntialiasLineWidth = 1.4f;
        public const int NONSAMPLE_FETCH_OUT_OF_RANGE_ACCESS_RESULT = 0;

        public const int PackedTile = unchecked((int)0xffffffff);

        public const int PIXEL_ADDRESS_RANGE_Bit_Count = 15;

        public const int PRE_SCISSOR_PIXEL_ADDRESS_RANGE_Bit_Count = 16;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_Components = 1;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_Count = 8;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_Reads_PER_INST = 1;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_READ_Ports = 1;

        public const int PixelShader_FRONTFACING_Default_VALUE = unchecked((int)0xffffffff);

        public const int PixelShader_FRONTFACING_FALSE_VALUE = 0;

        public const int PixelShader_FRONTFACING_TRUE_VALUE = unchecked((int)0xffffffff);

        public const int PixelShader_Input_Register_Components = 4;

        public const int PixelShader_Input_Register_Component_Bit_Count = 32;

        public const int PixelShader_Input_Register_Count = 32;

        public const int PixelShader_Input_Register_Reads_PER_INST = 2;

        public const int PixelShader_Input_Register_READ_Ports = 1;

        public const float PixelShader_LEGACY_PIXEL_CENTER_FRACTIONAL_Component = 0.0f;
        public const int PixelShader_Output_Depth_Register_Components = 1;

        public const int PixelShader_Output_Depth_Register_Component_Bit_Count = 32;

        public const int PixelShader_Output_Depth_Register_Count = 1;

        public const int PixelShader_Output_MASK_Register_Components = 1;

        public const int PixelShader_Output_MASK_Register_Component_Bit_Count = 32;

        public const int PixelShader_Output_MASK_Register_Count = 1;

        public const int PixelShader_Output_Register_Components = 4;

        public const int PixelShader_Output_Register_Component_Bit_Count = 32;

        public const int PixelShader_Output_Register_Count = 8;

        public const float PixelShader_PIXEL_CENTER_FRACTIONAL_Component = 0.5f;
        public const int RAW_UnorderedAccessView_SRV_BYTE_ALIGNMENT = 16;

        public const int RaytracingAABBByteAlignment = 8;

        public const int RaytracingAccelerationStructureByteAlignment = 256;

        public const int Raytracing_Instance_DESComputeShader_BYTE_ALIGNMENT = 16;

        public const int RaytracingMaxAttributeSizeInBytes = 32;

        public const int Raytracing_Max_DECLARABLE_TRACE_RECURSION_Depth = 31;

        public const int Raytracing_Max_GEOMETRIES_PER_BOTTOM_LEVEL_ACCELERATION_Structure = 16777216;

        public const int Raytracing_Max_Instances_PER_TOP_LEVEL_ACCELERATION_Structure = 16777216;

        public const int Raytracing_Max_Primitives_PER_BOTTOM_LEVEL_ACCELERATION_Structure = 536870912;

        public const int Raytracing_Max_RAY_GENERATION_SHADER_THReads = 1073741824;

        public const int Raytracing_Max_SHADER_RECORD_STRIDE = 4096;

        public const int Raytracing_SHADER_RECORD_BYTE_ALIGNMENT = 32;

        public const int Raytracing_SHADER_TABLE_BYTE_ALIGNMENT = 64;

        public const int RaytracingTransform3x4ByteAlignment = 16;

        public const int REQ_BLEND_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_Buffer_Resource_TEXEL_Count_2_TO_EXP = 27;

        public const int REQ_Constant_Buffer_Element_Count = 4096;

        public const int REQ_Depth_STENCIL_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_DrawIndexedIndexCount2ToExp = 32;

        public const int REQ_DRAW_Vertex_Count_2_TO_EXP = 32;

        public const int REQ_FILTERING_HW_ADDRESSABLE_Resource_DIMENSION = 16384;

        public const int REQ_GeometryShader_INVOCATION_32Bit_Output_Component_LIMIT = 1024;

        public const int RequestImmediateConstantBufferElementCount = 4096;

        public const int RequestMaxAnisotropy = 16;

        public const int RequestMipLevels = 15;

        public const int REQ_MULTI_Element_Structure_SIZE_IN_Bytes = 2048;

        public const int REQ_RASTERIZER_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_RENDER_TO_Buffer_WINDOW_Width = 16384;

        public const int REQ_Resource_SIZE_IN_MEGABytes_EXPRESSION_A_TERM = 128;

        public const float REQ_Resource_SIZE_IN_MEGABytes_EXPRESSION_B_TERM = 0.25f;
        public const int REQ_Resource_SIZE_IN_MEGABytes_EXPRESSION_C_TERM = 2048;

        public const int REQ_Resource_VIEW_Count_PER_DEVICE_2_TO_EXP = 20;

        public const int REQ_Sampler_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_SUBResourceS = 30720;

        public const int REQ_TEXTURE1D_ARRAY_AXIS_DIMENSION = 2048;

        public const int REQ_TEXTURE1D_U_DIMENSION = 16384;

        public const int REQ_TEXTURE2D_ARRAY_AXIS_DIMENSION = 2048;

        public const int REQ_TEXTURE2D_U_OR_V_DIMENSION = 16384;

        public const int REQ_TEXTURE3D_U_V_OR_W_DIMENSION = 2048;

        public const int REQ_TEXTURECUBE_DIMENSION = 16384;

        public const int RESINFO_Instruction_MISSING_Component_RETVAL = 0;

        public const int ResourceBarrierAllSubResources = unchecked((int)0xffffffff);

        public const int RS_SET_SHADING_RATE_COMBINER_Count = 2;

        public const int SHADER_IDENTIFIER_SIZE_IN_Bytes = 32;

        public const int SHADER_MAJOR_Version = 5;

        public const int SHADER_Max_Instances = 65535;

        public const int SHADER_Max_INTERFACES = 253;

        public const int SHADER_Max_INTERFACE_CALL_SITES = 4096;

        public const int SHADER_Max_TYPES = 65535;

        public const int SHADER_Minor_Version = 1;

        public const int SHIFT_Instruction_PAD_VALUE = 0;

        public const int SHIFT_Instruction_SHIFT_VALUE_Bit_Count = 5;

        public const int SimultaneousRenderTargetCount = 8;

        public const int SMALL_MSAA_Resource_PLACEMENT_ALIGNMENT = 65536;

        public const int SMALL_Resource_PLACEMENT_ALIGNMENT = 4096;

        public const int SO_Buffer_Max_STRIDE_IN_Bytes = 2048;

        public const int SO_Buffer_Max_WRITE_WINDOW_IN_Bytes = 512;

        public const int SO_Buffer_Slot_Count = 4;

        public const int SO_DDI_Register_Index_DENOTING_GAP = unchecked((int)0xffffffff);

        public const int SO_NO_RASTERIZED_STREAM = unchecked((int)0xffffffff);

        public const int SO_Output_Component_Count = 128;

        public const int SO_STREAM_Count = 4;

        public const int SpecDateDay = 14;
        public const int SpecDateMonth = 11;
        public const int SpecDateYes = 2014;
        public const float SpecVersion = 1.16f;

        public const float SrgbGamma = 2.2f;
        public const float SrgbToFloatDenoMinator1 = 12.92f;
        public const float SrgbToFloatDenoMinator2 = 1.055f;
        public const float SrgbToFloatExponent = 2.4f;
        public const float SrgbToFloatOffset = 0.055f;
        public const float SrgbToFloatThreshold = 0.04045f;
        public const float SrgbToFloatToleranceInUlp = 0.5f;
        public const int STANDARD_Component_Bit_Count = 32;

        public const int STANDARD_Component_Bit_Count_DOUBLED = 64;

        public const int STANDARD_MaxIMUM_Element_ALIGNMENT_BYTE_MULTIPLE = 4;

        public const int STANDARD_PIXEL_Component_Count = 128;

        public const int STANDARD_PIXEL_Element_Count = 32;

        public const int STANDARD_VECTOR_SIZE = 4;

        public const int STANDARD_Vertex_Element_Count = 32;

        public const int STANDARD_Vertex_TOTAL_Component_Count = 64;

        public const int SUBPIXEL_FRACTIONAL_Bit_Count = 8;

        public const int SUBTEXEL_FRACTIONAL_Bit_Count = 8;

        public const int SYSTEM_Reserved_Register_SPACE_VALUES_END = unchecked((int)0xffffffff);

        public const int SYSTEM_Reserved_Register_SPACE_VALUES_START = unchecked((int)0xfffffff0);

        public const int TESSELLATOR_Max_EVEN_TESSELLATION_FACTOR = 64;

        public const int TESSELLATOR_Max_ISOLINE_DENSITY_TESSELLATION_FACTOR = 64;

        public const int TESSELLATOR_Max_ODD_TESSELLATION_FACTOR = 63;

        public const int TESSELLATOR_Max_TESSELLATION_FACTOR = 64;

        public const int TESSELLATOR_Min_EVEN_TESSELLATION_FACTOR = 2;

        public const int TESSELLATOR_Min_ISOLINE_DENSITY_TESSELLATION_FACTOR = 1;

        public const int TESSELLATOR_Min_ODD_TESSELLATION_FACTOR = 1;

        public const int TEXEL_ADDRESS_RANGE_Bit_Count = 16;

        public const int TEXTURE_DATA_PITCH_ALIGNMENT = 256;

        public const int TEXTURE_DATA_PLACEMENT_ALIGNMENT = 512;

        public const int TILED_Resource_TILE_SIZE_IN_Bytes = 65536;

        public const int TRACKED_WORKLOAD_Max_Instances = 32;

        public const int UnorderedAccessView_CountER_PLACEMENT_ALIGNMENT = 4096;

        public const int UnorderedAccessView_Slot_Count = 64;

        public const int UNBOUND_MEMORY_ACCESS_RESULT = 0;

        public const int VIDEO_DECODE_Max_ARGUMENTS = 10;

        public const int VIDEO_DECODE_Max_HISTOGRAM_Components = 4;

        public const int VIDEO_DECODE_Min_BitSTREAM_Offset_ALIGNMENT = 256;

        public const int VIDEO_DECODE_Min_HISTOGRAM_Offset_ALIGNMENT = 256;

        public const int VIDEO_DECODE_STATUS_MACROBLOCKS_AFFECTED_UNKNOWN = unchecked((int)0xffffffff);

        public const int VIDEO_PROCESS_Max_FILTERS = 32;

        public const int VIDEO_PROCESS_STEREO_VIEWS = 2;

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

        public const int WHQLContextCountForResourceLimit = 10;
        public const int WHQLDrawIndexedIndexCount2ToExp = 25;
        public const int WHQLDrawVertexCount2ToExp = 25;
    }
}
