// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectML;
public partial struct BufferArrayBinding: IBinding, IBindingMarshal
{
    public BufferBinding[] Bindings;

    public BindingType BindingType => BindingType.BufferArray;

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public uint Count;
        public IntPtr Bindings;
    }

    unsafe IntPtr IBindingMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();


        BufferBinding.__Native* bindings = UnsafeUtilities.Alloc<BufferBinding.__Native>(Bindings.Length);
        for (int i = 0; i < Bindings.Length; i++)
        {
            Bindings[i].__MarshalTo(ref bindings[i]);
        }

        @ref->Count = (uint)Bindings.Length;
        @ref->Bindings = new(bindings);

        return new(@ref);
    }

    unsafe void IBindingMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        var bindings = (BufferBinding.__Native*)@ref->Bindings;
        for (int i = 0; i < Bindings.Length; i++)
        {
            Bindings[i].__MarshalFree(ref bindings[i]);
        }
        UnsafeUtilities.Free(@ref->Bindings);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator BindingDescription(BufferArrayBinding binding)
    {
        return new(binding);
    }
}
