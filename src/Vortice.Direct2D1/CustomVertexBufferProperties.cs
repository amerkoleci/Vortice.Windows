// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Direct2D1;

/// <summary>
/// Defines a vertex shader and the input element description to define the input layout. 
/// The combination is used to allow a custom vertex effect to create a custom vertex shader and pass it a custom layout.
/// </summary>
public partial class CustomVertexBufferProperties
{
    public CustomVertexBufferProperties(
        byte[] shaderBufferWithInputSignature,
        int stride,
        params InputElementDescription[] elements)
    {
        ShaderBufferWithInputSignature = shaderBufferWithInputSignature;
        Elements = elements;
        Stride = stride;
    }

    public byte[] ShaderBufferWithInputSignature { get; set; }

    /// <summary>	
    /// An array of <see cref="InputElementDescription"/> that describe the data types of the input-assembler stage.
    /// </summary>	
    public InputElementDescription[] Elements { get; set; }

    /// <summary>
    /// The vertex stride.
    /// </summary>
    public int Stride { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public byte* shaderBufferWithInputSignature;
        public uint shaderBufferSize;
        public InputElementDescription.__Native* inputElements;
        public uint elementCount;
        public uint stride;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (@ref.shaderBufferSize > 0)
        {
            NativeMemory.Free(@ref.shaderBufferWithInputSignature);
        }

        if (@ref.inputElements != null)
        {
            for (int i = 0; i < @ref.elementCount; i++)
            {
                Elements[i].__MarshalFree(ref @ref.inputElements[i]);
            }

            NativeMemory.Free(@ref.inputElements);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.shaderBufferSize = (uint)(ShaderBufferWithInputSignature?.Length ?? 0);
        if (@ref.shaderBufferSize > 0)
        {
            @ref.shaderBufferWithInputSignature = UnsafeUtilities.AllocToPointer(ShaderBufferWithInputSignature!);
        }

        @ref.elementCount = (uint)(Elements?.Length ?? 0);
        if (@ref.elementCount > 0)
        {
            var nativeElements = (InputElementDescription.__Native*)NativeMemory.Alloc(@ref.elementCount, (nuint)sizeof(InputElementDescription.__Native));
            for (int i = 0; i < @ref.elementCount; i++)
            {
                Elements![i].__MarshalTo(ref nativeElements[i]);
            }

            @ref.inputElements = nativeElements;
        }

        @ref.stride = (uint)Stride;
    }
    #endregion
}
