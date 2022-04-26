// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectML;
public partial struct BufferBinding: IBinding, IBindingMarshal
{
    public BindingType BindingType => BindingType.Buffer;

    #region Marshal
    unsafe IntPtr IBindingMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->Buffer = Buffer.NativePointer;
        @ref->Offset = Offset;
        @ref->SizeInBytes = SizeInBytes;

        return new(@ref);
    }

    unsafe void IBindingMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;
        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator BindingDescription(BufferBinding binding)
    {
        return new(binding);
    }
}
