// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the input-buffer data for the input-assembler stage.
/// </summary>
public partial class InputLayoutDescription
{
    public InputLayoutDescription() { }

    public InputLayoutDescription(params InputElementDescription[] elements)
    {
        Elements = elements;
    }

    /// <summary>	
    /// An array of <see cref="InputElementDescription"/> that describe the data types of the input-assembler stage.
    /// </summary>	
    public InputElementDescription[]? Elements { get; set; }

    /// <summary>
    /// Implicitely converts to an <see cref="InputLayoutDescription"/> from an array of <see cref="InputElementDescription"/>
    /// </summary>
    /// <param name="elements">Array of <see cref="InputElementDescription"/>.</param>
    public static implicit operator InputLayoutDescription(InputElementDescription[] elements)
    {
        return new InputLayoutDescription(elements);
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public InputElementDescription.__Native* pInputElementDescs;

        public int NumElements;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (@ref.pInputElementDescs != null)
        {
            for (int i = 0; i < @ref.NumElements; i++)
            {
                Elements![i].__MarshalFree(ref @ref.pInputElementDescs[i]);
            }

            Marshal.FreeHGlobal((IntPtr)@ref.pInputElementDescs);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.NumElements = Elements?.Length ?? 0;
        if (@ref.NumElements > 0)
        {
            var nativeElements = (InputElementDescription.__Native*)UnsafeUtilities.Alloc<InputElementDescription.__Native>(@ref.NumElements);
            for (int i = 0; i < @ref.NumElements; i++)
            {
                Elements![i].__MarshalTo(ref nativeElements[i]);
            }

            @ref.pInputElementDescs = nativeElements;
        }
    }
    #endregion
}
