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

        public const int CommonShader_Constant_Buffer_Api_Slot_Count = 14;

        public const int CommonShader_Constant_Buffer_ComponentS = 4;

        public const int CommonShader_Constant_Buffer_Component_Bit_Count = 32;

        public const int CommonShader_Constant_Buffer_HW_Slot_Count = 15;

        public const int CommonShader_Constant_Buffer_PARTInputAssemblerL_UPDATE_EXTENTS_BYTE_ALIGNMENT = 16;

        public const int CommonShader_Constant_Buffer_Register_ComponentS = 4;

        public const int CommonShader_Constant_Buffer_Register_Count = 15;

        public const int CommonShader_Constant_Buffer_Register_READS_PER_INST = 1;

        public const int CommonShader_Constant_Buffer_Register_READ_PORTS = 1;

        public const int CommonShader_FLOWCONTROL_NESTING_LIMIT = 64;

        public const int CommonShader_ImmedInputAssemblerte_Constant_Buffer_Register_ComponentS = 4;

        public const int CommonShader_ImmedInputAssemblerte_Constant_Buffer_Register_Count = 1;

        public const int CommonShader_ImmedInputAssemblerte_Constant_Buffer_Register_READS_PER_INST = 1;

        public const int CommonShader_ImmedInputAssemblerte_Constant_Buffer_Register_READ_PORTS = 1;

        public const int CommonShader_ImmedInputAssemblerte_VALUE_Component_Bit_Count = 32;

        public const int CommonShader_Input_Resource_Register_ComponentS = 1;

        public const int CommonShader_Input_Resource_Register_Count = 128;

        public const int CommonShader_Input_Resource_Register_READS_PER_INST = 1;

        public const int CommonShader_Input_Resource_Register_READ_PORTS = 1;

        public const int CommonShader_Input_Resource_Slot_Count = 128;

        public const int CommonShader_Sampler_Register_ComponentS = 1;

        public const int CommonShader_Sampler_Register_Count = 16;

        public const int CommonShader_Sampler_Register_READS_PER_INST = 1;

        public const int CommonShader_Sampler_Register_READ_PORTS = 1;

        public const int CommonShader_Sampler_Slot_Count = 16;

        public const int CommonShader_SUBROUTINE_NESTING_LIMIT = 32;

        public const int CommonShader_TEMP_Register_ComponentS = 4;

        public const int CommonShader_TEMP_Register_Component_Bit_Count = 32;

        public const int CommonShader_TEMP_Register_Count = 4096;

        public const int CommonShader_TEMP_Register_READS_PER_INST = 3;

        public const int CommonShader_TEMP_Register_READ_PORTS = 3;

        public const int CommonShader_TEXCOORD_RANGE_REDUCTION_Max = 10;

        public const int CommonShader_TEXCOORD_RANGE_REDUCTION_Min = -10;

        public const int CommonShader_TEXEL_OFFSET_Max_NEGATIVE = -8;

        public const int CommonShader_TEXEL_OFFSET_Max_POSITIVE = 7;

        public const int Constant_Buffer_DATA_PLACEMENT_ALIGNMENT = 256;

        public const int ComputeShader_4_X_BUCKET00_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 256;

        public const int ComputeShader_4_X_BUCKET00_Max_NUM_THREADS_PER_GROUP = 64;

        public const int ComputeShader_4_X_BUCKET01_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 240;

        public const int ComputeShader_4_X_BUCKET01_Max_NUM_THREADS_PER_GROUP = 68;

        public const int ComputeShader_4_X_BUCKET02_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 224;

        public const int ComputeShader_4_X_BUCKET02_Max_NUM_THREADS_PER_GROUP = 72;

        public const int ComputeShader_4_X_BUCKET03_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 208;

        public const int ComputeShader_4_X_BUCKET03_Max_NUM_THREADS_PER_GROUP = 76;

        public const int ComputeShader_4_X_BUCKET04_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 192;

        public const int ComputeShader_4_X_BUCKET04_Max_NUM_THREADS_PER_GROUP = 84;

        public const int ComputeShader_4_X_BUCKET05_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 176;

        public const int ComputeShader_4_X_BUCKET05_Max_NUM_THREADS_PER_GROUP = 92;

        public const int ComputeShader_4_X_BUCKET06_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 160;

        public const int ComputeShader_4_X_BUCKET06_Max_NUM_THREADS_PER_GROUP = 100;

        public const int ComputeShader_4_X_BUCKET07_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 144;

        public const int ComputeShader_4_X_BUCKET07_Max_NUM_THREADS_PER_GROUP = 112;

        public const int ComputeShader_4_X_BUCKET08_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 128;

        public const int ComputeShader_4_X_BUCKET08_Max_NUM_THREADS_PER_GROUP = 128;

        public const int ComputeShader_4_X_BUCKET09_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 112;

        public const int ComputeShader_4_X_BUCKET09_Max_NUM_THREADS_PER_GROUP = 144;

        public const int ComputeShader_4_X_BUCKET10_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 96;

        public const int ComputeShader_4_X_BUCKET10_Max_NUM_THREADS_PER_GROUP = 168;

        public const int ComputeShader_4_X_BUCKET11_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 80;

        public const int ComputeShader_4_X_BUCKET11_Max_NUM_THREADS_PER_GROUP = 204;

        public const int ComputeShader_4_X_BUCKET12_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 64;

        public const int ComputeShader_4_X_BUCKET12_Max_NUM_THREADS_PER_GROUP = 256;

        public const int ComputeShader_4_X_BUCKET13_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 48;

        public const int ComputeShader_4_X_BUCKET13_Max_NUM_THREADS_PER_GROUP = 340;

        public const int ComputeShader_4_X_BUCKET14_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 32;

        public const int ComputeShader_4_X_BUCKET14_Max_NUM_THREADS_PER_GROUP = 512;

        public const int ComputeShader_4_X_BUCKET15_Max_BYTES_TGeometryShaderM_WRITABLE_PER_THREAD = 16;

        public const int ComputeShader_4_X_BUCKET15_Max_NUM_THREADS_PER_GROUP = 768;

        public const int ComputeShader_4_X_DISPATCH_Max_THREAD_GROUPixelShader_IN_Z_DIMENSION = 1;

        public const int ComputeShader_4_X_RAW_UnorderedAccessView_BYTE_ALIGNMENT = 256;

        public const int ComputeShader_4_X_THREAD_GROUP_Max_THREADS_PER_GROUP = 768;

        public const int ComputeShader_4_X_THREAD_GROUP_Max_X = 768;

        public const int ComputeShader_4_X_THREAD_GROUP_Max_Y = 768;

        public const int ComputeShader_4_X_UnorderedAccessView_Register_Count = 1;

        public const int ComputeShader_DISPATCH_Max_THREAD_GROUPixelShader_PER_DIMENSION = 65535;

        public const int ComputeShader_TGeometryShaderM_Register_Count = 8192;

        public const int ComputeShader_TGeometryShaderM_Register_READS_PER_INST = 1;

        public const int ComputeShader_TGeometryShaderM_Resource_Register_ComponentS = 1;

        public const int ComputeShader_TGeometryShaderM_Resource_Register_READ_PORTS = 1;

        public const int ComputeShader_THREADGROUPID_Register_ComponentS = 3;

        public const int ComputeShader_THREADGROUPID_Register_Count = 1;

        public const int ComputeShader_THREADIDINGROUPFLATTENED_Register_ComponentS = 1;

        public const int ComputeShader_THREADIDINGROUPFLATTENED_Register_Count = 1;

        public const int ComputeShader_THREADIDINGROUP_Register_ComponentS = 3;

        public const int ComputeShader_THREADIDINGROUP_Register_Count = 1;

        public const int ComputeShader_THREADID_Register_ComponentS = 3;

        public const int ComputeShader_THREADID_Register_Count = 1;

        public const int ComputeShader_THREAD_GROUP_Max_THREADS_PER_GROUP = 1024;

        public const int ComputeShader_THREAD_GROUP_Max_X = 1024;

        public const int ComputeShader_THREAD_GROUP_Max_Y = 1024;

        public const int ComputeShader_THREAD_GROUP_Max_Z = 64;

        public const int ComputeShader_THREAD_GROUP_Min_X = 1;

        public const int ComputeShader_THREAD_GROUP_Min_Y = 1;

        public const int ComputeShader_THREAD_GROUP_Min_Z = 1;

        public const int ComputeShader_THREAD_LOCAL_TEMP_Register_POOL = 16384;

        public const float Default_BLEND_FACTOR_ALPHA = 1.0f;
        public const float Default_BLEND_FACTOR_BLUE = 1.0f;
        public const float Default_BLEND_FACTOR_GREEN = 1.0f;
        public const float Default_BLEND_FACTOR_RED = 1.0f;
        public const float Default_Border_Color_Component = 0.0f;
        public const int DefaultDepthBInputAssemblers = 0;
        public const float DefaultDepthBInputAssemblersClamp = 0.0f;
        public const int Default_Max_ANISOTROPY = 16;

        public const float Default_MIP_LOD_BInputAssemblerS = 0.0f;
        public const int Default_MSAA_Resource_PLACEMENT_ALIGNMENT = 4194304;

        public const int Default_RENDER_TARGET_ARRAY_Index = 0;

        public const int Default_Resource_PLACEMENT_ALIGNMENT = 65536;

        public const int Default_SAMPLE_MASK = unchecked((int)0xffffffff);

        public const int Default_SCISSOR_ENDX = 0;

        public const int Default_SCISSOR_ENDY = 0;

        public const int Default_SCISSOR_STARTX = 0;

        public const int Default_SCISSOR_STARTY = 0;

        public const float DefaultSlopeScaledDepthBInputAssemblers = 0.0f;
        public const byte DefaultStencilReadMask = 0xff;
        public const byte DefaultStencilWriteMask = 0xff;

        public const int Default_STENCIL_REFERENCE = 0;


        public const int Default_Viewport_AND_SCISSORRECT_Index = 0;

        public const int Default_Viewport_HEIGHT = 0;

        public const float Default_Viewport_Max_Depth = 0.0f;
        public const float Default_Viewport_Min_Depth = 0.0f;
        public const int Default_Viewport_TopLeftX = 0;

        public const int Default_Viewport_TopLeftY = 0;

        public const int Default_Viewport_Width = 0;

        public const int DescriptorRangeOffsetAppend = unchecked((int)0xffffffff);

        public const int DRIVER_RESERVED_Register_SPACE_VALUES_END = unchecked((int)0xfffffff7);

        public const int DRIVER_RESERVED_Register_SPACE_VALUES_START = unchecked((int)0xfffffff0);

        public const int DS_Input_CONTROL_POINTS_Max_TOTAL_SCALARS = 3968;

        public const int DS_Input_CONTROL_POINT_Register_ComponentS = 4;

        public const int DS_Input_CONTROL_POINT_Register_Component_Bit_Count = 32;

        public const int DS_Input_CONTROL_POINT_Register_Count = 32;

        public const int DS_Input_CONTROL_POINT_Register_READS_PER_INST = 2;

        public const int DS_Input_CONTROL_POINT_Register_READ_PORTS = 1;

        public const int DS_Input_DOMAIN_POINT_Register_ComponentS = 3;

        public const int DS_Input_DOMAIN_POINT_Register_Component_Bit_Count = 32;

        public const int DS_Input_DOMAIN_POINT_Register_Count = 1;

        public const int DS_Input_DOMAIN_POINT_Register_READS_PER_INST = 2;

        public const int DS_Input_DOMAIN_POINT_Register_READ_PORTS = 1;

        public const int DS_Input_PATCH_Constant_Register_ComponentS = 4;

        public const int DS_Input_PATCH_Constant_Register_Component_Bit_Count = 32;

        public const int DS_Input_PATCH_Constant_Register_Count = 32;

        public const int DS_Input_PATCH_Constant_Register_READS_PER_INST = 2;

        public const int DS_Input_PATCH_Constant_Register_READ_PORTS = 1;

        public const int DS_Input_PRIMITIVE_ID_Register_ComponentS = 1;

        public const int DS_Input_PRIMITIVE_ID_Register_Component_Bit_Count = 32;

        public const int DS_Input_PRIMITIVE_ID_Register_Count = 1;

        public const int DS_Input_PRIMITIVE_ID_Register_READS_PER_INST = 2;

        public const int DS_Input_PRIMITIVE_ID_Register_READ_PORTS = 1;

        public const int DS_OUTPUT_Register_ComponentS = 4;

        public const int DS_OUTPUT_Register_Component_Bit_Count = 32;

        public const int DS_OUTPUT_Register_Count = 32;

        public const double FLOAT16_FUSED_TOLERANCE_IN_ULP = 0.6;
        public const float FLOAT32_Max = float.MaxValue;
        public const float FLOAT32_TO_INTEGER_TOLERANCE_IN_ULP = 0.6f;
        public const float FLOAT_TO_SRGB_EXPONENT_DENOMinATOR = 2.4f;
        public const float FLOAT_TO_SRGB_EXPONENT_NUMERATOR = 1.0f;
        public const float FLOAT_TO_SRGB_OFFSET = 0.055f;
        public const float FLOAT_TO_SRGB_SCALE_1 = 12.92f;
        public const float FLOAT_TO_SRGB_SCALE_2 = 1.055f;
        public const float FLOAT_TO_SRGB_THRESHOLD = 0.0031308f;
        public const float FTOI_INSTRUCTION_Max_Input = 2147483647.999f;
        public const float FTOI_INSTRUCTION_Min_Input = -2147483648.999f;
        public const float FTOU_INSTRUCTION_Max_Input = 4294967295.999f;
        public const float FTOU_INSTRUCTION_Min_Input = 0.0f;
        public const int GeometryShader_Input_INSTANCE_ID_READS_PER_INST = 2;

        public const int GeometryShader_Input_INSTANCE_ID_READ_PORTS = 1;

        public const int GeometryShader_Input_INSTANCE_ID_Register_ComponentS = 1;

        public const int GeometryShader_Input_INSTANCE_ID_Register_Component_Bit_Count = 32;

        public const int GeometryShader_Input_INSTANCE_ID_Register_Count = 1;

        public const int GeometryShader_Input_PRIM_CONST_Register_ComponentS = 1;

        public const int GeometryShader_Input_PRIM_CONST_Register_Component_Bit_Count = 32;

        public const int GeometryShader_Input_PRIM_CONST_Register_Count = 1;

        public const int GeometryShader_Input_PRIM_CONST_Register_READS_PER_INST = 2;

        public const int GeometryShader_Input_PRIM_CONST_Register_READ_PORTS = 1;

        public const int GeometryShader_Input_Register_ComponentS = 4;

        public const int GeometryShader_Input_Register_Component_Bit_Count = 32;

        public const int GeometryShader_Input_Register_Count = 32;

        public const int GeometryShader_Input_Register_READS_PER_INST = 2;

        public const int GeometryShader_Input_Register_READ_PORTS = 1;

        public const int GeometryShader_Input_Register_VERTICES = 32;

        public const int GeometryShader_Max_INSTANCE_Count = 32;

        public const int GeometryShader_Max_OUTPUT_Vertex_Count_ACROSS_INSTANCES = 1024;

        public const int GeometryShader_OUTPUT_Elements = 32;

        public const int GeometryShader_OUTPUT_Register_ComponentS = 4;

        public const int GeometryShader_OUTPUT_Register_Component_Bit_Count = 32;

        public const int GeometryShader_OUTPUT_Register_Count = 32;

        public const int HullShader_CONTROL_POINT_PHASE_Input_Register_Count = 32;

        public const int HullShader_CONTROL_POINT_PHASE_OUTPUT_Register_Count = 32;

        public const int HullShader_CONTROL_POINT_Register_ComponentS = 4;

        public const int HullShader_CONTROL_POINT_Register_Component_Bit_Count = 32;

        public const int HullShader_CONTROL_POINT_Register_READS_PER_INST = 2;

        public const int HullShader_CONTROL_POINT_Register_READ_PORTS = 1;

        public const int HullShader_FORK_PHASE_INSTANCE_Count_UPPER_BOUND = unchecked((int)0xffffffff);

        public const int HullShader_Input_FORK_INSTANCE_ID_Register_ComponentS = 1;

        public const int HullShader_Input_FORK_INSTANCE_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_Input_FORK_INSTANCE_ID_Register_Count = 1;

        public const int HullShader_Input_FORK_INSTANCE_ID_Register_READS_PER_INST = 2;

        public const int HullShader_Input_FORK_INSTANCE_ID_Register_READ_PORTS = 1;

        public const int HullShader_Input_JOIN_INSTANCE_ID_Register_ComponentS = 1;

        public const int HullShader_Input_JOIN_INSTANCE_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_Input_JOIN_INSTANCE_ID_Register_Count = 1;

        public const int HullShader_Input_JOIN_INSTANCE_ID_Register_READS_PER_INST = 2;

        public const int HullShader_Input_JOIN_INSTANCE_ID_Register_READ_PORTS = 1;

        public const int HullShader_Input_PRIMITIVE_ID_Register_ComponentS = 1;

        public const int HullShader_Input_PRIMITIVE_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_Input_PRIMITIVE_ID_Register_Count = 1;

        public const int HullShader_Input_PRIMITIVE_ID_Register_READS_PER_INST = 2;

        public const int HullShader_Input_PRIMITIVE_ID_Register_READ_PORTS = 1;

        public const int HullShader_JOIN_PHASE_INSTANCE_Count_UPPER_BOUND = unchecked((int)0xffffffff);

        public const float HullShader_MaxTESSFACTOR_LOWER_BOUND = 1.0f;
        public const float HullShader_MaxTESSFACTOR_UPPER_BOUND = 64.0f;
        public const int HullShader_OUTPUT_CONTROL_POINTS_Max_TOTAL_SCALARS = 3968;

        public const int HullShader_OUTPUT_CONTROL_POINT_ID_Register_ComponentS = 1;

        public const int HullShader_OUTPUT_CONTROL_POINT_ID_Register_Component_Bit_Count = 32;

        public const int HullShader_OUTPUT_CONTROL_POINT_ID_Register_Count = 1;

        public const int HullShader_OUTPUT_CONTROL_POINT_ID_Register_READS_PER_INST = 2;

        public const int HullShader_OUTPUT_CONTROL_POINT_ID_Register_READ_PORTS = 1;

        public const int HullShader_OUTPUT_PATCH_Constant_Register_ComponentS = 4;

        public const int HullShader_OUTPUT_PATCH_Constant_Register_Component_Bit_Count = 32;

        public const int HullShader_OUTPUT_PATCH_Constant_Register_Count = 32;

        public const int HullShader_OUTPUT_PATCH_Constant_Register_READS_PER_INST = 2;

        public const int HullShader_OUTPUT_PATCH_Constant_Register_READ_PORTS = 1;

        public const int HullShader_OUTPUT_PATCH_Constant_Register_SCALAR_ComponentS = 128;

        public const int InputAssembler_Default_Index_Buffer_OFFSET_IN_BYTES = 0;

        public const int InputAssembler_Default_PRIMITIVE_TOPOLOGY = 0;

        public const int InputAssembler_Default_Vertex_Buffer_OFFSET_IN_BYTES = 0;

        public const int InputAssembler_Index_Input_Resource_Slot_Count = 1;

        public const int InputAssembler_INSTANCE_ID_Bit_Count = 32;

        public const int InputAssembler_INTEGER_ARITHMETIC_Bit_Count = 32;

        public const int InputAssembler_PATCH_Max_CONTROL_POINT_Count = 32;

        public const int InputAssembler_PRIMITIVE_ID_Bit_Count = 32;

        public const int InputAssembler_Vertex_ID_Bit_Count = 32;

        public const int InputAssembler_Vertex_Input_Resource_Slot_Count = 32;

        public const int InputAssembler_Vertex_Input_Structure_Elements_Components = 128;

        public const int InputAssembler_Vertex_Input_Structure_Element_Count = 32;

        public const int INTEGER_DIVIDE_BY_ZERO_QUOTIENT = unchecked((int)0xffffffff);

        public const int INTEGER_DIVIDE_BY_ZERO_REMAINDER = unchecked((int)0xffffffff);

        public const int KEEP_RENDER_TARGETS_AND_Depth_STENCIL = unchecked((int)0xffffffff);

        public const int KEEP_UNORDERED_ACCESS_VIEWS = unchecked((int)0xffffffff);

        public const float LinearGamma = 1.0f;
        public const int MajorVersion = 12;

        public const float Max_Border_Color_Component = 1.0f;
        public const float Max_Depth = 1.0f;
        public const int MaxLiveStaticSamplers = 2032;

        public const int Max_MaxAnisotropy = 16;

        public const int Max_MULTISAMPLE_SAMPLE_Count = 32;

        public const float Max_POSITION_VALUE = float.MaxValue;
        public const int Max_ROOT_COST = 64;

        public const int Max_SHADER_VISIBLE_DESCRIPTOR_HEAP_SIZE_TIER_1 = 1000000;

        public const int Max_SHADER_VISIBLE_DESCRIPTOR_HEAP_SIZE_TIER_2 = 1000000;

        public const int Max_SHADER_VISIBLE_Sampler_HEAP_SIZE = 2048;

        public const int Max_TEXTURE_DIMENSION_2_TO_EXP = 17;

        public const int Max_VIEW_INSTANCE_Count = 4;

        public const int Minor_Version = 0;

        public const float Min_Border_Color_Component = 0.0f;
        public const float Min_Depth = 0.0f;
        public const int Min_MaxAnisotropy = 0;

        public const float MIP_LOD_BInputAssemblerS_Max = 15.99f;
        public const float MIP_LOD_BInputAssemblerS_Min = -16.0f;
        public const int MIP_LOD_FRACTIONAL_Bit_Count = 8;

        public const int MIP_LOD_RANGE_Bit_Count = 8;

        public const float MULTISAMPLE_ANTInputAssemblerLInputAssemblerS_LINE_Width = 1.4f;
        public const int NONSAMPLE_FETCH_OUT_OF_RANGE_ACCESS_RESULT = 0;

        public const int PackedTile = unchecked((int)0xffffffff);

        public const int PIXEL_ADDRESS_RANGE_Bit_Count = 15;

        public const int PRE_SCISSOR_PIXEL_ADDRESS_RANGE_Bit_Count = 16;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_ComponentS = 1;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_Count = 8;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_READS_PER_INST = 1;

        public const int PixelShader_ComputeShader_UnorderedAccessView_Register_READ_PORTS = 1;

        public const int PixelShader_FRONTFACING_Default_VALUE = unchecked((int)0xffffffff);

        public const int PixelShader_FRONTFACING_FALSE_VALUE = 0;

        public const int PixelShader_FRONTFACING_TRUE_VALUE = unchecked((int)0xffffffff);

        public const int PixelShader_Input_Register_ComponentS = 4;

        public const int PixelShader_Input_Register_Component_Bit_Count = 32;

        public const int PixelShader_Input_Register_Count = 32;

        public const int PixelShader_Input_Register_READS_PER_INST = 2;

        public const int PixelShader_Input_Register_READ_PORTS = 1;

        public const float PixelShader_LEGACY_PIXEL_CENTER_FRACTIONAL_Component = 0.0f;
        public const int PixelShader_OUTPUT_Depth_Register_ComponentS = 1;

        public const int PixelShader_OUTPUT_Depth_Register_Component_Bit_Count = 32;

        public const int PixelShader_OUTPUT_Depth_Register_Count = 1;

        public const int PixelShader_OUTPUT_MASK_Register_ComponentS = 1;

        public const int PixelShader_OUTPUT_MASK_Register_Component_Bit_Count = 32;

        public const int PixelShader_OUTPUT_MASK_Register_Count = 1;

        public const int PixelShader_OUTPUT_Register_ComponentS = 4;

        public const int PixelShader_OUTPUT_Register_Component_Bit_Count = 32;

        public const int PixelShader_OUTPUT_Register_Count = 8;

        public const float PixelShader_PIXEL_CENTER_FRACTIONAL_Component = 0.5f;
        public const int RAW_UnorderedAccessView_SRV_BYTE_ALIGNMENT = 16;

        public const int RaytracingAABBByteAlignment = 8;

        public const int RaytracingAccelerationStructureByteAlignment = 256;

        public const int RAYTRACING_INSTANCE_DESComputeShader_BYTE_ALIGNMENT = 16;

        public const int RaytracingMaxAttributeSizeInBytes = 32;

        public const int RAYTRACING_Max_DECLARABLE_TRACE_RECURSION_Depth = 31;

        public const int RAYTRACING_Max_GEOMETRIES_PER_BOTTOM_LEVEL_ACCELERATION_Structure = 16777216;

        public const int RAYTRACING_Max_INSTANCES_PER_TOP_LEVEL_ACCELERATION_Structure = 16777216;

        public const int RAYTRACING_Max_PRIMITIVES_PER_BOTTOM_LEVEL_ACCELERATION_Structure = 536870912;

        public const int RAYTRACING_Max_RAY_GENERATION_SHADER_THREADS = 1073741824;

        public const int RAYTRACING_Max_SHADER_RECORD_STRIDE = 4096;

        public const int RAYTRACING_SHADER_RECORD_BYTE_ALIGNMENT = 32;

        public const int RAYTRACING_SHADER_TABLE_BYTE_ALIGNMENT = 64;

        public const int RaytracingTransform3x4ByteAlignment = 16;

        public const int REQ_BLEND_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_Buffer_Resource_TEXEL_Count_2_TO_EXP = 27;

        public const int REQ_Constant_Buffer_Element_Count = 4096;

        public const int REQ_Depth_STENCIL_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_DrawIndexedIndexCount2ToExp = 32;

        public const int REQ_DRAW_Vertex_Count_2_TO_EXP = 32;

        public const int REQ_FILTERING_HW_ADDRESSABLE_Resource_DIMENSION = 16384;

        public const int REQ_GeometryShader_INVOCATION_32Bit_OUTPUT_Component_LIMIT = 1024;

        public const int REQ_ImmedInputAssemblerte_Constant_Buffer_Element_Count = 4096;

        public const int REQ_MaxAnisotropy = 16;

        public const int REQ_MIP_LEVELS = 15;

        public const int REQ_MULTI_Element_Structure_SIZE_IN_BYTES = 2048;

        public const int REQ_RASTERIZER_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_RENDER_TO_Buffer_WINDOW_Width = 16384;

        public const int REQ_Resource_SIZE_IN_MEGABYTES_EXPRESSION_A_TERM = 128;

        public const float REQ_Resource_SIZE_IN_MEGABYTES_EXPRESSION_B_TERM = 0.25f;
        public const int REQ_Resource_SIZE_IN_MEGABYTES_EXPRESSION_C_TERM = 2048;

        public const int REQ_Resource_VIEW_Count_PER_DEVICE_2_TO_EXP = 20;

        public const int REQ_Sampler_OBJECT_Count_PER_DEVICE = 4096;

        public const int REQ_SUBResourceS = 30720;

        public const int REQ_TEXTURE1D_ARRAY_AXIS_DIMENSION = 2048;

        public const int REQ_TEXTURE1D_U_DIMENSION = 16384;

        public const int REQ_TEXTURE2D_ARRAY_AXIS_DIMENSION = 2048;

        public const int REQ_TEXTURE2D_U_OR_V_DIMENSION = 16384;

        public const int REQ_TEXTURE3D_U_V_OR_W_DIMENSION = 2048;

        public const int REQ_TEXTURECUBE_DIMENSION = 16384;

        public const int RESINFO_INSTRUCTION_MISSING_Component_RETVAL = 0;

        public const int ResourceBarrierAllSubResources = unchecked((int)0xffffffff);

        public const int RS_SET_SHADING_RATE_COMBINER_Count = 2;

        public const int SHADER_IDENTIFIER_SIZE_IN_BYTES = 32;

        public const int SHADER_MAJOR_Version = 5;

        public const int SHADER_Max_INSTANCES = 65535;

        public const int SHADER_Max_INTERFACES = 253;

        public const int SHADER_Max_INTERFACE_CALL_SITES = 4096;

        public const int SHADER_Max_TYPES = 65535;

        public const int SHADER_Minor_Version = 1;

        public const int SHIFT_INSTRUCTION_PAD_VALUE = 0;

        public const int SHIFT_INSTRUCTION_SHIFT_VALUE_Bit_Count = 5;

        public const int SimultaneousRenderTargetCount = 8;

        public const int SMALL_MSAA_Resource_PLACEMENT_ALIGNMENT = 65536;

        public const int SMALL_Resource_PLACEMENT_ALIGNMENT = 4096;

        public const int SO_Buffer_Max_STRIDE_IN_BYTES = 2048;

        public const int SO_Buffer_Max_WRITE_WINDOW_IN_BYTES = 512;

        public const int SO_Buffer_Slot_Count = 4;

        public const int SO_DDI_Register_Index_DENOTING_GAP = unchecked((int)0xffffffff);

        public const int SO_NO_RASTERIZED_STREAM = unchecked((int)0xffffffff);

        public const int SO_OUTPUT_Component_Count = 128;

        public const int SO_STREAM_Count = 4;

        public const int SPEC_DATE_DAY = 14;

        public const int SPEC_DATE_MONTH = 11;

        public const int SPEC_DATE_YEAR = 2014;

        public const float SpecVersion = 1.16f;
        public const float SrgbGamme = 2.2f;
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

        public const int SYSTEM_RESERVED_Register_SPACE_VALUES_END = unchecked((int)0xffffffff);

        public const int SYSTEM_RESERVED_Register_SPACE_VALUES_START = unchecked((int)0xfffffff0);

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

        public const int TILED_Resource_TILE_SIZE_IN_BYTES = 65536;

        public const int TRACKED_WORKLOAD_Max_INSTANCES = 32;

        public const int UnorderedAccessView_CountER_PLACEMENT_ALIGNMENT = 4096;

        public const int UnorderedAccessView_Slot_Count = 64;

        public const int UNBOUND_MEMORY_ACCESS_RESULT = 0;

        public const int VIDEO_DECODE_Max_ARGUMENTS = 10;

        public const int VIDEO_DECODE_Max_HISTOGRAM_ComponentS = 4;

        public const int VIDEO_DECODE_Min_BitSTREAM_OFFSET_ALIGNMENT = 256;

        public const int VIDEO_DECODE_Min_HISTOGRAM_OFFSET_ALIGNMENT = 256;

        public const int VIDEO_DECODE_STATUS_MACROBLOCKS_AFFECTED_UNKNOWN = unchecked((int)0xffffffff);

        public const int VIDEO_PROCESS_Max_FILTERS = 32;

        public const int VIDEO_PROCESS_STEREO_VIEWS = 2;

        public const int Viewport_AND_SCISSORRECT_Max_Index = 15;

        public const int ViewportAndScissorRectObjectCountPerPipeline = 16;

        public const int Viewport_Bounds_Max = 32767;

        public const int Viewport_Bounds_Min = -32768;

        public const int VertexShader_Input_Register_ComponentS = 4;

        public const int VertexShader_Input_Register_Component_Bit_Count = 32;

        public const int VertexShader_Input_Register_Count = 32;

        public const int VertexShader_Input_Register_READS_PER_INST = 2;

        public const int VertexShader_Input_Register_READ_PORTS = 1;

        public const int VertexShader_OUTPUT_Register_ComponentS = 4;

        public const int VertexShader_OUTPUT_Register_Component_Bit_Count = 32;

        public const int VertexShader_OUTPUT_Register_Count = 32;

        public const int WHQL_CONTEXT_Count_FOR_Resource_LIMIT = 10;

        public const int WHQL_DRAWIndexED_Index_Count_2_TO_EXP = 25;

        public const int WHQL_DRAW_Vertex_Count_2_TO_EXP = 25;
    }
}
