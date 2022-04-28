// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct BindingDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_BINDING_DESC::Desc']/*" />
    public IBindingDescription Description { get; set; }

    public BindingDescription(IBindingDescription binding)
    {
        Description = binding;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public BindingType Type;
        public IntPtr Description;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        ((IBindingDescriptionMarshal)Description).__MarshalFree(ref @ref.Description);
        @ref.Description = IntPtr.Zero;
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Description.BindingType;
        @ref.Description = ((IBindingDescriptionMarshal)Description).__MarshalAlloc();
    }
    #endregion
}
