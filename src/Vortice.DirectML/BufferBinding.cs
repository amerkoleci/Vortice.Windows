// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BufferBinding: IBindingDescription, IBindingDescriptionMarshal
{
    /// <summary>
    /// Gets the type of binding description.
    /// </summary>
    public BindingType BindingType => BindingType.Buffer;

    public override string ToString() => $"{BindingType} Size={SizeInBytes} Offset={Offset}";

    #region Marshal
    unsafe IntPtr IBindingDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->Buffer = Buffer.NativePointer;
        @ref->Offset = Offset;
        @ref->SizeInBytes = SizeInBytes;

        return new(@ref);
    }

    unsafe void IBindingDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        UnsafeUtilities.Free(pDesc);
    }
    #endregion

    public static implicit operator BindingDescription(BufferBinding binding)
    {
        return new(binding);
    }
}
